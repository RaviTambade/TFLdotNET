using SecureWebApp.Helpers;
using SecureWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.UseHttpsRedirection();

// Custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();