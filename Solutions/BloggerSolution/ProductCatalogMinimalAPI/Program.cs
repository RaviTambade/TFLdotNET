using ProductCatalogMinimalAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<List<Product>>();

var app = builder.Build();
app.UseHttpsRedirection();

// Define routes and handlers for CRUD operations
app.MapGet("/api/products", (List<Product> products) => products);

app.MapGet("/api/products/{id}", (int id, List<Product> products) => products.FirstOrDefault(p => p.Id == id));

app.MapPost("/api/products", (Product product, List<Product> products) =>
{
    product.Id = products.Count + 1;
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id}", (int id, Product product, List<Product> products) =>
{
    var existingProduct = products.FirstOrDefault(p => p.Id == id);
    if (existingProduct == null) return Results.NotFound();
    product.Id = id;
    products[products.IndexOf(existingProduct)] = product;
    return Results.NoContent();
});


app.MapDelete("/api/products/{id}", (int id, List<Product> products) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null) return Results.NotFound();
    products.Remove(product);
    return Results.NoContent();
});

app.Run();