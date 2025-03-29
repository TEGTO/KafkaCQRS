using MassTransit;
using OrderApi.Common;
using OrderWriteApi.Services;
using System.Text.Json;

namespace OrderWriteApi.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IConsumer<DeleteOrderCommand>
    {
        private readonly IProducer producer;
        private readonly IKafkaConsumer kafkaConsumer;
        private readonly IConfiguration configuration;

        public DeleteOrderCommandHandler(IProducer producer, IKafkaConsumer kafkaConsumer, IConfiguration configuration)
        {
            this.producer = producer;
            this.kafkaConsumer = kafkaConsumer;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<DeleteOrderCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            var existingOrder = await kafkaConsumer.GetOrderById(configuration[ConfigurationKeys.OrderTopic]!,
                request.Id,
                context.CancellationToken);

            existingOrder.IsDeleted = true;

            var orderJson = JsonSerializer.Serialize(existingOrder);
            await producer.ProduceAsync(configuration[ConfigurationKeys.OrderTopic]!, request.Id.ToString(), orderJson, context.CancellationToken);
        }
    }
}
