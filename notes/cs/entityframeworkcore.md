# **Understanding Entity Framework Core (EF Core)**

> *"Class, let me take you on a journey today — a journey from raw SQL to a smarter, modern way of accessing data in .NET applications..."*

## 🌱 The Origin: Why ORM?

*"Imagine you're running a café. You take orders on paper, go into the kitchen, shout the instructions to the chef, and then come back to the counter. It works, but it’s tedious, prone to errors, and wastes time.*

*Now imagine, there’s a digital screen — as soon as you take the order, it flashes in the kitchen. That’s what **ORM** does in programming: it simplifies and automates the communication between your code and the database."*

## 💡 What is Entity Framework Core?

**Entity Framework Core**, or **EF Core**, is like your digital waiter — it takes care of all the back-and-forth between your C# code and your SQL database.

* It was designed to work **cross-platform** (Windows, Mac, Linux).
* It's **open-source**, lightweight, and built for **.NET Core**.
* It maps your **classes (objects)** directly to **database tables** — so instead of writing long SQL queries, you just use C# objects.

## 🔄 ORM: Object-Relational Mapping

Let’s simplify:

* Your class `Student` maps to a database table `Students`.
* Your property `Student.Name` maps to a database column `name`.
* When you call `context.Students.Add()`, EF Core **writes the SQL INSERT** for you.
* When you call `context.SaveChanges()`, it **executes the SQL** on the database.

*You don’t need to be a SQL wizard anymore — just think in objects!*

## 🎯 EF Core in Action: Why Developers Love It

**Productivity** — EF Core cuts down 80% of repetitive SQL work.
**Maintainability** — your code looks clean, and business logic stays in one place.
**Flexibility** — switch between databases (SQL Server, MySQL, PostgreSQL) with minor changes.

## 🛠️ Two Ways to Work with EF Core

Let me tell you two stories — both real-life developer journeys:

### 📘 Code First Approach – *“I’m starting fresh.”*

*"We didn’t have a database yet, just a clear idea of the domain — Students, Departments, and Courses. We wrote C# classes for them, and let EF Core create the database for us using **Migrations**."*

* You write classes.
* You create a `DbContext`.
* EF Core builds the DB for you!

### 🗄️ Database First Approach – *“The database already exists.”*

*"Our team inherited a legacy system. We used EF Core commands to reverse-engineer the DB schema into classes. It saved weeks of manual effort!"*

* You connect to an existing DB.
* EF Core generates your C# classes and `DbContext`.

 
## 🧩 Meet the Hero: `DbContext`

Think of `DbContext` as the **central nervous system** of EF Core:

* It tracks changes (like your To-Do list).
* It talks to the database on your behalf.
* It manages **connections**, **transactions**, and even **relationships** between tables.

```csharp
public class CollectionContext : DbContext {
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string conString = "server=localhost;port=3306;user=root;password=password;database=transflower";
        optionsBuilder.UseMySQL(conString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Department>().ToTable("departments");
        modelBuilder.Entity<Department>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Location).IsRequired();
        });
    }
}
```

 ## 🔧 DbContext in Action – Our CRUD Story

*"Let’s say we’re working in an HR app. We want to store and manage departments."*

Here’s how we use EF Core in our real `DBManager` class:

```csharp
public void Insert(Department dept) {
    using (var context = new CollectionContext()) {
        context.Departments.Add(dept);
        context.SaveChanges();  
    }
}
```

Just add the object. Save. That’s it. EF Core handles the SQL under the hood.

Similarly:

* `GetAll()` uses LINQ to query.
* `Update()` modifies the object.
* `Delete()` removes it.

No SQL queries. Just C# logic.

## ⚙️ EF Core Methods You’ll Love

| Method            | Purpose                               |
| ----------------- | ------------------------------------- |
| `Add/AddAsync`    | Add new records                       |
| `Find/FindAsync`  | Find by primary key                   |
| `Update`          | Update modified records               |
| `Remove`          | Delete records                        |
| `SaveChanges()`   | Save it all to the DB                 |
| `OnConfiguring`   | Set up DB connection                  |
| `OnModelCreating` | Customize how models map to DB schema |

 
## 🔍 One More Thing: EF Core is Smart

* Tracks changes in objects.
* Caches results during one request.
* Supports relationships like **One-to-Many**, **Many-to-Many**.
* Translates LINQ queries to efficient SQL.

 ## 💬 Mentor’s Final Thought

*"When I was a beginner, I used to write raw SQL for every single operation — and guess what? I made mistakes, broke things, and got frustrated. EF Core came as a relief — it lets you focus on your app logic, not boilerplate data access code."*

So if you're planning to build a real-world .NET Core app, **learn EF Core deeply**. It’s a skill every modern .NET developer must master — not just for productivity, but for writing cleaner, scalable applications.

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