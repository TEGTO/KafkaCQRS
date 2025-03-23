using MassTransit;

namespace OrderReadApi.Commands.GetOrderById
{
    public class GetOrderByIdQueryHandler : IConsumer<GetOrderByIdQuery>
    {
        public async Task Consume(ConsumeContext<GetOrderByIdQuery> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            //await context.RespondAsync(new CreateOrderResult(orderId, "Order created successfully"));
        }
    }
}
