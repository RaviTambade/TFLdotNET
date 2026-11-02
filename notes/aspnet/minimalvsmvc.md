# Building ASP.NET Core Products Web API Step by Step

Learning Architecture: MVC + Services + Repository + JSON File Storage

Let's teach students how to transform a simple Product CRUD application into a properly structured ASP.NET Core Web API application.

The teaching approach should be incremental:

- First understand the problem.
- Then identify the application components.
- Design the folder structure.
- Create Model classes.
- Implement Repository.
- Implement Services.
- Create MVC Controllers.
- Configure Dependency Injection.
- Connect everything using ASP.NET Core.
- Test CRUD operations using Postman.

Our objective is not just to write code. It is to understand how different components collaborate to build an application.

## 1. Define the problem statement

Imagine Transflower wants to develop a Product Management System.

The application should support:

| Operation  | Description               |
| ---------- | ------------------------- |
| Create     | Add a new product         |
| Read       | Display all products      |
| Read by ID | Find a particular product |
| Update     | Modify product details    |
| Delete     | Remove a product          |

We will initially store product information inside a JSON file instead of using MySQL or SQL Server.

Technology stack

- Programming Language: C#
- Framework: ASP.NET Core Web API
- Architecture: MVC + Services + Repository
- Storage: JSON File
- Serialization: System.Text.Json
- Testing: Postman

## 2. Understand the application architecture

Before coding, explain the architecture using this diagram.

```
           CLIENT APPLICATION
        (React / Angular / Postman)
                    |
                    | HTTP Request
                    v
        +---------------------------+
        |       MVC LAYER           |
        |                           |
        |    ProductsController     |
        |                           |
        |  GET / POST / PUT / DELETE|
        +---------------------------+
                    |
                    | Calls
                    v
        +---------------------------+
        |     SERVICE LAYER         |
        |                           |
        |      IProductService      |
        |      ProductService       |
        |                           |
        |    Business Logic         |
        +---------------------------+
                    |
                    | Calls
                    v
        +---------------------------+
        |    REPOSITORY LAYER       |
        |                           |
        |    IProductRepository     |
        |    ProductRepository      |
        |                           |
        |     Data Operations       |
        +---------------------------+
                    |
                    | File I/O
                    v
        +---------------------------+
        |       DATA LAYER          |
        |                           |
        |       products.json       |
        |                           |
        |   JSON Serialization      |
        +---------------------------+
                    |
                    v
              HTTP RESPONSE
                    |
                    v
             CLIENT APPLICATION
```

Mentor explanation:

Think about a restaurant.

- Controller is the waiter who receives the customer's order.
- Service is the chef who understands what needs to be prepared.
- Repository is the storekeeper who manages ingredients.
- JSON file is the storage room where ingredients are kept.

Every component has a specific responsibility.

## 3. Step 1 – Create the ASP.NET Core Web API project

Open VS Code terminal or Visual Studio Developer PowerShell.

Execute:

```
dotnet new webapi -n ProductsWebAPI --use-controllers

cd ProductsWebAPI

code .
```

Check the project:

```
dotnet build
```

We are using Controllers rather than Minimal APIs because we want students to understand the MVC-based Web API development approach.

## 4. Step 2 – Create the application folder structure

Create the following folders and files.

```
ProductsWebAPI/
│
├── Controllers/
│   └── ProductsController.cs
│
├── Models/
│   └── Product.cs
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
│   └── products.json
│
├── Program.cs
│
├── appsettings.json
│
└── ProductsWebAPI.csproj
```

Explain the purpose of every folder:

| Folder       | Responsibility                  |
| ------------ | ------------------------------- |
| Controllers  | Handle HTTP requests            |
| Models       | Represent product data          |
| Services     | Implement business logic        |
| Repositories | Perform data access             |
| Data         | Store JSON data                 |
| Program.cs   | Configure and start application |

## 5. Step 3 – Create the Product Model

Create `Models/Product.cs`.

```
namespace ProductsWebAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Category { get; set; } = "";
}
```

A Model represents the structure of business data. In this example, Product is our business entity.

```
       PRODUCT MODEL
       -------------
       Id
       Name
       Price
       Category
```

One Product object represents one product record. A collection of Product objects represents multiple product records.

```
List<Product> products = new List<Product>();
```

## 6. Step 4 – Create the JSON data file

Create `Data/products.json`.

```
[
  {
    "id": 1,
    "name": "Laptop",
    "price": 55000,
    "category": "Electronics"
  },
  {
    "id": 2,
    "name": "Keyboard",
    "price": 1500,
    "category": "Accessories"
  },
  {
    "id": 3,
    "name": "Monitor",
    "price": 12000,
    "category": "Electronics"
  }
]
```

Explain:

This JSON file acts as our simple data store. Each JSON object represents a product record.

The application will read this file, deserialize JSON into C# objects, perform operations and serialize the updated collection back into JSON.

## 7. Step 5 – Create the Repository Interface

Create `Repositories/Interfaces/IProductRepository.cs`.

```
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<bool> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
}
```

Why an interface?

An interface defines a contract. It tells us what operations the repository must support, without specifying how those operations are implemented.

```
       IProductRepository
               |
      +--------+---------+
      |                  |
      v                  v
 ProductRepository   Future SQL Repository
   (JSON File)         (MySQL)
```

This is how we achieve loose coupling.

## 8. Step 6 – Implement the Product Repository

Create `Repositories/ProductRepository.cs`.

This is the most important step because students will learn how to connect C# objects with JSON file storage.

```
using System.Text.Json;
using ProductsWebAPI.Models;
using ProductsWebAPI.Repositories.Interfaces;

namespace ProductsWebAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _filePath;

    private static readonly SemaphoreSlim _lock = new(1, 1);

    private readonly JsonSerializerOptions _options =
        new(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        };

    public ProductRepository(IWebHostEnvironment environment)
    {
        _filePath = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "products.json"
        );
    }

    // Read JSON file and convert into C# objects
    private async Task<List<Product>> LoadProductsAsync()
    {
        if (!File.Exists(_filePath))
            return new List<Product>();

        string json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Product>>(json, _options) ?? new List<Product>();
    }

    // Convert C# objects into JSON and save
    private async Task SaveProductsAsync(List<Product> products)
    {
        string json = JsonSerializer.Serialize(products, _options );
        await File.WriteAllTextAsync(_filePath, json);
    }

    // GET ALL
    public async Task<List<Product>> GetAllAsync()
    {
        await _lock.WaitAsync();

        try
        {
            return await LoadProductsAsync();
        }
        finally
        {
            _lock.Release();
        }
    }

    // GET BY ID
    public async Task<Product?> GetByIdAsync(int id)
    {
        await _lock.WaitAsync();

        try
        {
            var products = await LoadProductsAsync();
            return products.FirstOrDefault(p => p.Id == id);
        }
        finally
        {
            _lock.Release();
        }
    }

    // ADD PRODUCT
    public async Task<Product> AddAsync(Product product)
    {
        await _lock.WaitAsync();

        try
        {
            var products = await LoadProductsAsync();
            product.Id = products.Count == 0 ? 1: products.Max(p => p.Id) + 1;
            products.Add(product);
            await SaveProductsAsync(products);
            return product;
        }
        finally
        {
            _lock.Release();
        }
    }

    // UPDATE PRODUCT
    public async Task<bool> UpdateAsync(
        int id, Product updatedProduct)
    {
        await _lock.WaitAsync();

        try
        {
            var products = await LoadProductsAsync();

            var product = products.FirstOrDefault( p => p.Id == id );

            if (product == null)
               return false;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Category = updatedProduct.Category;

            await SaveProductsAsync(products);
            return true;
        }
        finally
        {
            _lock.Release();
        }
    }

    // DELETE PRODUCT
    public async Task<bool> DeleteAsync(int id)
    {
        await _lock.WaitAsync();

        try
        {
            var products = await LoadProductsAsync();

            var product = products.FirstOrDefault( p => p.Id == id );

            if (product == null)
                return false;

            products.Remove(product);
            await SaveProductsAsync(products);
            return true;
        }
        finally
        {
            _lock.Release();
        }
    }
}
```

### Understand the two important methods

Read JSON – Deserialization

```
string json = await File.ReadAllTextAsync(_filePath);
var products =JsonSerializer.Deserialize<List<Product>>(json);
```

Execution:

```
products.json
     |
     v
ReadAllTextAsync()
     |
     v
JSON STRING
     |
     v
Deserialize<List<Product>>()
     |
     v
List<Product>
```

Write JSON – Serialization

```
string json =JsonSerializer.Serialize(products, _options);
await File.WriteAllTextAsync(_filePath, json);
```

Execution:

```
List<Product>
     |
     v
Serialize()
     |
     v
JSON STRING
     |
     v
WriteAllTextAsync()
     |
     v
products.json
```

Mentor checkpoint: Ask students to identify which method is responsible for reading and which method is responsible for writing data.

## 9. Step 7 – Create the Service Interface

Create `Services/Interfaces/IProductService.cs`.

```
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<bool> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
}
```

This defines the business operations available to the Controller.

## 10. Step 8 – Implement ProductService

Create `Services/ProductService.cs`.

```
using ProductsWebAPI.Models;
using ProductsWebAPI.Repositories.Interfaces;
using ProductsWebAPI.Services.Interfaces;

namespace ProductsWebAPI.Services;

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

    public async Task<Product> AddAsync(Product product)
    {
        ValidateProduct(product);

        return await _repository.AddAsync(product);
    }

    public async Task<bool> UpdateAsync(
        int id, Product product)
    {
        ValidateProduct(product);

        return await _repository.UpdateAsync(id, product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private void ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required.");

        if (product.Price < 0)
            throw new ArgumentException("Price cannot be negative.");
    }
}
```

### Understand the Service Layer

The service is responsible for business rules. For example:

```
New Product Request
        |
        v
   ProductService
        |
        v
  Validate Product
        |
        +---- Invalid
        |        |
        |        v
        |     Error
        |
        +---- Valid
                 |
                 v
          ProductRepository
```

Why not put all these rules inside the Controller?

Because business rules should be reusable. Another application, background job or API endpoint might also need the same service.

## 11. Step 9 – Create ProductsController

Create `Controllers/ProductsController.cs`.

This is the MVC presentation component.

```
using Microsoft.AspNetCore.Mvc;
using ProductsWebAPI.Models;
using ProductsWebAPI.Services.Interfaces;

namespace ProductsWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    // GET: api/products/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        try
        {
            var result = await _service.AddAsync(product);
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/products/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, Product product)
    {
        try
        {
            bool updated = await _service.UpdateAsync(id, product);
            if (!updated)
                return NotFound();
            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/products/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();
        return Ok("Product deleted successfully.");
    }
}
```

### Understand MVC Controller responsibilities

| Attribute / Method  | Purpose                                  |
| ------------------- | ---------------------------------------- |
| `[ApiController]`   | Enables API-specific controller behavior |
| `[Route]`           | Defines API routing                      |
| `[HttpGet]`         | Handles GET requests                     |
| `[HttpPost]`        | Handles POST requests                    |
| `[HttpPut]`         | Handles PUT requests                     |
| `[HttpDelete]`      | Handles DELETE requests                  |
| `Ok()`              | Returns HTTP 200                         |
| `NotFound()`        | Returns HTTP 404                         |
| `BadRequest()`      | Returns HTTP 400                         |
| `CreatedAtAction()` | Returns HTTP 201                         |

Notice that the Controller communicates with `IProductService`, not directly with `ProductRepository`.

This is an important architectural decision.

## 12. Step 10 – Configure Dependency Injection in Program.cs

Now we have all the components. But how will ASP.NET Core create objects and connect them? This is where Dependency Injection comes into the picture.

Open `Program.cs`.

```
using ProductsWebAPI.Repositories;
using ProductsWebAPI.Repositories.Interfaces;
using ProductsWebAPI.Services;
using ProductsWebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Register MVC Controllers
builder.Services.AddControllers();

// 2. Register Repository
builder.Services.AddScoped<IProductRepository,ProductRepository>();

// 3. Register Service
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// 4. Configure HTTP pipeline
app.UseHttpsRedirection();

// 5. Map Controller routes
app.MapControllers();

app.Run();
```

### Understand Dependency Injection

When a request arrives:

```
        HTTP REQUEST
              |
              v
       ProductsController
              |
              | Needs IProductService
              v
       Dependency Injection
              |
              v
         ProductService
              |
              | Needs IProductRepository
              v
       Dependency Injection
              |
              v
       ProductRepository
```

ASP.NET Core automatically creates and supplies the required objects. We don't need to manually write:

```
var repository = new ProductRepository(...);
var service = new ProductService(repository);
var controller = new ProductsController(service);
```

The DI container handles object creation and dependency resolution.

## 13. Step 11 – Configure JSON file copying

Open `ProductsWebAPI.csproj`.

Add this configuration inside the project element:

```
<ItemGroup>
  <None Update="Data/products.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
  </None>
</ItemGroup>
```

This ensures the JSON data file is included when building and publishing the application.

## 14. Step 12 – Build and run the application

Execute:

```
dotnet restore

dotnet build

dotnet run
```

ASP.NET Core displays the listening URLs in the terminal.

For example:

```
Now listening on: https://localhost:7001
Now listening on: http://localhost:5001
```

Use the actual URL displayed by your application.

## 15. Step 13 – Test CRUD operations using Postman

Practical Lab

Let's test all five API endpoints.

### Test 1: Retrieve all products

```
GET /api/products
```

Expected response:

```
[
  {
    "id": 1,
    "name": "Laptop",
    "price": 55000,
    "category": "Electronics"
  },
  {
    "id": 2,
    "name": "Keyboard",
    "price": 1500,
    "category": "Accessories"
  }
]
```

### Test 2: Retrieve a product by ID

```
GET /api/products/1
```

Expected response:

```
{
  "id": 1,
  "name": "Laptop",
  "price": 55000,
  "category": "Electronics"
}
```

### Test 3: Add a new product

```
POST /api/products
```

Request body:

```
{
  "name": "Mouse",
  "price": 800,
  "category": "Accessories"
}
```

Expected status: `201 Created`.

### Test 4: Update a product

```
PUT /api/products/1
```

Request body:

```
{
  "name": "Gaming Laptop",
  "price": 75000,
  "category": "Electronics"
}
```

Expected status: `200 OK`.

### Test 5: Delete a product

```
DELETE /api/products/2
```

Expected status: `200 OK`.

After every write operation, open `Data/products.json` and observe the updated records.

## 16. Understand the complete execution sequence

This diagram connects all the code students have written.

```
       CLIENT (React / Postman)
                  |
                  | HTTP Request
                  v
       +------------------------+
       |   ProductsController   |
       |                        |
       |   GET / POST / PUT     |
       |       DELETE           |
       +------------------------+
                  |
                  | IProductService
                  v
       +------------------------+
       |     ProductService     |
       |                        |
       |   Business Validation  |
       |   Application Rules    |
       +------------------------+
                  |
                  | IProductRepository
                  v
       +------------------------+
       |   ProductRepository    |
       |                        |
       |   LoadProductsAsync()  |
       |   SaveProductsAsync()  |
       +------------------------+
                  |
                  | File I/O
                  v
       +------------------------+
       |     products.json      |
       +------------------------+
                  |
                  v
       +------------------------+
       |  JSON Deserialization  |
       |  JSON Serialization    |
       +------------------------+
                  |
                  v
           List<Product>
                  |
                  v
          HTTP Response
                  |
                  v
       CLIENT (React / Postman)
```

## 17. What have students actually learned?

## Learning outcomes

- C# Object-Oriented Programming
- Classes, objects, interfaces, abstraction and encapsulation.
- ASP.NET Core MVC
- Controllers, routing, HTTP methods and status codes.
- Layered Architecture
- Separation of concerns and responsibility distribution.
- Dependency Injection
- Interface-based programming and loose coupling.
- JSON Serialization
- Serialization, deserialization and file handling.
- API Testing
- Testing CRUD operations using Postman.

## 18. Next learning progression for Transflower students

Once this lab is completed, progressively introduce more advanced concepts.

```
LEVEL 1
ASP.NET Core Minimal API
         |
         v
LEVEL 2
MVC Controllers + JSON Storage
         |
         v
LEVEL 3
Services + Repository + DI
         |
         v
LEVEL 4
ADO.NET + MySQL Connectivity
         |
         v
LEVEL 5
Entity Framework Core + LINQ
         |
         v
LEVEL 6
DTO + AutoMapper + Validation
         |
         v
LEVEL 7
Authentication + JWT + Authorization
         |
         v
LEVEL 8
Unit Testing + API Testing
         |
         v
LEVEL 9
Docker + CI/CD + Deployment
         |
         v
LEVEL 10
Enterprise Web API Architecture
```

Important engineering note: This JSON repository is a classroom implementation. The in-process semaphore helps serialize file access within this application instance, but it is not a replacement for database transactions or multi-instance concurrency control. For production, use a database, proper validation, centralized exception handling and logging.

## 19. Ravi Sir's classroom challenge

Ask students one interesting question:

"Tomorrow, management decides to replace `products.json` with a MySQL database. Do we need to rewrite our entire application?"

Expected answer:

NO!

We can create another implementation of `IProductRepository`:

```
             IProductRepository
                     |
             +-------+--------+
             |                |
             v                v
       ProductRepository  MySQLProductRepository
          (JSON)              (MySQL)
             |                |
             v                v
        products.json       MySQL DB
```

The Controller and Service can continue using the same interfaces.

That is the power of abstraction, dependency injection and separation of concerns.

Ravi Sir's final message to students:

"Don't learn ASP.NET Core just to write API endpoints. Learn how different components collaborate to build maintainable software. Frameworks will change, databases will change, technologies will evolve, but software engineering fundamentals will remain valuable."