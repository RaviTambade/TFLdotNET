using BOL;
using BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/api/employees", () => {

    List<string> employees=new List<string>();
    employees.Add("Manisha");
    employees.Add("Sandesh");
    employees.Add("Manoj");
    return Results.Ok(employees);
});


app.MapGet("/api/products", () => {

    CatalogManager mgr=new CatalogManager();
    List<Product> products = mgr.GetAllProducts();
    return Results.Ok(products);
});


app.MapPost("/api/products", (Product product) =>
{
    CatalogManager mgr=new CatalogManager();
    //Insert new Product  

    return Results.Created("Successfully  created new product in Inventory",product.Title);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
