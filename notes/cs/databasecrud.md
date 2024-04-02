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

## Connected Data Access Mode
In the case of Connection Oriented Data Access Architecture, an open and active connection is always required between the .NET Application and the database. 

An example is Data Reader, and when we are accessing the data from the database, the Data Reader object requires an active and open connection to access the data. If the connection is closed, we cannot access the data from the database; in that case, we will get the runtime error.

```
namespace DAL.Connected;
using BOL;
using MySql.Data.MySqlClient;

public class DBManager{

    public static string conString=@"server=localhost;port=3306;user=root; password=password;database=transflower";       
    public  static List<Department> GetAllDepartments(){
            List<Department> allDepartments=new List<Department>();
            MySqlConnection con=new MySqlConnection();
            con.ConnectionString=conString;
            string query = "SELECT * FROM departments";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                con.Open();        
                cmd.CommandText=query;
                MySqlDataReader reader=cmd.ExecuteReader();
                while(reader.Read()){
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string location = reader["location"].ToString();
                    Department dept=new Department{
                                                    Id = id,
                                                    Name = name,
                                                    Location = location
                    };
                    allDepartments.Add(dept);
                }
            }
            catch(Exception ee){
                Console.WriteLine(ee.Message);
            }
            finally{
                    con.Close();
            }

            return allDepartments;
    }

    public static Department GetDeparmentDetails(int id){
        Department dept = null;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "SELECT * FROM departments WHERE id=" + id;
            con.Open();
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = int.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                string location = reader["location"].ToString();
                dept = new Department
                {
                    Id = id,
                    Name = name,
                    Location = location
                };
            }

        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return dept;
    }

    //Employee Operations CRUD
    public static bool Insert(Department dept){
        bool status=false;
        string query = "INSERT INTO departments(name,location)" +
                            "VALUES('" + dept.Name + "','" + dept.Location + "')";

        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try{
            con.Open();
            MySqlCommand command = new MySqlCommand(query, con);
            command.ExecuteNonQuery();  //DML
            status = true;
        } 
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }               
        return status;
     }
    public static bool Update(Department dept)
    {
        bool status = false;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "UPDATE departments SET location='" + dept.Location + "', name='" + dept.Name + "' WHERE id=" + dept.Id;
            MySqlCommand command = new MySqlCommand(query, con);
            con.Open();
            command.ExecuteNonQuery();
            status = true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }
    public static bool Delete(int id){
        bool status=false;
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            string query = "DELETE FROM departments WHERE id=" + id;
            MySqlCommand command = new MySqlCommand(query, con);
            con.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
      return status;
    }


    //Data Analytics functions
    public static bool DoesEmployeeExists(int id)
    {
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        bool status = false;
        try
        {
            string query = "SELECT EXISTS (SELECT * FROM employees where id=" + id + ")";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if ((Int64)reader[0] == 1)
            {
                status = true;
            }

            reader.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
        }
        return status;
    }
   

    
    }
```


## Disconnected Data Access Mode
When you use disconnected data access, you keep a copy of your data in memory using the DataSet. You connect to the database just long enough to fetch your data and dump it into the DataSet, and then you disconnect immediately. 
DataAdapter and DataSet are two major classes , we do used in disconnected Data Access using .net

```
namespace DAL.DisConnected;
using BOL;
using System.Data;
using MySql.Data.MySqlClient;
public class DBManager{

    public static string conString=@"server=localhost;port=3306;user=root; password=password;database=ecommerce";       
    public  static List<Department> GetAllDepartments(){
        List<Department> allDepartments=new List<Department>();
        MySqlConnection con=new MySqlConnection();
        con.ConnectionString=conString;
        try{
            DataSet ds=new DataSet();  //empty data set
            MySqlCommand cmd=new MySqlCommand();
            cmd.Connection=con;
            string query="SELECT * FROM departments; SELECT * FROM employees; SELECT * FROM roles";
            cmd.CommandText=query;
            //disconnected Data Access logic
            MySqlDataAdapter da=new MySqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);  // this method would fetch data from backend mysql and 
                          //fill results into dataset collection
                          //deal with data which has been fetched from back end
            
            DataTable dt=ds.Tables[0];
            DataRowCollection rows=dt.Rows;
            foreach( DataRow row in rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                string location = row["location"].ToString();
                Department dept=new Department{
                                                Id = id,
                                                Name = name,
                                                Location = location
                };
                allDepartments.Add(dept);
            }
        }
        catch(Exception ee){
                Console.WriteLine(ee.Message);
        }
        return allDepartments;
    }

    public static List<Product> GetAllProducts(){
        List<Product> products=new List<Product> ();
        MySqlConnection con=new MySqlConnection();
        con.ConnectionString=conString;
        try{
            DataSet ds=new DataSet();  //empty data set
            MySqlCommand cmd=new MySqlCommand();
            cmd.Connection=con;
            string query="SELECT * FROM products";
            cmd.CommandText=query;
            //disconnected Data Access logic
            MySqlDataAdapter da=new MySqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);  // this method would fetch data from backend mysql and 
                          //fill results into dataset collection
                          //deal with data which has been fetched from back end
            
            DataTable dt=ds.Tables[0];
            DataRowCollection rows=dt.Rows;
            foreach( DataRow row in rows)
            {
                int id = int.Parse(row["product_id"].ToString());
                string title = row["product_title"].ToString();
                string description = row["description"].ToString();
                Product product=new Product{
                                                Id = id,
                                                Title = title,
                                                Description = description
                };
                products.Add(product);
            }
        }
        catch(Exception ee){
                Console.WriteLine(ee.Message);
        }
        return products;
    }
}
```