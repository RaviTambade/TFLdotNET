# Entity Framework

Entity Framework (EF) Core is an ORM (Object-Relational Mapper) Framework for data access in .NET Core. It was released along with .NET Core and is an Extensible, Lightweight, Open Source, and Cross-Platform Version of Entity Framework data access technology. It works on multiple operating systems like Windows, Mac, and Linux.

The Entity Framework is an Object/Relational Mapping (O/RM) framework that maps objects to relational databases. EF Core is designed to work with .NET Core applications but can also be used with standard .NET Framework applications based on Framework 4.5 or higher. The following diagram shows the supported types of applications that we can develop using EF Core.

## What is ORM?
The term ORM stands for Object-Relational Mapper, and it automatically creates classes based on database tables and vice versa is also true. It can also generate the necessary SQL to create the database based on the classes.

As a developer, we mostly work with data-driven applications, and the ORM Framework generates the necessary SQL (to perform the CRUD operation) that the underlying database can understand. So, in simple words, we can say that the ORM Framework eliminates the need for most of the data access code that, as a developer, we generally write.


Entity Framework Core is an ORM Tool that increases the developer’s productivity by reducing the redundant task of doing CRUD operations against a database in a .NET Core Application.

Entity Framework Core (EF Core) can generate the necessary database commands for doing the database CRUD Operation, i.e., it can generate SELECT, INSERT, UPDATE, and DELETE commands for us.
While working with Entity Framework Core, we can perform different operations on the domain objects (basically classes representing database tables) using LINQ to Entities.
Entity Framework Core will generate and execute the SQL Command in the database and then store the results in the instances of your domain objects so you can do different types of operations on the data.


## EF Core Development Approaches:
Entity Framework Core supports two development approaches. They are as follows:

- Code-First Approach
- Database-First Approach

###  EF Core Code First Approach:
In the EF Core Code First Approach, first, we need to create our application domain classes, such as Student, Branch, Address, etc., and a special class (called DBContext Class) that derives from the Entity Framework DbContext class. Then, based on the application domain classes and DBContext class, the EF Core creates the database and related tables. For a better understanding, please have a look at the following diagram.

In the Code-First Approach, the EF Core creates the database and tables using Code-First Migration based on the default conventions and configuration. You can also change the default conventions used to create the database and its related tables. This approach is useful in Domain-Driven Design (DDD), i.e., creating the database tables based on the Domain classes.

### EF Core Database First Approach:
If you have an existing database and database tables are already there, you must use the EF Core Database First Approach. In the database-first approach, the EF Core creates the DBContext and Domain Classes based on the existing database schema using EF Core Command.


### EF Core Database Providers:
The EF Core supports relational and non-relational databases, which is possible due to the database providers. The database providers are available as NuGet Packages. The Database Provider sits between the EF Core and the Database it supports.
The EF Core database provider usually contains the functionality specific to the database it supports.
- Functionality common to all the databases is in the EF Core Component.
- Functionality Specific to a Database, for example, Microsoft SQL Server-Specific - Functionality, is within the SQL Server Provider for EF Core. 

For the complete list of EF Core Database Providers, please visit the following URL.
https://learn.microsoft.com/en-us/ef/core/providers/

### The DbContext class

The DbContext class is an integral part of the Entity Framework. The DBContext Class in Entity Framework Core is used by our application to interact with the underlying database. This class manages the database connection and performs CRUD Operations with the underlying database. The DbContext in Entity Framework Core Performs the following tasks:

Object Set Representation: DbContext represents a session with the database that allows querying and saving of entity classes via DbSet<Entity> properties for each entity type in the model.
- <b>Change Tracking</b>: When retrieving data from the database using the DbContext, the changes made to that data are tracked by the DbContext. If any modifications, the entity’s properties are updated, DbContext keeps track of those changes. This enables EF Core to generate appropriate SQL statements to reflect the database changes accurately.
- <b>Database Operations</b>: Using methods on DbContext, such as SaveChanges(), we can insert, update, or delete records in the database. That means the changes we make to our entities will be updated in the database when we call the SaveChanges() method of the DbContext object.
- <b>Configuration</b>: The DbContext allows for the configuration of the database connection. It also tells how models are mapped to database schemas, caching policies, etc. We need to do this by overriding the OnConfiguring or OnModelCreating methods in our DbContext class (A class that is inherited from the DbContext class).
- <b>Querying</b>: DbContext allows us to use LINQ to construct database queries that are automatically translated into SQL queries based on the database provided and will be executed on the corresponding database.
- <b>Lazy Loading</b>: The DbContext class is also responsible for enabling lazy loading, which automatically loads the related data from the database when the navigation property is being accessed.
- <b>Caching</b>: Remember to use the DbContext to perform first-level caching by default. This means that if you request the same data multiple times while the DbContext instance is active, it will return the cached version of the data instead of querying the database again. This can greatly improve performance if you need to access the same data repeatedly.
- <b>Unit of Work</b>: DbContext acts as a unit of work pattern, batching all database DML operations into a single transaction to ensure data consistency. That means that by using DbContext, we can also implement transactions.
- <b>Connection Management</b>: DbContext manages database connections, i.e., it opens and closes the database connection as and when needed and handles transactions for batch operations. Batch operation means executing multiple SQL Statements.
- <b>Migrations</b>: The DbContext plays an important role in creating and managing database migrations, which are a way to manage database schema changes.
<b>Relationship Management</b>: DbContext manages relationships (one-to-one, one-to-many, and many-to-many) between entities and supports navigation properties for foreign key relationships in the database.
- <b>Database Provider Agnosticism</b>: When using DbContext, your code will be compatible with multiple databases (SQL Server, PostgreSQL, SQLite) regardless of the specific database provider. So, our code will be the same irrespective of the database provider and the backend database.

```
using Microsoft.EntityFrameworkCore;
namespace EFDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public EFCoreDbContext() : base()
        {
        }

        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //use this to configure the contex
        }

        //OnModelCreating() method is used to configure the model using ModelBuilder Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use this to configure the model
        }

        //Adding Domain Classes as DbSet Properties
        public DbSet<Student> Students { get; set; }
        public DbSet<Standard> Standards { get; set; }
    }
}

```
As you can see in the above code, the EFCoreDbContext class is derived from the DbContext class and contains the DbSet<Entity> properties of Student and Standard type. The parameterless constructor of our context class is called the base DbContext Class constructor. It also overrides the OnConfiguring and OnModelCreating methods. We must create an instance of the EFCoreDbContext class to connect to the database and save or retrieve Student or Standard data.

The OnConfiguring() method allows us to select and configure the data source to be used with a context using DbContextOptionsBuilder. In our upcoming article, we will discuss more about this method.

The OnModelCreating() method allows us to configure the model using ModelBuilder Fluent API. Again, in our upcoming article, we will discuss more about this method.


## DbContext Methods in Entity Framework Core:

1. <b>Add</b>: Adds a new entity to DbContext with an Added state and starts tracking it. This new entity data will be inserted into the database when SaveChanges() is called.
2. <b>AddAsync</b>: Asynchronous method for adding a new entity to DbContext with Added state and starts tracking it. This new entity data will be inserted into the database when SaveChangesAsync() is called.
3. <b>AddRange</b>: Adds a collection of new entities to DbContext with Added state and starts tracking it. This new entity data will be inserted into the database when SaveChanges() is called.
4. <b>AddRangeAsync</b>: Asynchronous method for adding a collection of new entities, which will be saved on SaveChangesAsync().
5. <b>Attach</b>: Attaches a new or existing entity to DbContext with an Unchanged state and starts tracking it. In this case, nothing will happen when we call the SaveChanges method, as the entity state is not modified.
6. <b>AttachRange</b>: Attaches a collection of new or existing entities to DbContext with an Unchanged state and starts tracking it. In this case, nothing will happen when we call the SaveChanges method, as the entity state is not modified.
7. <b>Entry</b>: Gets an EntityEntry for the given entity. The entry provides access to change tracking information and operations for the entity.  So, using this method, we can manually set the Entity State, which DbContext will also track.
8. <b>Find</b>: Find an entity with the given primary key values.
9. <b>FindAsync</b>: Asynchronous method for finding an entity with the given primary key values.
10. <b>Remove</b>: It sets the Deleted state to the specified entity, which will delete the data when SaveChanges() is called.
11. <b>RemoveRange</b>: Sets Deleted state to a collection of entities that will delete the data in a single DB round trip when SaveChanges() is called.
12. <b>SaveChanges</b>: Execute INSERT, UPDATE, or DELETE commands to the database for the entities with Added, Modified, or Deleted state.
13. <b>SaveChangesAsync</b>: Asynchronous method of SaveChanges()
14. <b>Set</b>: Creates a DbSet<Entity> that can be used to query and save instances of TEntity.
15. <b>Update</b>: Attaches disconnected entity with Modified state and starts tracking it. The data will be saved when SaveChagnes() is called.
16. <b>UpdateRange</b>: Attaches a collection of disconnected entities with a Modified state and starts tracking it. The data will be saved when SaveChagnes() is called.
17. <b>OnConfiguring</b>: Override this method to configure the database (and other options) for this context. This method is called for each instance of the context that is created.
18. <b>OnModelCreating</b>: Override this method to configure further the model discovered by convention from the entity types exposed in DbSet<Entity> properties on your derived context.
DbContext Properties in Entity Framework Core:
19. <b>ChangeTracker</b>: Provides access to information and operations for entity instances this context is tracking.
20. <b>Database</b>: Provides access to database-related information and operations for this context.
21. <b>Model</b>: Returns the metadata about the shape of entities, the relationships between them, and how they map to the database.


## Example using Entity Framework Core


#### Collection Context class


```
using Microsoft.EntityFrameworkCore;
using BOL;

namespace DAL;
    public class CollectionContext:DbContext{
        public DbSet<Department> Departments {get;set;}
        public CollectionContext(){

        } 
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conString=@"server=localhost;port=3306;user=root; password=password;database=transflower";       
            optionsBuilder.UseMySQL(conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>(entity => 
            {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Location).IsRequired();
            });
            modelBuilder.Entity<Department>().ToTable("departments");
        }
    }

```

#### DBManager with CRUD Operations

```
namespace DAL;
using BOL;
public class DBManager:IDBManager{
    public void Delete(int id)
    {
        using(var context = new CollectionContext())
        {
            context.Departments.Remove(context.Departments.Find(id));
            context.SaveChanges();
        }
    }

    public List<Department> GetAll()
    {
        using (var context = new CollectionContext())
        {
            var departments=from dept in context.Departments select dept;
            return departments.ToList<Department>();
        }
    }

    public Department GetById(int id)
    {
        using (var context = new CollectionContext())
        {
            var dept = context.Departments.Find(id);
            return dept;
        }
    }

    public void Insert(Department dept)
    {
        using (var context = new CollectionContext())
        {
            context.Departments.Add(dept);
            context.SaveChanges();  
        }
    }

    public void Update(Department dept)
    {
        using (var context = new CollectionContext())
        {
            var theDept = context.Departments.Find(dept.Id);
            theDept.Name =dept.Name;
            theDept.Location=dept.Location;
            context.SaveChanges();
        }
    }
}

```