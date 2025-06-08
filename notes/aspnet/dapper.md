##  **Dapper – The Swift Craftsman of Data Access**

*"Let me take you on a little journey today — into a world where data access is fast, simple, and elegant. No frills, no heavy lifting, just pure craftsmanship. That world is called **Dapper**."*

###  **Imagine This…**

"Have you ever watched a master carpenter at work? He doesn’t carry a giant toolkit like others. He has just a few sharp, trusted tools — and with those, he can build precise, beautiful things.

That’s **Dapper** in the .NET world — a **lightweight ORM** (Object-Relational Mapper) that doesn’t overwhelm you with too much abstraction like Entity Framework does. But oh boy, does it get the job done — fast and neat!"

### 🛠️ **Step 1: Add Dapper to Your Project**

"You don’t need a whole cargo of libraries. Just one simple NuGet command:"

```bash
dotnet add package Dapper
```

*"Done. You're ready to fly."* ✈️

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

*"This is your blueprint. Dapper will match database columns with these properties by name."* 🧩

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

* ✅ **Fast as Lightning**: It’s built by the same folks behind Stack Overflow. So, performance? Top-notch.
* 🧠 **No Heavy ORM Rules**: You write your SQL. Dapper simply maps the result to your C# classes.
* 🔐 **Safe**: All queries are parameterized, so SQL injection? No chance.
* 🤝 **Auto-Mapping**: Columns in your query match class properties? Dapper does the rest — no extra config.

### When Should You Use Dapper?

*"Listen carefully, dear learner... If you're building something where performance matters — like a high-traffic API — and you want control over your SQL, **Dapper is your friend**. But if you need complex relationships, migrations, and LINQ, then **Entity Framework** might suit better."* ⚖️

### More Resources

* 📚 [Dapper GitHub Repo](https://github.com/DapperLib/Dapper)
* 🛠️ [Interactive Tutorials](https://dapper-tutorial.net/)

### Mentor's Closing Advice

*"Remember, tools don’t make you great — knowing **when and why to use them** does. Dapper is like a sharp chisel in a master’s hand. Used wisely, it can carve beautiful, performant systems with precision."*

*"Don’t be afraid of writing SQL. Embrace it when needed. And when you do — let Dapper carry the load."* 🧙‍♂️

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