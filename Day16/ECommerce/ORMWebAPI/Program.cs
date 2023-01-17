using Microsoft.EntityFrameworkCore;
using BOL;
using DAL;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string conString=@"server=localhost;port=3306;user=root; password=password;database=transflower"; 
builder.Services.AddDbContext<CollectionContext>(opt => opt.UseMySQL(conString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//get welcome message
app.MapGet("/",()=>"Welcome to Transflower");

//get all items
app.MapGet("/departments", async (CollectionContext db) =>
    await db.Departments.ToListAsync());

app.Run();
