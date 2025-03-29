using Microsoft.AspNetCore.Mvc;

namespace OrderWriteApi.Endpoints.UpdateOrder
{
    public class UpdateOrderRequest
    {
        [FromRoute]
        public required Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CustomerId { get; set; }
        public int? Quantity { get; set; }
    }
}