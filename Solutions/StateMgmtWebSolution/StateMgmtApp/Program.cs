var builder = WebApplication.CreateBuilder(args);


//Service configuration

// Add services to the container.
// dependency injection container


//Application Services:
//1. Session State
//2. Caching
//3. Logging
//4. Database Context
//5. Identity(Authentication and Authorization)
//6. Custom Services (e.g., EmailService, PaymentService, etc.)
//7. Third-party services (e.g., Stripe, Twilio, etc.)
//8. MVC services (Controllers, Views, Razor Pages, etc.)

builder.Services.AddControllersWithViews();

builder.Services.AddSession();      //default session timeout is 20 minutes. You can change it by passing options to AddSession method
builder.Services.AddDistributedMemoryCache(); //In-memory cache for session state. You can use other distributed cache providers like Redis or SQL Server for production scenarios.

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//middleware configuration
//list of middleware components
//1. Exception Handling Middleware
//2. HSTS Middleware
//3. HTTPS Redirection Middleware
//4. Static Files Middleware
//5. Routing Middleware
//6. Authorization Middleware
//7. Session Middleware
//8. Custom Middleware (e.g., Logging Middleware, Authentication Middleware, etc.)
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
