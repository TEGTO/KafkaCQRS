namespace OrderReadApi.Endpoints.GetOrders
{
    public class GetOrdersResponse
    {
        public IEnumerable<GetOrdersResponseItem> GetOrdersResponseItems { get; set; } = default!;
    }

    public class GetOrdersResponseItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
