Console.WriteLine("Web Application Started.....");

var builder = WebApplication.CreateBuilder(args);

// Add service
Console.WriteLine("Web Application attach services MVC");

builder.Services.AddControllersWithViews();

Console.WriteLine("Build Web App infrastructure");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

Console.WriteLine("Web Application middleware configuration");
Console.WriteLine("Setting ASP.net core middleware Pipeline");

app.UseHttpsRedirection();
Console.WriteLine("Setting up SSL middleware ");
app.UseStaticFiles();
Console.WriteLine("Setting up Static resources middleware ");
app.UseRouting();       //Setting up URL Routing
Console.WriteLine("Setting up URL Routing middleware");
app.UseAuthorization();
Console.WriteLine("Setting up Authorization middleware ");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
Console.WriteLine("Setting  up Controller Mapping ");
Console.WriteLine("Kestrel server is listening on port .......");

app.Run();
Console.WriteLine("Stopping server.......");
