To implement **CRUD operations** using **disconnected data access** (i.e., using `DataSet`, `DataTable`, and `DataAdapter`) in **ADO.NET with .NET Core** (or .NET 5/6+), you follow this pattern:

- Use a `MySqlDataAdapter` to fill a `DataTable`.
- Perform operations on the `DataTable` (Add, Update, Delete rows).
- Use `DataAdapter.Update()` to sync changes back to the database.

Let me walk you through a sample implementation using a `Product` class and MySQL.

---

### ✅ 1. Prerequisites

Install MySQL ADO.NET provider:
```bash
dotnet add package MySql.Data
```

---

### ✅ 2. Sample `Product` Class

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

---

### ✅ 3. Database Table

```sql
CREATE TABLE Products (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100),
    Price DECIMAL(10, 2)
);
```

---

### ✅ 4. CRUD Implementation Using Disconnected ADO.NET

```csharp
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class ProductRepository
{
    private string connectionString = "Server=localhost;Database=your_db;Uid=your_user;Pwd=your_password;";

    private MySqlDataAdapter GetDataAdapter(MySqlConnection connection)
    {
        var selectCommand = new MySqlCommand("SELECT * FROM Products", connection);
        var adapter = new MySqlDataAdapter(selectCommand);

        // Auto-generate insert/update/delete commands
        var builder = new MySqlCommandBuilder(adapter);

        return adapter;
    }

    public DataTable GetProducts()
    {
        using var connection = new MySqlConnection(connectionString);
        var adapter = GetDataAdapter(connection);

        var table = new DataTable();
        adapter.Fill(table);

        return table;
    }

    public void InsertProduct(Product product)
    {
        using var connection = new MySqlConnection(connectionString);
        var adapter = GetDataAdapter(connection);

        var table = new DataTable();
        adapter.Fill(table);

        var newRow = table.NewRow();
        newRow["Name"] = product.Name;
        newRow["Price"] = product.Price;
        table.Rows.Add(newRow);

        adapter.Update(table);
    }

    public void UpdateProduct(Product product)
    {
        using var connection = new MySqlConnection(connectionString);
        var adapter = GetDataAdapter(connection);

        var table = new DataTable();
        adapter.Fill(table);

        var rows = table.Select($"Id = {product.Id}");
        if (rows.Length > 0)
        {
            rows[0]["Name"] = product.Name;
            rows[0]["Price"] = product.Price;
            adapter.Update(table);
        }
    }

    public void DeleteProduct(int productId)
    {
        using var connection = new MySqlConnection(connectionString);
        var adapter = GetDataAdapter(connection);

        var table = new DataTable();
        adapter.Fill(table);

        var rows = table.Select($"Id = {productId}");
        if (rows.Length > 0)
        {
            rows[0].Delete();
            adapter.Update(table);
        }
    }
}
```

---

### ✅ 5. Usage Example

```csharp
var repo = new ProductRepository();

// Create
repo.InsertProduct(new Product { Name = "Mouse", Price = 499.99m });

// Read
var products = repo.GetProducts();
foreach (DataRow row in products.Rows)
{
    Console.WriteLine($"{row["Id"]} - {row["Name"]} - ₹{row["Price"]}");
}

// Update
repo.UpdateProduct(new Product { Id = 1, Name = "Wireless Mouse", Price = 599.99m });

// Delete
repo.DeleteProduct(1);
```

---

### ✅ Notes

- This approach is suitable when working with in-memory changes and syncing later.
- `DataAdapter` + `DataTable` enables disconnected operations.
- You can also manually define the `InsertCommand`, `UpdateCommand`, `DeleteCommand` if you want finer control.

Would you like this code converted into a reusable service class for use with dependency injection in ASP.NET Core?