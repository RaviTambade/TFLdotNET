//Minimal Code Strategy
//Entry Point Main code
//HTTP Server for asp.net App
//Corss platform Web Server (Kestral)
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//URL Mapping with callback function
app.MapGet("/", () => "Hello World!");
app.MapGet("/aboutus", () => "IACSD, Pune");
app.MapGet("/contact", () => "ravi.tambade@iacsd.com");
app.Run();