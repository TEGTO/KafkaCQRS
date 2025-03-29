using Microsoft.AspNetCore.Mvc;

namespace OrderReadApi.Endpoints.GetOrders
{
    public class GetOrdersRequest
    {
        [FromQuery]
        public required int Page { get; set; }
        [FromQuery]
        public required int PageSize { get; set; }
    }
}
