using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Core.Models;
using OrderReadApi.Commands.GetOrderById;

namespace OrderReadApi.Endpoints.GetOrderById
{
    [Route("order")]
    [ApiController]
    public class GetOrderByIdController : ControllerBase
    {
        private readonly IRequestClient<GetOrderByIdQuery> client;
        private readonly IMapper mapper;

        public GetOrderByIdController(IRequestClient<GetOrderByIdQuery> client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetOrderByIdResponse>> GetOrderByIdAsync(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await client.GetResponse<Order>(new GetOrderByIdQuery(request), cancellationToken);

            var response = mapper.Map<GetOrderByIdResponse>(result.Message);

            return Ok(response);
        }
    }
}
