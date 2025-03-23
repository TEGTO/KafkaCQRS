namespace OrderReadApi.Endpoints.GetOrderById
{
    public class GetOrderByIdResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
