# 🧪 Hands-On Lab (Part 2)

Below is a **continuation lab** designed **on top of your existing ECommerce solution**, written in a **step-by-step mentor style**, exactly suitable for **students learning real-world .NET development using CLI**.


## Adding a Web App with Multiple `.cshtml` Pages to the ECommerce Solution

> **Continuation of previous lab**
> Existing projects:
>
> * `Cataog` → Class Library (Domain / Business)
> * `Test` → Console App (Runner)
> * `ECommerce.sln`


## 🎯 Lab Objective

Students will learn how to:

* Add an **ASP.NET Core Web App** to an existing solution
* Create **multiple Razor (.cshtml) pages**
* Use **shared layout**
* Reference **Catalog class library**
* Run and navigate a **multi-page web application**


## 🧠 Architecture After This Lab

```
ECommerce Solution
│
├── Cataog        → Business / Domain Layer
├── Test          → Console Client
└── WebApp        → Web UI (Razor Pages)
```

## 🛠 Prerequisites

* Previous lab completed successfully
* .NET SDK installed
* Basic HTML knowledge

## 📁 Step 1: Go to Solution Root

```cmd
D:
cd tryout\SeniorsTrg\ECommerce
```

Confirm solution file exists:

```cmd
dir *.sln
```

## 🌐 Step 2: Create ASP.NET Core Web App (Razor Pages)

Create a web app named **WebApp**:

```cmd
dotnet new webapp -o WebApp
```

📌 **Why Razor Pages?**

* Simple
* Page-focused
* Best for beginners
* Uses `.cshtml` pages directly


## ➕ Step 3: Add WebApp to Solution

```cmd
dotnet sln add .\WebApp\WebApp.csproj
```

✅ Now solution contains **three projects**

## 🔗 Step 4: Add Reference to Catalog Project

Navigate to WebApp:

```cmd
cd WebApp
```

Add reference:

```cmd
dotnet add reference ..\Cataog\Cataog.csproj
```

📌 **Why?**
Web pages can now use `Product`, `Catalog`, and business models.


## 📄 Step 5: Understand Default Razor Structure

Inside `WebApp`:

```
Pages
│
├── Index.cshtml
├── Privacy.cshtml
├── Shared
│   └── _Layout.cshtml
```

📌 **Key concept:**
Each `.cshtml` file = **one web page**

## 🧾 Step 6: Create New Razor Pages

Go to `Pages` folder:

```cmd
cd Pages
```

Create new pages:

```cmd
dotnet new page -n AboutUs
dotnet new page -n Contact
dotnet new page -n Services
```

📁 Result:

```
Pages
├── AboutUs.cshtml
├── AboutUs.cshtml.cs
├── Contact.cshtml
├── Contact.cshtml.cs
├── Services.cshtml
├── Services.cshtml.cs
```

📌 **Explanation**

* `.cshtml` → UI (HTML + Razor)
* `.cshtml.cs` → PageModel (C# logic)

## ✏ Step 7: Add Content to Pages

### 🔹 AboutUs.cshtml

```html
@page
@model WebApp.Pages.AboutUsModel

<h1>About Us</h1>
<p>
    We are an E-Commerce platform providing quality products at affordable prices.
</p>
```

### 🔹 Contact.cshtml

```html
@page
@model WebApp.Pages.ContactModel

<h1>Contact Us</h1>
<p>Email: support@ecommerce.com</p>
<p>Phone: +91-9999999999</p>
```

### 🔹 Services.cshtml

```html
@page
@model WebApp.Pages.ServicesModel

<h1>Our Services</h1>
<ul>
    <li>Online Shopping</li>
    <li>Fast Delivery</li>
    <li>Customer Support</li>
</ul>
```

## 🧭 Step 8: Update Navigation Menu

Open:

```
Pages/Shared/_Layout.cshtml
```

Find `<ul class="navbar-nav">` and add:

```html
<li class="nav-item">
    <a class="nav-link text-dark" asp-page="/AboutUs">About Us</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-page="/Services">Services</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-page="/Contact">Contact</a>
</li>
```

📌 **Why?**
This enables **top menu navigation** across all pages.


## 🏗 Step 9: Build the Solution

Go back to solution root:

```cmd
cd ..
```

Build everything:

```cmd
dotnet build
```

Expected result:

```
Cataog succeeded
Test succeeded
WebApp succeeded
Build succeeded
```

## ▶ Step 10: Run the Web Application

```cmd
dotnet run --project .\WebApp\WebApp.csproj
```

Output:

```
Now listening on: https://localhost:xxxx
```

Open browser:

```
https://localhost:xxxx
```

## 🧪 Step 11: Verify Pages

- ✔ Home Page
- ✔ About Us
- ✔ Services
- ✔ Contact

Navigation works without refreshing logic.


## 🧠 Concept Mapping for Students

| Concept           | Meaning                       |
| ----------------- | ----------------------------- |
| Razor Page        | One page = one responsibility |
| Layout            | Common UI across all pages    |
| PageModel         | Backend logic                 |
| Project Reference | Clean dependency              |

## 🧩 Optional Student Exercises

1. Display product list from `Cataog`
2. Add footer partial view
3. Add CSS customization
4. Convert Services to dynamic page
5. Add form in Contact page


## 🎓 Mentor Insight

> “Console apps teach logic.
> Web apps teach **user thinking**.
> Together, they build **real developers**.”
