namespace OrderWriteApi.Endpoints.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}