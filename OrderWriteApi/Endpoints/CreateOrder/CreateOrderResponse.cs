namespace OrderWriteApi.Endpoints.CreateOrder
{
    public class CreateOrderResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
