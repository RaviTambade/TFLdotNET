# 🌱  Database Access using C# and MySQL 

> *Hi students, today I want to walk you through a very practical and essential skill in the software industry — **accessing MySQL using C#**. Whether you're working on a simple CRUD app or an enterprise system, this foundational knowledge will help you speak the language of backend development confidently.*

Let’s begin the journey like a developer does — by setting up the environment and slowly diving deeper into both **connected** and **disconnected** data access models.


## 🛠️ 1. Setting Up Your Environment

Every builder needs their tools. First, let’s gather ours.

### ✅ Install MySQL

Head to the [official MySQL website](https://dev.mysql.com/downloads/mysql/) and install the server. Make sure it’s running on your local machine (usually port 3306).

### ✅ Set up Your C# Environment

Use **Visual Studio** or **Visual Studio Code**, and ensure you have the latest **.NET SDK** installed. For MySQL access, we use a special bridge library:

```bash
dotnet add package MySql.Data
```

Once all the tools are ready, it’s time to **connect our application to the database.**

## 🔗 2. Connecting to MySQL from C\#

> *"A developer’s first handshake with the database begins with a connection string."*

```csharp
string connectionString = "Server=localhost;Port=3306;Database=mydatabase;User=myuser;Password=mypassword;";
```

This tiny string is your gateway to interact with MySQL. Let’s write the first snippet to test the connection:

```csharp
using MySql.Data.MySqlClient;

class Program {
    static void Main() {
        MySqlConnection connection = new MySqlConnection(connectionString);
        try {
            connection.Open();
            Console.WriteLine("Connected to MySQL!");
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        } finally {
            connection.Close();
        }
    }
}
```

## 🧱 3. Creating a Table

Once connected, the next logical step is to create a home for your data — a **table**.

Let’s create a simple `users` table:

```csharp
string createTableSql = "CREATE TABLE IF NOT EXISTS users (" +
    "id INT AUTO_INCREMENT PRIMARY KEY," +
    "name VARCHAR(255) NOT NULL," +
    "email VARCHAR(255) NOT NULL)";
```

## 📝 4. Inserting Data

> *"Let’s add some characters to our story..."*

```csharp
string insertSql = "INSERT INTO users (name, email) VALUES (@name, @email)";
MySqlCommand insertCommand = new MySqlCommand(insertSql, connection);
insertCommand.Parameters.AddWithValue("@name", "Shital Patil");
insertCommand.Parameters.AddWithValue("@email", "shital@tfl.com");
```

This is how you securely **insert user data** into your MySQL table.


## 🔍 5. Querying Data

Reading from the database is like **reading the book of records**.

```csharp
string query = "SELECT * FROM users";
MySqlCommand queryCommand = new MySqlCommand(query, connection);

using (MySqlDataReader reader = queryCommand.ExecuteReader()) {
    while (reader.Read()) {
        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Email: {reader["email"]}");
    }
}
```


## ✏️ 6. Updating Data

> *"What if a user’s email changed?"*

```csharp
string updateSql = "UPDATE users SET email = @newEmail WHERE name = @name";
MySqlCommand updateCommand = new MySqlCommand(updateSql, connection);
updateCommand.Parameters.AddWithValue("@newEmail", "updated@tfl.com");
updateCommand.Parameters.AddWithValue("@name", "Shital Patil");
```

This is how we update records safely.


## ❌ 7. Deleting Data

> *"Time to say goodbye to some data."*

```csharp
string deleteSql = "DELETE FROM users WHERE name = @name";
MySqlCommand deleteCommand = new MySqlCommand(deleteSql, connection);
deleteCommand.Parameters.AddWithValue("@name", "John Doe");
```


## 🔄 Connected Data Access (Live Connection)

This is like **keeping your call active** with the database. You must **stay connected** for data operations to happen.

Let’s walk into a full CRUD example with a `Department` class:

```csharp
public static List<Department> GetAllDepartments() {
    List<Department> allDepartments = new List<Department>();
    MySqlConnection con = new MySqlConnection(conString);
    string query = "SELECT * FROM departments";

    MySqlCommand cmd = new MySqlCommand(query, con);
    con.Open();
    MySqlDataReader reader = cmd.ExecuteReader();
    while(reader.Read()) {
        int id = int.Parse(reader["id"].ToString());
        string name = reader["name"].ToString();
        string location = reader["location"].ToString();
        allDepartments.Add(new Department { Id = id, Name = name, Location = location });
    }
    con.Close();
    return allDepartments;
}
```

> *Tip: This is fast and efficient for quick reads, but not ideal if the connection breaks or network is unstable.*


## 🔌 Disconnected Data Access (Offline Copy)

Imagine you **take a copy of the data**, close the connection, and work with it locally.

```csharp
DataSet ds = new DataSet();
MySqlDataAdapter da = new MySqlDataAdapter();
da.SelectCommand = new MySqlCommand("SELECT * FROM departments", con);
da.Fill(ds);
```

From here, you work with the **DataTable** inside the `DataSet`.

```csharp
foreach (DataRow row in ds.Tables[0].Rows) {
    int id = int.Parse(row["id"].ToString());
    string name = row["name"].ToString();
    string location = row["location"].ToString();
    //...
}
```

> *Tip: Great for offline processing, analytics, or displaying data without needing constant DB calls.*


## 🧠 Final Thoughts

> Dear learner, understanding **both connected and disconnected modes** is like understanding how to drive both an automatic and manual car. Both serve their purpose based on the scenario.

* Use **Connected** mode when you need real-time, fast, forward-only data access (e.g., quick reports).
* Use **Disconnected** mode when you want scalability, offline processing, or batch updates.


### 📚 Mini Practice for You

* Create a new table `products` with `id`, `title`, and `description`.
* Implement insert, update, delete, and select using both connected and disconnected methods.
* Try simulating a network failure in connected mode to see what happens!

 
