using CacheApi.Repositories;
using CacheApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Add Memory Cache
builder.Services.AddMemoryCache();

// Dependency Injection
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<IProductCacheService, ProductCacheService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();