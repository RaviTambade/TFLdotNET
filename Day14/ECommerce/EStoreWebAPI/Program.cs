
using EStoreWebAPI;

var builder = WebApplication.CreateBuilder(args);


//*************************************************************
// Install the Microsoft.AspNetCore.Cors Nuget package.
 builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
});

app.UseAuthorization();
app.MapControllers();

//Minimal web API with ASP.NET Core

app.MapGet("/api/employees",() =>
      {
        List<Employee> employees=new List<Employee>();

        employees.Add(new Employee{ Id=12, FirstName="Ravi", LastName="Tambade"});
        employees.Add(new Employee{ Id=13, FirstName="Raji", LastName="Patil"});
        employees.Add(new Employee{ Id=14, FirstName="Seema", LastName="More"});
        return Results.Ok(employees);    
});
 
app.MapGet("/api/employees/{id}",(int id) =>
    {
    bool status=true; 
    var emp=new Employee{ Id=id, FirstName="Ravi", LastName="Tambade"};
    if(status){
        return Results.Ok(emp);
    }      
    return Results.NotFound();
});

app.MapPost("/api/employees",(Employee emp) =>
{
    List<Employee> employees=new List<Employee>();
    employees.Add(emp);
    return Results.Created($"/employees/{emp.Id}", emp);
});

app.MapPut("/api/employees/{id}",   (int id, Employee emp) =>
{
    var existingEmp =  new Employee{ Id=id, FirstName="Ravi", LastName="Tambade"};  ;
    if (existingEmp is null) return Results.NotFound();
    existingEmp.FirstName = emp.FirstName;
    existingEmp.LastName = emp.LastName;
    return Results.NoContent();
});

app.MapDelete("/api/employees/{id}",   (int id) =>
{
    bool status= false;
    if (status){
        return Results.Ok();
    }
     return Results.NotFound();
});
app.Run();