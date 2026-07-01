using Messaging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<StockConsumer>();
builder.Services.AddSingleton<KafkaProducer>();
builder.Services.AddSingleton<KafkaProducer>();
var app = builder.Build();


app.UseHttpsRedirection();




app.Run();

