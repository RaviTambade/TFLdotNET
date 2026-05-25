using Messaging;
using PaymentService.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<OrderCreatedConsumer>();
builder.Services.AddSingleton<KafkaProducer>();
var app = builder.Build();


app.UseHttpsRedirection();




app.Run();

