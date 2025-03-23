using MassTransit;

namespace OrderReadApi.Commands.GetOrders
{
    public class GetOrdersQueryHandler : IConsumer<GetOrdersQuery>
    {
        public async Task Consume(ConsumeContext<GetOrdersQuery> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            //await context.RespondAsync(new CreateOrderResult(orderId, "Order created successfully"));
        }
    }
}
