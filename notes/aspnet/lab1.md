# Step-by-step   **ASP.NET Core MVC** app (dotnet CLI) with:

* `HomeController` (actions: `Index`, `AboutUs`, `ContactUs`, `Services`) + views
* `ProductsController` (actions: `Index`, `Details`, `Create`, `Edit`, `Delete`) + views
* Bootstrap navigation in `_Layout.cshtml`
* small examples showing `ViewData` and `ViewBag` usage
* optional notes for using the ASP.NET scaffolder from CLI


# Step 0 — Prerequisites

1. Install .NET SDK (6/7/8/9 — use whichever LTS or newer you prefer).
2. A terminal (Windows PowerShell / macOS Terminal / Linux shell) and a code editor (VS Code).
3. Optional but recommended: install the ASP.NET code generator tool if you want to scaffold from CLI:

   ```bash
   dotnet tool install -g dotnet-aspnet-codegenerator
   ```

   (the code generator is distributed as a .NET global tool). ([Microsoft Learn][1])



# Step 1 — Create the MVC project

Open terminal and run:

```bash
mkdir TransflowerMvc
cd TransflowerMvc
dotnet new mvc -o TransflowerApp
cd TransflowerApp
```

This creates a new MVC project (template `mvc`). ([Microsoft Learn][2])



# Step 2 — (Optional) Add scaffolder support for advanced scaffolding

If you plan to use the CLI scaffolder to auto-generate CRUD views/controllers later, add the design-time package to your project and ensure the global tool is installed:

```bash
# inside TransflowerApp
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet restore

# if you didn't already install the global tool:
dotnet tool install -g dotnet-aspnet-codegenerator
```

The `Microsoft.VisualStudio.Web.CodeGeneration.Design` package is required by the scaffolder. ([NuGet][3])



# Step 3 — Run the app to verify skeleton works

```bash
dotnet run
```

Open `https://localhost:5001` or `http://localhost:5000` (addresses printed by the `dotnet run` output) to see the starter site.

---

# Step 4 — Create `HomeController` (manual, simple)

Create `Controllers/HomeController.cs` with these action methods:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace TransflowerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Welcome to Transflower";
            ViewBag.Message = "Learn with small, practical labs.";
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["Title"] = "About Us";
            ViewBag.Year = 2025;
            return View();
        }

        public IActionResult ContactUs()
        {
            ViewData["Title"] = "Contact Us";
            return View();
        }

        public IActionResult Services()
        {
            ViewData["Title"] = "Services";
            return View();
        }
    }
}
```

Notes: `ViewData` is a dictionary (`ViewData["Title"] = "..."`), `ViewBag` is a dynamic wrapper over `ViewData` (useful for quick small values). Use either depending on preference — both are supported. ([Microsoft Learn][4])


# Step 5 — Create Views for HomeController

Create these Razor views under `Views/Home/`:

1. `Views/Home/Index.cshtml`

```razor
@{
    ViewData["Title"] = ViewData["Title"] ?? "Home";
}
<h1>@ViewData["Title"]</h1>
<p>@ViewBag.Message</p>

<div class="card">
  <div class="card-body">
    <h5 class="card-title">Featured course</h5>
    <p class="card-text">Start with HTML basics and build up.</p>
    <a href="/Products" class="btn btn-primary">See products</a>
  </div>
</div>
```

2. `Views/Home/AboutUs.cshtml`

```razor
@{
    ViewData["Title"] = ViewData["Title"] ?? "About";
}
<h1>@ViewData["Title"]</h1>
<p>Transflower Learning — mission: make practical learning accessible.</p>
<p><strong>Established:</strong> @ViewBag.Year</p>
```

3. `Views/Home/ContactUs.cshtml`

```razor
@{
    ViewData["Title"] = ViewData["Title"] ?? "Contact";
}
<h1>@ViewData["Title"]</h1>
<form method="post" action="#">
  <div class="mb-3">
    <label for="name" class="form-label">Name</label>
    <input id="name" name="name" class="form-control" />
  </div>
  <div class="mb-3">
    <label for="email" class="form-label">Email</label>
    <input id="email" name="email" class="form-control" type="email" />
  </div>
  <button type="submit" class="btn btn-primary">Send</button>
</form>
```

4. `Views/Home/Services.cshtml`

```razor
@{
    ViewData["Title"] = ViewData["Title"] ?? "Services";
}
<h1>@ViewData["Title"]</h1>
<ul class="list-group">
  <li class="list-group-item">Mentoring</li>
  <li class="list-group-item">Hands-on labs</li>
  <li class="list-group-item">Project reviews</li>
</ul>
```


# Step 6 — Add `ProductsController` (manual CRUD skeleton)

Create `Controllers/ProductsController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TransflowerApp.Controllers
{
    public class ProductsController : Controller
    {
        // Temporary in-memory store (for lab/demo)
        private static List<Product> _products = new()
        {
            new Product{ Id=1, Name="Transflower Tee", Price=299 },
            new Product{ Id=2, Name="Transflower Mug", Price=199 },
        };

        public IActionResult Index()
        {
            ViewData["Title"] = "Products";
            return View(_products);
        }

        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (!ModelState.IsValid) return View(model);
            model.Id = _products.Max(p => p.Id) + 1;
            _products.Add(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid) return View(model);
            var existing = _products.FirstOrDefault(p => p.Id == model.Id);
            if (existing == null) return NotFound();
            existing.Name = model.Name;
            existing.Price = model.Price;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null) _products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }

    // Simple product model for the demo
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

This uses an in-memory list for lab/demo; for production, use EF Core + DB. If you prefer scaffolding with EF Core you can generate controllers and views automatically (see Step 11). ([Microsoft Learn][1])


# Step 7 — Create Products views (Razor) — minimal examples

Create `Views/Products/Index.cshtml`:

```razor
@model IEnumerable<TransflowerApp.Controllers.Product>
@{
    ViewData["Title"] = "Products";
}
<h1>@ViewData["Title"]</h1>

<p><a href="/Products/Create" class="btn btn-success">Create New</a></p>

<table class="table table-striped">
<thead>
  <tr><th>ID</th><th>Name</th><th>Price</th><th></th></tr>
</thead>
<tbody>
@foreach(var p in Model)
{
  <tr>
    <td>@p.Id</td>
    <td>@p.Name</td>
    <td>@p.Price</td>
    <td>
      <a class="btn btn-primary btn-sm" href="/Products/Details/@p.Id">Details</a>
      <a class="btn btn-warning btn-sm" href="/Products/Edit/@p.Id">Edit</a>
      <a class="btn btn-danger btn-sm" href="/Products/Delete/@p.Id">Delete</a>
    </td>
  </tr>
}
</tbody>
</table>
```

`Views/Products/Details.cshtml`:

```razor
@model TransflowerApp.Controllers.Product
<h1>Product details</h1>
<dl class="row">
  <dt class="col-sm-2">ID</dt><dd class="col-sm-10">@Model.Id</dd>
  <dt class="col-sm-2">Name</dt><dd class="col-sm-10">@Model.Name</dd>
  <dt class="col-sm-2">Price</dt><dd class="col-sm-10">@Model.Price</dd>
</dl>
<p><a class="btn btn-secondary" href="/Products">Back to list</a></p>
```

`Views/Products/Create.cshtml` and `Edit.cshtml` (similar, use form with Bootstrap classes):

```razor
@model TransflowerApp.Controllers.Product
<h1>Create product</h1>
<form asp-action="Create" method="post">
  <div class="mb-3">
    <label asp-for="Name" class="form-label"></label>
    <input asp-for="Name" class="form-control" />
  </div>
  <div class="mb-3">
    <label asp-for="Price" class="form-label"></label>
    <input asp-for="Price" class="form-control" />
  </div>
  <button type="submit" class="btn btn-primary">Save</button>
</form>
```

`Views/Products/Delete.cshtml`:

```razor
@model TransflowerApp.Controllers.Product
<h1>Delete product</h1>
<p>Are you sure you want to delete <strong>@Model.Name</strong>?</p>
<form asp-action="Delete" method="post">
  <input type="hidden" asp-for="Id" />
  <button type="submit" class="btn btn-danger">Yes, delete</button>
  <a class="btn btn-secondary" href="/Products">Cancel</a>
</form>
```

---

# Step 8 — Add Bootstrap to the project (quickest: CDN)

Open `Views/Shared/_Layout.cshtml` and add Bootstrap CDN references in `<head>` and before `</body>`:

In `<head>` (replace or add stylesheet):

```html
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
```

Before `</body>`:

```html
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
```

Alternatively use **LibMan** (Library Manager) to install Bootstrap into `wwwroot/lib` for local serving (LibMan is the recommended Microsoft client-side library tool). ([Microsoft Learn][5])

Also adjust the layout markup to include a Bootstrap navbar (see next step).

---

# Step 9 — Add Bootstrap navbar / navigation (edit `_Layout.cshtml`)

Replace the default navbar area or insert this (example snippet):

```razor
<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <div class="container">
    <a class="navbar-brand" href="/">Transflower</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNav">
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="mainNav">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item"><a class="nav-link" asp-controller="Home" asp-action="Index">Home</a></li>
        <li class="nav-item"><a class="nav-link" asp-controller="Home" asp-action="AboutUs">About Us</a></li>
        <li class="nav-item"><a class="nav-link" asp-controller="Home" asp-action="Services">Services</a></li>
        <li class="nav-item"><a class="nav-link" asp-controller="Home" asp-action="ContactUs">Contact</a></li>
        <li class="nav-item"><a class="nav-link" asp-controller="Products" asp-action="Index">Products</a></li>
      </ul>
    </div>
  </div>
</nav>
```

This gives you a responsive Bootstrap navigation bar.

---

# Step 10 — Build and run

```bash
dotnet build
dotnet run
```

Visit the site, navigate to `/Home/AboutUs`, `/Home/ContactUs`, `/Home/Services`, and `/Products` to test the pages and CRUD flows.

---

# Step 11 — (Optional) Use scaffolder to generate CRUD with EF Core

If you prefer auto-generating controller + views (scaffold) for a model and a `DbContext`:

1. Add a model class (e.g., `Models/Product.cs`), and set up `ApplicationDbContext` (EF Core) and configure connection string in `appsettings.json`.
2. Add EF Core packages and run migrations.
3. Run the code generator (example — scaffold an MVC controller with EF):

```bash
# Ensure tool & package set up
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

# scaffold
dotnet aspnet-codegenerator controller -name ProductsController -m Product -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout
```

Scaffolding requires the code generator and the design-time package. Use the official generator docs for details and matching versions. ([Microsoft Learn][1])

---

# Step 12 — Quick notes on ViewData vs ViewBag (reference)

* `ViewData` is a `ViewDataDictionary` (keyed dictionary): `ViewData["Title"] = "..."`.
* `ViewBag` is a dynamic wrapper over `ViewData`: `ViewBag.Message = "..."`.
  Both are intended for small pieces of data passed controller → view; use a strongly typed model for larger data. ([Microsoft Learn][4])

---

# Troubleshooting tips

* If `dotnet aspnet-codegenerator` complains, ensure the global tool is installed and your .NET SDK version matches the tool/package versions. You may need to uninstall/reinstall the tool (`dotnet tool uninstall -g dotnet-aspnet-codegenerator` then install). ([Microsoft Learn][1])
* For client libraries prefer LibMan for maintainability rather than direct CDN if you want local copies. ([Microsoft Learn][5])

---
# Lab checklist (what you should have at the end)

* [ ] `TransflowerApp` MVC project created (`dotnet new mvc`)
* [ ] `Controllers/HomeController.cs` with `Index`, `AboutUs`, `ContactUs`, `Services` actions
* [ ] `Controllers/ProductsController.cs` with `Index`, `Details`, `Create`, `Edit`, `Delete` actions
* [ ] Razor views for all actions (`Views/Home/*`, `Views/Products/*`) using Bootstrap classes
* [ ] `_Layout.cshtml` updated with Bootstrap CDN (or LibMan) and a navbar
* [ ] App runs with `dotnet run` and pages accessible in the browser

