using Confluent.Kafka;

namespace OrderWriteApi.Services
{
    public class KafkaProducerFactory : IKafkaProducerFactory
    {
        private readonly ProducerBuilder<string, string> producerBuilder;

        public KafkaProducerFactory(ProducerConfig config)
        {
            producerBuilder = new ProducerBuilder<string, string>(config);
        }

        public IProducer<string, string> CreateProducer()
        {
            var producer = producerBuilder.Build();
            return producer;
        }
    }
}
