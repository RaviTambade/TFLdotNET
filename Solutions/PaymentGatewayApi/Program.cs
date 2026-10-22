using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using PaymentGatewayApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<JsonPaymentStore>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<OutboxWorker>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddFixedWindowLimiter("payments", limiter =>
    {
        limiter.PermitLimit = 60;
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.QueueLimit = 0;
        limiter.AutoReplenishment = true;
    });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
        new BadRequestObjectResult(new { error = "Invalid request body.", details = context.ModelState });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    // Public health endpoint. API key protects all other /api routes.
    if (context.Request.Path.StartsWithSegments("/api") &&
        !context.Request.Path.Equals("/api/health", StringComparison.OrdinalIgnoreCase))
    {
        var expected = app.Configuration["PaymentGateway:ApiKey"] ?? "";
        var supplied = context.Request.Headers["X-Api-Key"].FirstOrDefault() ?? "";
        if (string.IsNullOrWhiteSpace(expected) ||
            !System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(
                System.Text.Encoding.UTF8.GetBytes(expected),
                System.Text.Encoding.UTF8.GetBytes(supplied)))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "Missing or invalid X-Api-Key." });
            return;
        }
    }
    await next();
});

app.UseRateLimiter();
app.MapGet("/api/health", () => Results.Ok(new
{
    status = "healthy",
    service = "PaymentGatewayApi",
    timestamp = DateTimeOffset.UtcNow
}));
app.MapControllers().RequireRateLimiting("payments");

app.Run();
