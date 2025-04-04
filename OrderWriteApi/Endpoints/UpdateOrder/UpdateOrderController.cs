﻿using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Core.Models;
using OrderWriteApi.Commands.UpdateOrder;

namespace OrderWriteApi.Endpoints.UpdateOrder
{
    [Route("order")]
    [ApiController]
    public class UpdateOrderController : ControllerBase
    {
        private readonly IRequestClient<UpdateOrderCommand> client;
        private readonly IMapper mapper;

        public UpdateOrderController(IRequestClient<UpdateOrderCommand> client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<UpdateOrderResponse>> UpdateAsync(UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await client.GetResponse<Order>(new UpdateOrderCommand(request), cancellationToken);

            var response = mapper.Map<UpdateOrderResponse>(result.Message);

            return Ok(response);
        }
    }
}
