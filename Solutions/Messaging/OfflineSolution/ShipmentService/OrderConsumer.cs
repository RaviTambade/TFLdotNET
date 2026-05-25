using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace ShipmentService
{
    public class OrderConsumer : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "orderQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<Order>(message);
                Console.WriteLine($"[ShipmentService] Shipping order: {order?.OrderId} - {order?.ProductName}");
            };

            channel.BasicConsume(queue: "orderQueue", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
