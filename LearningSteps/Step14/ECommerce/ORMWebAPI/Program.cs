using Microsoft.EntityFrameworkCore;
using BOL;
using DAL;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string conString=@"server=localhost;port=3306;user=root; password=password;database=transflower"; 
builder.Services.AddDbContext<CollectionContext>(opt => opt.UseMySQL(conString));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/",()=>"Welcome to Transflower");
app.MapGet("/departments", async (CollectionContext db) =>
    await db.Departments.ToListAsync());
app.Run();
