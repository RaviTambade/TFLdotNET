### 🌟 Disconnected Data Access Mode 


“Imagine you are a shop owner. You need the list of products from the warehouse (database). But here’s the catch — you don’t want to stay connected to the warehouse all the time because:

1. Continuous connection is costly.
2. You don’t always need live updates every second.
3. Sometimes, you want to take the data offline, make changes, and later sync it back.

This is where **Disconnected Data Access** comes in with **ADO.NET**.

In ADO.NET, the hero of this story is the **`DataSet` and `DataTable`**, managed through a **`DataAdapter`**.

* **DataAdapter** → Think of it like a delivery person who goes to the warehouse, fetches the goods (data), and comes back.
* **DataSet/DataTable** → Like your local shop’s notebook where you store that information offline.
* **Connection** → Open only while fetching/delivering. Otherwise, you’re not draining resources.



### 📌 Flow of Disconnected Access

1. Connect to the database **only when needed**.
2. Use a **DataAdapter** to fill a **DataSet** or **DataTable**.
3. Work on the **DataSet** offline (read, update, delete, add).
4. When ready, tell the **DataAdapter** to push changes back to the DB.

### ⚙️  Disconnected Access with MySQL in .NET Core

```csharp
using System;
using System.Data;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // 1. Connection string to MySQL
        string connString = "Server=localhost;Database=ShopDB;User ID=root;Password=yourpassword;";

        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            // 2. SQL Query
            string query = "SELECT * FROM Products";

            // 3. DataAdapter acts as a bridge
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

            // 4. DataSet - disconnected container
            DataSet ds = new DataSet();

            // 5. Fill DataSet (opens & closes connection internally)
            adapter.Fill(ds, "Products");

            // 6. Work with data offline
            foreach (DataRow row in ds.Tables["Products"].Rows)
            {
                Console.WriteLine($"{row["ProductId"]} - {row["ProductName"]} - {row["Price"]}");
            }

            // 7. Make an offline update
            DataRow newRow = ds.Tables["Products"].NewRow();
            newRow["ProductName"] = "Wireless Mouse";
            newRow["Price"] = 1200;
            ds.Tables["Products"].Rows.Add(newRow);

            // 8. CommandBuilder auto-generates Insert/Update/Delete commands
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);

            // 9. Push changes back to DB
            adapter.Update(ds, "Products");

            Console.WriteLine("Changes updated back to database!");
        }
    }
}
```
### 🔑 Key Learning Points

* **Connection is not always ON** → saves DB resources.
* **DataSet/DataTable** → let you work offline.
* **DataAdapter + CommandBuilder** → handle sync back.
* Good for apps where data doesn’t need to be real-time, e.g., reporting, temporary  

“Think of this like Google Docs offline mode. You download a copy, work on it without internet, and once you reconnect, your changes sync back. ADO.NET disconnected architecture gives the same advantage — you are not glued to the database all the time, but your changes can still be synchronized safely.”
 

> *"Let me take you back to a time when web APIs were not yet dominant, and developers worked closely with in-memory data — yet needed a way to reflect changes back to the database… That's where the power of disconnected ADO.NET came in."*

> *"Even today, understanding this model sharpens your grasp of how `DataTable`, `DataAdapter`, and connectionless architecture work — especially helpful for building scalable, decoupled desktop apps or hybrid systems."*

## 🎯 The Real-World Scenario

*"You are building a small product management module. You want to load all products once into memory, let users make changes (like Excel), and only sync when they hit 'Save' — that’s what disconnected ADO.NET gives you."*


## ✅ Step 1: The `Product` Entity

This is your in-memory model — a simple POCO (Plain Old CLR Object).

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```


## 🧱 Step 2: Your Database Table

You're working with MySQL, and your table looks like this:

```sql
CREATE TABLE Products (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100),
    Price DECIMAL(10, 2)
);
```

## 🛠️ Step 3: Repository with Disconnected ADO.NET

> "Now we build the repository — the heart of our data access logic. This is where the real power of `DataAdapter` + `DataTable` comes in."

```csharp
public class ProductRepository
{
    private string connectionString = "Server=localhost;Database=your_db;Uid=your_user;Pwd=your_password;";

    private MySqlDataAdapter GetDataAdapter(MySqlConnection connection)
    {
        var selectCommand = new MySqlCommand("SELECT * FROM Products", connection);
        var adapter = new MySqlDataAdapter(selectCommand);

        // CommandBuilder auto-generates InsertCommand, UpdateCommand, DeleteCommand
        new MySqlCommandBuilder(adapter);

        return adapter;
    }
```

### 🧠 Mentor Insight:

> *"This adapter is your intelligent agent — it **knows how to retrieve**, **track changes**, and **push updates back**. You work in-memory, and it syncs everything in one shot."*

---

## 📥 Read: Fetch Products

```csharp
    public DataTable GetProducts()
    {
        using var connection = new MySqlConnection(connectionString);
        var adapter = GetDataAdapter(connection);

        var table = new DataTable();
        adapter.Fill(table); // Brings all records into memory

        return table;
    }
```

## ➕ Create: Insert Product

```csharp
    public void InsertProduct(Product product)
    {
        using var connection = new MySqlConnection(connectionString);
        var adapter = GetDataAdapter(connection);

        var table = new DataTable();
        adapter.Fill(table); // Load existing data

        var newRow = table.NewRow();
        newRow["Name"] = product.Name;
        newRow["Price"] = product.Price;
        table.Rows.Add(newRow); // Add to memory

        adapter.Update(table); // Sync with DB
    }
```

## ✏️ Update: Modify Product

```csharp
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
```

## ❌ Delete: Remove Product

```csharp
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

## ✅ Final: Using the Repository

Here’s your simulation of user actions:

```csharp
var repo = new ProductRepository();

// Insert
repo.InsertProduct(new Product { Name = "Wireless Mouse", Price = 899.99m });

// Fetch and Display
var products = repo.GetProducts();
foreach (DataRow row in products.Rows)
{
    Console.WriteLine($"{row["Id"]} - {row["Name"]} - ₹{row["Price"]}");
}

// Update
repo.UpdateProduct(new Product { Id = 1, Name = "Ergonomic Mouse", Price = 1099.00m });

// Delete
repo.DeleteProduct(1);
```

## 🧠 Mentor Wisdom: Why Disconnected ADO.NET?

| Benefit                         | Why It Matters                                         |
| ------------------------------- | ------------------------------------------------------ |
| No persistent connection needed | Great for bandwidth-saving or offline-first apps       |
| In-memory operations            | Faster UI-bound processing like grids or forms         |
| Full control before commit      | Make multiple changes, commit all at once              |
| Built-in change tracking        | Tracks added, modified, and deleted rows automatically |

## 🧰 Optional Enhancements

> "Once you master this, you can go one step further…"

* ✅ Wrap `ProductRepository` as an ASP.NET Core **singleton or scoped service**
* ✅ Replace `DataTable` with a **binding source** in WinForms or WPF
* ✅ Replace auto-generated commands with **custom SQL + parameters** for better control

## 🎓 Mentor Challenge (Your Homework):

Try building a `CustomerRepository` using the same pattern:

1. Create `Customer` table
2. Create `Customer` class
3. Implement CRUD with `DataAdapter`, `DataTable`, and disconnected architecture

## 📣 Final Words from Mentor

> *"While the world has moved on to ORMs and APIs, the foundation of **disconnected data access** still teaches you valuable lessons in **memory management, sync logic, and database design**. Learn it once, and you'll use it forever — even in microservices, caching, and real-time syncing."*

