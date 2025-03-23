using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Core.Models;
using OrderReadApi.Commands.GetOrders;

namespace OrderReadApi.Endpoints.GetOrders
{
    [Route("order")]
    [ApiController]
    public class GetOrdersController : ControllerBase
    {
        private readonly IRequestClient<GetOrdersQuery> client;
        private readonly IMapper mapper;

        public GetOrdersController(IRequestClient<GetOrdersQuery> client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetOrdersResponse>> GetOrdersAsync(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            var result = await client.GetResponse<IEnumerable<Order>>(new GetOrdersQuery(request), cancellationToken);

            var responseOrders = result.Message.Select(mapper.Map<GetOrdersResponseItem>);

            return Ok(new GetOrdersResponse() { GetOrdersResponseItems = responseOrders });
        }
    }
}
