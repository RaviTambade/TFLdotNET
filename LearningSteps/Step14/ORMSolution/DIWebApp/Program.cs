using DIWebApp.Interfaces;
using DIWebApp.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHelloWorldService, HelloWorldService>();
builder.Services.AddSingleton<IProductCatalogService, ProductCatalogService>();
builder.Services.AddControllersWithViews();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
