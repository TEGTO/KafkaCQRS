using AutoMapper;
using MassTransit;
using OrderApi.Common;
using OrderReadApi.Endpoints.GetOrders;

namespace OrderReadApi.Commands.GetOrders
{
    public class GetOrdersQueryHandler : IConsumer<GetOrdersQuery>
    {
        private readonly IKafkaConsumer kafkaConsumer;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public GetOrdersQueryHandler(IKafkaConsumer kafkaConsumer, IConfiguration configuration, IMapper mapper)
        {
            this.kafkaConsumer = kafkaConsumer;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GetOrdersQuery> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            var orders = await kafkaConsumer.GetOrders(configuration[ConfigurationKeys.OrderTopic]!, request.Page, request.PageSize, context.CancellationToken);

            orders = orders
                .OrderByDescending(o => o.CreatedAt);

            var responseOrders = orders.Select(mapper.Map<GetOrdersResponseItem>);

            await context.RespondAsync(new GetOrdersResponse { GetOrdersResponseItems = responseOrders });
        }
    }
}
