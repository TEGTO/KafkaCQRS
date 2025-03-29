using MassTransit;
using OrderApi.Common;

namespace OrderReadApi.Commands.GetOrderById
{
    public class GetOrderByIdQueryHandler : IConsumer<GetOrderByIdQuery>
    {
        private readonly IKafkaConsumer kafkaConsumer;
        private readonly IConfiguration configuration;

        public GetOrderByIdQueryHandler(IKafkaConsumer kafkaConsumer, IConfiguration configuration)
        {
            this.kafkaConsumer = kafkaConsumer;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<GetOrderByIdQuery> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            var existingOrder = await kafkaConsumer.GetOrderById(configuration[ConfigurationKeys.OrderTopic]!,
                request.Id,
                context.CancellationToken);

            await context.RespondAsync(existingOrder);
        }
    }
}
