

**step-by-step mentor-style CLI-based guide** to create this ASP.NET Core Web API from scratch using the **.NET CLI**.

We’ll build everything using:

* 🔹 .NET CLI
* 🔹 Folder structure discipline
* 🔹 Controllers + Models
* 🔹 Run & Test

---

# ✅ Step 0 – Prerequisite

Make sure .NET SDK is installed:

```bash
dotnet --version
```

If not installed → download from
👉 Microsoft .NET official site

---

# ✅ Step 1 – Create Solution Folder

```bash
mkdir CustomerApi
cd CustomerApi
```

---

# ✅ Step 2 – Create Web API Project

```bash
dotnet new webapi -n CustomerService
```

Go inside project:

```bash
cd CustomerService
```

---

# ✅ Step 3 – Remove Default Weather API

Delete:

```
Controllers/WeatherForecastController.cs
WeatherForecast.cs
```

Using CLI:

```bash
rm Controllers/WeatherForecastController.cs
rm WeatherForecast.cs
```

(Windows PowerShell: `del` instead of `rm`)

---

# ✅ Step 4 – Create Models Folder

```bash
mkdir Models
```

Create Customer model:

```bash
touch Models/Customer.cs
```

Add:

```csharp
namespace CustomerService.Models;

public class Customer 
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
```

---

Create Credential model:

```bash
touch Models/Credential.cs
```

Add:

```csharp
namespace CustomerService.Models;

public class Credential
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
```

---

# ✅ Step 5 – Create Controllers

## 🔹 CustomersController

```bash
touch Controllers/CustomersController.cs
```

Add:

```csharp
using Microsoft.AspNetCore.Mvc;
using CustomerService.Models;

namespace CustomerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    public CustomersController()
    {
    }

    [HttpGet("{id}")]
    public ActionResult<Customer> GetById(int id)
    {
        return Ok(new Customer());
    }

    [HttpGet]
    public ActionResult<List<Customer>> GetAll()
    {
        List<Customer> customers = new List<Customer>();

        customers.Add(new Customer { Id = 2, FirstName = "kalyani", LastName = "Gadgil" });
        customers.Add(new Customer { Id = 3, FirstName = "test1", LastName = "test1" });
        customers.Add(new Customer { Id = 4, FirstName = "test2", LastName = "test2" });

        return Ok(customers);
    }

    [HttpPost]
    public ActionResult<Customer> Create(Customer customer)
    {
        return Ok("Created");
    }

    [HttpPut("{id}")]
    public ActionResult<Customer> Update(int id, Customer customer)
    {
        return Ok("Updated");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok("Deleted");
    }
}
```

---

## 🔹 AuthController

```bash
touch Controllers/AuthController.cs
```

Add:

```csharp
using Microsoft.AspNetCore.Mvc;
using CustomerService.Models;

namespace CustomerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public AuthController()
    {
    }

    [HttpPost]
    public ActionResult ValidateUser(Credential credential)
    {
        if (credential.UserName == "Kalyani" && credential.Password == "Kalyani")
        {
            return Ok("Validated");
        }
        else
        {
            return NotFound();
        }
    }
}
```

---

# ✅ Step 6 – Verify Program.cs

Open `Program.cs`

Make sure it contains:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

---

# ✅ Step 7 – Build Project

```bash
dotnet build
```

If successful → proceed.

---

# ✅ Step 8 – Run Application

```bash
dotnet run
```

You will see something like:

```
Now listening on: https://localhost:5001
```

---

# ✅ Step 9 – Test Using Swagger

Open browser:

```
https://localhost:5001/swagger
```

Swagger UI comes from
👉 Swagger

You can now test:

* GET /api/customers
* GET /api/customers/{id}
* POST /api/customers
* PUT /api/customers/{id}
* DELETE /api/customers/{id}
* POST /api/auth

---

# ✅ Sample JSON for Testing

## Create Customer

```json
{
  "id": 10,
  "firstName": "Ravi",
  "lastName": "Tambade"
}
```

## Validate User

```json
{
  "userName": "Kalyani",
  "password": "Kalyani"
}
```

---

# 🔥 Final Folder Structure

```
CustomerService
 ├── Controllers
 │     ├── CustomersController.cs
 │     └── AuthController.cs
 ├── Models
 │     ├── Customer.cs
 │     └── Credential.cs
 ├── Program.cs
 └── CustomerService.csproj
```



### The Shopkeeper, the Messenger, and the Magic Box"

*Dear Students,*

Imagine we are in a small town. There's a **famous shopkeeper** who has everything — toys, books, electronics — but his shop is locked behind a glass wall. You can't walk in directly and pick things. But don’t worry — there's a smart system in place.

There’s a **messenger** (we'll call him *Postman* 😊) who stands outside. He has a catalog and knows exactly how to ask for things. If you want a list of all toys — you tell him “**GET toys**.” If you want to add a new toy — “**POST new toy**.” If you want to update something — “**PUT toy ID 3**.” If you want to remove one — “**DELETE toy ID 3**.”

The shopkeeper never talks directly to you. Instead, the **Postman** sends requests using a fixed format — just like forms. He knocks on the glass and shows a **specific request**. Inside, the shopkeeper understands it, processes it, and sends the response back through the messenger.

This is what a **Web API** is.

---

### 🌐 **So, what exactly is a Web API?**

Think of Web API as a **digital shopkeeper** who sits behind the scenes and responds to client requests.

* It doesn't have a face or GUI.
* It only **responds to requests** sent using proper methods (GET, POST, PUT, DELETE).
* It helps **apps talk to each other** — like mobile apps, websites, even IoT devices.

---

### 📦 **REST: The Delivery Rules**

Long ago, before we had proper delivery systems, everything was chaotic. People used their own languages, formats, and roads to deliver things. Then came a man named **Roy Fielding**, who created a standardized rulebook for how messengers should behave. That’s **REST — Representational State Transfer.**

REST is like a **code of conduct** for Web APIs:

* Speak clearly: Use **URIs** like `/api/products`.
* Be honest and stateless: Every request should contain **everything needed** — no memory of past conversations.
* Use simple verbs: GET, POST, PUT, DELETE.
* Be layered: Like having middlemen in delivery — security, logging, caching.

---

### 🛒 **Example: Product Store API**

Let’s say you're building an online store — your **Product Web API** is your invisible shopkeeper.

* `GET /api/products` – Show all products
* `POST /api/products` – Add a new product
* `PUT /api/products/5` – Update product with ID 5
* `DELETE /api/products/5` – Remove product 5

Behind the scenes, ASP.NET Web API handles these using **Controllers**, **Routes**, and **Models**.

---

### 🧑‍💻 **Controller is the Shopkeeper's Brain**

Let’s peek inside the code — your controller is like the **intelligent shopkeeper**. Each method (action) responds to a type of knock (GET, POST...).

```csharp
[HttpGet]
public IActionResult GetProducts()
```

He listens for the **GET** knock, and then uses a **service** to fetch the products — like opening a drawer and giving you what you asked for.

---

### 🧪 **How do we test this invisible shop?**

With **Postman!**

You don’t need a fancy app to test the Web API. Just use **Postman**, type the address (`http://localhost:port/api/products`), select method (GET/POST/PUT/DELETE), and press SEND.

It shows you the **response** — like a delivery receipt. It’s a brilliant way to verify if your shopkeeper is awake, responding correctly, and returning proper packages (JSON data).

---

### ⚖️ **Minimal API: A Street Vendor Style**

Sometimes, you don't need a full shop. Just a table and a register.

**Minimal API** in .NET 6+ is like a **small shop setup on the roadside**. Fewer things, quick setup, fewer formalities.

Instead of full controllers and routing files, you define everything in one file (`Program.cs`):

```csharp
app.MapGet("/api/products", () => productList);
```

It’s good for small use-cases, lightweight APIs, or quick prototypes.

---

### 🔍 **Mentor's Wisdom: When to Use What?**

* Use **Web API (with controllers)** if your app will grow big, have layers (auth, logging), or needs clean structure.
* Use **Minimal API** if you're building a quick backend, small services, or want faster delivery with less overhead.

---

### 🧠 **Closing Thoughts**

An API is not about UI — it's about **communication**.

As software engineers, you're not just building apps — you're building **connections** between systems. You are the architects of invisible cities where servers and clients **speak clearly and act fast.**

Learn how to **design these conversations**, how to secure them, scale them, and make them meaningful.

---

### 🧭 Your Practice Assignment

1. Create a `Product` model with `Id`, `Name`, and `Price`.
2. Build a Minimal API that supports all 4 CRUD operations.
3. Test your API using Postman.
4. Reflect: Could you have done this better using controllers? Why or why not?

---

Let’s build the **invisible internet**, one clean API at a time.

Stay curious, stay humble, and keep shipping. 🚀

— *Your Mentor* 🧑‍🏫




