namespace OrderWriteApi.Endpoints.CreateOrder
{
    public class CreateOrderRequest
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
