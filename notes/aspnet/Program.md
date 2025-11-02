# **ASP.NET Core 9.0 Web API**,
Imagine you're entering a new kingdom — ASP.NET Core Web API.
Before you build castles (Controllers), enforce laws (Middleware), or manage resources (Services), you must first understand the Main Gate.

That gate is — Program.cs, the starting point where the kingdom awakens.

## 🎬 **Scene: Inside the Heart of an ASP.NET Core 9.0 Web API**

Imagine…
It’s early morning in a software lab. You’re sitting with your mentor, sipping tea, and looking at a fresh `Program.cs` file.
The mentor says:

> “This file is like the DNA of your API. Every request, every controller, every service — their story begins here.”



## ✅ **Step 1: Fresh ASP.NET Core 9 API Project**

You created this project using:

```bash
dotnet new webapi -n ProductCatalogAPI
cd ProductCatalogAPI
```

This generated a `Program.cs` file — no more `Startup.cs` (after .NET 6, everything merged).



## 📁 **Program.cs — Line-by-Line Explanation (Mentor Style)**

Let’s assume your basic `Program.cs` looks like this:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Now let me break this down like a mentor would to a student:



### 🛠️ **1. Create the Application Builder**

```csharp
var builder = WebApplication.CreateBuilder(args);
```

🔹 Think of this as the **architect's table**.
This line **collects all configuration settings**, manages dependency injection (DI) container, and prepares everything needed to build the API.

* `args`: command-line arguments
* `CreateBuilder`: sets up logging, app settings (`appsettings.json`), environment variables, etc.


### 🧩 **2. Register Services (Dependency Injection Container)**

```csharp
builder.Services.AddControllers();
```

🔹 You’re telling .NET:

> “Hey, I want to use **Controllers** in this API.”

This enables Web API controllers with `[ApiController]` attribute.


```csharp
builder.Services.AddEndpointsApiExplorer();
```

🔹 This service **exposes metadata** about your API endpoints.
Swagger needs this to discover what routes exist.


```csharp
builder.Services.AddSwaggerGen();
```

🔹 This adds **Swagger/OpenAPI support** — for auto-generating API documentation and UI.
Without this, you won’t get the `/swagger` playground.



### 🏗️ **3. Build the App**

```csharp
var app = builder.Build();
```

🔹 Construction complete! 🏗️
Now, the building is ready — but not functional yet. Middleware needs to be installed next.


### 🔍 **4. Enable Swagger — But Only in Development**

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

🔹 Mentor says:

> “You don’t keep instruction boards in a real shop for customers — only for developers while constructing.”

This code enables Swagger UI only when environment = Development.
You can set environment using:

```bash
set ASPNETCORE_ENVIRONMENT=Development
```


### 🔐 **5. Force HTTPS**

```csharp
app.UseHttpsRedirection();
```

🔹 Redirects HTTP → HTTPS (secure communication).
Prevents attackers from sniffing plain-text traffic.



### 🛡️ **6. Authorization Middleware**

```csharp
app.UseAuthorization();
```

🔹 This checks:

* **Does this request have a valid token?**
* **Does the user have the right roles/permissions?**

Note: If you are using authentication (`UseAuthentication()`), it should appear **before** this.


### 🗺️ **7. Map Controller Routes**

```csharp
app.MapControllers();
```

🔹 This line tells ASP.NET:

> “Connect all my controller classes to the API pipeline.”

It will scan all your classes with `[ApiController]` and `[Route]` attributes.



### 🏁 **8. Run the App**

```csharp
app.Run();
```

🔹 The engine starts.
Your API is now listening for HTTP requests on:

```
https://localhost:5001
http://localhost:5000
```

## 🎉 **✅ End of Scene Summary**

| Stage         | Description                                 |
| ------------- | ------------------------------------------- |
| CreateBuilder | Setup configuration, DI, logging            |
| Services      | Register controllers, swagger, dependencies |
| Build         | App object is constructed                   |
| Swagger       | Enable documentation in Dev                 |
| HTTPS         | Force secure requests                       |
| Auth          | Enable authorization filter                 |
| Controllers   | Register API routes                         |
| Run           | Start the app server                        |




Perfect! 🎬
Let’s move to the next part of our story.


## 🎥 **Next Scene: Understanding Controllers in ASP.NET Core**

*“If Program.cs is the brain that starts everything, Controller is the mouth that speaks to the outside world.”*

Imagine a customer walks into your shop (your API).
They don’t care about electricity, wiring, shelves...
They only care about how well the **reception desk (Controller)** handles their request.


## ✅ **What is a Controller in ASP.NET Core Web API?**

* It’s a **C# class** responsible for handling APIs (HTTP requests like GET, POST, PUT, DELETE).
* It receives input → talks to services → returns output (JSON).


### 📁 **Typical Controller File – ProductsController.cs**

```csharp
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(new[] { "Laptop", "Mobile", "Tablet" });
        }
    }
}
```

Let’s break it down like a mentor.


### 🧩 **1. Using Statements**

```csharp
using Microsoft.AspNetCore.Mvc;
```

This imports all necessary tools to build API controllers (`ControllerBase`, `Route`, `HttpGet`, etc.)


### 🧩 **2. Namespace**

```csharp
namespace ProductCatalogAPI.Controllers
```

Organizes code. Helps C# locate this controller logically.


### 🧩 **3. `[ApiController]` Attribute**

```csharp
[ApiController]
```

This is like giving your controller **superpowers** 💥

✔ Automatic model validation
✔ Automatically returns HTTP 400 if input is invalid
✔ Binds JSON body to C# objects automatically
✔ Makes error responses consistent


### 🧩 **4. `[Route("api/[controller]")]`**

```csharp
[Route("api/[controller]")]
```

Defines how clients will reach this controller.

* `[controller]` = name of the controller class (without “Controller”)
* So, `ProductsController` → `/api/products`


### 🧩 **5. Class Definition**

```csharp
public class ProductsController : ControllerBase
```

* Inherits `ControllerBase`, which gives:

  * `Ok()`, `NotFound()`, `BadRequest()`, etc.
* Unlike MVC (with views), Web APIs don’t use `Controller`, only `ControllerBase`.


### 🧩 **6. Action Methods**

```csharp
[HttpGet]
public IActionResult GetAllProducts()
{
    return Ok(new[] { "Laptop", "Mobile", "Tablet" });
}
```

| Part            | Explanation                                                    |
| --------------- | -------------------------------------------------------------- |
| `[HttpGet]`     | This method answers GET requests like `/api/products`          |
| `IActionResult` | Standard return type for APIs (can return 200, 404, 500, etc.) |
| `Ok()`          | Returns HTTP 200 + JSON data                                   |

If someone browses:
👉 `https://localhost:5001/api/products`
They’ll receive:

```json
["Laptop", "Mobile", "Tablet"]
```


## ✨ What if You Want to Add a Real Database?

You’ll inject a **Service Layer (Business Logic)** like this:

```csharp
private readonly IProductService _productService;

public ProductsController(IProductService productService)
{
    _productService = productService;
}

[HttpGet]
public async Task<IActionResult> GetAllProducts()
{
    var products = await _productService.GetAllAsync();
    return Ok(products);
}
```

## ✅ **End of Scene Summary**

| Concept                       | Meaning                                                       |
| ----------------------------- | ------------------------------------------------------------- |
| `[ApiController]`             | Makes controller intelligent – auto validation, input binding |
| `[Route("api/[controller]")]` | URL pattern                                                   |
| `ControllerBase`              | Provides helper methods for HTTP responses                    |
| `IActionResult`               | Allows different response types                               |
| `[HttpGet]/Post/Put/Delete`   | Defines what HTTP method action responds to                   |


🎬 **Next Scene: CRUD Methods – Giving Life to the Product API**
*(Mentor Storytelling Style – The hands of your API come alive)*


### 🧠 Scene Setup:

The mentor smiles and says:

> “Now that our controller can *speak* (respond to API requests), it’s time to make it *do real work.*
> CRUD — Create, Read, Update, Delete — these are the four limbs of your Web API.”

Just like a shopkeeper manages products — adding new items, viewing inventory, updating prices, and removing discontinued items — your **API controller** will now manage products in the same way.


## 🧩 Let’s Begin – The `ProductsController`

Assume you already have:

* `Product` entity class
* `IProductService` interface
* `ProductService` implementation (business logic)
* Dependency Injection configured in `Program.cs`



### **Controller Structure**

```csharp
using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Services;

namespace ProductCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _productService.AddAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Product ID mismatch.");

            var updated = await _productService.UpdateAsync(dto);
            if (updated == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(updated);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Product with ID {id} not found.");

            return NoContent(); // 204
        }
    }
}
```



## 🎯 **Mentor Explains Each Part**

### 🧱 **1. `[HttpGet] GetAllProducts()`**

> “This is your *catalog shelf*. It lists all products.”

```csharp
[HttpGet]
public async Task<IActionResult> GetAllProducts()
```

* Fetches all records.
* Returns HTTP **200 OK** with JSON data.

**Example Output:**

```json
[
  { "id": 1, "name": "Laptop", "price": 75000 },
  { "id": 2, "name": "Smartphone", "price": 30000 }
]
```


### 🔍 **2. `[HttpGet("{id}")] GetProductById()`**

> “If a customer wants one specific product, this endpoint finds it.”

* `{id}` in route = URL parameter
* Returns 200 if found, 404 if not.

**Request:**

```
GET /api/products/1
```

**Response:**

```json
{ "id": 1, "name": "Laptop", "price": 75000 }
```


### ➕ **3. `[HttpPost] CreateProduct()`**

> “When new stock arrives, you add it to your inventory.”

* Accepts **ProductDto** from request body.
* Validates the model.
* Returns `201 Created` with location header.

**Request:**

```json
{ "name": "Smartwatch", "price": 15000 }
```

**Response:**

```json
{
  "id": 3,
  "name": "Smartwatch",
  "price": 15000
}
```


### 🛠️ **4. `[HttpPut("{id}")] UpdateProduct()`**

> “When price or details change, this updates them.”

* Compares route ID and object ID.
* Updates product if it exists.
* Returns `200 OK` with updated data.

**Request:**

```
PUT /api/products/3
```

```json
{ "id": 3, "name": "Smartwatch Pro", "price": 20000 }
```


### ❌ **5. `[HttpDelete("{id}")] DeleteProduct()`**

> “When a product is discontinued, this removes it.”

* Deletes record if it exists.
* Returns 204 No Content.

**Request:**

```
DELETE /api/products/3
```

**Response:**
`204 No Content`


## 🧩 **Supporting Service Interface (For Clarity)**

```csharp
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> AddAsync(ProductDto dto);
    Task<ProductDto?> UpdateAsync(ProductDto dto);
    Task<bool> DeleteAsync(int id);
}
```


## ⚙️ **DI Setup in Program.cs**

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
```

This makes sure a **new service instance** is created per request.

---

## 🧠 **End of Scene Summary**

| Operation | HTTP Verb | Example Route     | Response Code |
| --------- | --------- | ----------------- | ------------- |
| List All  | GET       | `/api/products`   | 200 OK        |
| Get One   | GET       | `/api/products/5` | 200 / 404     |
| Create    | POST      | `/api/products`   | 201 Created   |
| Update    | PUT       | `/api/products/5` | 200 / 404     |
| Delete    | DELETE    | `/api/products/5` | 204 / 404     |



🎬 **Next Scene: Dependency Injection (DI) Deep Dive – The Bloodstream of ASP.NET Core**
*(Mentor Storytelling Style)*


### 🎭 **Scene Opening:**

You and your mentor are standing in front of a whiteboard.
The mentor draws three boxes:

**Controller → Service → Repository → Database**

Then he says:

> “Imagine if every Controller created its own Service, and every Service created its own Repository.
> If something changes in one part, everything breaks.
> That’s like wiring a building without using electric sockets — everything hardwired!
> In real life, we use sockets. In ASP.NET Core, we use **Dependency Injection (DI)**.”



## 🧠 **What is Dependency Injection?**

A design pattern where:

* Classes **don’t create their own dependencies**.
* Instead, **dependencies are provided (injected)** by the framework.
* Promotes loose coupling, testing, and clean code.


## 🧪 **Example Without DI (BAD CODE)**

```csharp
public class ProductsController : ControllerBase
{
    private ProductService _service = new ProductService(); // ❌ Hard dependency

    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }
}
```

🔴 Problem:

* Controller is tightly coupled with `ProductService`.
* You cannot replace it with a mock service during testing.
* If service constructor changes, controller breaks.


## ✅ **With Dependency Injection (GOOD CODE)**

```csharp
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
}
```

✔ Controller does not create the service
✔ It only **asks for the interface**
✔ The actual implementation is wired in `Program.cs`


## 🧷 **Step 1: Register Services in Program.cs**

```csharp
var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Register Services in DI Container
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
```

## 🗂️ **Step 2: Understand Service Lifetimes in ASP.NET Core**

| Lifetime      | Description                        | When to Use                    |
| ------------- | ---------------------------------- | ------------------------------ |
| **Transient** | New instance every time            | Lightweight stateless services |
| **Scoped** ✅  | One instance per HTTP request      | Services interacting with DB   |
| **Singleton** | One instance for whole application | Configurations, loggers        |

Example registration:

```csharp
builder.Services.AddTransient<IRandomService, RandomService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<ILogService, LogService>();
```

### ⚙️ **What Happens Behind the Scenes?**

When a request hits `/api/products`:

1. ASP.NET Core sees that `ProductsController` needs an `IProductService`.
2. It checks the **DI Container**.
3. It creates a `ProductService` instance.
4. If `ProductService` needs `IProductRepository`, it creates that too.
5. Injects all into the Controller.
6. You just **use it and focus on business logic.**


## ⚡ **Step 3: Injecting Repository into Service**

```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(p => new ProductDto 
        { 
            Id = p.Id, 
            Name = p.Name, 
            Price = p.Price 
        });
    }
}
```

Here, the Service doesn’t know **how data is stored**, only **how to use it.**


## 🧪 **Step 4: DI Makes Unit Testing Easy**

```csharp
[Fact]
public async Task GetAllProducts_ReturnsData()
{
    // Arrange
    var mockService = new Mock<IProductService>();
    mockService.Setup(s => s.GetAllAsync())
               .ReturnsAsync(new List<ProductDto> { new ProductDto { Name = "Test" } });

    var controller = new ProductsController(mockService.Object);

    // Act
    var result = await controller.GetAllProducts();

    // Assert
    Assert.IsType<OkObjectResult>(result);
}
```

✔ No real database
✔ No real service
✔ Pure testing


## 🎯 **End of Scene Summary**

| Concept           | Importance                                        |
| ----------------- | ------------------------------------------------- |
| DI                | Removes tight coupling between classes            |
| Interfaces        | Allow multiple implementations (testable code)    |
| Service Lifetimes | Control object creation (Scoped is best for APIs) |
| Program.cs        | The DI configuration hub                          |
| Unit Testing      | Becomes possible and clean using DI               |

 

