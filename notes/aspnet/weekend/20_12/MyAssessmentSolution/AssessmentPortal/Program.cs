//Applying Design pattern builder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Services configuration
builder.Services.AddControllersWithViews();


//returns WebAppliation instance

var app = builder.Build();

// middleware configuration
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();