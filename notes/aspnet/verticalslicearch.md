# Vertical Slice Architecture (VSA) 

Imagine you are building an e-commerce application in .NET.

A customer wants to place an order. Another customer wants to cancel an order. A third customer wants to view order details. The traditional approach organizes code by technical layers. Vertical Slice Architecture organizes code by business capabilities. Let's understand this from first principles.

## 1. Traditional Layered Architecture vs. Vertical Slice Architecture
  
```
       TRADITIONAL LAYERED ARCHITECTURE
       =================================

              Organize by
          TECHNICAL RESPONSIBILITY

       +---------------------------+
       |        Controllers        |
       +---------------------------+
                    |
                    v
       +---------------------------+
       |          Services         |
       +---------------------------+
                    |
                    v
       +---------------------------+
       |        Repositories       |
       +---------------------------+
                    |
                    v
       +---------------------------+
       |          Database         |
       +---------------------------+


          VERTICAL SLICE ARCHITECTURE
          ===========================

              Organize by
            BUSINESS USE CASE

  +------------------+  +------------------+  +------------------+
  |   CREATE ORDER   |  |   CANCEL ORDER   |  |    GET ORDER     |
  +------------------+  +------------------+  +------------------+
  | Endpoint         |  | Endpoint         |  | Endpoint         |
  | Business Logic   |  | Business Logic   |  | Business Logic   |
  | Data Access      |  | Data Access      |  | Data Access      |
  +--------+---------+  +--------+---------+  +--------+---------+
           |                     |                     |
           v                     v                     v
  +------------------+  +------------------+  +------------------+
  | Order Persistence|  | Order Persistence|  | Order Query      |
  | / Database       |  | / Database       |  | / Database       |
  +------------------+  +------------------+  +------------------+


  KEY DIFFERENCE
  ==============

  Layered Architecture  -> Organize by HOW the code works.
  Vertical Slice        -> Organize by WHAT the business does.
  
```

Neither approach eliminates the need for layers or good separation of concerns. The difference is how you organize and evolve the code.

## 2.  How a vertical slice works

Consider the `POST /api/orders` endpoint in an ASP.NET Core application.

```
                  CUSTOMER
                     |
                     v
             HTTP POST /api/orders
                     |
                     v
        +---------------------------+
        | CreateOrder Endpoint      |
        |---------------------------|
        | Bind request              |
        | Validate input            |
        | Check authorization       |
        +-------------+-------------+
                      |
                      v
        +---------------------------+
        | CreateOrder Handler       |
        |---------------------------|
        | Apply business rules      |
        | Calculate order total     |
        | Create order              |
        +-------------+-------------+
                      |
                      v
        +---------------------------+
        | Data Access               |
        |---------------------------|
        | Save order                |
        | Use EF Core / SQL         |
        +-------------+-------------+
                      |
                      v
                 DATABASE
                      |
                      v
        +---------------------------+
        | HTTP 201 Created          |
        | Order ID + Response       |
        +---------------------------+
```

All the code needed for this particular use case can live together in a `CreateOrder` feature folder. Some infrastructure, such as database configuration, authentication, logging, and shared domain rules, may still be shared.

## 3. What does the .NET project structure look like?

A conventional layered structure might look like this:

```
ECommerce/
├── Controllers/
│   └── OrdersController.cs
├── Services/
│   └── OrderService.cs
├── Repositories/
│   └── OrderRepository.cs
├── DTOs/
│   ├── CreateOrderRequest.cs
│   └── OrderResponse.cs
└── Models/
    └── Order.cs
```

With Vertical Slice Architecture, the structure could look like this:

```
ECommerce/
├── Features/
│   └── Orders/
│       ├── CreateOrder/
│       │   ├── Endpoint.cs
│       │   ├── Command.cs
│       │   ├── Handler.cs
│       │   ├── Validator.cs
│       │   └── Response.cs
│       │
│       ├── CancelOrder/
│       │   ├── Endpoint.cs
│       │   ├── Command.cs
│       │   └── Handler.cs
│       │
│       └── GetOrder/
│           ├── Endpoint.cs
│           ├── Query.cs
│           ├── Handler.cs
│           └── Response.cs
│
├── Domain/
│   └── Order.cs
│
├── Infrastructure/
│   ├── AppDbContext.cs
│   └── Authentication/
│
└── Program.cs
```

This is one possible structure, not a mandatory template. Small slices may need only one or two files; not every feature needs its own validator, repository, or command class.

## 4. A small C# example

Here is a simplified vertical slice using a Minimal API endpoint and a handler.

```
public record CreateOrderRequest(string ProductName,int Quantity,decimal UnitPrice);

public record CreateOrderResponse(int OrderId,decimal Total);

public static class CreateOrder
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (CreateOrderRequest request,AppDbContext db,CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(request.ProductName) || request.Quantity <= 0 || request.UnitPrice < 0)
            {
                return Results.BadRequest("Invalid order details.");
            }

            var order = new Order
            {
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync(ct);

            var response = new CreateOrderResponse(order.Id,order.Quantity * order.UnitPrice);

            return Results.Created($"/api/orders/{order.Id}", response);
        });
    }
}
```

Register the endpoint in `Program.cs`:

```
CreateOrder.MapEndpoint(app);
```

This example keeps the request handling, validation, order creation, persistence, and response close to the `CreateOrder` use case. For a larger application, move complex business rules into a handler or domain model and add suitable authorization, centralized error handling, and stronger validation.

## 5. Why is it useful?

- Reduced coupling: changing order creation does not automatically require changing order retrieval.
- Easier maintenance: developers can locate a use case without navigating multiple feature-unaware folders.
- Pragmatic abstractions: introduce interfaces and shared services where they solve a real problem, rather than creating one for every operation.
- Independent evolution: a read query might use optimized SQL, while a write operation uses EF Core and domain rules.
- Clearer ownership: teams can work on different business capabilities with fewer overlapping code changes.

One important caution: Vertical Slice Architecture does not mean duplicating every business rule. Invariants that must always hold—such as preventing an invalid order state—should remain protected by the appropriate domain or shared business logic.

## 6. Mentor's takeaway

Think of a restaurant.

- A layered organization groups all chefs, cashiers, and storage workers separately.
- A vertical slice groups the work needed to fulfill one customer request, while still using shared kitchen facilities and common rules.

Both can work. The best choice depends on your application's complexity and how its features change.

Remember: Organize code around the reason it changes—the business use case—not merely around the technical type of each class.

For modern .NET development, Vertical Slice Architecture is especially useful when combined with ASP.NET Core Minimal APIs, request handlers, EF Core, and automated tests. MediatR or CQRS can be added when justified, but VSA does not require either one.