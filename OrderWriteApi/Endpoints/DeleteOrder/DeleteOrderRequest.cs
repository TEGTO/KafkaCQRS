using Microsoft.AspNetCore.Mvc;

namespace OrderWriteApi.Endpoints.DeleteOrder
{
    public class DeleteOrderRequest
    {
        [FromRoute]
        public required Guid Id { get; set; }
    }
}
