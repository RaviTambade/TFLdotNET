using ProductCatalog.Repositories.Interfaces;
using ProductCatalog.Repositories;
using ProductCatalog.Services;
using ProductCatalog.Services.Interfaces;
using Serilog;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);


//Configuring Cross Functional Features to Web Application

 builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
   logging.AddFile("logs/catalog-{Date}.json", isJson: true);
});
 


builder.Services.AddControllers();

// Add Services to builder 
// Services configuration
builder.Services.AddScoped<IProductRepository, ProductRepositoryMySql>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();