using CRMAnalyticsApi.Repositories;
using CRMAnalyticsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
        builder.Configuration.GetConnectionString("Redis");

    options.InstanceName = "CRMAnalytics_";
});

builder.Services.AddScoped<CRMRepository>();

builder.Services.AddScoped<ICRMAnalyticsService,
    CRMAnalyticsService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();