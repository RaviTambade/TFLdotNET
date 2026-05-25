var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//change 1:Add distributed Cache memeory where session data will be stored
builder.Services.AddDistributedMemoryCache();
//change 2: Configure Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//3.Change set up session middleware in HTTP pipeline

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();