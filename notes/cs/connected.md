 

## 🌟 Connected Data Access 

“Imagine you are running a supermarket billing counter. A customer comes in, and you directly check the price of every product from the warehouse database in real-time.

This means:

* You are **always connected** to the database while working.
* The advantage is: you always get the **latest, live data**.
* The downside is: it consumes database resources as long as you stay connected.

This is what **Connected Data Access** is all about in ADO.NET.”

 

### 📌 Key Players in Connected Model

* **Connection (MySqlConnection)** → Like a telephone line. Always active while you talk.
* **Command (MySqlCommand)** → Your query request (“Give me all products under ₹1000”).
* **DataReader (MySqlDataReader)** → A forward-only, read-only cursor that streams results one by one (like a scanner at the billing counter).

 

### ⚙️ Example: Connected Data Access with MySQL in .NET Core

```csharp
using System;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // 1. Connection string to MySQL
        string connString = "Server=localhost;Database=ShopDB;User ID=root;Password=yourpassword;";

        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            conn.Open(); // 2. Open connection (we stay connected)

            // 3. Prepare SQL command
            string query = "SELECT ProductId, ProductName, Price FROM Products";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            // 4. Execute reader (connected, forward-only)
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("Product List (Connected Access):");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductId"]} - {reader["ProductName"]} - {reader["Price"]}");
                }
            }

            // 5. Insert example
            string insertQuery = "INSERT INTO Products (ProductName, Price) VALUES ('Keyboard', 1500)";
            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
            int rows = insertCmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} row(s) inserted.");

            conn.Close(); // 6. Close connection
        }
    }
}
```

 

### 🔑 Key Learning Points

* **Connection is ON** while working → live interaction.
* **DataReader** → fast, memory efficient (but read-only & forward-only).
* **Command** → executes SQL (SELECT, INSERT, UPDATE, DELETE).
* Great for **real-time apps** (banking transactions, billing systems, dashboards).

 

### 🧑‍🏫 Mentor Analogy Wrap-up

“Think of Connected Access like a **phone call**. As long as the call is active, you’re in constant touch and getting live updates. But calls consume balance and network resources. Similarly, Connected ADO.NET gives you live data but holds the connection to the database, so you should keep it open only when needed and close it quickly after use.”

 