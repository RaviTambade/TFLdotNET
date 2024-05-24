using CRMMinimalWebAPI.Entities;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

// Define routes and handlers for CRUD operations

app.MapGet("/api/customers", () => {
    List<Customer> customers = new List<Customer> { new Customer { Id=12, Name="Raj", Address="Pune"},
                                                    new Customer { Id=12, Name="Veeru", Address="Mumbai"},
                                                    new Customer { Id=12, Name="Samay", Address="Delhi"}
    };
    return customers;
});

app.MapGet("/api/customers/{id}", (int id) => {
    return Results.Created($"/api/customers/{id}", id);
});

app.MapPost("/api/customers", (Customer customer) =>
{    
    return Results.Created($"/api/customers/{customer.Id}", customer);
});

app.MapPut("/api/products/{id}", (int id, Customer product ) =>
{
    return Results.NoContent();
});

app.MapDelete("/api/products/{id}", (int id ) =>
{
    return Results.NoContent();
});
app.Run();