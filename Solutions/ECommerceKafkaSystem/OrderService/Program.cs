using Messaging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<KafkaProducer>();
var app = builder.Build();



app.UseHttpsRedirection();

app.MapControllers();




app.Run();

