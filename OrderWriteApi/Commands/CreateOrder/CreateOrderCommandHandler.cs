using AutoMapper;
using MassTransit;
using OrderApi.Common;
using OrderApi.Core.Models;
using OrderWriteApi.Services;
using System.Text.Json;

namespace OrderWriteApi.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        private readonly IProducer producer;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public CreateOrderCommandHandler(IProducer producer, IConfiguration configuration, IMapper mapper)
        {
            this.producer = producer;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            var order = mapper.Map<Order>(request);
            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;

            var orderJson = JsonSerializer.Serialize(order);

            await producer.ProduceAsync(configuration[ConfigurationKeys.OrderTopic]!, order.Id.ToString(), orderJson, context.CancellationToken);

            await context.RespondAsync(order);
        }
    }
}
