using Confluent.Kafka;

namespace OrderWriteApi.Services
{
    public interface IKafkaProducerFactory
    {
        public IProducer<string, string> CreateProducer();
    }
}