# ASP.NET Core Middleware – Understanding the HTTP Request Pipeline

Transflower Mentor Classroom | C# Developers → ASP.NET Core Developers

In our previous lesson, we understood `Program.cs`.

We learned three important statements:

```
CreateBuilder()  // Prepare application
Build()          // Create application
Run()            // Start application
```

Now let's explore an interesting question. When a client sends an HTTP request to an ASP.NET Core application, how does that request reach a Controller? Does it directly reach the Controller?

No!

Before reaching the Controller, the request travels through a sequence of components called Middleware.

Let's understand this step by step.

## 1. What is Middleware?

Middleware is a software component that participates in processing HTTP requests and responses in an ASP.NET Core application.

Each middleware component can:

1. Receive an HTTP request.
2. Perform some processing.
3. Pass the request to the next middleware.
4. Process the response while it travels back.

Examples:

- Exception Handling Middleware
- HTTPS Redirection Middleware
- Authentication Middleware
- Authorization Middleware
- CORS Middleware
- Logging Middleware

Mentor analogy: Airport security

Imagine a passenger entering an airport.

Before boarding an aircraft, the passenger passes through different checkpoints.

- Entry gate checks access.
- Security checks baggage.
- Immigration verifies identity.
- Boarding gate checks the ticket.

Similarly, an HTTP request passes through different middleware components before reaching its destination.

## 2. Understand the HTTP Request Pipeline

Let's visualize the complete process.

```
          CLIENT APPLICATION
         (React / Angular / Postman)
                    |
                    |
                    | HTTP REQUEST
                    v
          +---------------------+
          |    KESTREL SERVER   |
          |                     |
          | Receives HTTP       |
          | Request             |
          +---------------------+
                    |
                    v
          +---------------------+
          | EXCEPTION HANDLING  |
          | MIDDLEWARE          |
          +---------------------+
                    |
                    v
          +---------------------+
          | HTTPS REDIRECTION   |
          | MIDDLEWARE          |
          +---------------------+
                    |
                    v
          +---------------------+
          | CORS MIDDLEWARE     |
          +---------------------+
                    |
                    v
          +---------------------+
          | AUTHENTICATION      |
          | MIDDLEWARE          |
          +---------------------+
                    |
                    v
          +---------------------+
          | AUTHORIZATION       |
          | MIDDLEWARE          |
          +---------------------+
                    |
                    v
          +---------------------+
          | ENDPOINT ROUTING    |
          |                     |
          | ProductsController |
          +---------------------+
                    |
                    v
          +---------------------+
          | BUSINESS LOGIC      |
          | ProductService      |
          +---------------------+
                    |
                    v
          +---------------------+
          | DATA ACCESS         |
          | ProductRepository   |
          +---------------------+
                    |
                    v
          +---------------------+
          | JSON / MYSQL        |
          +---------------------+
                    |
                    v
             HTTP RESPONSE
                    |
                    v
             CLIENT APPLICATION
```

This is a conceptual pipeline. The actual middleware order depends on the application's configuration.

## 3. Create a simple middleware example

Let's create a new ASP.NET Core Web API project.

```
dotnet new webapi -n MiddlewareDemo
cd MiddlewareDemo
code .
```

Open `Program.cs`.

Start with:

```
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Welcome to Transflower");
app.Run();
```

Now let's introduce our first middleware.

## 4. Middleware using app.Use()

The `app.Use()` method allows us to add middleware to the request pipeline.

```
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 1 - Before");
    await next();
    Console.WriteLine("Middleware 1 - After");
});
```

Let's understand the code.

- `context` represents the current HTTP context.
- `next` represents the next middleware in the pipeline.
- `await next()` passes control to the next middleware.

Now add another middleware.

```
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 2 - Before");
    await next();
    Console.WriteLine("Middleware 2 - After");
});
```

Finally, add an endpoint.

```
app.MapGet("/", () =>
{
    Console.WriteLine("Endpoint Executed");
    return "Welcome to Transflower";
});
```

Complete `Program.cs`:

```
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 1 - Before");
    await next();
    Console.WriteLine("Middleware 1 - After");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 2 - Before");
    await next();
    Console.WriteLine("Middleware 2 - After");
});

app.MapGet("/", () =>
{
    Console.WriteLine("Endpoint Executed");
    return "Welcome to Transflower";
});

app.Run();
```

### What happens when we execute this application?

Send a GET request to `/`.

Expected console output:

```
Middleware 1 - Before

Middleware 2 - Before

Endpoint Executed

Middleware 2 - After

Middleware 1 - After
```

Understand the execution sequence

Middleware 1 – Before

Middleware 2 – Before

Endpoint Execution

Middleware 2 – After

Middleware 1 – After

Request travels downward. Response unwinds through middleware in reverse order.

Important learning: Middleware execution is nested. Code before `await next()` executes on the way in, and code after it executes on the way out.

## 5. Difference between Use(), Run() and Map()

These three methods are important in ASP.NET Core.

| Method      | Responsibility                                           |
| ----------- | -------------------------------------------------------- |
| `app.Use()` | Executes middleware and optionally calls next middleware |
| `app.Run()` | Adds terminal middleware that ends the pipeline          |
| `app.Map()` | Branches the pipeline based on a request path            |

Example of terminal middleware:

```
app.Run(async context =>
{
    await context.Response.WriteAsync("Request stopped here!");
});
```

This middleware does not call the next component.

Anything registered after it in the same pipeline will not execute for requests reaching it.

Remember: This `app.Run()` overload is different from the host-starting `app.Run()` statement at the end of Program.cs.

## 6. Build your own custom logging middleware

Now let's create a reusable middleware component.

Create a folder:

```
MiddlewareDemo/
|
+-- Middleware/
|   |
|   +-- RequestLoggingMiddleware.cs
|
+-- Program.cs
```

Create `Middleware/RequestLoggingMiddleware.cs`.

```
namespace MiddlewareDemo.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine(
            $"Request: {context.Request.Method} " +
            $"{context.Request.Path}"
        );

        await _next(context);

        Console.WriteLine($"Response Status: {context.Response.StatusCode}"
        );
    }
}
```

Now register it in `Program.cs`.

```
using MiddlewareDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapGet("/", () => "Welcome to Transflower");

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

Now every request passing through this middleware gets logged.

Example output:

```
Request: GET /products
Response Status: 200
```

This is how developers build reusable application infrastructure.

## 7. Important built-in middleware

Let's introduce the middleware commonly used in enterprise applications.

| Middleware              | Purpose                        |
| ----------------------- | ------------------------------ |
| `UseExceptionHandler()` | Centralized exception handling |
| `UseHttpsRedirection()` | Redirect HTTP to HTTPS         |
| `UseStaticFiles()`      | Serve static files             |
| `UseRouting()`          | Enable endpoint routing        |
| `UseCors()`             | Configure cross-origin access  |
| `UseAuthentication()`   | Identify the user              |
| `UseAuthorization()`    | Check user permissions         |
| `UseResponseCaching()`  | Cache eligible HTTP responses  |

A typical configuration might look like this:

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Here, `AddAuthentication()` and `AddAuthorization()` register the required services. A real application must also configure an appropriate authentication scheme, such as JWT Bearer authentication.

Middleware order matters. For example, CORS should run before authentication and authorization when those components are used.

## 8. Middleware versus Controller

Students often confuse these two concepts.

| Middleware                            | Controller                                   |
| ------------------------------------- | -------------------------------------------- |
| Works with the HTTP pipeline          | Handles application endpoints                |
| Can execute for many requests         | Executes for matched routes                  |
| Handles cross-cutting concerns        | Handles application-specific HTTP operations |
| Logging, security, exception handling | Product CRUD, Student CRUD                   |

Think about it this way:

Middleware manages the journey of the request.

Controller manages the destination's application operation.

## 9. Transflower practical assignment

Hands-on Lab

Build a Product API with custom middleware.

Requirements:

1. Create a Products Web API.
2. Implement custom request logging middleware.
3. Log HTTP method and URL.
4. Log response status code.
5. Create an exception handling middleware.
6. Test GET, POST, PUT and DELETE using Postman.
7. Observe middleware execution order.


## 10. What comes next in our ASP.NET Core journey?

We have now understood:

1. What is ASP.NET Core?
2. Program.cs and application startup.
3. Middleware and HTTP request pipeline.

The next logical topic is:

ASP.NET Core Routing and Controllers – How HTTP requests reach C# methods.

We will take a GET request such as:

`GET /api/products/101`

And trace how ASP.NET Core identifies the ProductsController, extracts the product ID, executes the corresponding C# method and returns JSON to the client.

Ravi Sir's classroom takeaway:

"Don't think of middleware as just another C# class. Think of it as a checkpoint in the journey of every HTTP request. Understanding this journey is the foundation of backend application development."