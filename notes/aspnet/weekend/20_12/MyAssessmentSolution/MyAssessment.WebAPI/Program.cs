var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOpenApi();
var app = builder.Build();
//mapping http requests url with  arrow function (Lamda function)  (Annonmous Delegate)}
app.MapGet("/api/user", () =>
{
    var data= new
    {
        username="Sarthak",
        email="sharthak.kadam@gmail.com",
        contact="9883487459"
    };
    return data;
});

 
app.MapGet("/api/assessment", () =>
{
    var data= new
    {
        Id = 64,
        TestId = 56,
        CandidateId = 99,

        Status = "completed",

        CreatedBy = "Ravi Tambade",
        CreatedOn = "12/12/2025",

        ModifiedBy = "Kajal Ghule",
        ModifiedOn = "16/12/2025",

        ScheduledStart = "20/12/2025",
        ScheduledEnd = "21/12/2025",
    };
    return  data;
});

app.MapPost("/api/user", (UserDto user) =>
{
    return Results.Created("/api/user/1", new
    {
        message = "User created",
        user
    });
});


app.MapPut("/api/user/{id:int}", (int id, UserDto user) =>
{
    //to access CRUD operations of Update logic
    return Results.Ok(new
    {
        message = "User updated",
        id,
        user
    });
});

app.MapDelete("/api/user/{id:int}", (int id) =>
{
    return Results.Ok(new
    {
        message = "User deleted",
        id
    });
});

app.Run();