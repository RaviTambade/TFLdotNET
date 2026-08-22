# 🌸 Transflower Mentor Session

## **Entity Framework Core — Insurance Application Perspective**

> **Mentor says:**
> *“Class, let me take you on a journey. Imagine you are not building a demo application. You are building an Insurance Management System used by customers, agents, managers and claims officers. Suddenly, database programming becomes a real engineering problem.”*

The uploaded material introduces EF Core through the idea of ORM, `DbContext`, CRUD, LINQ, migrations and asynchronous database operations. 

Let's understand the same concepts through our **Insurance Application**.

---

# 🏢 1. Our Insurance Application

Imagine **Max Insurance** has an application containing:

```text
Customer
   │
   ├── Insurance Policy
   │       │
   │       ├── Premium
   │       └── Renewal
   │
   └── Claims
           │
           └── Claim Payment
```

Our database may contain:

```text
customers
policies
premiums
claims
payments
agents
users
```

Now the developer has a problem.

The application is written in **C#**.

The data is stored in a **relational database**.

How do these two worlds communicate?

---

# 🌱 2. The Old Way — Raw SQL

Suppose we want all active policies.

We could write:

```sql
SELECT *
FROM policies
WHERE status = 'Active';
```

Then we have to:

1. Execute SQL
2. Read database rows
3. Create C# objects
4. Map columns to properties
5. Handle connections
6. Handle parameters
7. Handle transactions

Imagine doing this for every operation.

The mentor asks:

> **“Do I really want my business application to spend most of its time doing database plumbing?”**

Probably not.

---

# 💡 3. Enter ORM

**ORM = Object Relational Mapping**

ORM creates a bridge between:

```text
C# Objects
     ↕
Database Tables
```

For example:

```csharp
public class Policy
{
    public int Id { get; set; }

    public string PolicyNumber { get; set; }

    public int CustomerId { get; set; }

    public decimal Premium { get; set; }

    public string Status { get; set; }
}
```

can represent:

```text
policies
--------------------------------
Id
PolicyNumber
CustomerId
Premium
Status
```

The uploaded material describes this core ORM idea as mapping application objects to relational tables. 

---

# 🚀 4. Meet Entity Framework Core

**Entity Framework Core — EF Core** is Microsoft's ORM technology for .NET.

Think of EF Core as a **digital bridge between our Insurance Application and the database**.

```text
Insurance Application
        │
        │ C# Objects
        ▼
   EF Core / ORM
        │
        │ SQL
        ▼
     Database
```

Instead of constantly thinking:

> “How do I write SQL?”

we can often think:

> “What insurance data do I need?”

The source describes EF Core as cross-platform, open-source and capable of mapping classes to database tables. 

---

# 🧑‍💼 5. The Hero of EF Core — `DbContext`

Now we need someone to manage the interaction.

Meet:

```csharp
DbContext
```

Mentor says:

> **“Think of `DbContext` as the branch manager of our insurance database.”**

It knows about our entities:

```csharp
public class InsuranceDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Policy> Policies { get; set; }

    public DbSet<Premium> Premiums { get; set; }

    public DbSet<Claim> Claims { get; set; }
}
```

Conceptually:

```text
InsuranceDbContext
       │
       ├── Customers
       ├── Policies
       ├── Premiums
       └── Claims
```

The uploaded material similarly describes `DbContext` as the central component responsible for tracking entities and communicating with the database. 

---

# 📝 6. Creating an Insurance Policy

Suppose an agent sells a new policy.

Traditional SQL:

```sql
INSERT INTO policies
(
    PolicyNumber,
    CustomerId,
    Premium,
    Status
)
VALUES
(
    'POL1001',
    101,
    25000,
    'Active'
);
```

With EF Core:

```csharp
var policy = new Policy
{
    PolicyNumber = "POL1001",
    CustomerId = 101,
    Premium = 25000,
    Status = "Active"
};

context.Policies.Add(policy);

context.SaveChanges();
```

That's it.

We work with an **object**.

EF Core takes responsibility for generating and executing the appropriate SQL. The source demonstrates the same `Add()` and `SaveChanges()` pattern for CRUD operations. 

---

# 🔍 7. Finding Policies — LINQ Enters

Now the insurance manager asks:

> **“Show me all active policies.”**

We don't necessarily need to write raw SQL.

We can write:

```csharp
var policies = context.Policies
    .Where(p => p.Status == "Active")
    .ToList();
```

Read the code as English:

> “From Policies, give me policies where Status is Active.”

This is **LINQ**.

---

# 🧠 8. LINQ is Extremely Important

LINQ allows us to ask business questions about our data.

### Active policies

```csharp
var policies = context.Policies
    .Where(p => p.Status == "Active")
    .ToList();
```

### Policies of a particular customer

```csharp
var policies = context.Policies
    .Where(p => p.CustomerId == customerId)
    .ToList();
```

### High-value policies

```csharp
var policies = context.Policies
    .Where(p => p.SumAssured > 1000000)
    .ToList();
```

### Sort by premium

```csharp
var policies = context.Policies
    .OrderByDescending(p => p.Premium)
    .ToList();
```

### Get only policy numbers

```csharp
var policyNumbers = context.Policies
    .Select(p => p.PolicyNumber)
    .ToList();
```

The source explicitly identifies LINQ as the mechanism used for querying through EF Core and notes that EF Core translates LINQ queries into SQL.  

---

# 🔥 9. Business Requirement → LINQ

Imagine the insurance manager says:

> **“Give me active policies with premium greater than ₹25,000, sorted by highest premium.”**

Don't immediately think about SQL.

Think about the **business operations**:

```text
Policies
   ↓
Active
   ↓
Premium > ₹25,000
   ↓
Sort by Premium
   ↓
Return
```

Then:

```csharp
var result = context.Policies
    .Where(p => p.Status == "Active")
    .Where(p => p.Premium > 25000)
    .OrderByDescending(p => p.Premium)
    .ToList();
```

This is the mindset we want to develop at Transflower:

> **Requirement → Data operation → LINQ → SQL**

---

# 🧩 10. Extension Methods

Now let's connect this with your previous topic.

Suppose our application repeatedly needs to check whether a policy is active.

Instead of writing:

```csharp
p.Status == "Active"
```

everywhere, we can create an extension method:

```csharp
public static class PolicyExtensions
{
    public static bool IsActive(this Policy policy)
    {
        return policy.Status == "Active";
    }
}
```

Now:

```csharp
if (policy.IsActive())
{
    // process policy
}
```

And even inside LINQ:

```csharp
var activePolicies = context.Policies
    .Where(p => p.IsActive())
    .ToList();
```

Now our three concepts connect:

```text
Extension Method
       ↓
Reusable Policy Behavior
       ↓
Lambda Expression
       ↓
LINQ
       ↓
EF Core
       ↓
Database
```

**Important engineering note:** with EF Core, custom methods inside a query may not always translate to SQL. For database queries, prefer expressions EF Core can translate, or place the custom logic after materialization when appropriate.

---

# ✏️ 11. Updating a Policy

Suppose a customer's policy premium changes.

```csharp
var policy = context.Policies
    .FirstOrDefault(p => p.PolicyNumber == "POL1001");

if (policy != null)
{
    policy.Premium = 30000;

    context.SaveChanges();
}
```

We didn't write:

```sql
UPDATE policies
SET Premium = 30000
WHERE PolicyNumber = 'POL1001';
```

We changed the object.

EF Core tracks the change and persists it.

The source highlights EF Core's change-tracking capability. 

---

# 🗑️ 12. Cancelling a Policy

Suppose the business wants to remove a policy record:

```csharp
var policy = context.Policies
    .FirstOrDefault(p => p.PolicyNumber == "POL1001");

if (policy != null)
{
    context.Policies.Remove(policy);

    context.SaveChanges();
}
```

Again:

```text
Find
 ↓
Modify state
 ↓
SaveChanges()
 ↓
Database
```

---

# 💰 13. Insurance Premium Report

Now the management team asks:

> **“How much premium have we collected?”**

LINQ makes this simple:

```csharp
var totalPremium =
    context.Premiums.Sum(p => p.Amount);
```

Average premium:

```csharp
var averagePremium =
    context.Premiums.Average(p => p.Amount);
```

Number of policies:

```csharp
var policyCount =
    context.Policies.Count();
```

Highest premium:

```csharp
var highestPremium =
    context.Premiums.Max(p => p.Amount);
```

This is where LINQ becomes more than a syntax feature.

It becomes a **business-query language**.

---

# 🏥 14. Claims Processing

Suppose the claims officer asks:

> **“Show me all pending claims above ₹50,000.”**

```csharp
var claims = context.Claims
    .Where(c => c.Status == "Pending")
    .Where(c => c.Amount > 50000)
    .OrderByDescending(c => c.Amount)
    .ToList();
```

Business requirement:

```text
Pending Claims
       +
Amount > ₹50,000
       +
Highest amount first
```

becomes:

```text
LINQ Query
```

That's the beauty.

---

# ⚡ 15. Real Application — Async EF Core

Now imagine thousands of customers are accessing the insurance portal simultaneously.

We don't want database I/O to unnecessarily block request threads.

Instead of:

```csharp
var policies = context.Policies
    .Where(p => p.CustomerId == customerId)
    .ToList();
```

we can use:

```csharp
var policies = await context.Policies
    .Where(p => p.CustomerId == customerId)
    .ToListAsync();
```

For saving:

```csharp
await context.SaveChangesAsync();
```

The source covers asynchronous EF Core operations such as `ToListAsync()` and `SaveChangesAsync()` and explains their role in responsiveness and scalability for I/O-bound operations.  

---

# 🏗️ 16. Where EF Core Fits in Our Insurance Architecture

A real application might look like:

```text
                 Customer
                    │
                    ▼
             React / Angular
                    │
                    ▼
             ASP.NET Core API
                    │
                    ▼
              Controller
                    │
                    ▼
               Service
                    │
                    ▼
              Repository
                    │
                    ▼
               EF Core
                    │
             ┌──────┴──────┐
             │     LINQ    │
             └──────┬──────┘
                    │
                    ▼
                Database
```

The important point is:

> **EF Core is not the business layer.**

It is primarily the **data-access technology**.

Our business rules should remain organized in appropriate services/domain components rather than putting everything into the `DbContext`.

---

# 🔄 17. Code First vs Database First

Our insurance company may have two situations.

### Situation 1 — New Insurance Application

There is no database yet.

We create:

```text
Customer.cs
Policy.cs
Premium.cs
Claim.cs
```

Then create:

```text
InsuranceDbContext
```

and use migrations.

This is the **Code First** approach.

The uploaded material describes Code First as starting from C# classes and allowing EF Core migrations to create/update the database schema. 

---

### Situation 2 — Existing Insurance Database

Suppose Max Insurance already has a 10-year-old database.

We don't want to recreate it.

Instead:

```text
Existing Database
       ↓
EF Core Reverse Engineering
       ↓
Entity Classes
       +
DbContext
```

This is the **Database First** approach. The source describes this as reverse-engineering an existing database schema into C# classes and a `DbContext`. 

---

# 🧑‍🏫 18. Mentor's Way of Looking at EF Core

Don't memorize:

```text
DbContext
DbSet
Add()
Update()
Remove()
SaveChanges()
Where()
Select()
OrderBy()
ToListAsync()
```

as isolated API names.

Understand the story:

```text
Business Requirement
        ↓
Domain Model
        ↓
C# Objects
        ↓
LINQ
        ↓
EF Core
        ↓
SQL
        ↓
Database
```

For example:

> **“Find active policies for customer 101.”**

becomes:

```csharp
var policies = await context.Policies
    .Where(p => p.CustomerId == 101)
    .Where(p => p.Status == "Active")
    .ToListAsync();
```

That's the real learning.

---

# 🌱 Final Transflower Mentor Thought

> **“When you build an Insurance Application, don't think of EF Core as a library for avoiding SQL. Think of it as a tool that lets your application work with data using the language of the domain.”**

A customer doesn't say:

```text
SELECT * FROM policies...
```

The business says:

> **“Show me my active policies.”**

A claims officer doesn't say:

```text
SELECT ...
```

They say:

> **“Show me pending claims above ₹50,000.”**

A manager doesn't say:

```text
GROUP BY policy_type
```

They say:

> **“Give me policy count by type.”**

And your job as a software engineer is to translate:

```text
Business Language -> C# / LINQ -> EF Core ->SQL -> Data
```

### **That is the real power of EF Core.**

> **“Don't become a developer who merely knows EF Core APIs. Become an engineer who can translate insurance business problems into clean, maintainable data operations.”**

 # Entity Framework (ORM):
Entity Framework (EF) is an object-relational mapping (ORM) framework for .NET applications, developed by Microsoft. 

**Object-Relational Mapping (ORM)** refers to the technique of mapping between objects in your application and the tables in your relational database. In traditional programming, you have to write SQL queries to interact with the database, fetch data, and map it to objects manually. ORM frameworks like Entity Framework automate this process, allowing developers to work with objects directly, abstracting away the underlying SQL queries.

Here's how EF ORM works:

1. **Entity Classes**: You define your domain model as plain old CLR objects (POCOs) or Entity Framework Core's entity classes. These classes represent the structure of your database tables.

   ```csharp
   public class Product
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public decimal Price { get; set; }
   }
   ```

2. **DbContext**: You create a class that derives from `DbContext`, which represents your database session and acts as a bridge between your domain classes and the underlying database. It contains a set of properties representing database tables.

   ```csharp
   public class MyDbContext : DbContext
   {
       public DbSet<Product> Products { get; set; }
   }
   ```

3. **Mapping**: Entity Framework maps your entity classes to the corresponding database tables based on naming conventions or explicit mappings.

4. **CRUD Operations**: You can perform Create, Read, Update, and Delete operations on your entities using LINQ (Language Integrated Query) or fluent API provided by Entity Framework.

   ```csharp
   using (var context = new MyDbContext())
   {
       // Create
       var product = new Product { Name = "Widget", Price = 9.99 };
       context.Products.Add(product);
       context.SaveChanges();
       
       // Read
       var products = context.Products.ToList();
       
       // Update
       var productToUpdate = context.Products.First(p => p.Id == 1);
       productToUpdate.Price = 12.99;
       context.SaveChanges();
       
       // Delete
       var productToDelete = context.Products.First(p => p.Id == 1);
       context.Products.Remove(productToDelete);
       context.SaveChanges();
   }
   ```

ORM frameworks like Entity Framework provide several benefits, including:

- **Reduced Boilerplate Code**: ORM frameworks eliminate the need to write repetitive SQL queries, reducing development time and potential errors.

- **Portability**: ORM frameworks abstract the underlying database, allowing developers to switch between different database systems without changing the application code.

- **Object-Oriented Approach**: Developers can work with objects directly in their code, making it more natural and intuitive.

- **Automatic Mapping**: ORM frameworks handle the mapping between objects and database tables, reducing manual effort and ensuring consistency.

Entity Framework simplifies database interaction in .NET applications by providing a high-level abstraction over the underlying database, enabling developers to focus more on business logic rather than database plumbing.

### Step by Step Entity Framework Core

#### Step 1: Install Entity Framework Core

1. Open the NuGet Package Manager Console.
2. Run the following command to install Entity Framework Core:
   ```bash
   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   ```

#### Step 2: Create Database Context

1. Create a class that inherits from `DbContext`. This class represents your database context.
   ```csharp
   using Microsoft.EntityFrameworkCore;

   public class MyDbContext : DbContext
   {
       public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
       {
       }

       // Define DbSet properties for your entities
       public DbSet<MyEntity> MyEntities { get; set; }
   }
   ```

#### Step 3: Configure Database Connection

1. In your `appsettings.json` file, add a connection string for your database:
   ```json
   {
     "ConnectionStrings": {
       "MyDbConnection": "YourDatabaseConnectionString"
     }
   }
   ```

#### Step 4: Register Database Context with Dependency Injection

1. In your `Startup.cs` file, register the database context with the dependency injection container:
   ```csharp
   using Microsoft.EntityFrameworkCore;

   public void ConfigureServices(IServiceCollection services)
   {
       // Other services
       services.AddDbContext<MyDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
   }
   ```

#### Step 5: Create Entity Classes

1. Create classes to represent your database entities. Annotate these classes with attributes to define table and column mappings, relationships, etc.
   ```csharp
   public class MyEntity
   {
       public int Id { get; set; }
       public string Name { get; set; }
   }
   ```

#### Step 6: Create and Apply Migrations

1. In the Package Manager Console, navigate to your project directory.
2. Run the following command to create a migration:
   ```bash
   Add-Migration InitialCreate
   ```
3. Run the following command to apply the migration to your database:
   ```bash
   Update-Database
   ```

#### Step 7: Use Entity Framework in Your Application

1. Inject the `MyDbContext` into your services or controllers using constructor injection.
2. Use the `MyDbContext` to query and manipulate data in your database.
   ```csharp
   public class MyService
   {
       private readonly MyDbContext _dbContext;

       public MyService(MyDbContext dbContext)
       {
           _dbContext = dbContext;
       }

       public List<MyEntity> GetAllEntities()
       {
           return _dbContext.MyEntities.ToList();
       }

       // Other methods for CRUD operations
   }
   ```

#### Step 8: Test Your Application

1. Test your application to ensure that Entity Framework Core is working correctly. Verify that data is being retrieved, inserted, updated, and deleted from the database as expected.

By following these steps, you can successfully set up and use Entity Framework Core in your .NET Core application to interact with a database. Adjust the entity classes, database context, and migration steps based on your specific application requirements and database schema.

### Using async, await with Entity Frameowrk

Async programming with Entity Framework in C# allows you to perform database operations asynchronously, enhancing the responsiveness of your applications, especially in scenarios where there are many concurrent operations or long-running tasks.  Combining asynchronous programming with Entity Framework for CRUD (Create, Read, Update, Delete) operations in C# can improve the scalability and responsiveness of your applications. Here's a basic example of how you can achieve this:

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Define your model
public class MyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Add more properties as needed
}

// Define your DbContext
public class MyDbContext : DbContext
{
    public DbSet<MyEntity> MyEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Initialize your DbContext
        using (var context = new MyDbContext())
        {
            // Create
            var newEntity = new MyEntity { Name = "New Entity" };
            context.MyEntities.Add(newEntity);
            await context.SaveChangesAsync();

            // Read
            var entities = await context.MyEntities.ToListAsync();
            foreach (var entity in entities)
            {
                Console.WriteLine($"ID: {entity.Id}, Name: {entity.Name}");
            }

            // Update
            var entityToUpdate = await context.MyEntities.FirstOrDefaultAsync(e => e.Id == 1);
            if (entityToUpdate != null)
            {
                entityToUpdate.Name = "Updated Entity";
                await context.SaveChangesAsync();
            }

            // Delete
            var entityToDelete = await context.MyEntities.FirstOrDefaultAsync(e => e.Id == 1);
            if (entityToDelete != null)
            {
                context.MyEntities.Remove(entityToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
```

In this example:

- `MyEntity` represents the entity model.
- `MyDbContext` represents the database context, which is a subclass of `DbContext` provided by Entity Framework.
- Inside the `Main` method, asynchronous methods like `ToListAsync()`, `SaveChangesAsync()` are used to perform CRUD operations asynchronously.
- Ensure to replace `"your_connection_string_here"` with your actual database connection string.

Async programming allows your application to perform I/O-bound operations without blocking the main thread, thus improving the responsiveness and scalability of your application, especially in scenarios where there are many concurrent operations.