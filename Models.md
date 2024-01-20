
# MVC:Controller

A Controller is a special class in ASP.NET Core Application with .cs (for C# language) extension. By default, when you create a new ASP.NET Core Application using Model View Controller (MVC) Project Template, you can see the Controllers reside in the Controllers folder. In ASP.NET Core MVC Application, the controller class should and must be inherited from the Controller base class.


Controllers in MVC Design Pattern are the components that handle user interaction, work with the model, and ultimately select a view to render. In an MVC application, the one and only responsibility of a view is to render the information; the controller handles and responds to user input and interaction. In the MVC Design Pattern, the controller is the initial entry point and is responsible for selecting which model types to work with and which view to render (hence its name – it controls how the app responds to a given request).

The Controllers in the ASP.NET Core MVC Application logically group similar types of actions together. This aggregation of actions or grouping of similar types of action together allows us to define sets of rules, such as caching, routing, and authorization, which will be applied collectively.


When the client (browser) sends a request to the server, then that request first goes through the request processing pipeline. Once the request passes the request processing pipeline, it will hit the controller. Inside the controller, there are lots of methods (called action methods) that actually handle the incoming HTTP Requests. The action method inside the controller executes the business logic and prepares the response, which is sent back to the client who initially made the request.

## Role of Controller in MVC:
- A Controller is used to group actions, i.e., Action Methods.
- The Controller is responsible for handling the incoming HTTP Request.
- The Mapping of the HTTP Request to the Controller Action method is done using Routing. That is, for a given HTTP Request, which action method of which controller is going to be invoked is decided by the Routing Engine.
- Many important features, such as Caching, Routing, Security, etc., can be applied to the controller.



## What are Action Methods?
All the public methods of a controller class are known as Action Methods. Because they are created for a specific action or operation in the application. A controller class can have many related action methods. For example, adding a Student is an action. Modifying the student data is another action. Deleting a student is another action. So, you need to remember that all the related actions should be created inside a particular controller.


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


## How to Call an Action Method of a Controller?
When we get an HTTP Request, it is the controller action method gets that call. So, whenever we say we are hitting a controller, it means we are hitting an action method of a controller.

The default structure is: http:domain.com/ControllerName/ActionMethodName

As we are working with the development environment using visual studio, the domain name is going to be our local host with some available port numbers. So, if we want to access the GetAllStudents action method of the HomeController, then the URL is something like below.
```
http://localhost:<portnumber>/student/GetAllStudents

```

we want to search for students based on their names. To do so, add the following action method inside the Student Controller.

```
//http://localhost:<portnumber>/student/GetStudentsByName?name=rajiv
public string GetStudentsByName(string name)
{
    return $"Return All Students with Name : {name}";
}

```
