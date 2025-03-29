using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using OrderApi.Core.Models;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using System.Text.Json;

namespace OrderApi.Common
{
    public class KafkaConsumer : IKafkaConsumer, IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly List<KafkaStream> streamsToDispose = [];
        private bool disposed;

        public KafkaConsumer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Order> GetOrderById(string topic, Guid orderId, CancellationToken cancellationToken)
        {
            var builder = new StreamBuilder();
            Order? latestOrder = null;

            builder
                .Stream<string, string>(topic)
                .Filter((key, value, context) => key == orderId.ToString())
                .Foreach((key, value, context) =>
                {
                    var order = JsonSerializer.Deserialize<Order>(value);
                    if (order != null && !order.IsDeleted && (latestOrder == null || order.UpdatedAt > latestOrder.UpdatedAt))
                    {
                        latestOrder = order;
                    }
                });

            var stream = new KafkaStream(builder.Build(), GetStreamConfig());
            streamsToDispose.Add(stream);

            await stream.StartAsync(cancellationToken);

            await Task.Delay(5000, cancellationToken); // I have no idea how can I get the newest order (stupid Streamiz.Kafka.Net)

            if (latestOrder != null)
            {
                return latestOrder;
            }
            else
            {
                throw new InvalidDataException($"Order with ID {orderId} not found.");
            }
        }

        public async Task<IEnumerable<Order>> GetOrders(string topic, int page, int pageSize, CancellationToken cancellationToken)
        {
            var builder = new StreamBuilder();
            var ordersList = new List<Order>();
            var completionSource = new TaskCompletionSource<bool>();

            int startIndex = (page - 1) * pageSize;
            int endIndex = page * pageSize;

            builder
                .Stream<string, string>(topic)
                .Peek((key, value, context) =>
                {
                    if (ordersList.Count >= endIndex)
                    {
                        completionSource.TrySetResult(true);
                    }
                })
                .FilterNot((key, value, context) => ordersList.Any(x => x.Id.ToString() == key))
                .Filter((key, value, context) => ordersList.Count < endIndex)
                .Foreach((key, value, context) =>
                {
                    var order = JsonSerializer.Deserialize<Order>(value);
                    if (order != null && !order.IsDeleted)
                    {
                        ordersList.Add(order);
                    }
                });

            var stream = new KafkaStream(builder.Build(), GetStreamConfig());
            streamsToDispose.Add(stream);

            await stream.StartAsync(cancellationToken);

            await Task.WhenAny(completionSource.Task, Task.Delay(5000, cancellationToken));

            return ordersList.Skip(startIndex).Take(pageSize);
        }

        ~KafkaConsumer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                foreach (var stream in streamsToDispose)
                {
                    stream.Dispose();
                }
            }

            disposed = true;
        }

        private StreamConfig<StringSerDes, StringSerDes> GetStreamConfig()
        {
            return new StreamConfig<StringSerDes, StringSerDes>
            {
                ApplicationId = $"{Guid.NewGuid()}",
                BootstrapServers = configuration[ConfigurationKeys.KafkaBootstrapServers],
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
        }
    }
}
