
### 🧓 The Mentor’s Code Kitchen – Cooking Software the Right Way

Welcome to my kitchen, young developers! 🍲
Here, we don’t just *write* code — we **cook** solutions with patterns that bring *flavor*, *structure*, and *reusability* to every .NET dish we prepare. Let me introduce you to **six secret ingredients** every great .NET chef should know…


### 🔹 1. **CQRS – Divide and Rule**

Long ago, in the city of Application Nagar, a single service was doing everything — reading data and writing changes. It soon got too heavy and confused.

So, we brought in a new strategy called **CQRS**:

> “Command Query Responsibility Segregation.”

👨‍🍳 **In the kitchen analogy**: Imagine one chef only prepares orders (writes/updates), and another just answers customer queries (reads). No more mix-ups!

💡 Use this pattern when:

* Reads and writes have different performance or structure needs.
* You want to **scale** reads independently from writes.

### 🔹 2. **Mediator Pattern – Let the Waiter Handle It**

Have you seen the chaos when chefs talk to each other directly? That’s what happens when services and controllers are tightly coupled.

We solved it using a **Mediator** — like a smart waiter — who takes orders and routes them to the right chef.

📦 In .NET, we use **MediatR** for this.

💡 Benefits:

* Keeps controllers clean.
* Centralizes business logic handling.
* Makes testing and maintenance easier.

### 🔹 3. **Dependency Injection – Ingredients from the Pantry**

Every dish needs ingredients. But instead of chefs fetching onions from the storage room themselves, what if we just *give* them exactly what they need?

That’s **Dependency Injection**!

🔧 In .NET Core, it’s built-in. You register your services and let the framework serve them as needed.

💡 Why it matters:

* Loose coupling.
* Easy testing (you can replace services with mock ones).
* Better organization.

### 🔹 4. **Repository + Unit of Work – The Head Chef’s Ledger**

Now imagine we have different sous-chefs managing meats, veggies, and spices. They record all changes on their own paper. But only when the head chef (Unit of Work) signs off, the final order is placed.

That’s what **Repository + Unit of Work** does.

🗃️ Repository = a focused data access manager
📓 Unit of Work = coordinates commits to the database

💡 Use when:

* You want to abstract and centralize data access logic.
* You’re handling **complex transactions** across multiple entities.

### 🔹 5. **Outbox Pattern – Reliable Parcel Service**

Say you want to send a confirmation SMS *after* the order is cooked, but you don’t want to risk the message failing and the order being lost.

We write the SMS into a **box** (Outbox) and deliver it *later* reliably.

📬 This pattern ensures **eventual consistency** in **distributed systems**.

💡 Ideal for:

* Microservices sending messages/events (via RabbitMQ, Kafka).
* Avoiding inconsistencies when database and message queues are involved.

### 🔹 6. **Domain Events – When Something Important Happens**

Let’s say the biryani is ready. Instead of telling everyone manually, the kitchen bell rings — and everyone who *cares* responds (server, cleaner, SMS bot, etc.).

That bell is a **Domain Event**.

💡 Use when:

* One action triggers multiple independent reactions.
* You want to **decouple internal modules** cleanly.

### 🧠 Final Mentor Advice

> “Patterns are not rules. They are *recipes*. Use them when the dish demands it — not just because you learned them.”

Start small. Build a simple app with MediatR and DI. Add a Repository, then CQRS, then Domain Events. You’ll know their real value not in theory, but in **action**.

Great! Let’s cook up a **mini project** that’s simple but powerful enough to **practice all 6 patterns** we discussed — just like a real-world scenario.

## A Mini eCommerce Checkout System

### 🎯 **Goal**:

Build a simple .NET Core Web API where customers can place an order. You’ll use MediatR, CQRS, Domain Events, Repository pattern, Outbox simulation, and Dependency Injection — all in one flow.

## 🏗️ Project Structure

### ✅ Key Features:

1. Place an order (write side)
2. Get order details (read side)
3. Raise domain event: `OrderPlaced`
4. Simulate sending confirmation via outbox
5. Apply clean architecture with repository, services, and handlers

## 📦 Folders Overview

```
/QuickKart
│
├── Controllers/
│   └── OrderController.cs
│
├── Commands/
│   └── PlaceOrderCommand.cs
│   └── PlaceOrderHandler.cs
│
├── Queries/
│   └── GetOrderByIdQuery.cs
│   └── GetOrderByIdHandler.cs
│
├── Domain/
│   ├── Entities/Order.cs
│   └── Events/OrderPlacedEvent.cs
│
├── Interfaces/
│   ├── IOrderRepository.cs
│   └── IUnitOfWork.cs
│
├── Infrastructure/
│   └── Repositories/OrderRepository.cs
│   └── OutboxSimulator.cs
│
├── Services/
│   └── OrderService.cs
│
├── Program.cs
└── Startup.cs (if using .NET 5 or earlier)
```

## 🔧 Step-by-Step Pattern Integration

### 1️⃣ **CQRS: Separate Read & Write**

* `PlaceOrderCommand` = write model
* `GetOrderByIdQuery` = read model
* Use MediatR to handle both

### 2️⃣ **Mediator (MediatR)**

Install:

```bash
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
```

In `Program.cs`:

```csharp
builder.Services.AddMediatR(typeof(Program));
```

Now dispatch commands/queries via `IMediator`.

### 3️⃣ **Dependency Injection**

All your repositories, services, and outbox logic are registered via:

```csharp
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOutboxSimulator, OutboxSimulator>();
```

### 4️⃣ **Repository + Unit of Work**

```csharp
public interface IOrderRepository {
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
}

public interface IUnitOfWork {
    Task CommitAsync();
}
```

### 5️⃣ **Domain Events**

```csharp
public class OrderPlacedEvent : INotification {
    public Guid OrderId { get; set; }
}
```

Raise event in handler after saving order:

```csharp
await _mediator.Publish(new OrderPlacedEvent { OrderId = order.Id });
```

### 6️⃣ **Outbox Pattern (Simulated)**

```csharp
public class OutboxSimulator : INotificationHandler<OrderPlacedEvent> {
    public Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken) {
        Console.WriteLine($"📨 Sending order confirmation for {notification.OrderId}");
        return Task.CompletedTask;
    }
}
```

## 🎮 Sample Flow

1. `POST /api/orders`
   → Mediator runs `PlaceOrderCommandHandler`
   → Order is saved
   → `OrderPlacedEvent` is published
   → Outbox handler logs/send simulated message

2. `GET /api/orders/{id}`
   → Query is routed to handler
   → Data is returned from repo

## 📌 Extra Tasks (Optional)

* Add `OrderStatus` enum: Pending, Processed, Shipped
* Simulate DB context with `InMemoryDb`
* Add unit tests with Moq
* Add Swagger for API testing

## 🧑‍🍳 Mentor's Final Tip

> "You won’t become a chef by just reading recipes. You’ve got to enter the kitchen and mess up a few dishes."

Let’s walk through the **step-by-step guide to build `QuickKart`** live in class — *mentor style*, breaking each part down clearly. We'll keep this fun, hands-on, and pattern-rich 💡

## 🍽️ **Mentor's Live Coding Class Plan: "QuickKart" – Build Patterns the Right Way**

### 🧩 Tech Stack

* .NET 8 Web API
* MediatR
* Clean code with folders
* In-memory storage (for now)
* Simulated Outbox with Console
* Swagger for testing


## ✅ **Step 1: Project Setup**

🗣️ *“Let’s enter our software kitchen...”*

```bash
dotnet new webapi -n QuickKart
cd QuickKart
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add package Swashbuckle.AspNetCore
```

➡️ Clean up:

* Delete `WeatherForecast.cs` and its controller

## ✅ **Step 2: Folder Structure**

Inside the `QuickKart` project, create:

```
/Commands
/Queries
/Domain/Entities
/Domain/Events
/Interfaces
/Infrastructure/Repositories
/Infrastructure/Services
/Controllers
```

💬 *“Like sections in a restaurant – prep, cooking, serving, cleaning.”*

## ✅ **Step 3: Create the `Order` Entity**

📁 `Domain/Entities/Order.cs`

```csharp
public class Order {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CustomerName { get; set; } = string.Empty;
    public List<string> Items { get; set; } = new();
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
}
```

## ✅ **Step 4: Interfaces for Repository + UoW**

📁 `Interfaces/IOrderRepository.cs`

```csharp
public interface IOrderRepository {
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
}
```

📁 `Interfaces/IUnitOfWork.cs`

```csharp
public interface IUnitOfWork {
    Task CommitAsync();
}
```

## ✅ **Step 5: Create In-Memory Repositories**

📁 `Infrastructure/Repositories/OrderRepository.cs`

```csharp
public class OrderRepository : IOrderRepository {
    private readonly List<Order> _orders = new();

    public Task AddAsync(Order order) {
        _orders.Add(order);
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(Guid id) {
        return Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
    }
}
```

📁 `Infrastructure/Repositories/UnitOfWork.cs`

```csharp
public class UnitOfWork : IUnitOfWork {
    public Task CommitAsync() => Task.CompletedTask;
}
```

## ✅ **Step 6: Create Command + Handler**

📁 `Commands/PlaceOrderCommand.cs`

```csharp
public class PlaceOrderCommand : IRequest<Guid> {
    public string CustomerName { get; set; } = string.Empty;
    public List<string> Items { get; set; } = new();
}
```

📁 `Commands/PlaceOrderHandler.cs`

```csharp
public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Guid> {
    private readonly IOrderRepository _repo;
    private readonly IUnitOfWork _unit;
    private readonly IMediator _mediator;

    public PlaceOrderHandler(IOrderRepository repo, IUnitOfWork unit, IMediator mediator) {
        _repo = repo;
        _unit = unit;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken) {
        var order = new Order {
            CustomerName = request.CustomerName,
            Items = request.Items
        };

        await _repo.AddAsync(order);
        await _unit.CommitAsync();

        await _mediator.Publish(new OrderPlacedEvent { OrderId = order.Id });

        return order.Id;
    }
}
```

## ✅ **Step 7: Domain Event + Outbox Simulation**

📁 `Domain/Events/OrderPlacedEvent.cs`

```csharp
public class OrderPlacedEvent : INotification {
    public Guid OrderId { get; set; }
}
```

📁 `Infrastructure/Services/OutboxSimulator.cs`

```csharp
public class OutboxSimulator : INotificationHandler<OrderPlacedEvent> {
    public Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken) {
        Console.WriteLine($"📬 [OUTBOX] Order Confirmation Sent for Order ID: {notification.OrderId}");
        return Task.CompletedTask;
    }
}
```

## ✅ **Step 8: Create Query + Handler**

📁 `Queries/GetOrderByIdQuery.cs`

```csharp
public class GetOrderByIdQuery : IRequest<Order?> {
    public Guid OrderId { get; set; }
}
```

📁 `Queries/GetOrderByIdHandler.cs`

```csharp
public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order?> {
    private readonly IOrderRepository _repo;

    public GetOrderByIdHandler(IOrderRepository repo) {
        _repo = repo;
    }

    public Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) {
        return _repo.GetByIdAsync(request.OrderId);
    }
}
```

## ✅ **Step 9: Controller for API Endpoints**

📁 `Controllers/OrderController.cs`

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase {
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command) {
        var id = await _mediator.Send(command);
        return Ok(new { OrderId = id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id) {
        var order = await _mediator.Send(new GetOrderByIdQuery { OrderId = id });
        if (order == null) return NotFound();
        return Ok(order);
    }
}
```

## ✅ **Step 10: Program.cs Setup**

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<INotificationHandler<OrderPlacedEvent>, OutboxSimulator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();
```

## ✅ Step 11: Run & Test in Swagger

* Start the app: `dotnet run`
* Open Swagger: [https://localhost](https://localhost):<port>/swagger
* Test:

  * `POST /api/order` → pass `CustomerName`, `Items[]`
  * `GET /api/order/{id}` → fetch order

