using ShipmentService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<OrderConsumer>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();