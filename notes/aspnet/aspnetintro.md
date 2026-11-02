# Welcome to ASP.NET Core – A New Journey for C# Developers

Transflower Learning | Mentor Classroom

From C# Programming to Building Enterprise Web Applications

Dear C# Developers,

You have already learned how to write programs using C#.

You understand variables, data types, classes, objects, methods, interfaces, inheritance, collections and exception handling.

You have learned how to develop console applications and perhaps Windows desktop applications.

Now it is time to take the next step.

Let's enter the world of ASP.NET Core.

But before learning ASP.NET Core, let us understand why we need it.

## 1. Start with a simple question

Suppose you have developed a C# console application.

```
class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Transflower");
    }
}
```

You execute this program.

It displays:

```
Welcome to Transflower
```

Now imagine management asks you:

- Can students access this application through a browser?
- Can a React application communicate with it?
- Can multiple users access it simultaneously?
- Can we expose its functionality through HTTP APIs?
- Can we deploy it on a cloud server?

Can we directly use this console application as a web application?

Not without adding a web application infrastructure.

This is where ASP.NET Core comes into the picture.

## 2. What is ASP.NET Core?

ASP.NET Core is Microsoft's modern, open-source, cross-platform web development framework built on .NET.

It allows C# developers to build:

- Web Applications
- RESTful Web APIs
- Enterprise Backend Systems
- Microservices
- Real-time Applications
- Cloud-native Applications
- Web-based MVC Applications

You can use the same C# language and .NET ecosystem to build these applications.

Think of ASP.NET Core as an extension of your C# development journey into web application engineering.

## 3. Understand the evolution of C# applications

Let's look at how application development has evolved.

```
             C# PROGRAMMING
                   |
                   v
        +----------------------+
        |  Console Application |
        |                      |
        |  Input / Output      |
        |  Business Logic      |
        +----------------------+
                   |
                   v
        +----------------------+
        | Windows Application  |
        |                      |
        | Windows Forms / WPF  |
        | Desktop UI           |
        +----------------------+
                   |
                   v
        +----------------------+
        | Web Application      |
        |                      |
        | ASP.NET Core MVC     |
        | HTML / CSS / JS      |
        +----------------------+
                   |
                   v
        +----------------------+
        | Web API              |
        |                      |
        | REST / HTTP / JSON   |
        +----------------------+
                   |
                   v
        +----------------------+
        | Enterprise Systems   |
        |                      |
        | Cloud / Microservices|
        | Distributed Systems  |
        +----------------------+
```

The important observation is that the programming language remains C#.

What changes is the application execution environment, communication mechanism and architecture.

## 4. What changes when we move from C# to ASP.NET Core?

In console programming, we generally write code that executes when the user starts the application.

In web programming, the application continuously listens for incoming HTTP requests.

Consider this comparison:

| C# Console Application     | ASP.NET Core Web API                |
| -------------------------- | ----------------------------------- |
| `Main()` starts execution  | `Program.cs` configures application |
| `Console.ReadLine()`       | HTTP Request                        |
| `Console.WriteLine()`      | HTTP Response                       |
| Method invocation          | HTTP Endpoint invocation            |
| Objects in memory          | Objects + persistent storage        |
| Single process interaction | Multiple client interactions        |
| Console UI                 | Browser / React / Angular / Mobile  |

Let's understand the difference with an example.

Console application

```
Console.WriteLine("Enter Product Name:");
string name = Console.ReadLine();
Console.WriteLine($"Product: {name}");
```

ASP.NET Core Web API

```
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/products", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Laptop" },
        new { Id = 2, Name = "Keyboard" }
    };
});

app.Run();
```

Run the application:

```
dotnet run
```

Open the API endpoint in a browser or Postman:

```
GET http://localhost:5000/products
```

Response:

```
[
  {"id": 1, "name": "Laptop" },
  {"id": 2, "name": "Keyboard"}
]
```

Notice something interesting!

We are still writing C# code. But now our C# code is responding to HTTP requests and returning JSON data.

## 5. Understand the ASP.NET Core Web Application Architecture

Imagine Transflower wants to develop a Student Management System. Students access the application through a React or Angular frontend. The frontend communicates with the ASP.NET Core backend.

```
          USERS
            |
            v
   +--------------------+
   | Browser / Mobile   |
   | React / Angular    |
   +--------------------+
            |
            | HTTP Request
            | JSON Data
            v
   +--------------------+
   | ASP.NET CORE       |
   |                    |
   | Web Application    |
   |                    |
   | Controllers        |
   | Services           |
   | Repositories       |
   +--------------------+
            |
            v
   +--------------------+
   | BUSINESS LOGIC     |
   |                    |
   | Student Management |
   | Course Management  |
   | Assessment         |
   +--------------------+
            |
            v
   +--------------------+
   | DATA ACCESS        |
   |                    |
   | Entity Framework   |
   | ADO.NET            |
   +--------------------+
            |
            v
   +--------------------+
   | DATABASE           |
   |                    |
   | MySQL / SQL Server |
   +--------------------+
```

Mentor explanation:

The frontend is responsible for user interaction. The backend is responsible for processing requests and implementing application functionality. The database stores information permanently. ASP.NET Core connects these worlds.

## 6. What should C# developers already know?

Before entering ASP.NET Core, revise your C# fundamentals.

| C# Concept           | Where you will use it             |
| -------------------- | --------------------------------- |
| Classes and Objects  | Models and Entities               |
| Interfaces           | Services and Repositories         |
| Inheritance          | Controllers and base classes      |
| Collections          | List of Products, Students        |
| Generics             | Generic services and repositories |
| LINQ                 | Filtering and querying data       |
| Exception Handling   | Error management                  |
| Async/Await          | Asynchronous API operations       |
| Delegates and Lambda | Middleware and endpoint handlers  |
| Dependency Injection | Managing application dependencies |
| JSON Serialization   | HTTP request/response data        |

One concept deserves special attention: Async/Await.

Web applications handle multiple users. Asynchronous programming helps avoid blocking request-processing threads while waiting for operations such as database or file I/O.

Example:

```
public async Task<List<Product>> GetProductsAsync()
{
    return await repository.GetAllAsync();
}
```
 

## 7. Your first ASP.NET Core application

Let's create a simple application.

Open terminal:

```
dotnet new web -n MyFirstWebAPI

cd MyFirstWebAPI

code .
```

Open `Program.cs`.

```
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Welcome to Transflower ASP.NET Core!";
});

app.MapGet("/mentor", () =>
{
    return new
    {
        Name = "Ravi Tambade",
        Organization = "Transflower",
        Role = "Chief Mentor"
    };
});

app.Run();
```

Run:

```
dotnet run
```

Open the URL displayed in your terminal.

Test:

```
GET /
GET /mentor
```

Congratulations! Your first ASP.NET Core web application is running.

Notice that you didn't create an MVC Controller, configure MySQL or write any complicated infrastructure.

This is the beauty of ASP.NET Core Minimal APIs.

## 8. Your ASP.NET Core learning roadmap at Transflower

I recommend teaching the subject through progressive hands-on exercises.

## C# Developer → ASP.NET Core Developer

1. Foundation
   - Introduction to ASP.NET Core
   - .NET CLI
   - Project structure
   - Program.cs
   - HTTP fundamentals
2. Minimal API Development
   - GET, POST, PUT, DELETE
   - Route parameters
   - JSON serialization
   - File-based CRUD
3. MVC Web API Development
   - Models
   - Controllers
   - Routing
   - HTTP status codes
   - DTOs
4. Application Architecture
   - Services
   - Repository Pattern
   - Dependency Injection
   - Separation of Concerns
   - Middleware
5. Database Programming
   - ADO.NET
   - MySQL connectivity
   - Entity Framework Core
   - LINQ
   - Database CRUD
6. Security and Testing
   - Authentication
   - JWT
   - Authorization
   - Unit Testing with xUnit
   - API Testing
7. Enterprise Development
   - Logging and Observability
   - Docker
   - CI/CD
   - Cloud Deployment
   - Microservices

## 9. First major classroom project

Recommended Project

Build a Student Management Web API

Start with these entities:

```
STUDENT
-------
StudentId
FirstName
LastName
Email
Mobile
CourseId
```

```
COURSE
------
CourseId
CourseName
Duration
Fees
```

Develop the following endpoints:

| HTTP Method | Endpoint          | Purpose        |
| ----------- | ----------------- | -------------- |
| GET         | `/api/students`   | Get students   |
| GET         | `/api/students/1` | Get student    |
| POST        | `/api/students`   | Add student    |
| PUT         | `/api/students/1` | Update student |
| DELETE      | `/api/students/1` | Delete student |
| GET         | `/api/courses`    | Get courses    |

Start with JSON file storage, then move to MySQL.

Finally, connect your React frontend to the ASP.NET Core backend.

This project will give students an end-to-end understanding of full-stack application development.

## 10. Opening message for your classroom

"Dear C# Developers,

Until today, you have been learning how to write programs. From today, I want you to start thinking about how to build applications. A class is not an application. A collection of methods is not an application. An application is a collaboration of multiple components, each having a well-defined responsibility. ASP.NET Core gives us the platform to bring these components together. Don't rush to learn Controllers, Entity Framework or Microservices. First understand HTTP. Understand how a request reaches the server. Understand how C# objects become JSON. Understand how services communicate with repositories. Understand how data travels between an application and a database.Once you understand these fundamentals, learning any modern backend framework becomes easier."

Remember: Frameworks are tools. Programming languages are mediums. Software engineering is the real skill.