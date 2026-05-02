var builder = WebApplication.CreateBuilder(args);
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
    name: "office",
    pattern: "offices/{countryid}/odc/{odcid}",
    defaults: new { controller = "People", action = "List" }
 );
app.MapControllerRoute(
    name: "people",
    pattern: "people/{ssn}",
    defaults: new { controller = "People", action = "List" }
 );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
