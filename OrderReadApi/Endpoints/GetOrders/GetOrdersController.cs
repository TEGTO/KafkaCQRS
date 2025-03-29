using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderReadApi.Commands.GetOrders;

namespace OrderReadApi.Endpoints.GetOrders
{
    [Route("order")]
    [ApiController]
    public class GetOrdersController : ControllerBase
    {
        private readonly IRequestClient<GetOrdersQuery> client;

        public GetOrdersController(IRequestClient<GetOrdersQuery> client)
        {
            this.client = client;
        }

        [HttpGet]
        public async Task<ActionResult<GetOrdersResponse>> GetOrdersAsync(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            var result = await client.GetResponse<GetOrdersResponse>(new GetOrdersQuery(request), cancellationToken);
            return Ok(result.Message);
        }
    }
}
