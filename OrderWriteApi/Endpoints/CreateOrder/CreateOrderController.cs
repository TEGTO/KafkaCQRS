using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Core.Models;
using OrderWriteApi.Commands.CreateOrder;

namespace OrderWriteApi.Endpoints.CreateOrder
{
    [Route("order")]
    [ApiController]
    public class CreateOrderController : ControllerBase
    {
        private readonly IRequestClient<CreateOrderCommand> client;
        private readonly IMapper mapper;

        public CreateOrderController(IRequestClient<CreateOrderCommand> client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderResponse>> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await client.GetResponse<Order>(new CreateOrderCommand(request), cancellationToken);

            var response = mapper.Map<CreateOrderResponse>(result.Message);

            return Created(new Uri($"{Request.Scheme}://{Request.Host}/order/{response.Id}"), response);
        }
    }
}
