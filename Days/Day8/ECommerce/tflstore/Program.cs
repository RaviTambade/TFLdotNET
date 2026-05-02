Console.WriteLine("Web Application Started.....");
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Web Application attach services MVC");
builder.Services.AddControllersWithViews();
Console.WriteLine("Build Web App infrastructure");
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
Console.WriteLine("Web Application middleware configuration");
Console.WriteLine("Setting ASP.net core middleware Pipeline");
app.UseHttpsRedirection();
Console.WriteLine("Setting up SSL middleware ");
app.UseStaticFiles();
Console.WriteLine("Setting up Static resources middleware ");
app.UseRouting();
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
