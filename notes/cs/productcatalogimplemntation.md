## 🧑‍🏫 **Product Catalog with Dependency Injection (Sync Version)**

Imagine you are a chef running a kitchen, and you're about to cook an amazing dish—**“Product Catalog Management”**.
But remember—every successful dish needs a **recipe, ingredients, tools, and process**.
In our case:

* **Recipe** = Project structure
* **Ingredients** = Classes, Interfaces, DI, CRUD methods
* **Kitchen Tools** = .NET Core Console App, Visual Studio / CLI
* **Final Dish** = Menu-driven Product Catalog



## ✅ **Step 1: Create Solution & Projects**

```
ProductCatalogSolution
│
├── ProductCatalogApp       (Console UI Layer)
├── ProductCatalogLibrary   (Class Library – Business Logic + Models)
```

Using CLI:

```bash
dotnet new sln -n ProductCatalogSolution
dotnet new console -n ProductCatalogApp
dotnet new classlib -n ProductCatalogLibrary

dotnet sln add ProductCatalogApp/ProductCatalogApp.csproj
dotnet sln add ProductCatalogLibrary/ProductCatalogLibrary.csproj

dotnet add ProductCatalogApp/ProductCatalogApp.csproj reference ProductCatalogLibrary/ProductCatalogLibrary.csproj
```



## ✅ **Step 2: Create Product Model**

📂 File: `ProductCatalogLibrary/Models/Product.cs`

```csharp
namespace ProductCatalogLibrary.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
```


## ✅ **Step 3: Define Interfaces (For Dependency Injection)**

📂 File: `ProductCatalogLibrary/Contracts/IProductService.cs`

```csharp
using System.Collections.Generic;
using ProductCatalogLibrary.Models;

namespace ProductCatalogLibrary.Contracts
{
    public interface IProductService
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
```


## ✅ **Step 4: Implement ProductService (Business Logic)**

📂 File: `ProductCatalogLibrary/Services/ProductService.cs`

```csharp
using ProductCatalogLibrary.Contracts;
using ProductCatalogLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalogLibrary.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public List<Product> GetAllProducts() => _products;

        public Product GetProductById(int id) =>
            _products.FirstOrDefault(p => p.Id == id);

        public void UpdateProduct(Product product)
        {
            var existing = GetProductById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Quantity = product.Quantity;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
                _products.Remove(product);
        }
    }
}
```

## ✅ **Step 5: Setup Dependency Injection in Program.cs**

📂 File: `ProductCatalogApp/Program.cs`

```csharp
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogLibrary.Contracts;
using ProductCatalogLibrary.Services;

class Program
{
    static void Main()
    {
        // 1. Create DI Container
        var serviceProvider = new ServiceCollection()
                                .AddSingleton<IProductService, ProductService>()
                                .BuildServiceProvider();

        // 2. Resolve ProductService
        var productService = serviceProvider.GetService<IProductService>();

        // 3. Launch UI Menu
        Menu(productService);
    }

    static void Menu(IProductService productService)
    {
        while (true)
        {
            Console.WriteLine("\n=== Product Catalog Menu ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. List Products");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddProduct(productService); break;
                case "2": ListProducts(productService); break;
                case "3": UpdateProduct(productService); break;
                case "4": DeleteProduct(productService); break;
                case "5": return;
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }
```


## ✅ **Step 6: Implement UI Methods (Add, List, Update, Delete)**

```csharp
static void AddProduct(IProductService service)
{
    Console.Write("Enter ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Enter Name: ");
    string name = Console.ReadLine();

    Console.Write("Enter Price: ");
    double price = double.Parse(Console.ReadLine());

    Console.Write("Enter Quantity: ");
    int qty = int.Parse(Console.ReadLine());

    service.AddProduct(new Product { Id = id, Name = name, Price = price, Quantity = qty });
    Console.WriteLine("Product added!");
}

static void ListProducts(IProductService service)
{
    foreach (var p in service.GetAllProducts())
        Console.WriteLine($"{p.Id} | {p.Name} | {p.Price} | {p.Quantity}");
}

static void UpdateProduct(IProductService service)
{
    Console.Write("Enter Product ID to Update: ");
    int id = int.Parse(Console.ReadLine());

    var existing = service.GetProductById(id);
    if (existing == null)
    {
        Console.WriteLine("Product Not Found!");
        return;
    }

    Console.Write("Enter New Name: ");
    existing.Name = Console.ReadLine();

    Console.Write("Enter New Price: ");
    existing.Price = double.Parse(Console.ReadLine());

    Console.Write("Enter New Quantity: ");
    existing.Quantity = int.Parse(Console.ReadLine());

    service.UpdateProduct(existing);
    Console.WriteLine("Product updated!");
}

static void DeleteProduct(IProductService service)
{
    Console.Write("Enter Product ID to Delete: ");
    int id = int.Parse(Console.ReadLine());

    service.DeleteProduct(id);
    Console.WriteLine("Product deleted!");
}
```

## 🎯 **Why This Approach is Powerful?**

| Benefit                    | Explanation                                            |
| -------------------------- | ------------------------------------------------------ |
| ✅ Loose Coupling           | UI does not depend on concrete class—only on interface |
| ✅ Easy to Test             | You can replace ProductService with MockService        |
| ✅ Follows SOLID Principles | Specifically DIP (Dependency Inversion)                |
| ✅ Clean Architecture       | Separation between UI and Logic                        |

