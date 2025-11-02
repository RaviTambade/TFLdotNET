# 🎓 ** Building a Product Catalog with EF Core & MySQL**

### **📍 Scene 1: The Vision — A Digital Store Begins**

*Mentor walks into the lab where students are gathered.*

**Mentor:**
"Imagine you're opening a small online shop — a place where you sell books, gadgets, or handmade crafts. You need a system that can store product details like name, price, quantity, and update them when items are sold. That's our mission today.
And guess what? You're going to build it yourself using .NET, Entity Framework Core, and a MySQL database."

### **📍 Scene 2: Laying the Foundation (Creating the Project)**

**Mentor:**
“First things first — we need a home for our application.”

So we open the terminal and type:

```bash
mkdir ProductCatalog
cd ProductCatalog
dotnet new webapi -n ProductCatalog.Api
cd ProductCatalog.Api
```

*Mentor smiles:*
"This builds the skeleton of your store’s backend — the hallways and rooms are ready; now we bring in the furniture."


### **📍 Scene 3: Teaching .NET to Speak to MySQL (EF Core Setup)**

**Mentor:**
“A store needs a manager who knows how to talk to the database. That manager is Entity Framework Core (EF Core). But EF Core doesn't speak MySQL by default. So we teach it.”

We install the necessary packages:

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

“And we also give ourselves a tool to create database tables with commands — like magic.”

```bash
dotnet tool install --global dotnet-ef
```

### **📍 Scene 4: Connecting the Store to the Database (appsettings.json)**

**Mentor:**
“Now, imagine the store needs to know the address of the warehouse (database). That address is called a *connection string*.”

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=ProductCatalogDb;user=root;password=YourPassword;"
  }
}
```

This is like writing the warehouse address on a sticky note and giving it to the system.

### **📍 Scene 5: Designing the Product (Creating the Model)**

**Mentor:**
“Let’s create a blueprint — what exactly is a product?”

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

"This class is just like a product form you fill — name, price, quantity. EF Core will transform this into a table in MySQL."


### **📍 Scene 6: The Bridge Between C# and SQL (DbContext)**

**Mentor:**
“Now your cashier needs a supervisor — someone who tracks all tables and saves changes to the database. That’s the `DbContext`.”

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
```

*Student smiles:*
“So, this is like EF Core’s control room?”
**Mentor:** “Exactly.”

### **📍 Scene 7: Wiring Everything Together (Program.cs)**

Just like plugging in all the cables before turning on a computer.

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
```

*Mentor:* “Now .NET knows how to connect to MySQL and which context to use.”


### **📍 Scene 8: Bringing the Store to Life (Migrations)**

*Mentor leans forward:*
“Time for magic. You’ve designed your product in C#. Now tell EF Core to build the same structure in MySQL.”

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Boom! 🚀**
Your `Products` table appears in the MySQL database — like a carpenter building your first shelf.


### **📍 Scene 9: Opening the Doors — Exposing APIs**

To sell or update products, you need endpoints — like counters at your store.

```csharp
[HttpPost] // Add a product
[HttpGet]  // Get all products
[HttpGet("{id}")] // Get one product
[HttpPut("{id}")] // Update product
[HttpDelete("{id}")] // Delete product
```

Use Swagger → test the API → products begin appearing in your database.


### **📍 Scene 10: Mentor’s Final Words**

**Mentor:**
“Today, you didn’t just write code.
You **built a real-world system** — a store that talks to MySQL, stores product data, and serves APIs to the frontend or mobile apps.”

🛠️ You learned:
- ✅ Creating a .NET Web API
- ✅ Installing EF Core + MySQL provider
- ✅ Connection strings and DbContext
- ✅ Creating models and migrations
- ✅ Building CRUD API for Products

 