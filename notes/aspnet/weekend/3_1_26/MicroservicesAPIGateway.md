
# *Microservices + API Gateway *

> Good morning everyone 😊
> Before we talk about *microservices*, *API gateways*, or *architecture*, let me ask you one simple question.

👉 **When you open Amazon, Flipkart, or Swiggy…**
Do you think there is **one single program** running behind it?

*(Pause… let students respond)*

No.
If there was one single program, it would collapse the moment millions of users click at the same time.

Today, I’m going to tell you **how such systems are really built**, not in theory, but in a **way your mind can visualize**.

> We are building an **online shopping system**.

Users can come from:

* Website
* Mobile app
* Admin panel
* POS machine
* External partners

Now tell me honestly:

❓ *Should every client directly talk to every internal service?*

Correct.
That would be **chaos**.

So we design the system like a **well-planned city**.


## 🏙 Chapter 1: The City Entrance – Multi-Device Interfaces (5 minutes)

> Imagine a big city.

People enter the city from:

* Cars
* Bikes
* Buses
* Walking

In software, these are:

* Web apps
* Mobile apps
* Admin systems
* External APIs

Important rule:

> **All visitors must enter through ONE main gate.**

Clients should NOT know:

* Where services run
* How many services exist
* Which database is used

They only know:

> “There is one address to talk to.”

This is **good design**.


## 🚪 Chapter 2: The Main Gate – API Gateway (8 minutes)

> At the city entrance, we place a **smart gate**.

This gate:

* Checks identity (Authentication)
* Checks permission (Authorization)
* Controls traffic (Rate limiting)
* Sends people to the right department
* Logs everything

Let me be very clear:

🚫 API Gateway **does not**:

* Create orders
* Calculate prices
* Store data

✅ API Gateway **only**:

* Protects
* Routes
* Controls

👉 **API Gateway is like a security guard + traffic police.**

> If a traffic police starts driving buses, will traffic improve?

Exactly.
Same with API Gateway.


## 🧩 Chapter 3: Departments Inside the City – Microservices (10 minutes)

> Inside the city, we don’t have one giant office.

We have **specialized departments**.

Each department does **one job** and does it **well**.

### 🧾 Product Catalog Service

* Knows products
* Knows prices
* Knows categories

It doesn’t care about:

* Payments
* Shipments
* Customers

### 🛒 Shopping Cart Service

* Remembers selected items
* Temporary data
* Can be cleared anytime

It doesn’t place orders.

### 📦 Order Processing Service

* Creates orders
* Manages order status
* Coordinates with other services

This is the **brain of transactions**.


### 🚚 Shipment Service

* Handles delivery
* Tracks shipment
* Updates status

> Each microservice:

* Is a **separate Web API**
* Has **its own database**
* Can be deployed **independently**

👉 **Microservices are specialists, not multitaskers.**


## 🧱 Chapter 4: The Invisible Basement – Infrastructure (5 minutes)

> Below the city, underground, we have:

* Databases
* Cache
* Message queues
* Identity systems

Do customers see these?
No.

Do UI developers touch these?
No.

Only services use them.

And here’s the golden rule:

> **Infrastructure is replaceable.
> Business logic is not.**

Tomorrow:

* SQL can change
* Cache can change
* Message broker can change

But the system must still work.


## 🔄 Chapter 5: One Complete Flow – “Place Order” (7 minutes)

> Let’s walk through one real flow.

A user clicks **“Place Order”** on mobile 📱

1. Request goes to **API Gateway**
2. Gateway verifies token
3. Gateway forwards request to **Order Processing**
4. Order Processing:

   * Checks product prices
   * Reads cart items
5. Order is created
6. Shipment is initiated
7. Response goes back via Gateway

The user thinks:

> “Nice app. Very fast.”

But inside:

> Many small services did their job silently.

That is **good architecture**.

## 🎓 Mentor’s Golden Rules (5 minutes)

Write these on the board:

1. Clients never talk to services directly
2. Gateway never contains business logic
3. Each service owns its data
4. Services are independently deployable
5. Infrastructure is not business logic

Say this slowly:

> **If your Gateway becomes fat, your architecture becomes weak.**


## 🧠 One Line to Remember (Repeat Together)

Ask students to repeat:

> **Clients → Gateway → Services → Infrastructure**

No shortcuts.

> You don’t need fancy words to build good systems.

You need:

* Clear responsibility
* Discipline
* Simplicity

This architecture:

* Is used in real companies
* Scales naturally
* Is easy to understand
* Is easy to maintain

In the next session, we will:

* Write actual code
* Build API Gateway routing
* Run multiple services together

Until then, remember:

> **Good architecture is not complex.
> It is clear.**

# 🎓 Student Lab Assignment

## *Building a Microservices-based E-Commerce System with API Gateway*

## 🧠 Lab Theme

**“From One Entry Point to Many Smart Services”**

This lab helps students **experience** how real-world systems are built using:

* Multiple independent microservices
* A centralized API Gateway
* Clean responsibility boundaries


## 🎯 Learning Objectives

By the end of this lab, students will be able to:

* Understand **why API Gateway is needed**
* Create **independent ASP.NET Core Web APIs**
* Route requests using **API Gateway (YARP)**
* Apply **clean architecture discipline**
* Run multiple services together locally

## ⏱ Lab Duration

* **3 – 4 Hours**

## 🧩 System to Build

```
Client
  ↓
API Gateway
  ↓
Microservices
   ├─ ProductCatalog
   ├─ ShoppingCart
   ├─ OrderProcessing
   └─ Shipment
```

## 🛠 Prerequisites

Students must have:

* .NET SDK 9 or 10 installed
* Basic C# knowledge
* Understanding of REST & HTTP
* VS Code / Visual Studio
* Postman / Browser

## 🟢 TASK 1: Create the Solution Structure (30 mins)

### Instructions

1. Create a solution named:

```
ECommerceSolution
```

2. Inside it, create **5 Web API projects**:

* ECommerce.ApiGateway
* ProductCatalog.Api
* ShoppingCart.Api
* OrderProcessing.Api
* Shipment.Api

3. Add all projects to the solution.


### Expected Folder Structure

```
ECommerceSolution
│
├── src
│   ├── ApiGateway
│   │   └── ECommerce.ApiGateway
│   │
│   └── Services
│       ├── ProductCatalog
│       ├── ShoppingCart
│       ├── OrderProcessing
│       └── Shipment
```

## 🟢 TASK 2: Create Microservice APIs (45 mins)

Each microservice must:

* Be an independent ASP.NET Core Web API
* Have **one controller**
* Return dummy data (no DB required)


### 1️⃣ ProductCatalog Service

**Endpoint**

```
GET /api/products
```

**Response**

```json
[
  { "id": 1, "name": "Laptop", "price": 75000 },
  { "id": 2, "name": "Mobile", "price": 25000 }
]
```


### 2️⃣ ShoppingCart Service

**Endpoint**

```
POST /api/cart/add
```

**Response**

```
"Item added to cart"
```


### 3️⃣ OrderProcessing Service

**Endpoint**

```
POST /api/orders
```

**Response**

```
"Order placed successfully"
```

### 4️⃣ Shipment Service

**Endpoint**

```
POST /api/shipments
```

**Response**

```
"Shipment created"
```

## 🟢 TASK 3: Create API Gateway (YARP) (45 mins)

### Instructions

1. Add **YARP Reverse Proxy** package to API Gateway
2. Configure routing in `appsettings.json`
3. Map routes using `Program.cs`

### Gateway Routes (Mandatory)

| Public URL     | Target Service  |
| -------------- | --------------- |
| `/catalog/*`   | ProductCatalog  |
| `/cart/*`      | ShoppingCart    |
| `/orders/*`    | OrderProcessing |
| `/shipments/*` | Shipment        |

### Example Client Calls

```
GET  http://localhost:5000/catalog/api/products
POST http://localhost:5000/cart/api/cart/add
POST http://localhost:5000/orders/api/orders
POST http://localhost:5000/shipments/api/shipments
```

## 🟢 TASK 4: Run & Test the System (30 mins)

### Steps

1. Run all microservices on different ports
2. Run API Gateway on port `5000`
3. Call services **only via Gateway**
4. Verify responses

### Rule (Strict)

❌ Direct service access is **not allowed**
✅ All requests must pass through **API Gateway**

## 🟢 TASK 5: Architecture Validation (20 mins)

Students must answer:

1. Why should clients not call microservices directly?
2. Why should API Gateway not contain business logic?
3. What happens if ProductCatalog service is down?
4. Can ShoppingCart be deployed independently?

*(Written or oral evaluation)*


## 📦 Deliverables

Students must submit:

1. **Solution folder (ZIP)**
2. Screenshot of:

   * Running services
   * Gateway routing working
3. Short document answering Task 5 questions

## 🎯 Evaluation Criteria (Rubric)

| Criteria                     | Marks   |
| ---------------------------- | ------- |
| Correct project structure    | 20      |
| Independent microservices    | 20      |
| API Gateway routing          | 25      |
| Clean separation of concerns | 15      |
| Conceptual clarity (Q&A)     | 20      |
| **Total**                    | **100** |


## ⭐ Bonus Tasks (Optional)

(For advanced students)

* Add **logging middleware** in Gateway
* Add **rate limiting**
* Add **correlation ID**
* Convert to **Docker containers**

> “This lab is not about writing more code.
> It is about writing **clear code with clear responsibility**.
> If you understand this architecture,
> you understand how real software companies work.”


# ✅ Solution Reference (Answer Key)

## *Microservices + API Gateway (ASP.NET Core)*


## 1️⃣ Final Solution Structure (Expected)

```
ECommerceSolution
│
├── ECommerceSolution.sln
│
└── src
    ├── ApiGateway
    │   └── ECommerce.ApiGateway
    │       ├── Program.cs
    │       ├── appsettings.json
    │       └── ECommerce.ApiGateway.csproj
    │
    └── Services
        ├── ProductCatalog
        │   └── ProductCatalog.Api
        │       ├── Controllers
        │       │   └── ProductsController.cs
        │       └── Program.cs
        │
        ├── ShoppingCart
        │   └── ShoppingCart.Api
        │       ├── Controllers
        │       │   └── CartController.cs
        │       └── Program.cs
        │
        ├── OrderProcessing
        │   └── OrderProcessing.Api
        │       ├── Controllers
        │       │   └── OrdersController.cs
        │       └── Program.cs
        │
        └── Shipment
            └── Shipment.Api
                ├── Controllers
                │   └── ShipmentController.cs
                └── Program.cs
```

✔ Separate projects
✔ No project references between services
✔ API Gateway isolated

## 2️⃣ ProductCatalog Microservice (Reference)

### Port

```
http://localhost:5001
```

### Controller

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Laptop", Price = 75000 },
            new { Id = 2, Name = "Mobile", Price = 25000 }
        });
    }
}
```

✔ Stateless
✔ No DB
✔ Single responsibility


## 3️⃣ ShoppingCart Microservice (Reference)

### Port

```
http://localhost:5002
```

### Controller

```csharp
[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    [HttpPost("add")]
    public IActionResult Add(object item)
    {
        return Ok("Item added to cart");
    }
}
```

✔ Accepts POST
✔ Temporary behavior


## 4️⃣ OrderProcessing Microservice (Reference)

### Port

```
http://localhost:5003
```

### Controller

```csharp
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult PlaceOrder(object order)
    {
        return Ok("Order placed successfully");
    }
}
```

✔ No payment/shipment logic inside
✔ Represents order lifecycle

## 5️⃣ Shipment Microservice (Reference)

### Port

```
http://localhost:5004
```

### Controller

```csharp
[ApiController]
[Route("api/shipments")]
public class ShipmentController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateShipment(object shipment)
    {
        return Ok("Shipment created");
    }
}
```

✔ Independent service
✔ No coupling with OrderProcessing


## 6️⃣ API Gateway – YARP Configuration (Reference)

### Package Installed

```bash
dotnet add package Yarp.ReverseProxy
```

### `appsettings.json`

```json
{
  "ReverseProxy": {
    "Routes": {
      "catalog-route": {
        "ClusterId": "catalog-cluster",
        "Match": { "Path": "/catalog/{**catch-all}" }
      },
      "cart-route": {
        "ClusterId": "cart-cluster",
        "Match": { "Path": "/cart/{**catch-all}" }
      },
      "orders-route": {
        "ClusterId": "orders-cluster",
        "Match": { "Path": "/orders/{**catch-all}" }
      },
      "shipments-route": {
        "ClusterId": "shipments-cluster",
        "Match": { "Path": "/shipments/{**catch-all}" }
      }
    },

    "Clusters": {
      "catalog-cluster": {
        "Destinations": {
          "d1": { "Address": "http://localhost:5001/" }
        }
      },
      "cart-cluster": {
        "Destinations": {
          "d1": { "Address": "http://localhost:5002/" }
        }
      },
      "orders-cluster": {
        "Destinations": {
          "d1": { "Address": "http://localhost:5003/" }
        }
      },
      "shipments-cluster": {
        "Destinations": {
          "d1": { "Address": "http://localhost:5004/" }
        }
      }
    }
  }
}
```

✔ Routes = public URLs
✔ Clusters = services
✔ Destinations = instances


### `Program.cs` (Gateway)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();
```

✔ No controllers
✔ No business logic


## 7️⃣ Expected Client → Gateway Calls

| Client Call                | Actual Service  |
| -------------------------- | --------------- |
| `/catalog/api/products`    | ProductCatalog  |
| `/cart/api/cart/add`       | ShoppingCart    |
| `/orders/api/orders`       | OrderProcessing |
| `/shipments/api/shipments` | Shipment        |


## 8️⃣ How to Run (Correct Order)

```bash
dotnet run --project ProductCatalog.Api --urls=http://localhost:5001
dotnet run --project ShoppingCart.Api --urls=http://localhost:5002
dotnet run --project OrderProcessing.Api --urls=http://localhost:5003
dotnet run --project Shipment.Api --urls=http://localhost:5004
dotnet run --project ECommerce.ApiGateway --urls=http://localhost:5000
```

✔ Gateway must start **after services**


## 9️⃣ Architecture Validation – Expected Answers

### Q1: Why should clients not call microservices directly?

**Answer:**
To avoid tight coupling, expose a single entry point, enforce security, and hide internal service topology.


### Q2: Why should API Gateway not contain business logic?

**Answer:**
Because it becomes a bottleneck, violates separation of concerns, and makes scaling difficult.



### Q3: What happens if ProductCatalog is down?

**Answer:**
Only catalog-related requests fail; other services continue working.


### Q4: Can ShoppingCart be deployed independently?

**Answer:**
Yes. Microservices are independently deployable and scalable.


## 🔴 Common Mistakes (Mentor Checklist)

❌ Shared database
❌ Gateway calling database
❌ Gateway containing controllers for business logic
❌ Direct client → service calls
❌ Hardcoded service logic inside Gateway


## 🟢 What a Perfect Submission Looks Like

* Clean project separation
* Gateway only routes
* Services return expected responses
* No circular dependencies
* Clear understanding in Q&A


> “If a student understands **why** this structure exists,
> they are already thinking like a **software engineer**,
> not just a coder.”
