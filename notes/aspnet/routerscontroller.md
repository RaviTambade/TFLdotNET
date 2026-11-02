# ASP.NET Core – Routing and Controllers

In the previous lesson, we understood Middleware and the HTTP Request Pipeline. Now let us understand an important question:

When a client sends `GET /api/products/101`, how does ASP.NET Core know which C# method should execute?

The answer is Routing and Controllers.

## 1. Understand the concept of Routing

Imagine a customer visiting a shopping mall. The customer wants to visit the Electronics Department. The mall directory helps the customer find the correct department.

Similarly, in ASP.NET Core:

- URL represents the customer's destination.
- Routing identifies the destination.
- Controller handles the request.
- Controller action method performs the requested operation.

```
             CLIENT
               |
               | GET /api/products/101
               |
               v
       ASP.NET CORE APPLICATION
               |
               v
        ENDPOINT ROUTING
               |
               | Match URL + HTTP Verb
               |
               v
       ProductsController
               |
               v
          GetById(101)
               |
               v
         ProductService
               |
               v
       ProductRepository
               |
               v
          products.json
```

## 2. Create a Products Controller

Let's continue with our previous Products Web API project.

Project structure:

```
ProductsWebAPI/
|
+-- Controllers/
|   |
|   +-- ProductsController.cs
|
+-- Models/
|   |
|   +-- Product.cs
|
+-- Services/
|
+-- Repositories/
|
+-- Data/
|   |
|   +-- products.json
|
+-- Program.cs
```

First, create the Product model.

Models/Product.cs

```
namespace ProductsWebAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}
```

## 3. Understand Controller and Action Methods

A Controller is a C# class responsible for handling incoming HTTP requests. An Action Method is a public method inside a controller that handles a particular request.

Controllers/ProductsController.cs

```
using Microsoft.AspNetCore.Mvc;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> products =
    [
        new Product
        {
            Id = 101,
            Name = "Laptop",
            Price = 55000,
            Category = "Electronics"
        },
        new Product
        {
            Id = 102,
            Name = "Keyboard",
            Price = 1500,
            Category = "Accessories"
        }
    ];

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(products);
    }

    // GET: api/products/101
    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = products.Max(p => p.Id) + 1;
        products.Add(product);
        return CreatedAtAction(nameof(GetById),new { id = product.Id }, product );
    }

    // DELETE: api/products/101
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        products.Remove(product);
        return NoContent();
    }
}
```



Learning example

This controller uses an in-memory list to demonstrate routing. Data will reset when the application restarts. In our layered Products API, we will inject the ProductService instead.



## 4. Understand Routing Attributes

Look at these two attributes:

```
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
```

What does `[Route("api/[controller]")]` mean?

`[controller]` is a route token. ASP.NET Core replaces it with the controller name, without the `Controller` suffix.

For example:

```
ProductsController
       |
       v
Controller name = Products
       |
       v
Route = api/products
```

Now understand the action method attributes.

| Attribute                  | HTTP request | Purpose               |
| -------------------------- | ------------ | --------------------- |
| `[HttpGet]`                | GET          | Retrieve all products |
| `[HttpGet("{id:int}")]`    | GET          | Retrieve one product  |
| `[HttpPost]`               | POST         | Create a product      |
| `[HttpPut("{id:int}")]`    | PUT          | Update a product      |
| `[HttpDelete("{id:int}")]` | DELETE       | Delete a product      |

## 5. Understand URL Matching

Suppose the client sends these requests:

| URL                 | HTTP Verb | Controller Action |
| ------------------- | --------- | ----------------- |
| `/api/products`     | GET       | `GetAll()`        |
| `/api/products/101` | GET       | `GetById(101)`    |
| `/api/products`     | POST      | `Create()`        |
| `/api/products/101` | DELETE    | `Delete(101)`     |

The routing engine matches both:

1. The URL pattern.
2. The HTTP method.

For example, `GET /api/products/101` and `DELETE /api/products/101` have the same URL but execute different action methods.

## 6. Understand Model Binding

Here is another important concept. Suppose the client sends:

```
GET /api/products/101
```

ASP.NET Core extracts `101` from the URL and assigns it to the C# parameter `id`. This process is called Model Binding. Model binding can obtain data from different HTTP request locations:

| Attribute      | Data comes from | Example                 |
| -------------- | --------------- | ----------------------- |
| `[FromRoute]`  | URL path        | `/products/101`         |
| `[FromQuery]`  | Query string    | `?category=Electronics` |
| `[FromBody]`   | Request body    | JSON object             |
| `[FromHeader]` | HTTP headers    | `Authorization`         |
| `[FromForm]`   | Form data       | HTML form submission    |

Example:

```
[HttpGet("search")]
public IActionResult Search([FromQuery] string category,[FromQuery] decimal minPrice){
    return Ok(new {Category = category, MinimumPrice = minPrice });
}
```

Request:

```
GET /api/products/search?category=Electronics&minPrice=10000
```

Example of route binding:

```
[HttpGet("{id:int}")]
public IActionResult GetById([FromRoute] int id)
{
    return Ok(new { ProductId = id });
}
```

Example of body binding:

```
[HttpPost]
public IActionResult Create([FromBody] Product product)
{
    return Ok(product);
}
```

With `[ApiController]`, ASP.NET Core generally infers binding sources for common parameter types and automatically returns HTTP 400 for model validation errors. Explicit attributes are useful when you want to make the source clear.

## 7. Understand HTTP Response Codes

A controller does not simply return data. It also communicates the result through HTTP status codes.

| Status code | Meaning               | Example                   |
| ----------- | --------------------- | ------------------------- |
| 200         | OK                    | Product retrieved         |
| 201         | Created               | Product created           |
| 204         | No Content            | Product deleted           |
| 400         | Bad Request           | Invalid request           |
| 404         | Not Found             | Product does not exist    |
| 500         | Internal Server Error | Unexpected server failure |

For example:

```
return Ok(product);       // 200
return NotFound();        // 404
return BadRequest();      // 400
return NoContent();       // 204
```

## 8. Test the API using Postman

Start your application:

```
dotnet run
```

Use the actual HTTP or HTTPS URL displayed in your terminal.

Test 1: Retrieve all products

```
GET https://localhost:7001/api/products
```

Test 2: Retrieve one product

```
GET https://localhost:7001/api/products/101
```

Test 3: Create a product

```
POST https://localhost:7001/api/products
Content-Type: application/json
```

Request body:

```
{
    "name": "Monitor",
    "price": 12000,
    "category": "Electronics"
}
```

Test 4: Delete a product

```
DELETE https://localhost:7001/api/products/101
```

The port may differ on your machine. Check the application startup output.

## 9. Connect Routing with the Layered Architecture

Now let's connect the concepts we have learned.

```
              POSTMAN / REACT / ANGULAR
                         |
                         | HTTP Request
                         v
              ASP.NET CORE MIDDLEWARE
                         |
                         v
                  ENDPOINT ROUTING
                         |
                         v
                 PRODUCTS CONTROLLER
                         |
                         | Action Method
                         v
                  PRODUCT SERVICE
                         |
                         | Business Rules
                         v
                PRODUCT REPOSITORY
                         |
                         | Data Access
                         v
                  JSON / MYSQL DB
                         |
                         v
                  HTTP RESPONSE
```

This is how we separate responsibilities:

- Routing: Which endpoint should execute?
- Controller: What request should be handled?
- Service: What business operation should happen?
- Repository: How should data be accessed?
- Database: Where is the data stored?

## 10. Mentor Classroom Challenge

Ask students to implement the following endpoints:

| Requirement                | Endpoint                                        |
| -------------------------- | ----------------------------------------------- |
| Get all products           | `GET /api/products`                             |
| Get product by ID          | `GET /api/products/101`                         |
| Search by category         | `GET /api/products/search?category=Electronics` |
| Get products above a price | `GET /api/products/price/10000`                 |
| Create product             | `POST /api/products`                            |
| Update product             | `PUT /api/products/101`                         |
| Delete product             | `DELETE /api/products/101`                      |

Think like a developer:

1. Can two action methods have the same route but different HTTP verbs?
2. What happens if the requested product ID does not exist?
3. Why should business logic not be written directly inside a Controller?
4. What is the difference between route parameters and query parameters?

## What comes next?

The next lesson should be Model Binding, DTOs and Data Validation.

Students will learn how JSON from a React or Angular application becomes a C# object, why we should not expose database entities directly, and how to validate incoming API requests before executing business logic.