# Understanding Program.cs in an ASP.NET Core Application

Transflower Mentor Classroom | From C# Main() to ASP.NET Core Application

When C# developers start learning ASP.NET Core, one of the first files they encounter is `Program.cs`.

Students often ask:

- Where is the `Main()` method?
- Who creates the web server?
- What is `WebApplication.CreateBuilder()`?
- What is the purpose of `builder.Build()`?
- Why do we register services?
- What is middleware?
- What happens when `app.Run()` executes?

Let's understand these concepts step by step.

## 1. What is Program.cs?

In a traditional C# console application, execution starts from the `Main()` method.

```
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}
```

In modern ASP.NET Core applications, we use Top-Level Statements.

We don't need to explicitly write the `Main()` method.

Example:

```
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Welcome to Transflower!");

app.Run();
```

The compiler generates the entry-point code for us.

Mentor analogy:

Think of `Program.cs` as the control room of an application.

It configures the application, registers the required components, creates the application and starts its execution.

## 2. Understand the lifecycle of Program.cs

Before understanding individual statements, look at the complete execution sequence.

```
          PROGRAM.CS
               |
               v
    +------------------------+
    | 1. CreateBuilder()     |
    |                        |
    | Initialize Host        |
    | Load Configuration     |
    | Prepare DI Container   |
    +------------------------+
               |
               v
    +------------------------+
    | 2. Register Services   |
    |                        |
    | Controllers            |
    | Services               |
    | Repositories           |
    +------------------------+
               |
               v
    +------------------------+
    | 3. builder.Build()     |
    |                        |
    | Create WebApplication  |
    +------------------------+
               |
               v
    +------------------------+
    | 4. Configure Pipeline  |
    |                        |
    | Middleware             |
    | Authentication         |
    | Authorization          |
    +------------------------+
               |
               v
    +------------------------+
    | 5. Map Endpoints       |
    |                        |
    | MapControllers()       |
    | MapGet()               |
    +------------------------+
               |
               v
    +------------------------+
    | 6. app.Run()           |
    |                        |
    | Start HTTP Server      |
    | Listen for Requests    |
    +------------------------+
               |
               v
        APPLICATION RUNNING
```

There are two important objects in this process:

- `builder` – used to configure the application before building it.
- `app` – represents the configured web application that handles incoming requests.

## 3. Understand Program.cs line by line

Let's take a simple ASP.NET Core Web API application.

```
using ProductsWebAPI.Repositories;
using ProductsWebAPI.Repositories.Interfaces;
using ProductsWebAPI.Services;
using ProductsWebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register MVC Controllers
builder.Services.AddControllers();

// Register Repository
builder.Services.AddScoped<IProductRepository,ProductRepository>();

// Register Service
builder.Services.AddScoped<IProductService,ProductService>();

// Build application
var app = builder.Build();

// Configure HTTP pipeline
app.UseHttpsRedirection();

// Map controller endpoints
app.MapControllers();

// Start application
app.Run();
```

Let's understand every statement.

## 4. Statement 1 – CreateBuilder()

```
var builder = WebApplication.CreateBuilder(args);
```

This is the first important statement. What happens here? ASP.NET Core prepares the application environment. It initializes several important components:

- Application configuration
- Dependency Injection container
- Logging infrastructure
- Web hosting infrastructure
- Server configuration

The `args` parameter represents command-line arguments passed to the application.

For example:

```
dotnet run --environment Development
```

The builder prepares the application for configuration.

Mentor analogy:

Imagine constructing a house.

Before constructing rooms, you prepare the land, arrange resources and create the construction plan.

`CreateBuilder()` performs a similar preparation for our web application.

### What is WebApplicationBuilder?

`WebApplicationBuilder` is the object that allows us to configure the application before building it.

```
        WebApplicationBuilder
                 |
       +---------+---------+
       |         |         |
       v         v         v
   Services  Configuration Logging
       |
       v
   Hosting Configuration
```

## 5. Statement 2 – Register Services

```
builder.Services.AddControllers();
```

What is `builder.Services`?

It is the service collection used by ASP.NET Core's Dependency Injection system. We register application components here. `AddControllers()` registers the services needed for MVC controllers and API controller functionality. Now look at our Product API.

```
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();
```

What are we doing?

We are telling ASP.NET Core: "When someone requests an IProductRepository, create and provide a ProductRepository object."

Similarly:"When someone requests an IProductService, provide a ProductService object."

### Understand Dependency Injection

```
       SERVICE REGISTRATION
       --------------------

       IProductService
              |
              v
        ProductService

       IProductRepository
              |
              v
       ProductRepository
```

Consider the constructor of our Controller:

```
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }
}
```

We are not manually creating a ProductService object.

ASP.NET Core resolves the registered dependency and injects it into the Controller.

### Understand service lifetimes

ASP.NET Core provides three common dependency injection lifetimes.

| Lifetime  | Meaning                                        |
| --------- | ---------------------------------------------- |
| Transient | New object every time it is requested          |
| Scoped    | One object per HTTP request scope              |
| Singleton | One shared object for the application lifetime |

Example:

```
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IProductService, ProductService>();
```

For our Product API, we are using `AddScoped()`.

Mentor checkpoint: Ask students why we use an interface rather than directly creating a concrete class.

Expected answer: To achieve abstraction, loose coupling and easier replacement of implementations.

## 6. Statement 3 – Build()

```
var app = builder.Build();
```

This is another important statement. Until now, we have been configuring the application. Now we build it. The builder creates a `WebApplication` instance using the registered services and configuration.

Understand the relationship:

```
    WebApplicationBuilder
             |
             | Configure
             |
      +------+------+
      |             |
      v             v
   Services     Configuration
      |             |
      +------+------+
             |
             v
        Build()
             |
             v
      WebApplication
             |
             v
       var app
```

Important distinction:

- `builder` is used to configure the application.
- `app` is used to configure its request pipeline and run it.

## 7. Statement 4 – Configure Middleware

```
app.UseHttpsRedirection();
```

What is middleware?

Middleware is software that participates in processing HTTP requests and responses. Imagine a request entering an ASP.NET Core application. Before reaching a Controller, it may pass through several middleware components.

For example:

- HTTPS Redirection
- Exception Handling
- Authentication
- Authorization
- CORS
- Request Logging

Consider this diagram:

```
           HTTP REQUEST
                |
                v
      +--------------------+
      | HTTPS Redirection  |
      +--------------------+
                |
                v
      +--------------------+
      | Exception Handler  |
      +--------------------+
                |
                v
      +--------------------+
      | Authentication     |
      +--------------------+
                |
                v
      +--------------------+
      | Authorization      |
      +--------------------+
                |
                v
      +--------------------+
      | Controller Endpoint|
      +--------------------+
                |
                v
          HTTP RESPONSE
```

Middleware components form a pipeline.

Each middleware can:

1. Process the incoming request.
2. Pass control to the next middleware.
3. Process the outgoing response.

For example:

```
app.UseHttpsRedirection();
```

This redirects HTTP requests to HTTPS when the HTTPS endpoint is configured.

## 8. Statement 5 – MapControllers()

```
app.MapControllers();
```

What does this statement do?

It connects the Controller endpoints with the ASP.NET Core application's endpoint routing system.

Consider our Controller:

```
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("All products");
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok($"Product ID: {id}");
    }
}
```

ASP.NET Core identifies the routes using the Controller and HTTP method attributes.

The resulting endpoints are:

| Attribute              | Endpoint                 |
| ---------------------- | ------------------------ |
| `[HttpGet]`            | `GET /api/products`      |
| `[HttpGet("{id}")]`    | `GET /api/products/1`    |
| `[HttpPost]`           | `POST /api/products`     |
| `[HttpPut("{id}")]`    | `PUT /api/products/1`    |
| `[HttpDelete("{id}")]` | `DELETE /api/products/1` |

The route:

```
[Route("api/[controller]")]
```

Uses the Controller name without the `Controller` suffix.

Therefore:

`ProductsController` becomes `products`.

Important: `AddControllers()` registers the MVC controller services, whereas `MapControllers()` makes the controller routes available to incoming HTTP requests.

## 9. Statement 6 – Run()

```
app.Run();
```

This statement starts the web application.

ASP.NET Core starts its hosting infrastructure and listens for incoming HTTP requests using its configured web server, typically Kestrel.

For example:

```
Now listening on: https://localhost:7001
Now listening on: http://localhost:5001
```

The application is now ready to accept requests.

Consider what happens:

```
       ASP.NET CORE APPLICATION
                   |
                   v
              app.Run()
                   |
                   v
            KESTREL SERVER
                   |
                   v
          LISTENING ON PORT
                   |
                   v
          WAITING FOR REQUESTS
                   |
          +--------+--------+
          |                 |
          v                 v
       GET Request       POST Request
          |                 |
          v                 v
      Controller        Controller
          |                 |
          v                 v
       Response          Response
```

One important observation: `app.Run()` in this context starts the web host and blocks while the application runs. It is different from the `app.Run(...)` middleware overload, which adds terminal middleware to the request pipeline.

## 10. Complete Program.cs – With all major components

Now let's bring all the concepts together. This is a practical `Program.cs` for our Products Web API.

```
using ProductsWebAPI.Repositories;
using ProductsWebAPI.Repositories.Interfaces;
using ProductsWebAPI.Services;
using ProductsWebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// STEP 1: Register MVC Controllers
builder.Services.AddControllers();

// STEP 2: Register Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// STEP 3: Register Business Services
builder.Services.AddScoped<IProductService,ProductService>();

// STEP 4: Build Application
var app = builder.Build();

// STEP 5: Configure Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// STEP 6: Map Controller Routes
app.MapControllers();

// STEP 7: Start Web Application
app.Run();
```

## 11. Understand the two execution phases

This is a very important concept for C# developers. Program.cs has two major phases.

## Phase 1 – Application Startup

Runs during application initialization

```
CreateBuilder()
     |
     v
Register Services
     |
     v
Build()
     |
     v
Configure Middleware
     |
     v
MapControllers()
     |
     v
Application Ready
```

## Phase 2 – Request Processing

Runs for incoming HTTP requests

```
HTTP Request
     |
     v
Middleware Pipeline
     |
     v
Controller Routing
     |
     v
Service Execution
     |
     v
Repository Execution
     |
     v
JSON File / Database
     |
     v
HTTP Response
```

The first phase prepares the application. The second phase handles user requests. This distinction helps students understand why we don't write business operations directly in the startup configuration.

## 12. What else does CreateBuilder() configure internally?

This is an interesting discussion for students who want to understand the .NET framework.

When we execute:

```
var builder = WebApplication.CreateBuilder(args);
```

ASP.NET Core prepares several infrastructure components.

| Component            | Responsibility                                   |
| -------------------- | ------------------------------------------------ |
| Configuration        | Reads appsettings.json and environment variables |
| Logging              | Configures application logging                   |
| Dependency Injection | Creates service container                        |
| Hosting              | Configures application hosting                   |
| Kestrel              | Provides the web server                          |
| Environment          | Identifies Development, Staging or Production    |

For example, application configuration can be read using:

```
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
```

And environment information can be accessed using:

```
var environment = builder.Environment.EnvironmentName;
```

This information is useful when developing applications that behave differently in development and production environments.

## 13. Understand the complete ASP.NET Core application lifecycle

```
       C# DEVELOPER WRITES CODE
                   |
                   v
              Program.cs
                   |
                   v
       WebApplication.CreateBuilder()
                   |
                   v
       Configure Application Services
                   |
                   v
            builder.Build()
                   |
                   v
       Configure Middleware Pipeline
                   |
                   v
            MapControllers()
                   |
                   v
              app.Run()
                   |
                   v
          KESTREL WEB SERVER
                   |
                   v
          HTTP REQUEST ARRIVES
                   |
                   v
          MIDDLEWARE PIPELINE
                   |
                   v
          CONTROLLER EXECUTION
                   |
                   v
           SERVICE EXECUTION
                   |
                   v
         REPOSITORY EXECUTION
                   |
                   v
          JSON FILE / DATABASE
                   |
                   v
           HTTP RESPONSE
                   |
                   v
                CLIENT
```

## 14. Classroom exercise

Ask students to create a new ASP.NET Core project and experiment with the following statements.

| Exercise | Statement                            |
| -------- | ------------------------------------ |
| 1        | `WebApplication.CreateBuilder(args)` |
| 2        | `builder.Services.AddControllers()`  |
| 3        | `builder.Services.AddScoped()`       |
| 4        | `builder.Build()`                    |
| 5        | `app.UseHttpsRedirection()`          |
| 6        | `app.MapControllers()`               |
| 7        | `app.Run()`                          |

Ask them to temporarily remove one statement at a time and observe the compiler errors or runtime behavior. This is an effective way to understand the responsibility of each statement.

## 15. Questions for classroom discussion

1. What is the difference between `builder` and `app`?
2. Why do we need Dependency Injection?
3. What happens inside `builder.Build()`?
4. What is middleware?
5. What is the responsibility of Kestrel?
6. What is the difference between `AddControllers()` and `MapControllers()`?
7. Does `app.Run()` execute for every HTTP request?
8. Why should business logic not be written inside Program.cs?

## 16. Ravi Sir's final explanation

I would conclude this lesson with the following message:

"Dear C# developers,You already know how to write classes, methods, interfaces and objects. Now ASP.NET Core introduces you to an application hosting environment. Program.cs is where we assemble the application.

We register services, configure infrastructure, prepare middleware and expose endpoints. Once the application starts, HTTP requests enter the pipeline and get processed by the appropriate components. Remember these three statements:

- `CreateBuilder()` – Prepare the application.
- `Build()` – Create the application.
- `Run()` – Start the application.

Everything else helps configure how the application behaves."

The next important topic to learn is ASP.NET Core Middleware and the HTTP Request Pipeline. This will help students understand how a request travels from Kestrel to a Controller and how the response returns to the client.