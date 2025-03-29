namespace OrderWriteApi.Endpoints.CreateOrder
{
    public class CreateOrderRequest
    {
        public required Guid ProductId { get; set; }
        public required Guid CustomerId { get; set; }
        public required int Quantity { get; set; }
    }
}
