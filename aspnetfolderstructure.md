
We created the ASP.NET Core MVC application. The following is a default project structure of the ASP.NET Core MVC application in Visual Studio.


Let's understand the significanse of each file and folder.

## Solution File
Visual Studio creates the top-level solution file (.sln) that can contain one or more ASP.NET projects. Here, "Solution 'SampleMVCCoreApp' (1 of 1 project)" is the solution file. You can right-click on it an click on 'Open Folder in File Explorer' and see the 'SampleMVCCoreApp.sln' file is created. This is our solution file.

## Project File
Under the solution node in the Solution Explorer, you can see our project node 'SampleMVCCoreApp'. All the files under this node will be for 'SampleMVCCoreApp' project.

## Dependencies
The Dependencies node contains the list of all the dependencies that your project relies on, including NuGet packages, project references, and framework dependencies.


## wwwroot
By default, the wwwroot folder in the ASP.NET Core project is treated as a web root folder. Static files can be stored in any folder under the web root and accessed with a relative path to that root.

All the css, JavaScript, and external library files should be stored here which are being reference in the HTML file.


## Controllers, Models, Views
The Controllers, Models, and Views folders include controller classes, model classes and cshtml or vbhtml files respectively for MVC application.

## appsettings.json
The appsettings.json file is a configuration file commonly used in .NET applications, including ASP.NET Core and ASP.NET 5/6, to store application-specific configuration settings and parameters. It allows developers to use JSON format for the configurations instead of code, which makes it easier to add or update settings without modifying the application's source code.

## program.cs
The last file 'program.cs' is an entry point of an application. ASP.NET Core web application is a console application that builds and launches a web application.

# Program.cs in ASP.NET Core MVC

Program.cs file in ASP.NET Core MVC application is an entry point of an application. It contains logic to start the server and listen for the requests and also configure the application.

Every ASP.NET Core web applications starts like a console application then turn into web application. When you press F5 and run the application, it starts the executing code in the Program.cs file.

The following is the default Program.cs file created in Visual Studio for ASP.NET 7 (ASP.NET Core) MVC application.

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
et us understand the code written in Program.cs step by step:

Notice that there is no Main() method in the above Progrm.cs file because it uses top-level statements feature of C# 9.

The first line creates an object of WebApplicationBuilder with preconfigured defaults using the CreateBuilder() method.

The CreateBuilder() method setup the internal web server which is Kestrel. It also specifies the content root and read application settings file appsettings.json.

Using this builder object, you can configure various things for your web application, such as dependency injection, middleware, and hosting environment. You can pass additional configurations at runtime based on the runtime parameters.

The builder object has the Services() method which can be used to add services to the dependency injection container.


The AddControllersWithViews() is an extension method that register types needed for MVC application (model, view, controller) to the dependency injection. It includes all the necessary services and configurations for MVC So that your application can use MVC architecture.

The builder.Build() method returns the object of WebApplication using which you can configure the request pipeline using middleware and hosting environment that manages the execution of your web application.

Now, using this WebApplication object app, you can configure an application based on the environment it runs on e.g. development, staging or production. The following adds the middleware that will catch the exceptions, logs them, and reset and execute the request path to '"/home/error" if the application runs on the development environment.

Note that the method starts with "Use" word means it configures the middleware. The following configures the static files, routing, and authorization middleware respectively.

The UseStaticFiles() method configures the middleware that returns the static files from the wwwroot folder only.

The MapControllerRoute() defines the default route pattern that specifies which controller, action, and optional route parameters should be used to handle incoming requests.

Finally, app.run() method runs the application,start listening the incomming request. It turns a console application into an MVC application based on the provided configuration.

So, program.cs contains codes that sets up all necessary infrastructure for your application.
