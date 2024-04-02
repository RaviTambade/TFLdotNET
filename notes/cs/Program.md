# Program.cs FILE in ASP.NET Core MVC

- Program.cs file in ASP.NET Core MVC application is an entry point of an application. 
- It contains logic to start the server and listen for the requests and also configure the application.
- Every ASP.NET Core web applications starts like a console application then turn into web application.
- When you press F5 and run the application, it starts the executing code in the Program.cs file.

The following is the default Program.cs file created in Visual Studio for ASP.NET 8 (ASP.NET Core) MVC application.

```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

let us understand the code written in Program.cs step by step:

1. Notice that there is no Main() method in the above Progrm.cs file because it uses top-level statements feature of C# 9.
2. The first line creates an object of <b>WebApplicationBuilder</b> with preconfigured defaults using the CreateBuilder() method.
3. The <b>CreateBuilder()</b> method setup the internal web server which is Kestrel. It also specifies the content root and read application settings file appsettings.json.
4. Using this builder object, you can configure various things for your web application, such as dependency injection, middleware, and hosting environment. You can pass additional configurations at runtime based on the runtime parameters.
5. The builder object has the <b>Services()</b> method which can be used to add services to the <b>Dependency Injection Container</b>.
6. The <b>AddControllersWithViews()</b> is an extension method that register types needed for MVC application (model, view, controller) to the dependency injection. It includes all the necessary services and configurations for MVC So that your application can use MVC architecture.
7. The <b>builder.Build()</b> method returns the object of <b>WebApplication</b> using which you can configure the request pipeline using middleware and hosting environment that manages the execution of your web application.
8. Now, using this WebApplication object app, you can configure an application based on the environment it runs on e.g. development, staging or production. The following adds the middleware that will catch the exceptions, logs them, and reset and execute the request path to '"/home/error" if the application runs on the development environment.
9. Note that the method starts with "Use" word means it configures the <b>Middleware</b>. The following configures the static files, routing, and authorization middleware respectively.
10. The <b>UseStaticFiles()</b> method configures the middleware that returns the static files from the wwwroot folder only.
11. The <b>MapControllerRoute()</b> defines the default route pattern that specifies which controller, action, and optional route parameters should be used to handle incoming requests.
12. Finally, <b>app.run()</b> method runs the application,start listening the incomming request. It turns a console application into an MVC application based on the provided configuration.
So, program.cs contains codes that sets up all necessary infrastructure for your application.
