var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGet("/aboutus", () => "IACSD, Pune");
app.MapGet("/contact", () => "ravi.tambade@iacsd.com");
app.Run();
