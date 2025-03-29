using Microsoft.AspNetCore.Mvc;

namespace OrderReadApi.Endpoints.GetOrderById
{
    public class GetOrderByIdRequest
    {
        [FromRoute]
        public required Guid Id { get; set; }
    }
}
