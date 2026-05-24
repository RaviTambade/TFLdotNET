using ShoppingCartRedisApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "ShoppingCart_";
});

builder.Services.AddScoped<IShoppingCartService,ShoppingCartService>();
var app = builder.Build();
app.UseAuthorization();
app.MapControllers();

app.Run();