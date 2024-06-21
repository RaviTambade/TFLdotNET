using SecureWebApp.Helpers;
using SecureWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// configure strongly typed settings object

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
