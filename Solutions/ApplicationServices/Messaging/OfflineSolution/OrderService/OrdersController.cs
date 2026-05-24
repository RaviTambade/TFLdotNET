using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderService
{

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpPost("place")]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "orderQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "orderQueue", basicProperties: null, body: body);

            return Ok("Order placed successfully");
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
