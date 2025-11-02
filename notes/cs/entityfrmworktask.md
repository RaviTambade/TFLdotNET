# 🎓 Building a Product Catalog with EF Core & MySQL

### **📍 Scene 1: The Vision — A Digital Store Begins**

*Mentor walks into the lab where students are gathered.*

**Mentor:**
"Imagine you're opening a small online shop — a place where you sell books, gadgets, or handmade crafts. You need a system that can store product details like name, price, quantity, and update them when items are sold. That's our mission today.
And guess what? You're going to build it yourself using .NET, Entity Framework Core, and a MySQL database."

### **📍 Scene 2: Laying the Foundation (Creating the Project)**

**Mentor:**
“First things first — we need a home for our application.”

So we open the terminal and type:

```bash
mkdir ProductCatalog
cd ProductCatalog
dotnet new webapi -n ProductCatalog.Api
cd ProductCatalog.Api
```

*Mentor smiles:*
"This builds the skeleton of your store’s backend — the hallways and rooms are ready; now we bring in the furniture."


### **📍 Scene 3: Teaching .NET to Speak to MySQL (EF Core Setup)**

**Mentor:**
“A store needs a manager who knows how to talk to the database. That manager is Entity Framework Core (EF Core). But EF Core doesn't speak MySQL by default. So we teach it.”

We install the necessary packages:

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

“And we also give ourselves a tool to create database tables with commands — like magic.”

```bash
dotnet tool install --global dotnet-ef
```


### **📍 Scene 4: Connecting the Store to the Database (appsettings.json)**

**Mentor:**
“Now, imagine the store needs to know the address of the warehouse (database). That address is called a *connection string*.”

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=ProductCatalogDb;user=root;password=YourPassword;"
  }
}
```

This is like writing the warehouse address on a sticky note and giving it to the system.


### **📍 Scene 5: Designing the Product (Creating the Model)**

**Mentor:**
“Let’s create a blueprint — what exactly is a product?”

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

"This class is just like a product form you fill — name, price, quantity. EF Core will transform this into a table in MySQL."

### **📍 Scene 6: The Bridge Between C# and SQL (DbContext)**

**Mentor:**
“Now your cashier needs a supervisor — someone who tracks all tables and saves changes to the database. That’s the `DbContext`.”

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
```

*Student smiles:*
“So, this is like EF Core’s control room?”
**Mentor:** “Exactly.”

### **📍 Scene 7: Wiring Everything Together (Program.cs)**

Just like plugging in all the cables before turning on a computer.

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
```

*Mentor:* “Now .NET knows how to connect to MySQL and which context to use.”


### **📍 Scene 8: Bringing the Store to Life (Migrations)**

*Mentor leans forward:*
“Time for magic. You’ve designed your product in C#. Now tell EF Core to build the same structure in MySQL.”

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Boom! 🚀**
Your `Products` table appears in the MySQL database — like a carpenter building your first shelf.

### **📍 Scene 9: Opening the Doors — Exposing APIs**

To sell or update products, you need endpoints — like counters at your store.

```csharp
[HttpPost] // Add a product
[HttpGet]  // Get all products
[HttpGet("{id}")] // Get one product
[HttpPut("{id}")] // Update product
[HttpDelete("{id}")] // Delete product
```

Use Swagger → test the API → products begin appearing in your database.

### **📍 Scene 10: Mentor’s Final Words**

**Mentor:**
“Today, you didn’t just write code.
You **built a real-world system** — a store that talks to MySQL, stores product data, and serves APIs to the frontend or mobile apps.”

🛠️ You learned:
- ✅ Creating a .NET Web API
- ✅ Installing EF Core + MySQL provider
- ✅ Connection strings and DbContext
- ✅ Creating models and migrations
- ✅ Building CRUD API for Products


### **💬 Want to Continue the Story?**



# 🎯 **Scene 11: Organizing the Store – Introducing Categories**

**Mentor:**
“Your store is growing… chaos has begun! Electronics are mixed with books, shoes with mobile phones. Customers are confused.
What do we need?”

**Student:**
“Shelves! Categories!”

**Mentor:**
“Exactly. Let’s teach the system how to group products under categories.”

---

### ✅ Step 1: Create Category Model

📁 `Models/Category.cs`

```csharp
namespace ProductCatalog.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigation property (One Category has many Products)
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
```

📁 Update Product Model → `Models/Product.cs`

```csharp
public int CategoryId { get; set; }  // Foreign Key
public Category? Category { get; set; }
```

---

### ✅ Step 2: Update DbContext

📁 `Data/AppDbContext.cs`

```csharp
public DbSet<Category> Categories => Set<Category>();

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Category>()
        .HasMany(c => c.Products)
        .WithOne(p => p.Category!)
        .HasForeignKey(p => p.CategoryId);
}
```

---

### ✅ Step 3: Add Migration & Update Database

Run these commands:

```bash
dotnet ef migrations add AddCategoryTable
dotnet ef database update
```

✔ This creates a **Categories table** and adds a **CategoryId foreign key** to the Products table.

---

# 🏗️ **Scene 12: Making the Store Manageable – Repository Pattern**

**Mentor:**
“You’ve created database tables, but your controllers are directly talking to DbContext…
That’s like customers going directly to the warehouse — not scalable!”

“We need **Repository Pattern** — trained employees who handle database work.”

---

### ✅ Step 4: Create Base Repository Interface

📁 `Repositories/IRepository.cs`

```csharp
using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
```

---

### ✅ Step 5: Implement Generic Repository

📁 `Repositories/Repository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Remove(T entity) => _dbSet.Remove(entity);
}
```

---

# 🔄 **Scene 13: Managing Everything Together – Unit of Work Pattern**

**Mentor:**
“Now imagine each repository is a department in your store.
Unit of Work is like a **store manager** who ensures everything is saved together.”

---

### ✅ Step 6: Create Unit of Work Interface

📁 `Repositories/IUnitOfWork.cs`

```csharp
using ProductCatalog.Api.Models;

public interface IUnitOfWork : IDisposable
{
    IRepository<Product> Products { get; }
    IRepository<Category> Categories { get; }
    Task<int> CompleteAsync(); // Save changes
}
```

---

### ✅ Step 7: Implement Unit of Work

📁 `Repositories/UnitOfWork.cs`

```csharp
using ProductCatalog.Api.Data;
using ProductCatalog.Api.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepository<Product> Products { get; }
    public IRepository<Category> Categories { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new Repository<Product>(_context);
        Categories = new Repository<Category>(_context);
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
```

---

### ✅ Step 8: Register in Program.cs (Dependency Injection)

```csharp
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

---

### ✅ Step 9: Use Unit of Work in Controller

📁 `Controllers/ProductsController.cs`

```csharp
private readonly IUnitOfWork _uow;

public ProductsController(IUnitOfWork uow)
{
    _uow = uow;
}

[HttpPost]
public async Task<IActionResult> Create(Product product)
{
    await _uow.Products.AddAsync(product);
    await _uow.CompleteAsync();
    return Ok(product);
}
```

---

# 🎉 **Final Scene: Mentor Smiles**

**Mentor:**
“You didn’t just build tables. You built a scalable, maintainable architecture — just like a real-world enterprise system.”

---

# ✅ **What You Achieved:**

| Feature                       | Status |
| ----------------------------- | ------ |
| Category Table + Relationship | ✅      |
| Repository Pattern            | ✅      |
| Unit of Work Pattern          | ✅      |
| Database updated via EF Core  | ✅      |
| Clean Architecture            | ✅      |



# 🎯 **Scene 14: The Store Gets Smarter – Introducing the Service Layer**

**Mentor:**
“You’ve built shelves (Categories), stored products, and have managers (Unit of Work) handling database work.
But imagine a customer asks:
👉 ‘Show me all products in Electronics priced below ₹5000.’
👉 ‘Don’t allow adding a product without a category.’
Who will handle this logic? The database guy? No.
We need a smart employee — the **Service Layer** — who understands business rules.”

The **Service Layer**:
✅ Sits between **Controllers** and **Repositories**
✅ Contains **business logic, validations, workflows**
✅ Makes controllers clean and professional

---

## ✅ Step 1: Create Service Interfaces

📁 `Services/Interfaces/IProductService.cs`

```csharp
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
```

📁 `Services/Interfaces/ICategoryService.cs`

```csharp
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
    }
}
```

---

## ✅ Step 2: Implement Product Service (Business Logic)

📁 `Services/ProductService.cs`

```csharp
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Repositories;
using ProductCatalog.Api.Services.Interfaces;

namespace ProductCatalog.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            // ✅ Business rule: Category must exist before adding Product
            var category = await _unitOfWork.Categories.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new Exception("Category does not exist!");

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existing = await _unitOfWork.Products.GetByIdAsync(product.Id);
            if (existing == null) return false;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            existing.CategoryId = product.CategoryId;

            _unitOfWork.Products.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return false;

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
```

---

## ✅ Step 3: Implement Category Service

📁 `Services/CategoryService.cs`

```csharp
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Repositories;
using ProductCatalog.Api.Services.Interfaces;

namespace ProductCatalog.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return category;
        }
    }
}
```

---

## ✅ Step 4: Register Services in Dependency Injection

In `Program.cs`, add:

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
```

---

## ✅ Step 5: Use Service Layer in Controller

📁 `Controllers/ProductsController.cs`

```csharp
private readonly IProductService _service;

public ProductsController(IProductService service)
{
    _service = service;
}

[HttpPost]
public async Task<IActionResult> Create(Product product)
{
    var result = await _service.AddProductAsync(product);
    return Ok(result);
}
```

---

## ✅ 🎉 Scene 15: Mentor’s Wisdom

**Mentor:**
“Today, you didn't just write code.
You designed a system like a real software architect.”

✔ Controllers are now clean
✔ Business logic is inside **Service Layer**
✔ Database logic is inside **Repository + UoW**
✔ EF Core handles database communication
✔ Architecture is now scalable and professional

---


# 🎓 **Scene 16: The Store Learns Communication Etiquette — DTOs & AutoMapper**

**Mentor:**
“Right now, your API sends raw database models to customers. That’s like a chef serving food along with kitchen tools, recipes, and raw ingredients — too much exposure!”

“We need a **clean plate** — only the required information should go out.
That’s where **DTOs (Data Transfer Objects)** come in.”

---

## ✅ Step 1: Create DTO Classes

📁 `DTOs/ProductDTO.cs`

```csharp
namespace ProductCatalog.Api.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; } = "";  // Optional for display
    }
}
```

📁 `DTOs/CreateProductDTO.cs` (For POST requests)

```csharp
namespace ProductCatalog.Api.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
```

📁 `DTOs/CategoryDTO.cs`

```csharp
namespace ProductCatalog.Api.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
```

---

## ✅ Step 2: Install AutoMapper

### 📦 Install NuGet Packages via CLI:

```bash
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

---

## ✅ Step 3: Create AutoMapper Profile

📁 `Mapping/MappingProfile.cs`

```csharp
using AutoMapper;
using ProductCatalog.Api.DTOs;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity → DTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""));

            // DTO → Entity
            CreateMap<CreateProductDTO, Product>();

            // Category Mapping
            CreateMap<Category, CategoryDTO>();
        }
    }
}
```

---

## ✅ Step 4: Register AutoMapper in Program.cs

```csharp
builder.Services.AddAutoMapper(typeof(Program));
```

Or if this doesn’t work (depending on structure):

```csharp
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
```

---

## ✅ Step 5: Use DTOs in Controller

📁 `Controllers/ProductsController.cs`

```csharp
using AutoMapper;
using ProductCatalog.Api.DTOs;
using ProductCatalog.Api.Services.Interfaces;

public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;

    public ProductsController(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllProductsAsync();
        var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productDTOs);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDTO dto)
    {
        var product = _mapper.Map<Product>(dto);
        var created = await _service.AddProductAsync(product);
        var productDTO = _mapper.Map<ProductDTO>(created);
        return Ok(productDTO);
    }
}
```

---

# 💡 **Why This is Powerful?**

| Without DTOs                   | With DTOs + AutoMapper         |
| ------------------------------ | ------------------------------ |
| Exposes database models        | Only sends required fields     |
| Hard to update database schema | DTOs protect API layer         |
| Manual mapping = repetitive    | AutoMapper = clean & automatic |
| Security risk                  | Safe & clean API responses     |

---

# ✅ **What You’ve Achieved So Far:**

✔ Product + Category with EF Core + MySQL
✔ Repository + Unit of Work
✔ Service Layer (Business Logic)
✔ DTOs + AutoMapper (Clean API Shape)



## 🎭 **Scene 6: The Gatekeeper of Quality – FluentValidation**

The application is now structured like a well-run kingdom —
🔹 Entities in the database
🔹 Services applying business rules
🔹 DTOs handling data exchange

But something is still missing…

Imagine a castle that lets anyone walk in without checking their background —
⚠ Wrong product names
⚠ Missing prices
⚠ Invalid category IDs

Chaos.

So, the king (your application) appoints a **gatekeeper** at the entrance —
**FluentValidation** — a powerful knight whose sole purpose is:
✅ Validate all inputs **before** they enter the service/business layer.

---

## ✅ **Why FluentValidation?**

| Problem                              | Solution (via FluentValidation)                     |
| ------------------------------------ | --------------------------------------------------- |
| Strings too short, blank, or invalid | `RuleFor(x => x.Name).NotEmpty().MinimumLength(3)`  |
| Price = 0 or negative                | `RuleFor(x => x.Price).GreaterThan(0)`              |
| Invalid CategoryId                   | Custom validation to check from DB                  |
| Simplifies controller code           | Keeps controllers clean and business logic separate |
| Works harmoniously with DTOs         | Validates input before mapping                      |

---

## ⚙️ **Step-by-Step Integration**

### **1. Install FluentValidation**

```bash
dotnet add package FluentValidation.AspNetCore
```

---

### **2. Create Validator for Product DTO**

```csharp
using FluentValidation;
using ProductCatalog.DTOs;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required");
    }
}
```

---

### **3. Register Validation in Program.cs**

```csharp
using FluentValidation;
using FluentValidation.AspNetCore;

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
```

---

### **4. Use It in Controller**

Your controller doesn't need `if` checks anymore like:

```csharp
if(!ModelState.IsValid) { return BadRequest(ModelState); }
```

FluentValidation does that automatically.

---

## 🧠 **Bonus: Custom Database Validation Example**

You can even ensure category exists in DB:

```csharp
RuleFor(x => x.CategoryId)
    .MustAsync(async (id, cancellation) => await categoryService.ExistsAsync(id))
    .WithMessage("Category does not exist");
```

---

## 🎯 **Final Outcome**

Your project now has:
✅ Clean separation: Controller → DTO → Validation → Service → Repository
✅ No invalid product can enter your system
✅ Reusable and testable validation logic
✅ Professional-grade architecture



# 🎭 **Scene 8: The Royal Shield – Global Exception Handling Middleware**

Your application is now structured beautifully with layers of validation, logic, and data flow.

But… what happens when unexpected things occur?

❌ Database connection fails
❌ Null reference in service
❌ Invalid ID causes a crash
❌ Unhandled exception breaks the API

If we don’t prepare for this, errors will directly reach the user — like enemies breaking into the castle from hidden tunnels.

So the king appoints **a Royal Shield** — **Global Exception Handling Middleware**.

This middleware catches all unwanted errors, logs them, and responds with a clean, friendly, consistent message.

---

## ✅ **Why Global Exception Handling?**

| Without it ❌                      | With it ✅                        |
| --------------------------------- | -------------------------------- |
| Application crashes on exceptions | App stays alive and handles 🛡   |
| Stack traces exposed to users     | Clear, safe error messages       |
| Duplicate try-catch everywhere    | Centralized error-handling logic |
| Hard to log & debug               | Logs + meaningful response       |

---

## 🏗 **Step 1: Create Custom Middleware**

```csharp
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ProductCatalog.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue pipeline
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Catch any exception
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new
            {
                Success = false,
                Message = ex.Message,
                Details = "An unexpected error occurred. Please try again later."
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
```

---

## 🛠 **Step 2: Register Middleware in Program.cs**

```csharp
using ProductCatalog.Middleware;

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Add this BEFORE app.UseAuthorization()

// Other middleware
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

## 🎯 **Optional: Customize Exceptions for Better Clarity**

You can create custom exception classes for business rules:

```csharp
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
```

Then modify `HandleExceptionAsync` to detect:

```csharp
if (ex is NotFoundException)
{
    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
}
```

---

## ✅ **Final Output Example**

If something wrong happens, instead of crashing, the API will respond like this:

```json
{
  "success": false,
  "message": "Object reference not set to an instance of an object",
  "details": "An unexpected error occurred. Please try again later."
}
```

---

## 🌟 **Your Kingdom Is Now Safe**

✔ All unhandled exceptions are caught
✔ No more ugly error pages
✔ No internal technical details leaked
✔ Users get meaningful replies
✔ Developers get logs and control

---


## ✅ ✅ **So what’s next?**

Since Global Exception Handling is already done, we can naturally move to the **next level in API maturity**, which is:

### 🎯 **Scene 9: Unified API Response Wrapper**

*(Recommended next step)*

Right now:

* Success responses and error responses may look different.
* Sometimes you return raw entity objects, sometimes DTOs.
* Validation errors look one way, exceptions another.

To make your API feel clean, professional, and easy to consume — we introduce a **standard response format**.

---

### 🛡 Suggested Storyline Progression:

✅ Scene 8 → Global Exception Handling ✅
⬇
🎯 **Scene 9 → Standard API Response Format (Success/Error Wrapper)**
⬇
🔐 Scene 10 → JWT Authentication & Role-Based Authorization
⬇
📝 Scene 11 → Logging (Serilog)
⬇
✅ Scene 12 → Unit Testing Services, Validators, and Middleware

---


# 🎭 **Scene 9: The Royal Messenger — Standard API Response Wrapper**

Your application is now protected with validation and global exception handling — like a secure kingdom.

But there’s still *one problem.*
Every messenger (API response) speaks in a different style.

Sometimes your API returns:

```json
{ "id": 1, "name": "Laptop", "price": 50000 }
```

Other times:

```json
{ "success": false, "message": "Something went wrong" }
```

⚠️ Inconsistent.
⚠️ Hard for front-end developers to handle.
⚠️ Not suitable for real-world API standards.

So, the king decides:
📜 **"Every response in this kingdom must follow one format."**

---

## ✅ **Goal of This Scene**

We’ll create a **standard response format (wrapper)** for:
✔ Successful API responses
✔ Error / exception responses
✔ Validation failures

---

## 🏗 **Step 1: Create a Generic Response Model**

```csharp
namespace ProductCatalog.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(T data, string message = null)
        {
            Success = true;
            Message = message ?? "Request was successful";
            Data = data;
        }

        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
```

---

## 🏗 **Step 2: Use ApiResponse in Controller (Success Case)**

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    var product = await _productService.GetProductByIdAsync(id);

    if (product == null)
        return NotFound(new ApiResponse<string>("Product not found"));

    return Ok(new ApiResponse<ProductDto>(product));
}
```

---

## ✅ **Step 3: Update Global Exception Middleware to Use Wrapper**

Modify your `HandleExceptionAsync` method:

```csharp
private Task HandleExceptionAsync(HttpContext context, Exception ex)
{
    var response = new ApiResponse<string>(ex.Message)
    {
        Success = false,
        Message = "Internal Server Error"
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    return context.Response.WriteAsync(JsonSerializer.Serialize(response));
}
```

---

## ✅ **Step 4: Wrap Validation Errors**

Add this to `Program.cs` or inside `ExceptionHandlingMiddleware`:

```csharp
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { Field = x.Key, Error = x.Value.Errors.First().ErrorMessage })
            .ToList();

        var response = new ApiResponse<object>(null)
        {
            Success = false,
            Message = "Validation Failed",
            Data = errors
        };

        return new BadRequestObjectResult(response);
    };
});
```

---

## 📦 **Final Output Example (Success)**

```json
{
  "success": true,
  "message": "Request was successful",
  "data": {
    "id": 1,
    "name": "Laptop",
    "price": 50000,
    "categoryName": "Electronics"
  }
}
```

---

## ❌ **Final Output Example (Error / Exception)**

```json
{
  "success": false,
  "message": "Internal Server Error",
  "data": null
}
```

---

## 🎯 **What We Achieved:**

✔ Consistent API response format
✔ Frontend developers love it — easier parsing
✔ Works with success, validation, and exceptions
✔ Makes API production-ready



# 🎭 **Scene 10: The Royal Scribe – Logging with Serilog**

Your kingdom (API) now has order:
✔ Validation gates (FluentValidation)
✔ Guard shields against crashes (Global Exception Handling)
✔ Messengers with a standard language (API Response Wrapper)

But the king still wonders…
🕵️ “When something goes wrong, who keeps record of it?”
📜 “Who writes down every battle, every request, every failure so we can learn and improve?”

Enter **Serilog — the Royal Scribe**.

Serilog records everything:
✅ API requests
✅ Exceptions and warnings
✅ Business actions (Product added, Category deleted)
✅ Logs to **Console**, **File**, or even a **Database**

---

## 🛠 **Step 1: Install Serilog Packages**

Run these commands:

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.MySQL     // If saving logs to MySQL (optional)
```

---

## 🛠 **Step 2: Configure Serilog in Program.cs**

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/app_log.txt", rollingInterval: RollingInterval.Day)
    //.WriteTo.MySQL(builder.Configuration.GetConnectionString("DefaultConnection"), "Logs") // Optional – to DB
    .CreateLogger();

builder.Host.UseSerilog(); // Replace default logging
```

---

## 🛠 **Step 3: Log Information in Controllers / Services**

```csharp
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService service, ILogger<ProductsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        _logger.LogInformation("Fetching product with ID: {ProductId}", id);

        var product = await _service.GetProductByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", id);
            return NotFound(new ApiResponse<string>("Product not found"));
        }

        _logger.LogInformation("Product fetched successfully: {ProductId}", id);
        return Ok(new ApiResponse<ProductDto>(product));
    }
}
```

---

## 🛠 **Step 4: Log Exceptions in Middleware**

Update your exception middleware:

```csharp
private Task HandleExceptionAsync(HttpContext context, Exception ex)
{
    _logger.LogError(ex, "An unhandled exception occurred");

    var response = new ApiResponse<string>("Internal Server Error")
    {
        Success = false,
        Data = null
    };

    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Response.ContentType = "application/json";

    return context.Response.WriteAsync(JsonSerializer.Serialize(response));
}
```

---

## 🛢 **Step 5 (Optional): Log to MySQL Database**

```csharp
dotnet add package Serilog.Sinks.MySQL
```

Your connection must have a `Logs` table. Then:

```csharp
.WriteTo.MySQL(builder.Configuration.GetConnectionString("DefaultConnection"), "Logs")
```

---

## ✅ **Final Log Output Example (Console/File)**

```
[10:15:32 INF] Fetching product with ID: 5
[10:15:32 WRN] Product with ID 5 not found
[10:20:14 ERR] System.NullReferenceException: Object reference not set to an instance of an object.
```

---

## 🎯 **What We Achieved**

✔ Centralized logging using Serilog
✔ Logs stored in console and files with dates
✔ Optionally store in MySQL database
✔ Debugging becomes easier and faster
✔ Works perfectly with our API architecture



# 🎭 **Scene 11: The King’s Gate & Royal Guards — JWT Authentication & Role-Based Access Control**

Your application is now well-built, validated, logged, and protected from crashes.

But the kingdom still has *open gates* — anyone can enter and access data.

The king declares:
**“From now on, only trusted citizens may enter. And only certain roles (Admin, Customer, Staff) can access certain areas.”**

This is where **JWT (JSON Web Token) Authentication** and **Role-Based Authorization** come into play.

---

## ✅ **What is JWT? (Simple Explanation)**

Imagine the king gives a sealed scroll (token) to a citizen after verifying their identity.

* ✅ The scroll contains their **UserId**, **Name**, and **Role**
* ✅ It is **digitally signed** so nobody can forge it
* ✅ Guards (APIs) check the scroll and allow or deny access

That's JWT authentication.

---

## 🛠 **Step 1: Install JWT Authentication Packages**

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
```

---

## 🛠 **Step 2: Add JWT Settings in appsettings.json**

```json
"Jwt": {
  "Key": "THIS_IS_A_SUPER_SECRET_KEY_12345",
  "Issuer": "ProductCatalogAPI",
  "Audience": "ProductCatalogClient"
}
```

---

## 🛠 **Step 3: Configure JWT in Program.cs**

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();
app.UseAuthentication();   // 🔥 Very important - must be before Authorization
app.UseAuthorization();
```

---

## 🛠 **Step 4: Create a Login Endpoint to Generate JWT Token**

```csharp
[HttpPost("login")]
public IActionResult Login(LoginDto login)
{
    if (login.Username == "admin" && login.Password == "admin123")
    {
        var token = GenerateJwtToken(login.Username, "Admin");
        return Ok(new { Token = token });
    }

    return Unauthorized(new ApiResponse<string>("Invalid credentials"));
}

private string GenerateJwtToken(string username, string role)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(2),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```

---

## 🔐 **Step 5: Secure Endpoints Using [Authorize] & Roles**

```csharp
[Authorize]  // Only authenticated users
[HttpGet("all")]
public async Task<IActionResult> GetAllProducts()
{
    return Ok(new ApiResponse<IEnumerable<ProductDto>>(await _productService.GetAllAsync()));
}

[Authorize(Roles = "Admin")] // Only Admin can delete
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    await _productService.DeleteProductAsync(id);
    return Ok(new ApiResponse<string>("Product deleted successfully"));
}
```

---

## ✅ **Final Result**

| Scenario                   | Result                             |
| -------------------------- | ---------------------------------- |
| No token provided          | ❌ 401 Unauthorized                 |
| Token provided but invalid | ❌ 401 Unauthorized                 |
| Valid Token (Normal User)  | ✅ Can access [Authorize] endpoints |
| Valid Token + Admin Role   | ✅ Can access Admin-only APIs       |

---

## 🎯 **What We Achieved**

✔ Secure login using JWT Authentication
✔ Authorization using Roles like *Admin*, *User*
✔ Protected APIs from unauthorized access
✔ Token-based, stateless authentication → Perfect for Web & Mobile Apps
✔ Real-world production-ready security setup



# 🎭 **Scene 12: The Royal Inspectors — Unit Testing Services, Controllers & Validators**

By now your application kingdom has:

✔ Strong walls (Validation + Exception Handling)
✔ Guarded gates (JWT Authentication)
✔ Organized messengers (API Response Wrapper)
✔ Royal scribes (Serilog Logging)

But the King now worries…
🧐 “How do we make sure every guard, every gate, every service does its job correctly — before battle?”

So, he appoints the **Royal Inspectors** — *Unit Tests*.
They test every layer **without running the full application**, ensuring each piece works perfectly on its own.

---

## 🎯 **What We Will Cover in This Scene**

✅ Setting up Unit Testing Project using `.NET CLI`
✅ Writing Unit Tests for **Service Layer (Business Logic)**
✅ Writing Unit Tests for **Controllers**
✅ Testing **Validators (FluentValidation)**
✅ Mocking Database/Repository using **Moq or InMemory DbContext**

---

## 🛠 **Step 1: Create a Test Project**

```bash
dotnet new xunit -n ProductCatalog.Tests
dotnet add ProductCatalog.Tests reference ProductCatalog.API   // Or your main project
dotnet add ProductCatalog.Tests package Moq
dotnet add ProductCatalog.Tests package FluentAssertions
dotnet add ProductCatalog.Tests package Microsoft.EntityFrameworkCore.InMemory
dotnet add ProductCatalog.Tests package FluentValidation
```

---

## 🛠 **Step 2: Unit Test for Product Service (Mock Repository)**

```csharp
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct_WhenExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop", Price = 50000 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _service.GetProductByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Laptop");
    }
}
```

---

## 🛠 **Step 3: Unit Test for Controller (Mock service + check HTTP responses)**

```csharp
public class ProductsControllerTests
{
    private readonly Mock<IProductService> _service;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _service = new Mock<IProductService>();
        _controller = new ProductsController(_service.Object, null);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_IfNoProduct()
    {
        // Arrange
        _service.Setup(s => s.GetProductByIdAsync(10)).ReturnsAsync((ProductDto)null);

        // Act
        var result = await _controller.GetProduct(10);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }
}
```

---

## 🛠 **Step 4: Unit Test for FluentValidation Validator**

```csharp
public class ProductDtoValidatorTests
{
    private readonly ProductCreateDtoValidator _validator;

    public ProductDtoValidatorTests()
    {
        _validator = new ProductCreateDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new ProductCreateDto { Name = "", Price = 500 };

        var result = _validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "Name");
    }
}
```

---

## 📦 **Folder Structure (Recommended)**

```
ProductCatalog.API/
ProductCatalog.Core/
ProductCatalog.Infrastructure/
ProductCatalog.Tests/
    ├── Services/
    ├── Controllers/
    ├── Validators/
```

---

## 🔍 **What We Achieved**

✔ You now know how to unit test **Service, Controller, and Validator layers**
✔ You used **Moq** to fake repositories & avoid real DB
✔ You tested validation logic with **FluentValidation**
✔ Your codebase is now more **professional, maintainable & bug-proof**

### 🎬 **Next Scene: Swagger Documentation & API Versioning**

*(Mentor Storytelling Style — The Royal Library & Time Machine of the Kingdom)*

---

#### 🏰 **Scene Setup:**

The Digital Kingdom (your API) is growing rapidly — new controllers, services, business rules, security layers, validations, exception shields, JWT guards…

Your team of knights (developers) and visiting kingdoms (frontend teams, mobile apps, third parties) come asking:

> “How do we know what endpoints are available? What data should we send? How do we test these securely? What if future versions change?”

So the King (you) declares:

📜 **“We need a Royal Library — a place where every API, every request, every response is documented and testable.”**

This library is called: **Swagger (OpenAPI)**.
And to preserve changes across history? You build a **Time Machine — API Versioning**.

---

### ✅ **🎯 Mission Objectives in This Scene:**

| Goal                        | Description                                                                   |
| --------------------------- | ----------------------------------------------------------------------------- |
| 📘 Swagger UI               | Auto-generate interactive documentation of your APIs                          |
| 🧪 Swagger with JWT Support | Allow users to login via Bearer token and test authenticated calls            |
| ⏳ API Versioning            | Maintain multiple API versions like `/api/v1/products` and `/api/v2/products` |
| 🔧 Configure in Program.cs  | Add services + middleware                                                     |

---

### ⚙️ **Step 1: Install Required Packages**

```bash
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
```

---

### ⚙️ **Step 2: Configure Swagger in Program.cs**

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Catalog API",
        Version = "v1",
        Description = "API for managing Products & Categories with JWT Auth, Validation, and Clean Architecture."
    });

    // Add JWT Authentication to Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer <your_token>'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});
```

---

### ⚙️ **Step 3: Add API Versioning in Program.cs**

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
```

---

### ⚙️ **Step 4: Enable Swagger Middleware**

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalog API v1");
    });
}
```

---

### 🛠 **Step 5: Versioning Controllers Example**

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    // Actions...
}
```

---

### 🌟 **Scene Outcome:**

✅ Developers can explore & test APIs in browser using Swagger UI
✅ JWT-secured endpoints can be tested by adding token in Swagger
✅ API versions (/v1/, /v2/) allow safe evolution of your system
✅ Your API is now documented, discoverable, and future-proof



## 🎬 **Scene: The Shipyard of the Kingdom — Docker Deployment**

Your API kingdom is now powerful and well-structured, but it still sits inside your local fortress (localhost).
You want it to sail across the digital oceans — to servers, cloud platforms, or other machines.
For that, you need **Docker Ships 🛳️** — portable containers carrying your API, database, and full environment.

---

### ✅ **🎯 Goals of This Scene:**

| Goal                                                    | Description |
| ------------------------------------------------------- | ----------- |
| 📦 Dockerize ASP.NET Core Product Catalog API           |             |
| 🐬 Run MySQL in a Docker container                      |             |
| 🔁 Use Docker Compose to connect API + MySQL            |             |
| 🔐 Configure environment variables & connection strings |             |
| ✅ Test containers locally                               |             |

---

## 🏗️ **Step 1: Add Docker Support to the Project**

First, create a file named **`Dockerfile`** in your API project root:

```dockerfile
# Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY ProductCatalogAPI/*.csproj ProductCatalogAPI/
RUN dotnet restore

COPY . .
WORKDIR /src/ProductCatalogAPI
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalogAPI.dll"]
```

---

## 🐬 **Step 2: MySQL Configuration via Docker**

Create a **docker-compose.yml** at the solution root:

```yaml
version: '3.8'

services:
  mysql-db:
    image: mysql:8.0
    container_name: mysql-db
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: root123
      MYSQL_DATABASE: ProductCatalogDB
      MYSQL_USER: appuser
      MYSQL_PASSWORD: app@123
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql

  product-api:
    build:
      context: .
      dockerfile: ProductCatalogAPI/Dockerfile
    container_name: product-api
    depends_on:
      - mysql-db
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__DefaultConnection: "server=mysql-db;port=3306;database=ProductCatalogDB;user=appuser;password=app@123"
    ports:
      - "5000:80"  # http://localhost:5000/swagger

volumes:
  mysql_data:
```

---

## ⚙️ **Step 3: Update `appsettings.json`**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=mysql-db;port=3306;database=ProductCatalogDB;user=appuser;password=app@123"
  }
}
```

---

## 🚀 **Step 4: Build & Run Docker Containers**

```bash
# Build and run both containers
docker-compose up --build

# Stop containers
docker-compose down
```

---

## ✅ **Step 5: Test It!**

✅ Open Swagger:
👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

✅ Check MySQL is running in Docker:

```bash
docker exec -it mysql-db mysql -u appuser -p
SHOW DATABASES;
```

---

### 🌟 **Scene Outcome:**

✅ Your API runs in a Docker container
✅ MySQL runs in another isolated container
✅ Both communicate using Docker Compose
✅ You’re now ready to move to Cloud Deployment (AWS, Azure, etc.) 🚀

-## 🎬 **Scene: “The Cloud Kingdom – Journey to AWS EC2”**

Your API fleet (Docker containers) is ready. Now it’s time to sail into the **Cloud Kingdom (AWS)** — specifically to **EC2**, a virtual Linux server that will host your Product Catalog API and MySQL database.

Think of EC2 as renting a digital land where you can build your own server fortress.

---

### 🏁 **🎯 Mission Goals for This Scene:**

| Goal                                               | Result |
| -------------------------------------------------- | ------ |
| 🛡 Launch EC2 instance (Ubuntu)                    |        |
| 🔐 Configure SSH access & security groups          |        |
| 🐳 Install Docker & Docker Compose on EC2          |        |
| 📦 Deploy Product API + MySQL using docker-compose |        |
| ✅ Test using public IP or domain                   |        |

---

## 🛠️ **Step 1: Launch EC2 Instance (Ubuntu Server)**

✔ Go to **AWS Console → EC2 → Launch Instance**
✔ Choose **Ubuntu 22.04 LTS**
✔ Instance type: **t2.micro (Free Tier eligible)**
✔ Key Pair: Create or use an existing `.pem`
✔ Allow Ports in Security Group:

* SSH (22)
* HTTP (80)
* HTTPS (443 if SSL later)
* Custom TCP: **5000** (for Swagger API)

✔ Launch instance ✅

---

## 🗝️ **Step 2: Connect to EC2 via SSH**

Open terminal or PowerShell:

```bash
ssh -i "your-key.pem" ubuntu@ec2-your-public-ip.compute.amazonaws.com
```

---

## 🐳 **Step 3: Install Docker & Docker Compose on EC2**

```bash
sudo apt update && sudo apt upgrade -y
sudo apt install docker.io -y
sudo systemctl start docker
sudo systemctl enable docker
sudo usermod -aG docker ubuntu

# Install Docker Compose
sudo curl -L "https://github.com/docker/compose/releases/download/2.24.6/docker-compose-linux-x86_64" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
docker-compose --version
```

---

## 📂 **Step 4: Copy Your Project to EC2**

From your local system terminal:

```bash
scp -i "your-key.pem" -r ./ProductCatalogAPI ubuntu@ec2-your-ip:/home/ubuntu/
```

---

## 🛠️ **Step 5: Add docker-compose.yml on EC2**

Inside EC2:

```bash
cd /home/ubuntu/ProductCatalogAPI
nano docker-compose.yml
```

Paste:

```yaml
version: '3.8'
services:
  mysql-db:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: root123
      MYSQL_DATABASE: ProductCatalogDB
      MYSQL_USER: appuser
      MYSQL_PASSWORD: app@123
    restart: always
    volumes:
      - mysql_data:/var/lib/mysql
    ports:
      - "3306:3306"

  api:
    build: .
    environment:
      ASPNETCORE_ENVIRONMENT: Production
      ConnectionStrings__DefaultConnection: "server=mysql-db;port=3306;database=ProductCatalogDB;user=appuser;password=app@123"
    ports:
      - "5000:80"
    depends_on:
      - mysql-db

volumes:
  mysql_data:
```

---

## 🚀 **Step 6: Build & Run on EC2**

```bash
docker-compose up --build -d
docker ps
```

✅ API is now live at:

👉 **[http://EC2-PUBLIC-IP:5000/swagger](http://EC2-PUBLIC-IP:5000/swagger)**

---

## ✅ **Scene Outcome:**

✅ Your .NET API is deployed on AWS EC2 🟢
✅ MySQL and API are running as Docker containers 🐳
✅ Accessible from anywhere in the world 🌍
✅ Secure / Reusable Infrastructure — Ready for SSL, CI/CD, Load Balancer next

## 🎬 **Scene: The Royal Automated Army (CI/CD)**

Your API is now hosted in the cloud kingdom (AWS EC2), but your knights (developers) still travel miles manually to update the server — building the app, copying files, restarting containers.

The King (you) says:

> “Why send knights every time? Let’s build a *Royal Automated Army* that auto-deploys updates whenever we push to GitHub.”

This is called **CI/CD (Continuous Integration / Continuous Deployment)**.

---

### ✅ **🎯 Goals for This Scene:**

| Task                                                | Outcome |
| --------------------------------------------------- | ------- |
| ✅ Configure GitHub Actions for CI/CD                |         |
| ✅ On each push to `main`, build & test the API      |         |
| ✅ SSH into EC2 automatically                        |         |
| ✅ Pull project & run `docker-compose up --build -d` |         |
| ✅ Zero manual deployment from now on ⚡              |         |

---

## 🛠️ **Step 1: Add SSH Keys for GitHub to Access EC2**

### 1️⃣ On LOCAL machine:

Generate SSH deployment key:

```bash
ssh-keygen -t rsa -b 4096 -C "ci-cd-key"
```

This creates:

* ✅ `id_rsa` (private key)
* ✅ `id_rsa.pub` (public key)

### 2️⃣ On **EC2 Server:**

```bash
nano ~/.ssh/authorized_keys
```

Paste contents of `id_rsa.pub`.

### 3️⃣ In **GitHub Repository → Settings → Secrets and Variables → Actions → New Repository Secrets**

Add these:

| Name       | Value                            |
| ---------- | -------------------------------- |
| `EC2_HOST` | Public IP or DNS of EC2          |
| `EC2_USER` | ubuntu                           |
| `EC2_KEY`  | Contents of `id_rsa` private key |
| `EC2_PATH` | `/home/ubuntu/ProductCatalogAPI` |

---

## 🛠️ **Step 2: Create GitHub Workflow File**

In your project:
📁 `.github/workflows/deploy.yml`

```yaml
name: CI/CD Pipeline - Deploy to AWS EC2

on:
  push:
    branches: [ "main" ]

jobs:
  deploy:
    runs-on: ubuntu-latest

    steps:
    - name: Checkout code
      uses: actions/checkout@v3

    - name: Set up .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'

    - name: Restore dependencies
      run: dotnet restore

    - name: Build API
      run: dotnet build --configuration Release --no-restore

    - name: Run Tests (if available)
      run: dotnet test --no-build --verbosity normal

    - name: Copy files to EC2 via SSH
      uses: appleboy/scp-action@v0.1.7
      with:
        host: ${{ secrets.EC2_HOST }}
        username: ${{ secrets.E2_USER }}
        key: ${{ secrets.EC2_KEY }}
        source: "."
        target: ${{ secrets.EC2_PATH }}

    - name: Deploy via SSH (Docker Compose Restart)
      uses: appleboy/ssh-action@v1.0.3
      with:
        host: ${{ secrets.EC2_HOST }}
        username: ${{ secrets.EC2_USER }}
        key: ${{ secrets.EC2_KEY }}
        script: |
          cd ${{ secrets.EC2_PATH }}
          docker-compose down
          docker-compose up --build -d
```

---

## ✅ **Step 3: Push Code & Watch Magic**

1. Commit and push changes to GitHub:

   ```bash
   git add .
   git commit -m "CI/CD Pipeline added"
   git push origin main
   ```

2. Go to:
   ✅ GitHub → Repo → Actions → Watch Pipeline Run 🟢
   ✅ Login to EC2 → Run `docker ps` to confirm updated containers

---

## 🎉 **Scene Outcome:**

✅ Fully automated build + deploy pipeline
✅ No more SSH, file copying, manual restart
✅ Dev pushes → API auto-updates on EC2
✅ You just leveled up from Developer to *DevOps Knight* ⚔️


🔥 **Scene: Monitoring – The Watchtower of the API Kingdom**
*(Mentor storytelling style: Making your system observable like a kingdom keeping watch for enemies at night)*

---

### 🏰 **Story Setup:**

Your API kingdom is now live on AWS EC2. CI/CD troops are working. Ships (Docker containers) are sailing smoothly.

But… the King now asks the Royal Engineer (you):

> “How do we know if monsters (exceptions), thieves (slow APIs), or spies (failed logins) sneak into our kingdom at midnight?”

You reply boldly:

> “My King, we need **Watchtowers & Signal Fires** — a Monitoring System.”

---

### 🎯 **In this Scene, You’ll Build:**

| Feature                                 | Purpose                                                |
| --------------------------------------- | ------------------------------------------------------ |
| 📜 **Serilog Structured Logging**       | Log everything into files & cloud-friendly format      |
| 📊 **Serilog + Seq / Kibana / Grafana** | Visual dashboard for logs & queries                    |
| 📈 **Health Checks**                    | API health endpoints (`/health`) for uptime monitoring |
| ⚡ **Request Performance Tracking**      | Track slow APIs with middleware                        |

---

## ✅ **Step 1: Install Monitoring Tools (Serilog + Seq)**

Run:

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.Seq
dotnet add package Serilog.Enrichers.Environment
dotnet add package Serilog.Enrichers.Thread
```

---

## ✅ **Step 2: Configure Serilog in Program.cs**

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Setup Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/product-api-.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341")  // Change to EC2 IP if needed
    .CreateLogger();

builder.Host.UseSerilog();

```

---

## ✅ **Step 3: Add Health Checks**

```bash
dotnet add package AspNetCore.HealthChecks.UI
dotnet add package AspNetCore.HealthChecks.MySql
```

In `Program.cs`:

```csharp
builder.Services.AddHealthChecks()
    .AddMySql(builder.Configuration.GetConnectionString("DefaultConnection"), name: "MySQL");

var app = builder.Build();
app.MapHealthChecks("/health");
```

✅ Check health:
🔹 `http://localhost:5000/health`
Returns ✅ Healthy or ❌ Unhealthy JSON.

---

## ✅ **Step 4: Optional – Add SEQ Logging Dashboard (Developer Monitoring Tool)**

Add to **docker-compose.yml**:

```yaml
seq:
  image: datalust/seq
  container_name: seq-logs
  environment:
    - ACCEPT_EULA=Y
  ports:
    - "5341:80"
```

Then open:
👉 [http://localhost:5341](http://localhost:5341)
You can **search logs, filter by errors, track performance, and more.**

---

## ✅ **Step 5: Log API Performance (Middleware)**

```csharp
public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        await _next(context);
        watch.Stop();

        _logger.LogInformation("Request {method} {url} => {statusCode} took {time} ms",
            context.Request?.Method,
            context.Request?.Path.Value,
            context.Response?.StatusCode,
            watch.ElapsedMilliseconds);
    }
}
```

Add in Program.cs:

```csharp
app.UseMiddleware<RequestTimingMiddleware>();
```

---

## 🎉 **Scene Outcome:**

✅ Logs saved to files + console + Seq
✅ `/health` endpoint monitors MySQL + API health
✅ API performance tracked in ms
✅ Errors visible in dashboards instead of guessing in darkness

---

### 🚀 **Next Scene Options:**

What should we build next, Commander?

| Option                                                 | Description |
| ------------------------------------------------------ | ----------- |
| 🔒 **SSL + HTTPS using Nginx, Certbot, Let's Encrypt** |             |
| ⚡ **Redis Caching (Product & Category data)**          |             |
| 🧱 **API Rate Limiting & Security Best Practices**     |             |
| 👑 **Admin Dashboard or UI using React/Angular**       |             |

---

💬 *Reply with:*
👉 **Next Scene: SSL**
👉 **Next Scene: Redis Cache**
👉 **Next Scene: Rate Limiting**

Your command, my king 👑


🔥 **Scene: Redis Cache – The Royal Treasury of Fast Responses**
*(Mentor Storytelling Style: Performance Boost for Product Catalog API)*

---

### 🏰 **Story Setup:**

Your Product API kingdom is now stable, secured, and monitored.

But the King notices:

> “Every time a merchant requests the product list, our scribes (database) must run to the library (MySQL), fetch scrolls, and return… It’s slow when repeated thousands of times.”

You reply with a smile:

> “My Lord, we will build a **Royal Treasury of Memory** — a lightning-fast data vault called **Redis Cache**.”

---

### 🎯 **🎯 Scene Goals:**

| Goal                                                | Description |
| --------------------------------------------------- | ----------- |
| ✅ Setup Redis (in Docker / EC2)                     |             |
| ✅ Configure .NET API to connect to Redis            |             |
| ✅ Cache Products & Categories                       |             |
| ✅ Use Cache Aside Pattern                           |             |
| ✅ Auto-refresh cache when new data is added/updated |             |

---

## 🐳 **Step 1: Add Redis to `docker-compose.yml`**

Add this to your existing `docker-compose.yml`:

```yaml
redis:
  image: redis:latest
  container_name: redis-cache
  restart: always
  ports:
    - "6379:6379"
```

Start it:

```bash
docker-compose up -d
```

---

## 📦 **Step 2: Install Redis Package in .NET API**

```bash
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
```

---

## ⚙️ **Step 3: Configure Redis in `Program.cs`**

```csharp
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // If running in EC2 use: "redis:6379"
    options.InstanceName = "ProductCatalog_";
});
```

---

## ⚡ **Step 4: Apply Redis Cache in ProductService**

Use **Cache-Aside Pattern**:

* First check Redis cache
* If data exists → return cached data
* If not → fetch from DB → store in Redis → return

```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepo, IMapper mapper, IDistributedCache cache, ILogger<ProductService> logger)
    {
        _productRepo = productRepo;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        string cacheKey = "product_list";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            _logger.LogInformation("✅ Products loaded from Redis Cache");
            return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(cachedData);
        }

        _logger.LogInformation("⚠️ Cache Miss - Fetching from Database...");
        var products = await _productRepo.GetAllAsync();
        var result = _mapper.Map<IEnumerable<ProductDto>>(products);

        // Save result to cache
        var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), options);
        return result;
    }
}
```

---

## 🧹 **Step 5: Clear Cache When New Product Added / Updated**

```csharp
public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
{
    var product = _mapper.Map<Product>(dto);
    var addedProduct = await _productRepo.AddAsync(product);

    await _cache.RemoveAsync("product_list"); // 🧹 Remove outdated cache

    return _mapper.Map<ProductDto>(addedProduct);
}
```

---

## 👑 **Scene Outcome:**

✅ Redis Cache enabled for super-fast responses ⚡
✅ MySQL load reduced → high performance
✅ Cache-auto-clears on data changes
✅ Perfect setup for high-traffic, scalable APIs


### ✅ **Next Scene: Admin Panel – The Control Tower of Your Product Catalog System**

---

**🎬 Scene Setup:**

By now, our product catalog system is no longer just a backend API—it’s a living, breathing system in production. Products are flowing, categories are structured, users are logging in, JWT security is in place, data is cached with Redis, and logs are being captured.

But who is controlling it all?
Who decides which product should be active or inactive?
Who manages users, handles approvals, or updates inventory?

💡 **Answer: We need an Admin Panel.**

Think of it like the flight control tower of an airport—only authorized people can enter, and from here, they can control the entire system.

---

### 🛠️ **🎥 Scene: Building the Admin Panel**

#### 🧩 **1. Tech Stack Choices**

| Layer          | Choices                                                             |
| -------------- | ------------------------------------------------------------------- |
| Frontend       | React.js / Angular / Blazor Server / Razor Pages                    |
| UI Framework   | Bootstrap / Tailwind CSS / Material UI                              |
| Authentication | JWT Token stored securely (HTTP-only cookie or localStorage)        |
| Authorization  | Role-based – only `Admin` role can access this UI                   |
| API Usage      | Uses existing `/api/products`, `/api/categories`, `/api/users` APIs |

---

#### 🎯 **2. Features an Admin Panel Must Have**

| Feature                       | Description                                                             |
| ----------------------------- | ----------------------------------------------------------------------- |
| ✅ Admin Login Page            | Uses JWT Authentication                                                 |
| ✅ Dashboard                   | Overview: Total Products, Categories, Users, Recent Orders (optional)   |
| ✅ Manage Products             | Create, Update, Delete, Activate/Deactivate products                    |
| ✅ Manage Categories           | CRUD operations with validation                                         |
| ✅ User Management             | Show users, assign/remove roles (Admin/User)                            |
| ✅ View Logs (Serilog output)  | Display recent API errors, warnings, user activities                    |
| ✅ Role-Based Access           | Only admin role should access this panel                                |
| ✅ Protected Routes (Frontend) | Admin routes like `/admin/products` require JWT + Admin role validation |

---

### ⚙️ **3. Implementing Admin Panel Step-by-Step**

#### ✅ **Step 1: Add Admin Role During User Seeding**

```csharp
var adminUser = new ApplicationUser
{
    UserName = "admin@catalog.com",
    Email = "admin@catalog.com"
};

if (userManager.Users.All(u => u.Email != adminUser.Email))
{
    await userManager.CreateAsync(adminUser, "Admin@123");
    await userManager.AddToRoleAsync(adminUser, "Admin");
}
```

---

#### ✅ **Step 2: Create Secure Admin API Routes**

```csharp
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly IProductService _service;

    public AdminProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
    {
        var result = await _service.AddProductAsync(dto);
        return Ok(result);
    }
}
```

---

#### ✅ **Step 3: React/Blazor Admin UI Sample Routing**

```jsx
<Route path="/admin" element={<AdminLayout />}>
    <Route path="dashboard" element={<Dashboard />} />
    <Route path="products" element={<ManageProducts />} />
    <Route path="categories" element={<ManageCategories />} />
    <Route path="users" element={<UserManagement />} />
</Route>
```

---

### 🔐 **4. Securing the Frontend Admin Panel**

* When logging in as admin, store the JWT securely.
* On each admin request, send the token:

```js
axios.get('/api/admin/products', {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
});
```

* Protect routes using a PrivateRoute component:

```jsx
const PrivateRoute = ({ element }) => {
  const token = localStorage.getItem("token");
  const role = localStorage.getItem("role");
  return token && role === "Admin" ? element : <Navigate to="/login" />;
};
```

---

### 🎖️ **5. Final Outcome of this Scene**

✅ Admin can control every aspect of the product catalog
✅ Safe, secure, and role-protected interface
✅ Fully connected with APIs, JWT, and database
✅ Foundation ready for future features like Order Management, Inventory, Discounts, Analytics

---

### 🎬 **Would You Like the Next Scene?**

Here are possible next chapters:

* 📈 **Analytics & Dashboard Charts**
* 📦 **Inventory Management Integration**
* 📤 **Email Notifications (SMTP/MailKit)**
* 🌐 **Multi-Tenancy Support**
* 🛒 **E-Commerce Cart & Checkout System**

