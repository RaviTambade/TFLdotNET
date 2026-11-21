# **RabbitMQ — Products & Orders**

> **"A microservice should not depend on another microservice being available all the time. It should communicate, collaborate, and continue working independently."**

Today, we will build a practical microservices application using **ASP.NET Core Web API, RabbitMQ, and Product & Order services**.

We will learn step by step, from the problem statement to implementation, message publishing, message consumption, and eventually distributed system behavior.

## **1. Understand the business problem first**

Imagine we are building an e-commerce application.

A customer visits the website, selects a product, and places an order.

We have two independently developed microservices:

- **Products Service** — manages products, pricing, inventory.
- **Orders Service** — manages customer orders and order lifecycle.

### **Traditional approach: Synchronous communication**

When the customer places an order, Orders API directly calls Products API.

For example:

```
Customer
   |
   | Place Order
   v
Orders API
   |
   | HTTP Request
   v
Products API
   |
   | Product Details
   v
Orders API
   |
   | Save Order
   v
Database
```

### **What is the problem?**

Suppose the Products API is down.

```
Products API ❌

Orders API
    |
    | Waiting for Products API
    |
    | Timeout
    |
    ❌ Order creation fails
```

Even if the Orders service is healthy, it cannot perform its work because another service is unavailable.

This is called **tight runtime coupling**.

## **2. Introduce RabbitMQ**

Instead of Orders API directly depending on Products API for every operation, we introduce a message broker.

RabbitMQ acts like a communication post office.

Imagine a post office:

| Real-world concept      | RabbitMQ concept |
| ----------------------- | ---------------- |
| Person sending letter   | Producer         |
| Post office             | Message Broker   |
| Sorting center          | Exchange         |
| Mailbox                 | Queue            |
| Address                 | Routing Key      |
| Person receiving letter | Consumer         |

The sender does not need to know where the receiver is currently working.

It simply sends a message.

# **3. Our application architecture**

We will implement the following architecture.

```
                   ┌─────────────────┐
                   │     Client      │
                   │ React / Postman │
                   └────────┬────────┘
                            │
                            │ HTTP POST
                            ▼
                   ┌─────────────────┐
                   │   Orders API    │
                   │  ASP.NET Core   │
                   └────────┬────────┘
                            │
                            │ Publish Message
                            ▼
                   ┌─────────────────┐
                   │    RabbitMQ     │
                   │  Message Broker │
                   └────────┬────────┘
                            │
                            │ Consume Message
                            ▼
                   ┌─────────────────┐
                   │ Products Worker │
                   │ / Products API  │
                   └────────┬────────┘
                            │
                            ▼
                   ┌─────────────────┐
                   │ Products DB     │
                   │ Inventory       │
                   └─────────────────┘
```

Our first workflow:

```
Customer places order
        |
        v
Orders API
        |
        | OrderCreated event
        v
RabbitMQ
        |
        v
Products Service
        |
        | Reserve product stock
        v
Products Database
```

The important design principle:

**Orders Service publishes an event. Products Service reacts to that event.**

Products Service does not need to know who created the order.

# **4. Define our microservices**

We will create three independent projects.

```
ECommerceSolution
│
├── Products.API
│     ├── Controllers
│     ├── Services
│     ├── Models
│     └── Database
│
├── Orders.API
│     ├── Controllers
│     ├── Services
│     ├── Models
│     └── Database
│
└── Messaging.Contracts
      └── Events
```

### **Responsibilities**

| Service             | Responsibility                     |
| ------------------- | ---------------------------------- |
| Products.API        | Product catalog and inventory      |
| Orders.API          | Order creation and order lifecycle |
| RabbitMQ            | Message routing and delivery       |
| Messaging.Contracts | Shared event contracts             |

We will avoid sharing business logic between services. A shared project should contain only message contracts and other carefully selected shared abstractions.

# **5. Step 1 — Install RabbitMQ**

RabbitMQ requires Erlang, but the easiest development setup is Docker.

Run the following command:

```
docker run -d \
  --name rabbitmq \
  -p 5672:5672 \
  -p 15672:15672 \
  rabbitmq:4-management
```

For Windows PowerShell, run it on one line:

```
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management
```

### **Ports**

| Port  | Purpose                |
| ----- | ---------------------- |
| 5672  | Application messaging  |
| 15672 | RabbitMQ Management UI |

Open the management dashboard:

[http://localhost:15672](http://localhost:15672?utm_source=chatgpt.com)

Default development credentials:

```
Username: guest
Password: guest
```

Verify the container:

```
docker ps
```

You should see RabbitMQ running.

# **6. Step 2 — Create ASP.NET Core projects**

Open terminal.

```
mkdir ECommerceSolution
cd ECommerceSolution
```

Create Products API:

```
dotnet new webapi -n Products.API
```

Create Orders API:

```
dotnet new webapi -n Orders.API
```

Create shared contracts:

```
dotnet new classlib -n Messaging.Contracts
```

Add projects to solution:

```
dotnet new sln -n ECommerceSolution

dotnet sln add Products.API
dotnet sln add Orders.API
dotnet sln add Messaging.Contracts
```

Add contract references:

```
dotnet add Orders.API reference Messaging.Contracts
dotnet add Products.API reference Messaging.Contracts
```

Our solution is ready.

# **7. Step 3 — Create the Message Contract**

Before implementing RabbitMQ, let's decide what message our microservices will exchange.

When an order is placed, Orders Service should publish an event.

We call this event:

```
OrderCreated
```

Create this file:

`Messaging.Contracts/Events/OrderCreatedEvent.cs`

```
namespace Messaging.Contracts.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }
}
```

Example event data:

```
{
  "orderId": 101,
  "productId": 1,
  "quantity": 2,
  "createdAt": "2026-11-21T12:00:00"
}
```

### **Mentor explanation**

This is not an HTTP request.

This is a **business event**.

It communicates something that has already happened:

> An order has been created.

This distinction is important.

| Command       | Event                               |
| ------------- | ----------------------------------- |
| CreateOrder   | A request to perform something      |
| OrderCreated  | Something has successfully happened |
| ReserveStock  | A request to reserve inventory      |
| StockReserved | Inventory reservation succeeded     |

Our first message is `OrderCreated`.

# **8. Step 4 — Configure RabbitMQ connection**

Install RabbitMQ client package in both projects.

```
cd Orders.API
dotnet add package RabbitMQ.Client --version 6.8.1
```

And:

```
cd Products.API
dotnet add package RabbitMQ.Client --version 6.8.1
```

For this lab, we use RabbitMQ.Client 6.x to make the connection, channel, exchange and queue concepts easier to understand.

Create configuration in both projects.

`appsettings.json`

```
{
  "RabbitMQ": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest"
  }
}
```

# **9. Step 5 — Understand Exchange and Queue**

Before writing code, understand this RabbitMQ architecture.

```
                 Orders.API
                     |
                     |
              Publish Event
                     |
                     v
            ┌────────────────┐
            │    Exchange    │
            │  order.events  │
            └───────┬────────┘
                    |
             Routing Key
              order.created
                    |
                    v
            ┌────────────────┐
            │     Queue      │
            │ product.order  │
            │    created     │
            └───────┬────────┘
                    |
                    v
             Products Worker
```

We will use:

- Exchange: `order.events`
- Exchange type: `topic`
- Routing key: `order.created`
- Queue: `product.order.created`

The topic exchange provides flexibility when we later add consumers.

For example:

```
order.created
     |
     ├── Inventory Service
     ├── Notification Service
     ├── Billing Service
     └── Analytics Service
```

One event can trigger multiple independent business workflows.

# **10. Step 6 — Implement Message Publisher**

In Orders.API, create:

```
Services
    RabbitMQPublisher.cs
```

The publisher is responsible for:

1. Connecting to RabbitMQ.
2. Creating a channel.
3. Declaring the exchange.
4. Serializing the message.
5. Publishing the message.

### **RabbitMQPublisher.cs**

```
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Orders.API.Services;

public class RabbitMQPublisher
{
    private readonly IConfiguration _configuration;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Publish<T>(T message, string routingKey)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:HostName"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]!),
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"]
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(
            exchange: "order.events",
            type: ExchangeType.Topic,
            durable: true);

        var json = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(json);

        var properties = channel.CreateBasicProperties();

        properties.Persistent = true;

        channel.BasicPublish(
            exchange: "order.events",
            routingKey: routingKey,
            basicProperties: properties,
            body: body);

        Console.WriteLine($"Message Published: {json}");
    }
}
```

### **What happens inside&#x20;**`Publish()`**?**

```
OrderCreatedEvent
        |
        v
JsonSerializer
        |
        v
JSON String
        |
        v
UTF8 Bytes
        |
        v
RabbitMQ Channel
        |
        v
Exchange
```

For example:

```
publisher.Publish(
    orderCreatedEvent,
    "order.created");
```

The publisher does not need to know:

- Where Products API is running.
- Which machine hosts the consumer.
- Whether the consumer is currently processing another request.
- How the consumer implements inventory logic.

It only needs to know the messaging contract and routing key.

# **11. Step 7 — Implement Orders API**

Let's create a simple Order model.

`Models/Order.cs`

```
namespace Orders.API.Models;

public class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = "Created";
}
```

For our first exercise, we will use an in-memory collection instead of a database.

> Later, we will replace this with MySQL or SQL Server and introduce proper transaction handling.

Create an order request:

`Models/CreateOrderRequest.cs`

```
namespace Orders.API.Models;

public class CreateOrderRequest
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
```

# **12. Step 8 — Create Orders Controller**

Create:

`Controllers/OrdersController.cs`

```
using Microsoft.AspNetCore.Mvc;
using Orders.API.Models;
using Orders.API.Services;
using Messaging.Contracts.Events;

namespace Orders.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> orders = new();

    private readonly RabbitMQPublisher _publisher;

    public OrdersController(RabbitMQPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public IActionResult CreateOrder(CreateOrderRequest request)
    {
        var order = new Order
        {
            Id = orders.Count + 1,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Status = "Created"
        };

        orders.Add(order);

        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        _publisher.Publish(
            orderCreatedEvent,
            "order.created");

        return Ok(new
        {
            Message = "Order created successfully",
            Order = order
        });
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(orders);
    }
}
```

### **Mentor's observation**

Look carefully at this code:

```
var order = new Order
{
    Id = orders.Count + 1,
    ProductId = request.ProductId,
    Quantity = request.Quantity
};

orders.Add(order);
```

Orders API creates its own order.

Then:

```
_publisher.Publish(
    orderCreatedEvent,
    "order.created");
```

It publishes a notification about the order.

**The Orders service does not directly call Products API.**

This is the first step toward loose coupling.

# **13. Step 9 — Register Publisher in Dependency Injection**

Open:

`Orders.API/Program.cs`

Add:

```
using Orders.API.Services;
```

Register the service:

```
builder.Services.AddSingleton<RabbitMQPublisher>();
```

A simplified `Program.cs`:

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RabbitMQPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
```

Now the framework can inject `RabbitMQPublisher` into the controller.

```
OrdersController
       |
       | Dependency Injection
       v
RabbitMQPublisher
       |
       v
RabbitMQ
```

# **14. Step 10 — Create Products Consumer**

Now comes the interesting part.

Orders API is a **Producer**.

Products Service is a **Consumer**.

When Products Service starts, it should listen continuously for messages.

We will implement this using:

```
BackgroundService
```

Why?

Because consuming messages is not an HTTP request.

It is a background process.

```
Products.API starts
       |
       v
Background Service starts
       |
       v
Connect to RabbitMQ
       |
       v
Listen to Queue
       |
       v
Message arrives
       |
       v
Process message
```

Create:

`Products.API/Services/OrderConsumer.cs`

```
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Messaging.Contracts.Events;
using System.Text;
using System.Text.Json;

namespace Products.API.Services;

public class OrderConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;

    public OrderConsumer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:HostName"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]!),
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"]
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(
            exchange: "order.events",
            type: ExchangeType.Topic,
            durable: true);

        channel.QueueDeclare(
            queue: "product.order.created",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.QueueBind(
            queue: "product.order.created",
            exchange: "order.events",
            routingKey: "order.created");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, args) =>
        {
            var body = args.Body.ToArray();

            var json = Encoding.UTF8.GetString(body);

            var orderEvent =
                JsonSerializer.Deserialize<OrderCreatedEvent>(json);

            if (orderEvent != null)
            {
                ProcessOrder(orderEvent);
            }

            channel.BasicAck(
                deliveryTag: args.DeliveryTag,
                multiple: false);
        };

        channel.BasicConsume(
            queue: "product.order.created",
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }

    private void ProcessOrder(OrderCreatedEvent order)
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Order Received by Products Service");
        Console.WriteLine($"Order ID: {order.OrderId}");
        Console.WriteLine($"Product ID: {order.ProductId}");
        Console.WriteLine($"Quantity: {order.Quantity}");
        Console.WriteLine("Processing Inventory...");
        Console.WriteLine("--------------------------------");
    }
}
```

## **15. Understand the Consumer**

Let's understand the important parts.

### **A. Declare Exchange**

```
channel.ExchangeDeclare(
    exchange: "order.events",
    type: ExchangeType.Topic,
    durable: true);
```

This creates the exchange if it doesn't already exist.

The exchange is responsible for routing messages.

### **B. Declare Queue**

```
channel.QueueDeclare(
    queue: "product.order.created",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);
```

This creates a queue where the Products service receives messages.

```
product.order.created
---------------------
Message 1
Message 2
Message 3
Message 4
```

A durable queue survives broker restart, provided the broker's data is persisted. Durable queues alone don't guarantee that every message survives a restart.

### **C. Bind Queue to Exchange**

```
channel.QueueBind(
    queue: "product.order.created",
    exchange: "order.events",
    routingKey: "order.created");
```

This tells RabbitMQ:

> When a message arrives at the `order.events` exchange with routing key `order.created`, route it to the `product.order.created` queue.

### **D. Receive the message**

```
consumer.Received += (sender, args) =>
{
    // Process message
};
```

This event handler is called when a message is delivered to the consumer.

### **E. Deserialize the message**

```
var json = Encoding.UTF8.GetString(body);

var orderEvent =
    JsonSerializer.Deserialize<OrderCreatedEvent>(json);
```

RabbitMQ transports bytes.

Our application converts the bytes back into a business event.

```
RabbitMQ
   |
   | byte[]
   v
UTF-8 JSON
   |
   v
OrderCreatedEvent
```

### **F. Acknowledge message**

```
channel.BasicAck(
    deliveryTag: args.DeliveryTag,
    multiple: false);
```

This acknowledgement tells RabbitMQ:

> I have successfully processed this message. You can remove it from the queue.

Why are we using:

```
autoAck: false
```

Because we want manual acknowledgement.

If the consumer crashes before acknowledging, RabbitMQ can redeliver the message.

This gives us the possibility of reliable delivery—but **it also means duplicate processing is possible**.

Therefore, consumers should be designed to be idempotent.

# **16. Step 11 — Register Products Consumer**

Open:

`Products.API/Program.cs`

Register our background service.

```
using Products.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<OrderConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
```

This line is the key:

```
builder.Services.AddHostedService<OrderConsumer>();
```

ASP.NET Core starts the background consumer when the application starts.

Our Products API is now doing two jobs:

```
Products.API
│
├── HTTP Endpoints
│      │
│      └── Product Catalog
│
└── Background Worker
       │
       └── Order Event Consumer
```

# **17. Step 12 — Run the application**

We need three processes running.

### **Terminal 1 — RabbitMQ**

```
docker start rabbitmq
```

### **Terminal 2 — Orders API**

```
cd Orders.API

dotnet run
```

### **Terminal 3 — Products API**

```
cd Products.API

dotnet run
```

Now all three components are running.

```
RabbitMQ        🟢
Orders.API      🟢
Products.API    🟢
```

# **18. Step 13 — Test with Postman**

Open Orders API Swagger.

Suppose the application runs at:

```
https://localhost:7001
```

Send:

```
POST /api/orders
```

Request body:

```
{
  "productId": 101,
  "quantity": 2
}
```

Orders API response:

```
{
  "message": "Order created successfully",
  "order": {
    "id": 1,
    "productId": 101,
    "quantity": 2,
    "status": "Created"
  }
}
```

Orders API console:

```
Message Published:
{
  "OrderId": 1,
  "ProductId": 101,
  "Quantity": 2,
  "CreatedAt": "2026-11-21T..."
}
```

Products API console:

```
--------------------------------
Order Received by Products Service

Order ID: 1
Product ID: 101
Quantity: 2

Processing Inventory...
--------------------------------
```

Congratulations!

You have implemented your first asynchronous communication between two WebAPI microservices.

# **19. Observe the message flow**

```
HTTP Request
     |
     v
Orders API
     |
     v
RabbitMQ
     |
     v
HTTP Response ──────────> Client

                    (Later)

RabbitMQ
     |
     v
Products Consumer
     |
     v
Inventory Processing
```

This is **asynchronous communication**.

# **20. What happens if Products API is down?**

Let's conduct an experiment.

### **Experiment**

1. Stop Products API.
2. Keep RabbitMQ running.
3. Send 10 order requests.
4. Observe RabbitMQ queue.
5. Restart Products API.

When Products API is down:

```
Orders.API       🟢
RabbitMQ         🟢
Products.API     🔴
```

Orders API publishes messages.

RabbitMQ stores them in the queue.

```
product.order.created
┌─────────────────────────┐
│ Order 101               │
│ Order 102               │
│ Order 103               │
│ Order 104               │
│ Order 105               │
└─────────────────────────┘
```

When Products API restarts:

```
Products.API
     |
     v
Connect to RabbitMQ
     |
     v
Consume pending messages
     |
     v
Process orders
```

This is one of the key advantages of message brokers.

**The producer and consumer don't have to be available at the same moment.**

# **21. Step 14 — Improve the Consumer Lifecycle**

Mentor observation: our earlier consumer code returns `Task.CompletedTask` from `ExecuteAsync`. We should keep the hosted service alive and clean up RabbitMQ resources correctly.

Replace the ending of `ExecuteAsync` with:

```
channel.BasicConsume(
    queue: "product.order.created",
    autoAck: false,
    consumer: consumer);

// Keep the BackgroundService alive
await Task.Delay(
    Timeout.Infinite,
    stoppingToken);
```

For a more complete implementation, manage the connection and channel lifetime using `try/finally`, handle cancellation, and close resources during application shutdown.

In production, we should additionally implement:

- Connection recovery
- Consumer recovery
- Retry policies
- Dead-letter queues
- Message idempotency
- Structured logging
- Graceful shutdown

This is where a classroom implementation becomes production engineering.

# **22. Step 15 — Implement actual inventory processing**

So far, Products Service only prints the received event.

Let's simulate inventory.

Suppose Products Service maintains:

```
ProductId     ProductName       Stock
-------------------------------------
101           Laptop            25
102           Mouse             50
103           Keyboard          40
```

When an order event arrives:

```
{
  "orderId": 1001,
  "productId": 101,
  "quantity": 2
}
```

The consumer performs:

```
Receive OrderCreated
        |
        v
Find Product
        |
        v
Check Inventory
        |
        v
Stock >= Requested Quantity?
       / \
     Yes  No
      |    |
      v    v
 Reserve  Reject
 Stock    Request
      |
      v
Publish StockReserved
```

A simplified inventory method:

```
private void ProcessOrder(OrderCreatedEvent order)
{
    Console.WriteLine(
        $"Processing Order {order.OrderId}");

    // Simulated inventory
    int availableStock = 25;

    if (availableStock >= order.Quantity)
    {
        Console.WriteLine("Inventory available");

        Console.WriteLine(
            $"Reserving {order.Quantity} units");

        Console.WriteLine("Stock Reserved Successfully");
    }
    else
    {
        Console.WriteLine("Insufficient inventory");
    }
}
```

However, in a real application, inventory must be persisted in the Products database and stock deduction must be atomic.

# **23. The next level: Order lifecycle**

Now our architecture should evolve from a simple event to a complete workflow.

Instead of saying:

> Order Created = Order Completed

we introduce states.

```
Order Created
     |
     v
Inventory Reservation Requested
     |
     v
Inventory Service
     |
     ├── Stock Available
     │        |
     │        v
     │   Stock Reserved
     │        |
     │        v
     │   Order Confirmed
     │
     └── Stock Unavailable
              |
              v
        Order Rejected
```

The next events might be:

```
OrderCreated
StockReserved
StockReservationFailed
OrderConfirmed
OrderRejected
PaymentRequested
PaymentCompleted
PaymentFailed
```

# **24. Important engineering lessons**

| Concept         | What we learned                                                     |
| --------------- | ------------------------------------------------------------------- |
| Producer        | Sends messages                                                      |
| Consumer        | Receives and processes messages                                     |
| Exchange        | Routes messages                                                     |
| Queue           | Stores messages                                                     |
| Routing Key     | Determines routing                                                  |
| Event           | Represents something that happened                                  |
| Acknowledgement | Confirms successful message processing                              |
| Durability      | Helps preserve broker structures across restarts                    |
| Asynchronous    | Producer doesn't wait for consumer completion                       |
| Loose coupling  | Services don't directly depend on each other's runtime availability |
| Idempotency     | Repeated events should not create repeated business effects         |

## **Don't make this mistake**

RabbitMQ does **not** automatically make our system reliable.

A production application must consider:

```
Message Delivery
      +
Database Transactions
      +
Idempotency
      +
Retry
      +
Dead Letter Queue
      +
Monitoring
      =
Reliable Messaging Design
```

For example, what if Products Service deducts stock successfully but crashes before acknowledging the message?

RabbitMQ may redeliver the event.

If we deduct stock twice, we have a serious business bug.

We should therefore store processed event IDs and make processing idempotent.

# **25. Classroom assignment**

## **Lab: E-Commerce Microservices with RabbitMQ**

### **Phase 1 — Foundation**

- Create Products.API
- Create Orders.API
- Run RabbitMQ in Docker
- Create OrderCreatedEvent
- Implement Publisher
- Implement Consumer
- Verify message delivery

### **Phase 2 — Business logic**

- Create Product catalog
- Add inventory quantity
- Create order
- Reserve inventory
- Publish StockReserved
- Publish StockReservationFailed
- Update order status

### **Phase 3 — Reliability**

- Add persistent messages
- Add durable queues
- Add manual acknowledgements
- Add retry mechanism
- Add dead-letter queue
- Add duplicate event detection
- Add structured logging

### **Phase 4 — Persistence**

- Orders database
- Products database
- Order repository
- Product repository
- Transaction handling
- Outbox pattern

## **Transflower Mentor's closing thought**

A beginner writes two APIs and makes them communicate.

An engineer asks:

- What happens if the receiver is down?
- What happens if the message arrives twice?
- What happens if processing fails?
- What happens if the database succeeds but the acknowledgement fails?
- How do we trace one order across multiple services?
- How do we recover without losing business data?

**The real skill in microservices is not creating more services. It is designing how those services survive failure and collaborate reliably.**
