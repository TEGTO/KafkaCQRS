using MassTransit;

namespace OrderWriteApi.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IConsumer<DeleteOrderCommand>
    {
        public async Task Consume(ConsumeContext<DeleteOrderCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);
            var request = context.Message.Request;
        }
    }
}
