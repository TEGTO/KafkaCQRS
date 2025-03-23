using MassTransit;

namespace OrderWriteApi.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IConsumer<UpdateOrderCommand>
    {
        public async Task Consume(ConsumeContext<UpdateOrderCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            //await context.RespondAsync(new CreateOrderResult(orderId, "Order created successfully"));
        }
    }
}
