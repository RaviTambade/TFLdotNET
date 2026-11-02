# ASP.NET Core Web API

Transflower Mentor Classroom | Hands-on CRUD Lab

The objective is to help students understand how to build an ASP.NET Core Web API using the Minimal API approach, without introducing unnecessary complexity such as MVC Controllers, Entity Framework Core, or a database in the first lesson.

We will build a simple Product Management API.

Students will learn:

1. What is an HTTP API?
2. How ASP.NET Core Minimal APIs work.
3. How HTTP methods map to CRUD operations.
4. How to store products in a JSON file.
5. How JSON serialization and deserialization work.
6. How to test APIs using Swagger or Postman.

## 1. Start with the real-world problem

Imagine Transflower has a small product catalogue.

We want to perform the following operations:

| Operation         | HTTP Method | API Endpoint      |
| ----------------- | ----------- | ----------------- |
| Get all products  | GET         | `/api/products`   |
| Get product by ID | GET         | `/api/products/1` |
| Add product       | POST        | `/api/products`   |
| Update product    | PUT         | `/api/products/1` |
| Delete product    | DELETE      | `/api/products/1` |

Initially, we don't need MySQL or SQL Server.

We will use a simple JSON file as our data store.

## 2. Understand the architecture

```
             CLIENT APPLICATION
             (Browser / React /
                Postman)
                    |
                    | HTTP Request
                    v
        ASP.NET CORE WEB API
        -----------------------
        Minimal API Endpoints
        -----------------------
        GET    /api/products
        GET    /api/products/{id}
        POST   /api/products
        PUT    /api/products/{id}
        DELETE /api/products/{id}
                    |
                    v
             PRODUCT LOGIC
                    |
                    v
          JSON SERIALIZATION
          -------------------
          C# Objects <--> JSON
          -------------------
                    |
                    v
              products.json
              (File Storage)
```

Mentor's explanation:

Think of the Web API as a receptionist.

- Client asks for products.
- API receives the request.
- API reads the JSON file.
- JSON data is converted into C# objects.
- API returns the response in JSON format.

When a client adds or modifies a product, the API saves the updated collection back into the JSON file.

## 3. Create the ASP.NET Core project

Open terminal or VS Code.

```
dotnet new web -n ProductApi
cd ProductApi
code .
```

This creates a lightweight ASP.NET Core application.

The project structure:

```
ProductApi/
│
├── Program.cs
├── ProductApi.csproj
│
└── products.json
```

We will keep most of our implementation in `Program.cs` to demonstrate the Minimal API strategy.

## 4. Create the JSON database

Create a file named `products.json` in the project root.

Add the following data:

products.json

```
[
  {
    "id": 1,
    "name": "Laptop",
    "price": 55000,
    "category": "Electronics"
  },
  {
    "id": 2,
    "name": "Keyboard",
    "price": 1500,
    "category": "Accessories"
  },
  {
    "id": 3,
    "name": "Monitor",
    "price": 12000,
    "category": "Electronics"
  }
]
```

Explain to students:

This JSON file acts like a very simple database table.

- Each JSON object represents one product record.
- Each property represents a column.
- The `id` represents the unique identifier.

## 5. Understand JSON Serialization

Before writing the API, understand two important operations.

Serialization

C# Object → JSON

```
string json = JsonSerializer.Serialize(products);
```

We convert a collection of C# objects into JSON text so that it can be stored in a file.

Deserialization

JSON → C# Object

```
List<Product> products =
    JsonSerializer.Deserialize<List<Product>>(json);
```

We convert JSON text back into a collection of C# objects so that we can work with it in our application.

Think about it:

```
         C# APPLICATION
               |
               | Serialize()
               v
          JSON STRING
               |
               | File.WriteAllTextAsync()
               v
           products.json


           products.json
               |
               | File.ReadAllTextAsync()
               v
          JSON STRING
               |
               | Deserialize()
               v
          C# OBJECTS
```

ASP.NET Core uses `System.Text.Json`, which is part of the modern .NET platform.

## 6. Build the complete Minimal API

Open `Program.cs` and write the following code.

```

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// JSON file location
string filePath = Path.Combine(app.Environment.ContentRootPath,"products.json");

// JSON serialization settings
var options = new JsonSerializerOptions{ WriteIndented = true};

// Read products from JSON file
async Task<List<Product>> ReadProducts()
{
    if (!File.Exists(filePath))
    {
        return new List<Product>();
    }

    string json = await File.ReadAllTextAsync(filePath);

    return JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
}

// Save products into JSON file
async Task SaveProducts(List<Product> products)
{
    string json = JsonSerializer.Serialize(products, options);
    await File.WriteAllTextAsync(filePath, json);
}

// ----------------------------------
// GET: Retrieve all products
// ----------------------------------
app.MapGet("/api/products", async () =>
{
    var products = await ReadProducts();

    return Results.Ok(products);
});

// ----------------------------------
// GET: Retrieve product by ID
// ----------------------------------
app.MapGet("/api/products/{id:int}", async (int id) =>
{
    var products = await ReadProducts();

    var product = products.FirstOrDefault(p => p.Id == id);

    if (product == null)
        return Results.NotFound();

    return Results.Ok(product);
});

// ----------------------------------
// POST: Add a new product
// ----------------------------------
app.MapPost("/api/products", async (Product product) =>
{
    var products = await ReadProducts();

    if (string.IsNullOrWhiteSpace(product.Name))
        return Results.BadRequest("Product name is required.");

    if (product.Price < 0)
        return Results.BadRequest("Price cannot be negative.");

    product.Id = products.Count == 0
        ? 1
        : products.Max(p => p.Id) + 1;

    products.Add(product);

    await SaveProducts(products);

    return Results.Created(
        $"/api/products/{product.Id}", product
    );
});

// ----------------------------------
// PUT: Update an existing product
// ----------------------------------
app.MapPut("/api/products/{id:int}", async (
    int id, Product updatedProduct) =>
{
    var products = await ReadProducts();

    var product = products.FirstOrDefault( p => p.Id == id);

    if (product == null)
        return Results.NotFound();

    if (string.IsNullOrWhiteSpace(updatedProduct.Name))
        return Results.BadRequest("Product name is required.");

    if (updatedProduct.Price < 0)
        return Results.BadRequest("Price cannot be negative.");

    product.Name = updatedProduct.Name;
    product.Price = updatedProduct.Price;
    product.Category = updatedProduct.Category;

    await SaveProducts(products);

    return Results.Ok(product);
});

// ----------------------------------
// DELETE: Remove a product
// ----------------------------------
app.MapDelete("/api/products/{id:int}", async (int id) =>
{
    var products = await ReadProducts();

    var product = products.FirstOrDefault(p => p.Id == id);

    if (product == null)
        return Results.NotFound();

    products.Remove(product);

    await SaveProducts(products);

    return Results.Ok("Product deleted successfully.");
});

app.Run();

// ----------------------------------
// Product Model
// ----------------------------------
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Category { get; set; } = "";
}

```

## 7. Understand the execution sequence

The most important teaching point is the relationship between HTTP methods, C# methods and file operations.

```
                  CLIENT
                    |
                    |
             HTTP REQUEST
                    |
                    v
             MINIMAL API
                    |
          +---------+---------+
          |         |         |
         GET       POST      PUT/DELETE
          |         |         |
          v         v         v
       Read      Read       Read
       JSON      JSON       JSON
          |         |         |
          v         v         v
       Convert   Convert    Convert
       to List   to List    to List
          |         |         |
          v         v         v
       Return    Add New    Modify/
       Data      Product    Remove
                    |         |
                    +----+----+
                         |
                         v
                  Serialize()
                         |
                         v
                  Save JSON File
                         |
                         v
                   HTTP RESPONSE
```

## 8. Test the CRUD operations

Run the application:

```
dotnet run
```

Assume the application is running at:

`http://localhost:5000`

Use the actual URL and port displayed by `dotnet run`.

### Test 1 – GET all products

```
GET http://localhost:5000/api/products
```

Expected response:

```
[
  {
    "id": 1,
    "name": "Laptop",
    "price": 55000,
    "category": "Electronics"
  }
]
```

### Test 2 – GET product by ID

```
GET http://localhost:5000/api/products/2
```

### Test 3 – POST a new product

```
POST http://localhost:5000/api/products
Content-Type: application/json
```

Request body:

```
{
  "name": "Mouse",
  "price": 800,
  "category": "Accessories"
}
```

Expected response: HTTP `201 Created`.

### Test 4 – PUT update product

```
PUT http://localhost:5000/api/products/2
Content-Type: application/json
```

Request body:

```
{
  "name": "Mechanical Keyboard",
  "price": 2500,
  "category": "Accessories"
}
```

Expected response: HTTP `200 OK`.

### Test 5 – DELETE product

```
DELETE http://localhost:5000/api/products/3
```

Expected response: HTTP `200 OK`.

After POST, PUT or DELETE, open `products.json`. Students can observe the changes directly in the file.

## 9. Map CRUD to HTTP concepts

This is an important classroom discussion.

| CRUD   | HTTP   | Meaning          | C# operation        |
| ------ | ------ | ---------------- | ------------------- |
| Create | POST   | Add new record   | `products.Add()`    |
| Read   | GET    | Retrieve records | `FirstOrDefault()`  |
| Update | PUT    | Modify record    | Property assignment |
| Delete | DELETE | Remove record    | `products.Remove()` |

Mentor asks students:

Why do we need different HTTP methods?

Because an API is not just a collection of URLs. It represents operations on resources.

For example:

- GET should retrieve information.
- POST should create information.
- PUT should update information.
- DELETE should remove information.

## 10. Important JSON configuration

One small but important detail: our initial JSON uses camelCase properties (`id`, `name`), whereas C# uses PascalCase (`Id`, `Name`).

Update the serialization settings in `Program.cs`:

```
var options = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
```

This ensures consistent JSON property naming.

## 11. Learning progression – From File to Database

Once students understand this implementation, gradually introduce better storage strategies.

## Evolution of Product API

1. Level 1 – JSON File Storage

   Current Lab

   Minimal API + List of Products + JSON Serialization
2. Level 2 – In-memory Storage

   `List<Product>` as application memory storage.
3. Level 3 – MySQL Connectivity

   ASP.NET Core API + ADO.NET + MySQL CRUD.
4. Level 4 – Entity Framework Core

   ASP.NET Core API + DbContext + LINQ + MySQL.
5. Level 5 – Enterprise API

   Controllers + Services + Repository + Dependency Injection + Authentication + Logging.

## 12. Classroom assignments

Ask students to extend the existing Product API.

1. Implement a search API using product name.
2. Implement filtering products by category.
3. Implement an API to find products within a price range.
4. Implement an API to calculate the total value of all products.
5. Prevent duplicate product names.
6. Add product stock quantity.
7. Implement an API to retrieve products sorted by price.

## 13. Important mentor discussion: Is JSON file storage production-ready?

Not necessarily.

This implementation is for learning serialization, HTTP methods and Minimal APIs.

It has limitations:

- Concurrent requests can overwrite each other's changes.
- There is no transaction management.
- There is no database indexing.
- There is no multi-user data consistency.
- A file corruption can affect the entire dataset.

For production systems, use a proper database and appropriate concurrency, validation and persistence mechanisms.

## 14. Final classroom takeaway

The five concepts students should remember

1. Minimal API – Exposes HTTP endpoints with minimal ceremony.
2. HTTP – Defines communication between client and server.
3. C# Objects – Represent business data.
4. JSON Serialization – Converts objects into JSON and vice versa.
5. File I/O – Persists application data.

First understand the data flow. Then understand the framework.

Ravi Sir's classroom question:

"Today we stored products in a JSON file. Tomorrow, if I replace `products.json` with MySQL, should my React frontend know about this change?"

Expected answer: Ideally, NO!

The frontend communicates with the API contract. The backend can change its storage implementation without changing the API contract.

That is the beginning of understanding Separation of Concerns and Software Architecture.