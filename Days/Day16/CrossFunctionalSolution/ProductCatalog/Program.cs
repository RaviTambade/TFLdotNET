using ProductCatalog.Repositories.Interfaces;
using ProductCatalog.Repositories;
using ProductCatalog.Services;
using ProductCatalog.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add Services to builder 
// Services configuration
builder.Services.AddScoped<IProductRepository, ProductRepositoryMySql>();
builder.Services.AddScoped<IProductService, ProductService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();