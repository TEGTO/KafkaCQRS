using Confluent.Kafka;

namespace OrderWriteApi.Services
{
    public class KafkaProducer : IProducer
    {
        private readonly IKafkaProducerFactory producerFactory;

        public KafkaProducer(IKafkaProducerFactory producerFactory)
        {
            this.producerFactory = producerFactory;
        }

        public async Task ProduceAsync(string topic, string key, string value, CancellationToken cancellationToken)
        {
            using var producer = producerFactory.CreateProducer();
            var message = new Message<string, string>
            {
                Value = value,
                Key = key,
                Timestamp = Timestamp.Default,
            };

            await producer.ProduceAsync(topic, message, cancellationToken);
        }
    }
}
