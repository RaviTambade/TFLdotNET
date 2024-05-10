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