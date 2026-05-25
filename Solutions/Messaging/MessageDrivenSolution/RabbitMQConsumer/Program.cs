using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");

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
// Create a consumer
var consumer = new EventingBasicConsumer(channel);
// Register an event handler for the Received event
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};


channel.BasicConsume(queue: "hello",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine("Waiting for incomming messages from RabbitMQ Service");
// Wait for user input to exit
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();