using OrderApi.Core.Models;

namespace OrderApi.Common
{
    public interface IKafkaConsumer
    {
        public Task<Order> GetOrderById(string topic, Guid orderId, CancellationToken cancellationToken);
        public Task<IEnumerable<Order>> GetOrders(string topic, int page, int pageSize, CancellationToken cancellationToken);
    }
}