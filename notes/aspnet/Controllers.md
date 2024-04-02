# MVC:Controllers

Controllers in MVC Design Pattern are the components that handle user interaction, work with the model, and ultimately select a view to render. In an MVC application, the one and only responsibility of a view is to render the information; the controller handles and responds to user input and interaction. In the MVC Design Pattern, the controller is the initial entry point and is responsible for selecting which model types to work with and which view to render (hence its name – it controls how the app responds to a given request).

The Controllers in the ASP.NET Core MVC Application logically group similar types of actions together. This aggregation of actions or grouping of similar types of action together allows us to define sets of rules, such as caching, routing, and authorization, which will be applied collectively.

When the client (browser) sends a request to the server, then that request first goes through the request processing pipeline. Once the request passes the request processing pipeline, it will hit the controller. Inside the controller, there are lots of methods (called action methods) that actually handle the incoming HTTP Requests. The action method inside the controller executes the business logic and prepares the response, which is sent back to the client who initially made the request.


## Role of Controller in MVC:

- A Controller is used to group actions, i.e., Action Methods.
- The Controller is responsible for handling the incoming HTTP Request.
- The Mapping of the HTTP Request to the Controller Action method is done using Routing. That is, for a given HTTP Request, which action method of which controller is going to be invoked is decided by the Routing Engine.
- Many important features, such as Caching, Routing, Security, etc., can be applied to the controller.

```
using Microsoft.AspNetCore.Mvc;
namespace FirstCoreMVCWebApplication.Controllers
{
    public class StudentController : Controller
    {
        public string GetAllStudents()
        {
            return "Return All Students";
        }
    }
}
```

## How is Controller Instance Created in ASP.NET Core MVC?
In order to create ASP.NET Core MVC Application, we need to add the required MVC Services and Middleware into the Request Processing Pipeline. For example, you can add the MVC services using the following statement within your Main method of the Program.cs class file in .NET 6 Application. So, you can use either AddMvc or AddControllersWithViews method to add MVC services.

```
var builder=WebApplication.CreateBuilder(args);
//Add MVC Services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddMVC
```

Then we need to configure the MVC Middleware into the Request Processing Pipeline either using conventional or attribute routing. For example, the following code will add the MVC Middleware Component to the Application Processing Pipeline.

```
var app =builder.Build();

app.UseRouting();
//Conifiguring End Points for controller action methods

app.MapControllerRoute(
name:"default",
pattern:"{controller=Home}/{action=Index}/{id?}"
)
```
In ASP.NET Core MVC Web Application, the MVC Middleware Component will receive the request when the client sends an HTTP Request. Once the MVC Middleware receives the request, based either on the conventional or attribute routing, it will select the controller and action method to execute.

But, in order to execute the action method, the MVC Middleware must create an instance of the selected controller. And it makes sense, as we know if we want to invoke a non-static method, then we need an instance of the class. And this is not different in the case of a controller action method. So, to execute an action method, we need an instance of the controller class.

In order to create an instance of the Controller class, the MVC Middleware uses the concept called reflection.
