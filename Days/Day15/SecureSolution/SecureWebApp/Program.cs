using System;
using SecureWebApp.Services;
using SecureWebApp.Helpers;
using Microsoft.Extensions.Configuration;
var builder = WebApplication.CreateBuilder(args); 
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.Run();
