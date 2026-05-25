using Microsoft.AspNetCore.Mvc;
using Messaging;
using Shared.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly KafkaProducer _producer;

        public OrdersController(KafkaProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto order)
        {
            order.OrderId = Random.Shared.Next(1000, 9999);

            var evt = new EventMessage
            {
                EventId = Guid.NewGuid(),
                EventType = "OrderCreated",
                Timestamp = DateTime.UtcNow,
                Data = order
            };

            await _producer.PublishAsync("order-created", evt);
             Console.WriteLine("order created: ");

            return Ok(order);
        }
    }
}