using Confluent.Kafka;
using System.Text.Json;

namespace Messaging
{
    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task PublishAsync(string topic, object data)
        {
            var json = JsonSerializer.Serialize(data);

            await _producer.ProduceAsync(topic,
                new Message<Null, string>
                {
                    Value = json
                });
        }
    }
}