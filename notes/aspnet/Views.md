## ASP.NET Core Razor View 

### **The View – The Face of Your Application**

> **"Imagine you're watching a live orchestra. The musicians (Models) create the music, the conductor (Controller) coordinates everyone, but what the audience actually experiences is the beautiful performance on stage. In ASP.NET Core MVC, that performance is called the View."**


## Learning Objectives

After completing this chapter, you will understand:

* What is a View?
* What is Razor View Engine?
* How Views work with Controllers and Models
* Razor Syntax
* ViewData, ViewBag, TempData and Model
* Layouts
* Partial Views
* View Components
* Tag Helpers
* Forms
* Validation
* Sections
* Shared Views
* Best Practices

### 1. What is a View?

A **View** is the **User Interface (UI)** of an ASP.NET Core MVC application.

It displays information to users and accepts their input.

A View is simply a **.cshtml** file.

```
.cshtml
```

means

* HTML
* CSS
* JavaScript
* C#
* Razor Syntax

all inside one file.

### MVC Relationship

```
        User
          │
          ▼
   +---------------+
   |     View      |
   +---------------+
          ▲
          │
   +---------------+
   |  Controller   |
   +---------------+
          ▲
          │
   +---------------+
   |     Model     |
   +---------------+
```

Flow

```
User
   ↓
Controller
   ↓
Model
   ↓
Controller
   ↓
View
   ↓
User
```

### 2. Where are Views Stored?

```
Project
│
├── Controllers
│      HomeController.cs
│      ProductController.cs
│
├── Models
│
├── Views
│      │
│      ├── Home
│      │      Index.cshtml
│      │      About.cshtml
│      │      Contact.cshtml
│      │
│      ├── Product
│      │      Index.cshtml
│      │      Details.cshtml
│      │      Create.cshtml
│      │
│      └── Shared
│             _Layout.cshtml
│             Error.cshtml
│             _ValidationScriptsPartial.cshtml
```

Convention:

```
Views
   └── ControllerName
           └── ActionName.cshtml
```

Example

```
HomeController

public IActionResult About()
{
    return View();
}
```

ASP.NET automatically searches

```
Views/Home/About.cshtml
```


### 3. Razor View Engine

The Razor View Engine converts

```
HTML + C#
```

into

```
Pure HTML
```

before sending it to the browser.

Browser never sees

```
@Model
@foreach
@if
```

It only receives

```
HTML
```

#### Razor Symbol

Everything starts with

```
@
```

Example

```razor
<h2>Hello @ViewBag.Name</h2>
```

Output

```
Hello Ravi
```

### 4. Anatomy of a Razor View

Example

```razor
@model Student

@{
    ViewData["Title"] = "Student Details";
}

<h2>@Model.Name</h2>

<p>Age : @Model.Age</p>

<p>City : @Model.City</p>
```

Parts

```
@model
```

Specifies model type.

```
@{}
```

C# code block.

```
@Model.Name
```

Displays property.


### 5. Razor Syntax

##### Display Variable

```razor
@DateTime.Now
```

Output

```
02-Aug-2026
```

---

#### Code Block

```razor
@{
    var name="Ravi";
}

<h2>@name</h2>
```

#### If Statement

```razor
@if(Model.Age>=18)
{
    <p>Adult</p>
}
else
{
    <p>Minor</p>
}
```

  

#### Loop

```razor
@foreach(var item in Model)
{
    <p>@item.Name</p>
}
```


#### For Loop

```razor
@for(int i=1;i<=5;i++)
{
    <h3>@i</h3>
}
```

#### Switch

```razor
@switch(Model.Status)
{
    case "Active":
        <p>Green</p>
        break;

    default:
        <p>Gray</p>
        break;
}
```


### 6. Passing Data to Views

#### Method 1 : ViewData

Controller

```csharp
ViewData["Name"]="Ravi";
```

View

```razor
<h2>@ViewData["Name"]</h2>
```

#### Method 2 : ViewBag

Controller

```csharp
ViewBag.City="Pune";
```

View

```razor
<p>@ViewBag.City</p>
```

#### Method 3 : TempData

Used across requests.

Controller

```csharp
TempData["Message"]="Saved Successfully";
```

View

```razor
<h3>@TempData["Message"]</h3>
```

#### Method 4 : Model (Recommended)

Controller

```csharp
Student student=new Student()
{
    Id=1,
    Name="Ravi",
    City="Pune"
};

return View(student);
```

View

```razor
@model Student

<h2>@Model.Name</h2>
```


#### Comparison

| Feature         | ViewData   | ViewBag    | TempData   | Model |
| --------------- | ---------- | ---------- | ---------- | ----- |
| Type Safe       | ❌        | ❌          | ❌          | ✅     |
| Object          | Dictionary | Dynamic    | Dictionary | Class |
| Across Redirect | ❌          | ❌          | ✅          | ❌     |
| Recommended     | Small Data | Small Data | Messages   | Yes   |


### 7. Strongly Typed Views

Model

```csharp
public class Student
{
    public int Id {get;set;}

    public string Name {get;set;}

    public string City {get;set;}
}
```

Controller

```csharp
public IActionResult Details()
{
    Student student=new Student();

    student.Id=1;
    student.Name="Ravi";
    student.City="Pune";

    return View(student);
}
```

View

```razor
@model Student

<h2>@Model.Name</h2>

<p>@Model.City</p>
```

Advantages

* Compile-time checking
* IntelliSense
* Easier maintenance
* Cleaner code

### 8. List View Example

Controller

```csharp
List<Student> students=new List<Student>();

return View(students);
```

View

```razor
@model List<Student>

<table>

@foreach(var student in Model)
{
<tr>

<td>@student.Id</td>

<td>@student.Name</td>

<td>@student.City</td>

</tr>
}

</table>
```

#### 9. Layout Page

Instead of repeating

```
Header

Menu

Footer
```

on every page,

ASP.NET uses

```
_Layout.cshtml
```

```
 ____________________________

 Header

 Navigation

 ----------------------------

 @RenderBody()

 ----------------------------

 Footer

_____________________________
```

Layout

```razor
<body>

<header>

</header>

<nav>

</nav>

<div>

@RenderBody()

</div>

<footer>

</footer>

</body>
```

### 10. Partial Views

Suppose Login Box appears everywhere. Instead of copying HTML,

Create

```
_LoginPartial.cshtml
```

Use

```razor
<partial name="_LoginPartial"/>
```

Benefits

* Reusable
* Maintainable
* Smaller Views

### 11. View Components

Partial View

```
Only UI
```

View Component

```
UI + Business Logic
```

Example

```
Shopping Cart

Notification Panel

Latest Products

Weather Widget
```


### 12. Tag Helpers

Old HTML

```html
<input type="text" name="Name"/>
```

ASP.NET Core

```razor
<input asp-for="Name"/>
```

Generates

```html
<input id="Name" name="Name"/>
```

Other Tag Helpers

```razor
<form asp-action="Create">
<a asp-controller="Home" asp-action="Index">
<label asp-for="Name">
<select asp-for="City">
<span asp-validation-for="Name">
```

Benefits

* IntelliSense
* Cleaner HTML
* Strong typing
* Automatic URL generation

### 13. Forms

Example

```razor
<form asp-action="Create">
    <label asp-for="Name"></label>
    <input asp-for="Name"/>
    <button type="submit">Save</button>
</form>
```

### 14. Validation

```razor
<span asp-validation-for="Name"></span>
```

Shows

```
Name is required.
```

without writing JavaScript.

### 15. Sections

Layout

```razor
@RenderSection("Scripts",required:false)
```

View

```razor
@section Scripts{
<script>
    alert("Hello");
</script>
}
```

### 16. HTML Encoding

Razor automatically protects against Cross-Site Scripting (XSS).

```razor
@Model.Name
```

If value is

```
<script>alert('Hack')</script>
```

Browser displays it as text instead of executing it. To intentionally render raw HTML (only with trusted content):

```razor
@Html.Raw(Model.Description)
```

Use this carefully, as it bypasses HTML encoding.

### 17. View Lifecycle

```
User
   │
   ▼
Controller Action
   │
   ▼
Model Created
   │
   ▼
return View(model)
   │
   ▼
Razor Engine
   │
   ▼
HTML Generated
   │
   ▼
Browser
```

# 18. Best Practices

- ✅ Use Strongly Typed Views

```razor
@model Product
```

instead of ViewBag whenever possible.

- ✅ Keep Business Logic in Controller or Services, not in Views.

Bad

```razor
@if(totalSalary>50000)
```

Good

```razrazor
@if(Model.IsEligible)
```

- ✅ Use Partial Views for reusable UI.
- ✅ Use Layouts.
- ✅ Use Tag Helpers.
- ✅ Keep Views simple.


### Complete Example

#### Controller

```csharp
public IActionResult Details()
{
    Student student = new Student
    {
        Id = 1,
        Name = "Ravi",
        City = "Pune",
        Age = 24
    };

    return View(student);
}
```

#### Details.cshtml

```razor
@model Student

@{
    ViewData["Title"] = "Student Details";
}

<h1>Student Details</h1>

<table class="table table-bordered">
    <tr>
        <th>Id</th>
        <td>@Model.Id</td>
    </tr>

    <tr>
        <th>Name</th>
        <td>@Model.Name</td>
    </tr>

    <tr>
        <th>City</th>
        <td>@Model.City</td>
    </tr>

    <tr>
        <th>Age</th>
        <td>@Model.Age</td>
    </tr>
</table>

@if (Model.Age >= 18)
{
    <div class="alert alert-success"> Eligible for Placement </div>
}
else
{
    <div class="alert alert-warning"> Not Eligible </div>
}

<a asp-controller="Student" asp-action="Index" class="btn btn-primary">  Back to List </a>
```

## View Folder Structure for a Student Module

```
Views
│
├── Student
│      Index.cshtml
│      Details.cshtml
│      Create.cshtml
│      Edit.cshtml
│      Delete.cshtml
│
├── Shared
│      _Layout.cshtml
│      _ValidationScriptsPartial.cshtml
│      Error.cshtml
│      _StudentCard.cshtml
│
└── _ViewImports.cshtml
└── _ViewStart.cshtml
```

* **`_ViewImports.cshtml`** imports namespaces and enables Tag Helpers globally.
* **`_ViewStart.cshtml`** sets the default layout for all views.
* **`_Layout.cshtml`** defines the common page structure.
* **Partial Views** (such as `_StudentCard.cshtml`) provide reusable UI components.


### Summary from Your Transflower Mentor

> **"A View is where software engineering meets user experience. Models hold the data, Controllers decide what should happen, but Views are what users remember because they interact with them every day. Master Razor syntax, strongly typed views, layouts, partial views, and Tag Helpers. When your Views are clean, reusable, and maintainable, your entire ASP.NET Core MVC application becomes easier to build, test, and evolve."**