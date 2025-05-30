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

 