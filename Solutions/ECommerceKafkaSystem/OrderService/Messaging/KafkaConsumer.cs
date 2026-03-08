using Confluent.Kafka;

namespace Messaging
{
    public abstract class KafkaConsumer : BackgroundService
    {
        protected abstract string Topic { get; }
        protected abstract string GroupId { get; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => Consume(stoppingToken));
        }

        private void Consume(CancellationToken token)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer =
                new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(Topic);

            while (!token.IsCancellationRequested)
            {
                var msg = consumer.Consume(token);
                ProcessMessage(msg.Message.Value);
            }
        }

        protected abstract void ProcessMessage(string message);
    }
}