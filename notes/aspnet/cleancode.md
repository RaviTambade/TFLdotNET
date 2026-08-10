# Clean Architecture — Think Like a Software Architect

Imagine you are building an **Insurance Management System** for Max Insurance. You have:

* Customers
* Policies
* Premiums
* Claims
* Payments
* Agents
* Employees

A beginner often starts writing:

```text
Controller
    ↓
Database
```

Then business rules slowly get mixed everywhere:

```text
Controller
 ├── Validation
 ├── Business rules
 ├── SQL
 ├── Email
 ├── Payment
 └── Response formatting
```

It works initially. But after six months, one simple change becomes dangerous.

> **Mentor's question:**
> "What happens if tomorrow MySQL is replaced by PostgreSQL?"

If database code is everywhere, you have a problem. Clean Architecture gives us a better answer:

```text
                ┌─────────────────────┐
                │    Presentation     │
                │   Web API / React   │
                └──────────┬──────────┘
                           ↓
                ┌─────────────────────┐
                │    Application      │
                │    Use Cases        │
                └──────────┬──────────┘
                           ↓
                ┌─────────────────────┐
                │       Domain        │
                │ Business Rules/Core │
                └─────────────────────┘
                           ↑
                           │
                ┌─────────────────────┐
                │   Infrastructure    │
                │ DB / Email / APIs   │
                └─────────────────────┘
```

The important idea is **not the number of projects**. The important idea is:

> **Business knowledge should not depend on technology.**


# 1. Domain — The Heart of the Application

Ask yourself:

> "If I remove ASP.NET Core, MySQL, React and Azure, what business knowledge remains?"

That knowledge belongs in the **Domain**. For our insurance system:

```text
Domain
 ├── Customer
 ├── Policy
 ├── Premium
 ├── Claim
 ├── Payment
 └── Business Rules
```

For example:

```csharp
public class Policy
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Premium { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive =>
        DateTime.UtcNow >= StartDate &&
        DateTime.UtcNow <= EndDate;
}
```

The domain should not care whether data is stored in:

```text
MySQL
SQL Server
MongoDB
PostgreSQL
```

It should not care whether the application is hosted on:

```text
AWS
Azure
Docker
Kubernetes
```

That is the beauty of the Domain layer.


# 2. Domain Interfaces — "I Need This Service"

Now imagine the domain/application needs customer data. Should Domain say:

```csharp
new MySqlConnection(...)
```

❌ No.

Instead, define an abstraction:

```csharp
public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int customerId);

    Task<IEnumerable<Customer>> GetAllAsync();
}
```

This is a very important architectural idea. The Domain/Application says:

> "I need a customer repository."

It does **not** say:

> "I need MySQL."

That distinction creates flexibility.

# 3. Application — The Use-Case Manager

Now we move one level outside the Domain. The Application layer answers:

> **"What does the application do?"**

Examples:

```text
Create Customer
Purchase Policy
Pay Premium
Renew Policy
Submit Claim
Approve Claim
Get Customer Policies
Get Claims
```

These are **use cases**.

For example:

```text
Purchase Policy
       ↓
Validate Customer
       ↓
Validate Policy
       ↓
Calculate Premium
       ↓
Create Policy
       ↓
Save Policy
       ↓
Return Policy
```

That workflow belongs in the Application layer.

# 4. Command and Query Thinking

This is where **CQRS** becomes useful. Instead of creating one giant service:

```text
InsuranceService
```

we can think in terms of use cases.

### Commands

Commands change state.

```text
PurchasePolicyCommand
RenewPolicyCommand
PayPremiumCommand
SubmitClaimCommand
ApproveClaimCommand
```

### Queries

Queries read data.

```text
GetPolicyByIdQuery
GetCustomerPoliciesQuery
GetClaimsQuery
GetPremiumHistoryQuery
```

So we get:

```text
                 Application
                     │
          ┌──────────┴──────────┐
          ↓                     ↓
      Commands                Queries
          ↓                     ↓
     Change State           Read State
```

This makes the application easier to understand.


# 5. Presentation — The Receptionist 

Now think about the API Controller. A receptionist receives your request. The receptionist should **not perform the actual business operation**.

For example:

```http
POST /api/policies
```

Controller:

```csharp
[HttpPost]
public async Task<IActionResult> PurchasePolicy(
    PurchasePolicyRequest request)
{
    var command = new PurchasePolicyCommand(
        request.CustomerId,
        request.PolicyId);

    var result = await mediator.Send(command);

    return Ok(result);
}
```

Notice something important. The Controller doesn't calculate:

```text
Premium
Eligibility
Policy validity
Claim rules
Payment rules
```

It delegates.

> **Controller = Traffic Police**
>
> It directs the request to the right place.


# 6. Infrastructure — The Technician 

Now comes Infrastructure. Infrastructure knows technology.

For example:

```text
Infrastructure
 ├── EF Core
 ├── MySQL
 ├── Dapper
 ├── SMTP
 ├── Redis
 ├── Azure Blob
 ├── AWS S3
 └── External APIs
```

Suppose Domain/Application defines:

```csharp
public interface IPolicyRepository
{
    Task<Policy?> GetByIdAsync(int id);

    Task AddAsync(Policy policy);
}
```

Infrastructure implements it:

```csharp
public class PolicyRepository : IPolicyRepository
{
    private readonly InsuranceDbContext _context;

    public PolicyRepository(InsuranceDbContext context)
    {
        _context = context;
    }

    public async Task<Policy?> GetByIdAsync(int id)
    {
        return await _context.Policies
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Policy policy)
    {
        await _context.Policies.AddAsync(policy);
        await _context.SaveChangesAsync();
    }
}
```

Now the business logic doesn't know about EF Core.


# 7. The Most Important Rule — Dependency Direction 

This is where many developers misunderstand Clean Architecture. The dependency should point **toward the core**.

```text
Presentation
      ↓
Application
      ↓
Domain
```

And:

```text
Infrastructure
      ↓
Domain
```

Therefore:

```text
        Presentation
              ↓
        Application
              ↓
           Domain
              ↑
              │
        Infrastructure
```

The Domain should never say:

```text
I know ASP.NET Core.
I know EF Core.
I know MySQL.
I know React.
```

Instead:

```text
Domain
  ↓
"I know the business."
```

That's it.

# 8. Dependency Injection — The Bridge

Now you may ask:

> "If Application depends only on an interface, who creates the actual implementation?"

**Dependency Injection.**

In `Program.cs`:

```csharp
builder.Services.AddScoped<IPolicyRepository,
                           PolicyRepository>();
```

Now Application asks:

```csharp
IPolicyRepository
```

and .NET provides:

```text
PolicyRepository
```

The Application doesn't need to know how it was created. This is **Dependency Inversion Principle** in action.


# 9. A Practical .NET Solution Structure

For our Insurance Management System, I would recommend:

```text
MaxInsurance.sln

├── MaxInsurance.Domain
│   ├── Entities
│   │   ├── Customer.cs
│   │   ├── Policy.cs
│   │   ├── Premium.cs
│   │   └── Claim.cs
│   │
│   ├── Interfaces
│   │   ├── IPolicyRepository.cs
│   │   ├── ICustomerRepository.cs
│   │   └── IClaimRepository.cs
│   │
│   └── Rules
│
├── MaxInsurance.Application
│   ├── Policies
│   │   ├── Commands
│   │   │   ├── PurchasePolicyCommand.cs
│   │   │   └── RenewPolicyCommand.cs
│   │   │
│   │   └── Queries
│   │       ├── GetPolicyByIdQuery.cs
│   │       └── GetCustomerPoliciesQuery.cs
│   │
│   ├── Claims
│   ├── Premiums
│   └── Customers
│
├── MaxInsurance.Infrastructure
│   ├── Persistence
│   │   ├── InsuranceDbContext.cs
│   │   └── Repositories
│   │
│   ├── ExternalServices
│   └── Logging
│
└── MaxInsurance.API
    ├── Controllers
    ├── Middleware
    ├── Filters
    └── Program.cs
```

This is much better than:

```text
Controllers
Services
Repositories
Models
Helpers
Utils
```

where everything eventually becomes a giant shared bucket.


# 10. Clean Architecture vs Traditional 3-Tier

A traditional application might look like:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Database
```

Clean Architecture asks a deeper question:

> **Who owns the business rules?**

The answer is:

```text
Domain
```

And then:

```text
API
 ↓
Application
 ↓
Domain
 ↑
Infrastructure
```

The difference is **dependency control**, not merely folder arrangement.

# 11. Where Does React Fit?

This is especially important when building your modern full-stack application. You could have:

```text
React
  ↓
ASP.NET Core Web API
  ↓
Application
  ↓
Domain
  ↑
Infrastructure
  ↓
MySQL
```

React is simply another **Presentation/UI technology**. For example:

```text
React Component
      ↓
fetch()
      ↓
POST /api/policies
      ↓
PolicyController
      ↓
PurchasePolicyCommand
      ↓
Domain Rules
      ↓
IPolicyRepository
      ↓
PolicyRepository
      ↓
MySQL
```

This gives you a very clean separation.

# 12. Testing Becomes Much Easier 

Suppose you want to test:

> "A customer cannot purchase an expired policy."

You don't need:

```text
Browser
+
React
+
ASP.NET server
+
MySQL
```

You can test the business rule directly.

```text
        Unit Test
            ↓
     Application/Domain
            ↓
      Fake Repository
```

This is one of the biggest advantages of Clean Architecture.

# 13. Clean Architecture Is NOT "More Projects"

This is a very important mentor lesson. A developer may create:

```text
Domain
Application
Infrastructure
API
```

and claim:

> "I am using Clean Architecture."

Not necessarily. You can have four projects and still write bad architecture.

For example:

```csharp
public class PolicyController : ControllerBase
{
    // 500 lines of business logic
    // SQL queries
    // payment processing
    // email
    // validation
}
```

That's not clean. Clean Architecture is primarily about:

> **Separation of responsibilities + dependency direction + business rules independence.**


# 14. Connect It With SOLID

Clean Architecture naturally reinforces SOLID.

### S — Single Responsibility

```text
Controller → HTTP
Application → Use case
Domain → Business rules
Infrastructure → Technology
```

### O — Open/Closed

You can add:

```text
MySQL
PostgreSQL
MongoDB
```

without rewriting business rules.

### L — Liskov Substitution

Implementations can substitute abstractions.

```text
IPolicyRepository
       ↑
 ┌─────┴─────┐
MySqlRepo  FakeRepo
```

### I — Interface Segregation

Prefer:

```csharp
IPolicyRepository
ICustomerRepository
IClaimRepository
```

over a gigantic:

```csharp
IInsuranceEverythingService
```

### D — Dependency Inversion

High-level business logic depends on abstractions.

```text
Application
     ↓
IPolicyRepository
     ↑
PolicyRepository
```

# 15. Mentor's Golden Rule 

When designing a new feature, don't start with:

> "Which controller should I create?"

Start with:

> **"What is the business use case?"**

For example:

```text
Business Requirement
        ↓
"Customer wants to renew policy"
        ↓
Use Case
        ↓
RenewPolicy
        ↓
Application
        ↓
Domain Rules
        ↓
Repository Interface
        ↓
Infrastructure Implementation
        ↓
Database
```

That is architectural thinking.

# 16. The Transflower Mental Model

I would teach Clean Architecture using four questions:

| Layer              | Ask this question                      |
| ------------------ | -------------------------------------- |
| **Presentation**   | How does the user communicate with us? |
| **Application**    | What does the system need to do?       |
| **Domain**         | What are the business rules?           |
| **Infrastructure** | How do we technically implement it?    |

Remember this:

```text
                USER
                 ↓
        ┌─────────────────┐
        │  PRESENTATION   │
        │ "How?"          │
        └────────┬────────┘
                 ↓
        ┌─────────────────┐
        │  APPLICATION    │
        │ "What?"         │
        └────────┬────────┘
                 ↓
        ┌─────────────────┐
        │     DOMAIN      │
        │ "Why / Rules?"  │
        └────────▲────────┘
                 │
        ┌────────┴────────┐
        │ INFRASTRUCTURE  │
        │ "Technology?"   │
        └─────────────────┘
```

### Final Mentor Message

**Clean Architecture is not about creating folders.** It is about protecting the **business brain** of your application from technological changes.

Today:

```text
React + ASP.NET Core + MySQL
```

Tomorrow:

```text
Angular + ASP.NET Core + PostgreSQL
```

The business rule:

> **"A policy can be renewed only when the renewal conditions are satisfied."**

should remain intact. That is the real power of Clean Architecture:

> **Technology changes. Business survives.**

And that is exactly what we want when building an enterprise system such as **Policy + Premium + Claim + Customer Management**.