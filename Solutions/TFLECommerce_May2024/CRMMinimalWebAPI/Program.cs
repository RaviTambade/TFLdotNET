using CRMMinimalWebAPI.Entities;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

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

app.MapPut("/api/customers/{id}", (int id, Customer customerToUpdate ) =>
{
    return Results.NoContent();
});

app.MapDelete("/api/customers/{id}", (int id ) =>
{
    return Results.NoContent();
});

//Server is running in Listen Mode
//Always server is started first
//HTTP server is kept waiting for incomming requst 


app.Run();
