# Database Access using C#

MySQL is a widely-used open-source relational database management system, and C# is a versatile programming language. In this tutorial, we will explore the basics of performing CRUD (Create, Read, Update, Delete) operations in MySQL using C#.

## Setting Up Your Environment:
1. <b>Installing MySQL. </b>
    - Setting up your C# development environment.
    - Connecting to MySQL:
2. <b>Creating a connection to your MySQL server.</b>
    - Creating a Table:
3. <b>Writing C# code to create a table in your MySQL database.</b>
    - Inserting Data:
4. <b>Demonstrating how to insert data into the table</b>
    - Querying Data:
5. <b>Retrieving data from the table.</b?>
    - Updating Data:
6. Modifying existing records in the table.
    - Deleting Data:
7. Deleting records from the table.

### 1. Setting Up Your Environment
- Installing MySQL
- Download and install MySQL from the official MySQL website.
- Setting Up Your C# Development Environment
- Install Visual Studio or Visual Studio Code, and ensure you have the .NET SDK installed.
- Connecting to MySQL

### 2. Connecting to MySQL
To connect to your MySQL server from a C# application, you can use the MySQL .NET Connector (MySql.Data). Install it using NuGet Package Manager or the .NET CLI:

```
    dotnet add package MySql.Data
```

Let  us write code for database connection.
```
using System;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Port=3306;Database=mydatabase;User=myuser;Password=mypassword;";
        MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Connected to MySQL!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
}
```

### 3. Creating a Table
Let us create table users
```
string createTableSql = "CREATE TABLE IF NOT EXISTS users (" +
    "id INT AUTO_INCREMENT PRIMARY KEY," +
    "name VARCHAR(255) NOT NULL," +
    "email VARCHAR(255) NOT NULL)";
MySqlCommand createTableCommand = new MySqlCommand(createTableSql, connection);

try
{
    connection.Open();
    createTableCommand.ExecuteNonQuery();
    Console.WriteLine("Table created!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    connection.Close();
}
```
### 4. Inserting Data
Now, let’s insert a user into the users table:
```
string insertSql = "INSERT INTO users (name, email) VALUES (@name, @email)";
MySqlCommand insertCommand = new MySqlCommand(insertSql, connection);

// Parameters
insertCommand.Parameters.AddWithValue("@name", "Shital Patil");
insertCommand.Parameters.AddWithValue("@email", "shital@tfl.com");

try
{
    connection.Open();
    int rowsAffected = insertCommand.ExecuteNonQuery();
    Console.WriteLine($"Inserted {rowsAffected} row(s)!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    connection.Close();
}
```

### 5. Querying Data
Let’s retrieve data from the users table:
```
string query = "SELECT * FROM users";
MySqlCommand queryCommand = new MySqlCommand(query, connection);

try
{
    connection.Open();
    using (MySqlDataReader reader = queryCommand.ExecuteReader())
    {
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Email: {reader["email"]}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    connection.Close();
}
```

### 6. Updating Data
```
    string updateSql = "UPDATE users SET email = @newEmail WHERE name = @name";
    MySqlCommand updateCommand = new MySqlCommand(updateSql, connection);

    // Parameters
    updateCommand.Parameters.AddWithValue("@newEmail", "updated@tfl.com");
    updateCommand.Parameters.AddWithValue("@name", "Shital Patil");

    try
    {
        connection.Open();
        int rowsAffected = updateCommand.ExecuteNonQuery();
        Console.WriteLine($"Updated {rowsAffected} row(s)!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    finally
    {
        connection.Close();
    }
```

### 7. Deleting Data
```
    string deleteSql = "DELETE FROM users WHERE name = @name";
    MySqlCommand deleteCommand = new MySqlCommand(deleteSql, connection);

    // Parameter
    deleteCommand.Parameters.AddWithValue("@name", "John Doe");

```