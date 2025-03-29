
namespace OrderWriteApi.Services
{
    public interface IProducer
    {
        public Task ProduceAsync(string topic, string key, string value, CancellationToken cancellationToken);
    }
}