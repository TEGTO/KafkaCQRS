using MassTransit;

namespace OrderWriteApi.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        public async Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;

            //await context.RespondAsync(new CreateOrderResult(orderId, "Order created successfully"));
        }
    }
}
