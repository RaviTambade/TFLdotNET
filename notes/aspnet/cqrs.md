
🌱 **CQRS in .NET — Don't Mix Everything Together**

One of the mistakes I see developers make while building enterprise applications is putting **everything into one service, one model, and one database operation.**

Create.
Update.
Delete.
Read.

All through the same path. It works. Until the application grows. Then comes an interesting question:

> **Why should reading data and changing data have the same responsibility?**

That's where **CQRS** becomes useful.



## 🚀 What is CQRS?

**CQRS = Command Query Responsibility Segregation**

The idea is simple:

**Commands change state.**
**Queries read state.**

Instead of thinking:

```text
Controller
    ↓
Service
    ↓
Database
```

we think:

```text
                 Application
                     │
          ┌──────────┴──────────┐
          ↓                     ↓
      COMMANDS                QUERIES
      Change State            Read State
          ↓                     ↓
   Command Handler       Query Handler
          ↓                     ↓
     Write Model           Read Model
```

## ✍️ Command = “Change something”

Examples:

```text
CreateOrderCommand
UpdateOrderCommand
CancelOrderCommand
DeleteOrderCommand
```

A command represents an **intention to change the system**.

For example:

```text
CancelOrderCommand
        ↓
CancelOrderHandler
        ↓
Validate business rules
        ↓
Update Order
        ↓
Save
```

The important word is: **Change.**


## 🔎 Query = “Tell me something”

Examples:

```text
GetOrderByIdQuery
GetOrdersQuery
GetCustomerOrdersQuery
GetOrderSummaryQuery
```

A query should answer a question.

For example:

```text
GetOrderByIdQuery
        ↓
GetOrderByIdHandler
        ↓
Read data
        ↓
Return DTO
```

The important word is: **Read.**


# 🧠 Think like a business person

Imagine an insurance application. A customer asks:  “Show me my policies.” That's a **Query**. But when the customer says:  “Buy this policy.” That's a **Command**. One asks: **What is the current state?** The other says: **Change the state.** This distinction is more important than the framework you use.


# 🛠️ CQRS in ASP.NET Core

A typical .NET implementation could look like:

```text
Client
   ↓
ASP.NET Core Controller
   ↓
MediatR
   ↓
 ┌───────────────────┐
 │                   │
Command           Query
 │                   │
 ↓                   ↓
Handler            Handler
 │                   │
 ↓                   ↓
EF Core            EF Core
 │                   │
 ↓                   ↓
Write DB           Read DB
```

For example:

```csharp
public record CreateOrderCommand(
    int CustomerId,
    decimal Amount
);
```

Handler:

```csharp
public class CreateOrderHandler
{
    public async Task Handle(CreateOrderCommand command)
    {
        // Validate
        // Apply business rules
        // Create Order
        // Save changes
    }
}
```

And the query:

```csharp
public record GetOrderByIdQuery(int OrderId);
```

Handler:

```csharp
public class GetOrderByIdHandler
{
    public async Task<OrderDto> Handle(
        GetOrderByIdQuery query)
    {
        // Read data
        // Map to DTO
        // Return result
    }
}
```

Now the responsibilities are clear.

# ⚡ Why would we do this?

Because **reading and writing often have different requirements.** Imagine an e-commerce system. Writing an order may require:

→ Validation
→ Pricing rules
→ Inventory checks
→ Payment
→ Business rules
→ Transaction management

But the order listing screen might require:

→ Fast queries
→ Filtering
→ Sorting
→ Pagination
→ Aggregated information
→ Denormalized data

Why force both workloads into exactly the same model? CQRS gives us the freedom to optimize them independently.



# 📈 And this can lead to independent scaling

For example:

```text
              Application
                   │
          ┌────────┴────────┐
          ↓                 ↓
       Commands           Queries
          ↓                 ↓
    Write Services      Read Services
          ↓                 ↓
      Write DB          Read DB
                            ↑
                      Many Clients
```

If your application has: **10 write requests** but **100,000 read requests** you may want to scale the read side differently. That's where CQRS starts becoming interesting.



# 🚨 But here's the mentor warning

Don't introduce CQRS just because:

**“It's an enterprise application.”**

And don't introduce:

- ❌ MediatR
- ❌ Event Sourcing
- ❌ Kafka
- ❌ Multiple databases
- ❌ Microservices

just to make the architecture look sophisticated. A simple CRUD application might be perfectly happy with:

```text
Controller
   ↓
Service
   ↓
Repository
   ↓
Database
```

That's not bad architecture. **That's appropriate architecture.**


# 🔥 CQRS does NOT automatically mean two databases

This is one of the most common misunderstandings. You can start with:

```text
Command ──→ Handler ──→ SQL Server
Query  ──→ Handler ──→ SQL Server
```

Same database. Different responsibilities. Later, if the business requirements justify it, you might evolve toward:

```text
Command
   ↓
Write DB
   ↓
Events
   ↓
Read Model
   ↓
Read DB
```

That's a much more sophisticated architecture. But you don't start there.


# 🌱 My rule as a mentor

**Don't implement CQRS because you learned CQRS.**
 
Implement CQRS because your **problem demands separation of responsibilities.** Ask these questions first: 

> Are my read and write models significantly different?
> Do reads and writes have different performance characteristics?
> Do I need independent scaling?
> Is the business logic on the command side becoming complex?
> Would separate read models simplify the application?

If the answer is **yes**, CQRS may help. If the answer is **no**… keep it simple.



### Remember this:

**CRUD**  One model can be enough.

**CQRS**  Separate the responsibility of changing state from reading state.

**Event Sourcing** > Store business events as the source of truth.

**CQRS + Event Sourcing** > A powerful combination—but one that introduces considerably more complexity.

The maturity of an architect is not measured by how many patterns they know. It is measured by knowing:

**when to use them**

—and even more importantly—

**when NOT to use them.**

