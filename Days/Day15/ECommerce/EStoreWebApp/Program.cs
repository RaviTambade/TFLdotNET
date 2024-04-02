var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Middleware Configuration

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//Simple Routing

/*app.MapGet("/", () => "Hello World!");
app.MapGet("/aboutus", () => "Transflower");
app.MapGet("/contact", () => "ravi.tambade@transflower.in");
app.MapGet("/services", () => "Mentoring, teaching, demonstration");
*/

//SEO :Search Engine Optimization  url


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
