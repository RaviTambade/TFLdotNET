using Microsoft.AspNetCore.Authentication.Cookies;
using SecureWebApp.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<UserService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) 
.AddCookie(
    options => { 
    options.LoginPath = "/auth/login";
    options.LogoutPath = "/auth/logout";
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
