# Dependency Injection (DI)

### *The Invisible Team That Builds Your Application*

> **"Good morning, future solution architects! Today, we are not just learning a framework feature. We are learning one of the greatest engineering principles that transformed modern software development. By the end of this session, you'll understand why ASP.NET Core feels so organized, scalable, and easy to maintain."**


## The Story Begins

Imagine you have been appointed as the **Chief Architect** of a brand-new five-star smart hotel.

The hotel has:

* 🛎 Reception
* 🛏 Room Service
* 🍽 Restaurant
* 🔒 Security
* 🧹 Housekeeping
* 💳 Billing
* 📦 Inventory

Every department has its own responsibility. Now imagine if every hotel room started hiring its own employees. Room 101 hires its own cleaner. Room 102 hires another cleaner. Room 103 hires another cook.
Room 104 buys its own CCTV camera. Would this hotel survive? Of course not. It would become chaos.
 

## Software Without Dependency Injection

Many beginners unknowingly build applications exactly like that.

```text
ProductsController
      |
      |
new ProductService()
      |
      |
new ProductRepository()
      |
      |
new DbContext()
      |
      |
new SqlConnection()
```

Every object creates another object. Every class becomes responsible for infrastructure. Instead of solving business problems... it spends time creating dependencies.

 
## The Architect's Question

As architects, we ask ourselves one important question.

> **"Should a ProductController know how to build a ProductService?"**

Should a Service know how to create a Repository? Should a Repository know how to create a Database Connection? Absolutely not. Each class should focus only on **its own responsibility**.


## The Philosophy of Dependency Injection

Dependency Injection simply says:

> **"Don't create what you need. Declare what you need, and let someone else provide it."**

Think about joining a company. On your first day, you don't purchase:

* Laptop
* Office chair
* Email account
* Network access
* Visual Studio
* Company ID card
 
The IT department prepares everything before you arrive. You simply start working. That is exactly what Dependency Injection does.



## Meet the IoC Container

Inside ASP.NET Core lives an invisible manager.

```text
+--------------------------------+
|        IoC Container           |
|--------------------------------|
| Registers Services             |
| Creates Objects                |
| Resolves Dependencies          |
| Injects Dependencies           |
| Manages Lifetime               |
| Disposes Resources             |
+--------------------------------+
```

You never call it directly. Yet it creates almost everything inside ASP.NET Core.



## Building an ASP.NET Core 10 Web API
 
Suppose we're building an Online Shopping API. Our architecture looks like this.

```text
                Browser
                   |
            HTTP Request
                   |
                   ▼
        ProductsController
                   |
                   ▼
          ProductService
                   |
                   ▼
       ProductRepository
                   |
                   ▼
         ApplicationDbContext
                   |
                   ▼
             SQL Server
```

Notice something. The Controller never creates the Service. The Service never creates the Repository. The Repository never creates DbContext. Everything is injected.


## Step 1 – Define the Contract

A professional architect always begins with contracts. Interfaces describe **what** should happen. They never describe **how**.

```csharp
public interface IProductService
{
    IEnumerable<Product> GetAll();
}

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
}

public interface ILoggerService
{
    void Log(string message);
}
```

Think of these as job descriptions.

```text
Restaurant Manager

Needs

Cook
Cashier
Waiter
```

Not specific people. Just roles.

 

## Step 2 – Hire the Specialists

Now professionals join the company.

```csharp
public class ProductRepository : IProductRepository
{
    public IEnumerable<Product> GetProducts()
    {
        return new List<Product>();
    }
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> GetAll()
    {
        return _repository.GetProducts();
    }
}
```

Each class performs one responsibility. No unnecessary object creation.

 

## Step 3 – Teach the IoC Container

In ASP.NET Core 10, **Program.cs** becomes the application's composition root.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();

var app = builder.Build();
```

Many beginners think this creates objects.

It doesn't. It only teaches the container. You're saying:

```text
Whenever someone asks for IProductService please provide ProductService.
```

Another instruction:

```text
Whenever someone requests IProductRepository provide ProductRepository.
```

The container memorizes these mappings.

 

## Step 4 – Constructor Injection

Now look at our Controller.

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetAll());
    }
}
```

Notice something beautiful. There is **no** `new ProductService()`. The controller simply declares:

> "I need an IProductService."

The IoC Container responds:

> "Already arranged."


## Behind the Scenes

When the first request arrives...

```text
GET /api/products
```

ASP.NET Core begins constructing objects.

```text
ProductsController needs IProductService needs IProductRepository needs ApplicationDbContext
```

The IoC Container walks this dependency tree.

```text
                ProductsController
                         |
                         ▼
                 IProductService
                         |
                         ▼
                  ProductService
                         |
                         ▼
               IProductRepository
                         |
                         ▼
                ProductRepository
                         |
                         ▼
                ApplicationDbContext
                         |
                         ▼
                    SQL Server
```

Everything is created automatically.


## The Dependency Tree

Think of it as assembling LEGO blocks.

```text
Controller
↓
Service
↓
Repository
↓
DbContext
↓
Database
```

Each block depends on the block below it. The container assembles the entire structure.
 

## Service Lifetimes

One of the IoC Container's biggest responsibilities is deciding **how long an object should live**.

### Singleton

```csharp
builder.Services.AddSingleton<ICacheService, CacheService>();
```

```text
Application Starts 
Create Once
Reuse Forever
Application Ends
```

Examples:

* Configuration
* Cache
* Logging

### Scoped

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
```

```text
HTTP Request 1

New ProductService

Dispose

------------------

HTTP Request 2

New ProductService

Dispose
```

Perfect for:

* DbContext
* Services
* Repositories



### Transient

```csharp
builder.Services.AddTransient<IValidator, ProductValidator>();
```

```text
Need Object?
Create One

Need Again?
Create Another
```

Always fresh.

## Lifetime Comparison

```text
                    Application

              Singleton
      +--------------------------+
      |                          |
      |     One Instance         |
      |                          |
      +--------------------------+

Request 1
----------------------
Scoped Instance A

Transient 1
Transient 2

----------------------

Request 2
----------------------
Scoped Instance B

Transient 3
Transient 4
```

## Why Testing Becomes Easy

Without Dependency Injection:

```text
ProductService
|
new ProductRepository()
```

Testing always connects to the database. With Dependency Injection:

```text
                ProductService
                      |
        +-------------+-------------+
        |                           |
        ▼                           ▼
Real Repository             Mock Repository
```

During testing, simply inject a mock implementation. No database required.


## ASP.NET Core Uses DI Everywhere

Many students think Dependency Injection is only for Controllers. Actually...

```text
+----------------------------------------+
| Controllers                            |
| Services                               |
| Repositories                           |
| DbContext                              |
| Identity                               |
| Authentication                         |
| Authorization                          |
| Logging                                |
| Configuration                          |
| Middleware                             |
| HttpClientFactory                      |
| Memory Cache                           |
| Distributed Cache                      |
| Background Services                    |
| Minimal APIs                           |
+----------------------------------------+
```

The framework itself is built around Dependency Injection.

## Complete Request Lifecycle with DI

```text
Browser
    |
HTTP Request
    |
    ▼
Kestrel
    |
    ▼
Middleware Pipeline
    |
    ▼
Routing
    |
    ▼
IoC Container
    |
    +-------------------------------+
    | Create Controller             |
    | Inject ProductService         |
    | Inject Repository             |
    | Inject DbContext              |
    | Inject Logger                 |
    | Inject Configuration          |
    +-------------------------------+
    |
    ▼
Controller Executes
    |
    ▼
JSON Response
    |
    ▼
Browser
```

## Mentor's Wisdom

> **"Dependency Injection is not about avoiding the `new` keyword. It is about separating responsibilities. Your business classes should focus on business rules, not on object creation. When you let the IoC Container build and manage your application's objects, you gain loose coupling, easier testing, better maintainability, and the flexibility to evolve your architecture without rewriting everything."**


## Final Takeaway

```text
Without Dependency Injection
----------------------------

Controller
      |
new Service
      |
new Repository
      |
new DbContext

Result:
Tight Coupling
Hard Testing
Difficult Maintenance


With Dependency Injection
-------------------------

Controller
      |
Requests Service
      |
IoC Container
      |
Creates Everything
      |
Injects Dependencies

Result:
Loose Coupling
Easy Testing
Clean Architecture
Scalable Applications
```

> **"Whenever you write `builder.Services.AddScoped()`, imagine you're onboarding a new employee into your software company. You're not creating the employee immediately—you're teaching the IoC Container who to hire, when to hire them, and how long they should stay. That's why I call the IoC Container the invisible Project Manager of ASP.NET Core. It quietly coordinates the entire team so your application can focus on delivering value to its users."**