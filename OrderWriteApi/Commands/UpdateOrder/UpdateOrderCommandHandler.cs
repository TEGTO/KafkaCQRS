using MassTransit;
using OrderApi.Common;
using OrderWriteApi.Services;
using System.Text.Json;

namespace OrderWriteApi.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IConsumer<UpdateOrderCommand>
    {
        private readonly IProducer producer;
        private readonly IKafkaConsumer kafkaConsumer;
        private readonly IConfiguration configuration;

        public UpdateOrderCommandHandler(IProducer producer, IKafkaConsumer kafkaConsumer, IConfiguration configuration)
        {
            this.producer = producer;
            this.kafkaConsumer = kafkaConsumer;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<UpdateOrderCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            var existingOrder = await kafkaConsumer.GetOrderById(configuration[ConfigurationKeys.OrderTopic]!,
                request.Id,
                context.CancellationToken);

            if (request.ProductId.HasValue) existingOrder.ProductId = request.ProductId.Value;
            if (request.CustomerId.HasValue) existingOrder.CustomerId = request.CustomerId.Value;
            if (request.Quantity.HasValue) existingOrder.Quantity = request.Quantity.Value;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            var orderJson = JsonSerializer.Serialize(existingOrder);
            await producer.ProduceAsync(configuration[ConfigurationKeys.OrderTopic]!, request.Id.ToString(), orderJson, context.CancellationToken);

            await context.RespondAsync(existingOrder);
        }
    }
}
