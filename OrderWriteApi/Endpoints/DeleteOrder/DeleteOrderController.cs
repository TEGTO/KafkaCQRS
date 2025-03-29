using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderWriteApi.Commands.DeleteOrder;

namespace OrderWriteApi.Endpoints.DeleteOrder
{
    [Route("order")]
    [ApiController]
    public class DeleteOrderController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;

        public DeleteOrderController(IBus publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish(new DeleteOrderCommand(request), cancellationToken);
            return Ok();
        }
    }
}
