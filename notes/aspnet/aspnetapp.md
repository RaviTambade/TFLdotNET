# Standard ASP.NET Core Web API – Application Folder Structure

Transflower Mentor Classroom | From Minimal API to Clean Application Architecture

Now that students have implemented CRUD operations using Minimal APIs and JSON file serialization, the next step is to introduce a standard ASP.NET Core Web API project structure.

The objective is to teach students how to organize code into meaningful folders instead of writing everything inside `Program.cs`.

Mentor's principle:

"Initially, focus on making the application work. Then learn how to organize the application so that it becomes maintainable, testable and scalable."

## 1. Understand the problem with putting everything in Program.cs

In our previous Product API, we placed:

- API endpoints
- Product model
- File reading and writing
- JSON serialization
- CRUD business logic

Everything was in one file.

This is acceptable for a beginner's demonstration, but imagine an application with:

- 50 API endpoints
- 20 business entities
- Authentication and authorization
- Database connectivity
- Logging and exception handling
- Business validation

Maintaining everything in `Program.cs` becomes difficult.

We need separation of responsibilities.

## 2. Standard ASP.NET Core Web API folder structure

Here is a practical folder structure for a medium-sized ASP.NET Core Web API application.

```
ProductManagementAPI/
│
├── Controllers/
│   └── ProductsController.cs
│
├── Models/
│   └── Product.cs
│
├── DTOs/
│   ├── ProductRequestDto.cs
│   └── ProductResponseDto.cs
│
├── Services/
│   ├── Interfaces/
│   │   └── IProductService.cs
│   │
│   └── ProductService.cs
│
├── Repositories/
│   ├── Interfaces/
│   │   └── IProductRepository.cs
│   │
│   └── ProductRepository.cs
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── products.json
│
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs
│
├── Validators/
│   └── ProductValidator.cs
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── ProductManagementAPI.csproj
```

This is a suggested practical structure, not a mandatory ASP.NET Core convention. A small application may need fewer folders, while larger systems may use separate projects for different architectural layers.

## 3. Understand the responsibility of each folder

Let's understand why each folder exists.

### 1. Controllers – Handle HTTP requests

Presentation Layer

Controllers are responsible for receiving HTTP requests and sending HTTP responses.

Example: `ProductsController.cs`

```
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
```

Responsibilities:

- Receive HTTP requests.
- Read route parameters and request bodies.
- Call appropriate services.
- Return HTTP status codes and responses.

Remember: Controllers should not contain complex business logic or database queries.

### 2. Models – Represent application data

Domain / Data Representation

Example: `Models/Product.cs`

```
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public string Category { get; set; } = "";
}
```

A model represents a product in our application.

Think of it as a blueprint for creating Product objects.

### 3. DTOs – Data Transfer Objects

Communication Contract

DTO means Data Transfer Object.

Why do we need DTOs?

Suppose our internal Product model contains:

```
Product
-------
Id
Name
Price
Category
CostPrice
SupplierId
CreatedDate
InternalRemarks
```

But the frontend only needs:

```
ProductResponseDto
------------------
Id
Name
Price
Category
```

We don't necessarily want to expose internal application data to external clients.

Example: `DTOs/ProductRequestDto.cs`

```
public class ProductRequestDto
{
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public string Category { get; set; } = "";
}
```

Example: `DTOs/ProductResponseDto.cs`

```
public class ProductResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public string Category { get; set; } = "";
}
```

Mentor analogy:

A Product model is your internal business record. A DTO is the information you choose to share with another person.

### 4. Services – Implement business logic

Business Logic Layer

Services contain application operations and business rules.

Example: `Services/Interfaces/IProductService.cs`

```
public interface IProductService
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);
}
```

Example: `Services/ProductService.cs`

```
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
```

Responsibilities:

- Implement business rules.
- Coordinate multiple repositories or other services.
- Perform business validation.
- Prepare application results.

For example:

```
Check product availability
Calculate product discount
Validate product price
Check stock quantity
```

### 5. Repositories – Handle data access

Data Access Layer

Repositories communicate with the storage mechanism.

In our earlier practical, the storage mechanism was `products.json`.

Later, we can replace it with MySQL or SQL Server.

Example: `Repositories/Interfaces/IProductRepository.cs`

```
public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);
}
```

Example: `Repositories/ProductRepository.cs`

```
public class ProductRepository : IProductRepository
{
    private readonly string _filePath;

    public ProductRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "products.json"
        );
    }

    public async Task<List<Product>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
            return new List<Product>();

        var json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<Product>>(json)
               ?? new List<Product>();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var products = await GetAllAsync();

        return products.FirstOrDefault(p => p.Id == id);
    }
}
```

The repository hides the details of data storage from the service.

For this teaching example, we have implemented only read operations. POST, PUT and DELETE can be added to the same repository.

### 6. Data – Database and persistence configuration

This folder contains database-related infrastructure.

Example:

```
Data/
│
├── ApplicationDbContext.cs
├── products.json
└── DatabaseSeeder.cs
```

When using Entity Framework Core:

```
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
```

For our current JSON lab, `products.json` is the persistence file. We don't need `ApplicationDbContext` yet.

### 7. Middleware – Handle cross-cutting concerns

Middleware executes as part of the HTTP request pipeline.

Examples:

- Exception handling
- Logging
- Authentication
- Authorization
- Request/response monitoring

```
HTTP REQUEST
     |
     v
Exception Middleware
     |
     v
Authentication Middleware
     |
     v
Authorization Middleware
     |
     v
Controller
     |
     v
HTTP RESPONSE
```

### 8. Validators – Validate incoming data

Validators ensure that incoming data follows application rules.

For example:

```
Product Validation
------------------
Name must not be empty
Price must be greater than zero
Category must be provided
```

For simple projects, use Data Annotations or manual validation. For more complex validation, a dedicated validation library can be introduced.

### 9. Program.cs – Application entry point

This is where the application starts and services are registered.

Example:

```
var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure HTTP pipeline
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
```

Notice something important:

We are registering interfaces with their implementations.

This is called Dependency Injection (DI).

ASP.NET Core creates the required objects and supplies them to controllers and services.

## 4. Understand the complete request execution flow

This is the most important diagram to explain in the classroom.

## ASP.NET Core Web API Architecture

React / Angular / Postman

Client Application

HTTP Request

Controllers

HTTP Routing and Response Handling

Services

Business Rules and Application Logic

Repositories

Data Access Operations

Data Storage

JSON File / MySQL / SQL Server

Response travels back to the client

Data → Repository → Service → Controller → HTTP Response

The important observation is that each layer has a specific responsibility.

The Controller doesn't need to know how the JSON file is read.

The Service doesn't need to know how HTTP routing works.

The Repository doesn't need to know whether the request came from React or Postman.

That is separation of concerns.

## 5. How folders communicate with each other

Let's visualize the dependency relationship.

```
          Program.cs
              |
              | Dependency Injection
              v
         Controller
              |
              | IProductService
              v
          Service
              |
              | IProductRepository
              v
         Repository
              |
              | File I/O / SQL
              v
         Data Storage
```

Notice the use of interfaces:

```
IProductService
       |
       v
ProductService

IProductRepository
       |
       v
ProductRepository
```

This allows us to change implementations without rewriting the entire application.

For example:

```
                 IProductRepository
                         |
              +----------+-----------+
              |                      |
              v                      v
      JsonProductRepository    MySqlProductRepository
              |                      |
              v                      v
         products.json             MySQL
```

The service can continue using `IProductRepository`.

This is an introduction to Dependency Inversion Principle (DIP) and loosely coupled application design.

## 6. Standard folder structure versus Clean Architecture

Students should understand that these are two different levels of architectural organization.

| Standard Web API | Clean Architecture             |
| ---------------- | ------------------------------ |
| Controllers      | Presentation                   |
| Models           | Domain                         |
| DTOs             | Application contracts          |
| Services         | Application use cases          |
| Repositories     | Infrastructure implementations |
| Data             | Infrastructure                 |
| Program.cs       | Composition root               |

A standard ASP.NET Core Web API can have all these folders in one project.

In Clean Architecture, we can separate them into multiple projects.

```
ProductManagement/
│
├── ProductManagement.API/
│   └── Controllers/
│
├── ProductManagement.Application/
│   ├── Interfaces/
│   ├── DTOs/
│   └── Services/
│
├── ProductManagement.Domain/
│   ├── Entities/
│   └── Interfaces/
│
└── ProductManagement.Infrastructure/
    ├── Data/
    └── Repositories/
```

Do not introduce this complexity immediately. First make students comfortable with a standard Web API.

## 7. Practical assignment for Transflower students

Take the earlier Product JSON CRUD application and reorganize it.



Hands-on Lab

&#x20;Product Management API – Refactoring Exercise



| Step | Student activity                      |
| ---- | ------------------------------------- |
| 1    | Create Controllers folder             |
| 2    | Move Product class into Models        |
| 3    | Create DTOs                           |
| 4    | Create IProductService interface      |
| 5    | Implement ProductService              |
| 6    | Create IProductRepository interface   |
| 7    | Implement ProductRepository           |
| 8    | Move products.json into Data          |
| 9    | Configure Dependency Injection        |
| 10   | Test all CRUD endpoints using Postman |

Assessment challenge: After completing JSON-based CRUD, replace JSON file storage with MySQL without changing the ProductsController.

This tests whether students have genuinely understood separation of concerns.


## 8. Layered Architecture

```
       ASP.NET CORE WEB API
       PRODUCT MANAGEMENT SYSTEM
       =========================

              CLIENT LAYER
       +-------------------------+
       | React / Angular /       |
       | Postman / Browser       |
       +-------------------------+
                   |
                   | HTTP Request
                   | GET / POST / PUT / DELETE
                   v
       +-------------------------+
       |   PRESENTATION LAYER    |
       |-------------------------|
       | Controllers/            |
       | ProductsController.cs   |
       |                         |
       | Receives HTTP Requests  |
       | Returns HTTP Responses  |
       +-------------------------+
                   |
                   | Calls Interface
                   v
       +-------------------------+
       |   BUSINESS LOGIC LAYER  |
       |-------------------------|
       | Services/               |
       | IProductService.cs      |
       | ProductService.cs       |
       |                         |
       | Business Rules          |
       | Validation              |
       | Application Logic       |
       +-------------------------+
                   |
                   | Calls Interface
                   v
       +-------------------------+
       |     DATA ACCESS LAYER   |
       |-------------------------|
       | Repositories/           |
       | IProductRepository.cs   |
       | ProductRepository.cs    |
       |                         |
       | Read / Write Operations |
       | Serialization           |
       | Deserialization         |
       +-------------------------+
                   |
                   | File I/O
                   v
       +-------------------------+
       |       DATA LAYER        |
       |-------------------------|
       | Data/                   |
       |                         |
       | products.json           |
       |                         |
       | Product Records         |
       +-------------------------+


       CROSS-CUTTING CONCERNS
       ======================

       +-------------------------+
       | Middleware              |
       | Exception Handling      |
       | Logging                 |
       | Authentication          |
       | Authorization           |
       +-------------------------+

       +-------------------------+
       | Validators              |
       | Input Validation        |
       | Business Validation     |
       +-------------------------+

       +-------------------------+
       | Configuration           |
       | appsettings.json        |
       | Connection Strings      |
       +-------------------------+

```


## 9. Application flow

```
       CLIENT (React / Postman)
                 |
                 | HTTP Request
                 v
       +---------------------+
       | ProductsController  |
       +---------------------+
                 |
                 v
       +---------------------+
       |   ProductService    |
       +---------------------+
                 |
                 v
       +---------------------+
       | ProductRepository   |
       +---------------------+
                 |
                 v
       +---------------------+
       |    products.json    |
       +---------------------+
                 |
                 | Read JSON
                 v
       +---------------------+
       | JSON Deserialization|
       +---------------------+
                 |
                 v
       +---------------------+
       |   List<Product>     |
       +---------------------+
                 |
                 | Business Operation
                 v
       +---------------------+
       | JSON Serialization  |
       +---------------------+
                 |
                 v
       +---------------------+
       |    products.json    |
       +---------------------+
                 |
                 v
       +---------------------+
       | HTTP Response       |
       +---------------------+
                 |
                 v
              CLIENT

```



## 10.  Mapping Architecture for Folder structure

```

ProductManagementAPI/
|
+-- Controllers/       --> Presentation Layer
|
+-- Services/          --> Business Logic Layer
|   |
|   +-- Interfaces/
|
+-- Repositories/      --> Data Access Layer
|   |
|   +-- Interfaces/
|
+-- Models/            --> Application Data Models
|
+-- DTOs/              --> Data Transfer Objects
|
+-- Data/              --> Data Storage
|   |
|   +-- products.json
|
+-- Middleware/        --> Cross-cutting Concerns
|
+-- Validators/        --> Input Validation
|
+-- Program.cs         --> Application Entry Point
|
+-- appsettings.json   --> Configuration

```

## 8. Ravi Sir's classroom discussion

Ask these questions:

1. Why should we avoid writing database queries inside Controllers?
2. What is the difference between Model and DTO?
3. Why do we create interfaces?
4. What is Dependency Injection?
5. Can we replace JSON storage with MySQL without changing the frontend?
6. What happens if business logic is written inside Controllers?
7. Why do enterprise applications separate Domain, Application and Infrastructure?

My teaching philosophy:

A good developer knows how to write code.

A better developer knows where to write that code.

An architect understands why the code belongs there.

First learn to build an application. Then learn to structure it. Finally, learn to design its architecture.