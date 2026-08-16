

### Dapper : Data Access 

Imagine you are building an **Insurance Management System**.

You have a `Policy` table in MySQL.

A customer asks:

> “Sir, I want to see all my insurance policies.”

Now, as a developer, you have a choice.

You could use **Entity Framework Core**.

EF Core says:

> “Don't worry about SQL. Give me your C# objects, configure your relationships, and I will take care of much of the database interaction.”

That's powerful.

But sometimes, you don't need a giant machine.

Sometimes you simply want to say:

> **“Here is my SQL. Execute it and give me my objects.”**

That's where **Dapper** walks into the room.

---

## ⚡ Dapper's Philosophy

Dapper follows a very simple philosophy:

> **You write the SQL. Dapper maps the result.**

For example:

```csharp
string sql = """
    SELECT Id, PolicyNumber, CustomerName, Premium
    FROM Policies
    WHERE CustomerId = @CustomerId
""";

var policies = connection.Query<Policy>(
    sql,
    new { CustomerId = customerId }
);
```

Look carefully.

There is no complicated configuration here.

You wrote SQL.

You supplied parameters.

Dapper executed the query.

And Dapper mapped every returned row into a `Policy` object.

That's it.

---

## 🧠 What Is Dapper Actually Doing?

A student may ask:

> “Sir, is Dapper replacing MySQL?”

No.

> “Is Dapper replacing ADO.NET?”

Not exactly.

Dapper is built **on top of ADO.NET**.

Think of the relationship like this:

```text
Your Application
       ↓
     Dapper
       ↓
    ADO.NET
       ↓
   MySQL / SQL Server
       ↓
     Database
```

ADO.NET gives you the fundamental database connectivity APIs.

Dapper removes much of the repetitive plumbing.

It gives you convenient methods such as:

```csharp
Query<T>()
QueryFirst<T>()
QuerySingle<T>()
Execute()
ExecuteScalar<T>()
```

So I tell my students:

> **“Dapper doesn't hide SQL from you.
> Dapper makes working with SQL comfortable.”**

---

# 🛠️ The Craftsman's Toolkit

Suppose our insurance application needs to retrieve policies.

### 1. Query

When you expect multiple records:

```csharp
var policies = connection.Query<Policy>(
    "SELECT * FROM Policies"
);
```

Think:

> **Query = Give me many objects.**

---

### 2. QueryFirst

When you want the first matching record:

```csharp
var policy = connection.QueryFirst<Policy>(
    "SELECT * FROM Policies WHERE Id = @Id",
    new { Id = 101 }
);
```

Think:

> **QueryFirst = Give me the first matching object.**

---

### 3. QuerySingle

When exactly one record should exist:

```csharp
var policy = connection.QuerySingle<Policy>(
    "SELECT * FROM Policies WHERE PolicyNumber = @PolicyNumber",
    new { PolicyNumber = "POL1001" }
);
```

Think:

> **QuerySingle = I expect exactly one.**

---

### 4. Execute

Now suppose the customer wants to renew the policy.

You don't need an object back.

You simply want to execute an update:

```csharp
var rows = connection.Execute(
    """
    UPDATE Policies
    SET Status = @Status
    WHERE Id = @Id
    """,
    new
    {
        Id = 101,
        Status = "Renewed"
    }
);
```

Think:

> **Execute = Perform an INSERT, UPDATE or DELETE.**

---

# 🎯 The Important Lesson — SQL Still Matters

This is where I challenge my students.

I ask:

> **“If you learn Dapper, can you forget SQL?”**

They usually think for a moment.

The answer is:

> **No.**

In fact, Dapper encourages you to understand SQL better.

You should know:

```text
SELECT
WHERE
JOIN
GROUP BY
ORDER BY
INSERT
UPDATE
DELETE
INDEXES
TRANSACTIONS
STORED PROCEDURES
```

Because Dapper doesn't try to completely abstract the database.

It puts the **developer closer to the database**.

And sometimes, that's exactly what an experienced developer wants.

---

# ⚔️ Dapper vs Entity Framework Core

Now imagine two craftsmen.

### Entity Framework Core

EF Core says:

> “Give me your domain model. I'll manage much of the database interaction for you.”

### Dapper

Dapper says:

> “Give me your SQL. I'll efficiently map the result to your objects.”

Neither is automatically **better**.

They solve different problems.

| Requirement                       | EF Core  | Dapper          |
| --------------------------------- | -------- | --------------- |
| Full ORM                          | ✅        | ❌               |
| SQL control                       | Moderate | Excellent       |
| Lightweight                       | Moderate | ✅               |
| Change tracking                   | ✅        | ❌               |
| LINQ queries                      | ✅        | ❌               |
| Automatic relationship management | ✅        | Limited         |
| Raw SQL experience                | Possible | Natural         |
| Performance-oriented data access  | Good     | Often excellent |

So don't teach students:

> “Dapper is better than EF Core.”

Teach them:

> **“A good engineer chooses the right tool for the problem.”**

---

# 🌱 Transflower Developer Mindset

Suppose tomorrow you join an organization.

You may encounter:

```text
ASP.NET Core
       +
Entity Framework Core
```

in one application.

And:

```text
ASP.NET Core
       +
Dapper
       +
Stored Procedures
```

in another.

And perhaps:

```text
ASP.NET Core
       +
ADO.NET
```

in a legacy application.

A job-ready developer shouldn't say:

> “Sir, I only know EF Core.”

Instead, the mindset should be:

> **“I understand how data access works.
> Now I can learn the tool used by this organization.”**

That is the difference between being **framework-dependent** and being an **engineering-minded developer**.

---

## 🚀 The Dapper Learning Journey

At Transflower, I would teach Dapper in this sequence:

```text
ADO.NET Fundamentals
        ↓
Database Connection
        ↓
Dapper Installation
        ↓
Query<T>()
        ↓
Parameterized Queries
        ↓
CRUD Operations
        ↓
Stored Procedures
        ↓
JOINs & DTOs
        ↓
Transactions
        ↓
Repository Pattern
        ↓
Dapper + ASP.NET Core Web API
        ↓
Dapper + Dependency Injection
        ↓
Dapper + MySQL / SQL Server
        ↓
Production-ready Data Access
```

And finally I tell my students:

> **“Don't learn Dapper merely to write `Query<T>()`.
> Learn Dapper to understand what really happens between your application and your database.”**

That is where the real craftsmanship begins.


### 🛠️ **Step 1: Add Dapper to Your Project**

"You don’t need a whole cargo of libraries. Just one simple NuGet command:"

```bash
dotnet add package Dapper
```

*"Done. You're ready to fly."* 

### **Step 2: Your Model Class – Let’s Say a Product**

Here’s your domain class:

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

*"This is your blueprint. Dapper will match database columns with these properties by name."* 

### 🔄 **Step 3: Connect and Perform Operations**

Now the magic begins. Let’s write code like a real craftsman:

```csharp
using System.Data;
using System.Data.SqlClient;
using Dapper;

string connectionString = "your_connection_string_here";

using (IDbConnection db = new SqlConnection(connectionString))
{
    // 1. Read
    var products = db.Query<Product>("SELECT * FROM Products");
    foreach (var product in products)
        Console.WriteLine($"{product.Id} - {product.Name} - ₹{product.Price}");

    // 2. Insert
    var newProduct = new Product { Name = "Coffee Mug", Price = 149.99m };
    db.Execute("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", newProduct);

    // 3. Update
    var updatedProduct = new Product { Id = 1, Name = "Updated Mug", Price = 199.99m };
    db.Execute("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", updatedProduct);

    // 4. Delete
    db.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = 1 });
}
```

### **Key Takeaways**

* **Fast as Lightning**: It’s built by the same folks behind Stack Overflow. So, performance? Top-notch.
* **No Heavy ORM Rules**: You write your SQL. Dapper simply maps the result to your C# classes.
* **Safe**: All queries are parameterized, so SQL injection? No chance.
* **Auto-Mapping**: Columns in your query match class properties? Dapper does the rest — no extra config.

### When Should You Use Dapper?

*"Listen carefully, dear learner... If you're building something where performance matters — like a high-traffic API — and you want control over your SQL, **Dapper is your friend**. But if you need complex relationships, migrations, and LINQ, then **Entity Framework** might suit better."* 

### More Resources

* [Dapper GitHub Repo](https://github.com/DapperLib/Dapper)
* [Interactive Tutorials](https://dapper-tutorial.net/)

### Mentor's Closing Advice

*"Remember, tools don’t make you great — knowing **when and why to use them** does. Dapper is like a sharp chisel in a master’s hand. Used wisely, it can carve beautiful, performant systems with precision."*

*"Don’t be afraid of writing SQL. Embrace it when needed. And when you do — let Dapper carry the load."* 

## Dapper 
Dapper is a lightweight, open-source Object-Relational Mapping (ORM) library for .NET developed by the Stack Overflow team. Unlike Entity Framework, which is a full-fledged ORM, Dapper focuses on providing high-performance, simple mapping between your application's domain objects and database tables. Here's a brief overview of how you can use Dapper in C#:

1. **Installation**:
   You can install Dapper via NuGet Package Manager or using the .NET CLI:

   ```
   dotnet add package Dapper
   ```

2. **Basic Usage**:
   Dapper provides extension methods on IDbConnection interface (such as SqlConnection) to execute queries and map results to objects. Here's an example of how you can use Dapper:

   ```csharp
   using System;
   using System.Data;
   using System.Data.SqlClient;
   using Dapper;

   public class Product
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public decimal Price { get; set; }
   }

   public class Program
   {
       public static void Main(string[] args)
       {
           string connectionString = "your_connection_string_here";

           using (IDbConnection dbConnection = new SqlConnection(connectionString))
           {
               // Query
               var products = dbConnection.Query<Product>("SELECT * FROM Products");
               foreach (var product in products)
               {
                   Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
               }

               // Insert
               var newProduct = new Product { Name = "New Product", Price = 19.99m };
               dbConnection.Execute("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", newProduct);

               // Update
               var productToUpdate = new Product { Id = 1, Name = "Updated Product", Price = 29.99m };
               dbConnection.Execute("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", productToUpdate);

               // Delete
               dbConnection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = 1 });
           }
       }
   }
   ```

3. **Key Features**:
   - **High Performance**: Dapper is known for its high-performance capabilities due to its lightweight nature and efficient query mapping.
   - **Simplicity**: Dapper provides simple extension methods for executing queries and mapping results, making it easy to use.
   - **Parameterized Queries**: Dapper supports parameterized queries, helping to prevent SQL injection attacks.
   - **Automatic Mapping**: Dapper automatically maps query results to objects based on property names, making it easy to work with data.

4. **Additional Resources**:
   - [Dapper Documentation](https://github.com/DapperLib/Dapper)
   - [Dapper Tutorial](https://dapper-tutorial.net/)

Dapper is often chosen when performance is critical, or when developers prefer more control over the SQL queries being executed. It's particularly popular in scenarios where raw SQL queries are preferred over the abstraction provided by full-fledged ORMs like Entity Framework.
