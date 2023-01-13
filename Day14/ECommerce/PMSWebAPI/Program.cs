

//Step 1:

//Install Dependecies for ORM using Entity Framework in asp.net core app
//*************************************************************************************
//dotnet add package Microsoft.EntityFrameworkCore.InMemory
//dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

//Step 2:
//Define POCO class 

//Step 3:
//Define DataContext for TodoItems


//Step 4:
//import Entity Frameowork namespace
using Microsoft.EntityFrameworkCore;

using PMS;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Step 5:
//Add In memory EntityFramework sercvices 
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Middleware configuration

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//get welcome message
app.MapGet("/",()=>"Welcome to Transflower");

//get all items
app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

//get all items which are completed in status
app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

//get item whose id matching
app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

//insert new item
app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

//update existing item
app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(todo);
    }
    return Results.NotFound();
});



app.Run();

