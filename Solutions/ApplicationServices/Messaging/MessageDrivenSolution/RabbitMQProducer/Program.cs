using System.Text;
using RabbitMQ.Client;
Console.WriteLine("Rabbit MQ Producer Application!");
// Create a connection factory
var factory = new ConnectionFactory() { HostName = "localhost" };
// Create a connection
using var connection = factory.CreateConnection();
// Create a channel
using var channel = connection.CreateModel();
// Declare a queue
channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

// Create a message (Payload)
var message = "Hello  from Transflower  Producer";
// Create a byte array from the message
var body = Encoding.UTF8.GetBytes(message);

// Publish the message to the queue
channel.BasicPublish(exchange: "",
                     routingKey: "hello",
                     basicProperties: null,
                     body: body);
Console.WriteLine($" [x] Sent {message}");
//-RabbitMQ---Producer---Consumer---Queue---Message
