# .NET Application Structure & Minimal Web App

## Student Notes (Beginner → Industry Ready)

### 1. Role of VS Code (Important Mindset)

* VS Code is **only a code editor**
* It does **not** run or understand .NET by itself
* Real power comes from **.NET CLI (`dotnet`)**

👉 Learning .NET = understanding **commands + structure**, not just UI clicks



### 2. What Is a .NET Application?

A .NET application is a **software system**, not just code files.

#### Core Components

* **Source Code** → C#
* **Build System** → .NET CLI
* **Runtime** → CLR
* **Host** → `dotnet.exe`
* **Web Server** → Kestrel (for web apps)



### 3. EXE vs DLL (Very Important)

| EXE                | DLL                  |
| ------------------ | -------------------- |
| Self-executable    | Not executable alone |
| Entry point exists | No entry point       |
| Starts application | Supports application |
| Hosts DLLs         | Loaded by EXE        |

### Key Rule

> **DLL cannot run on its own**

- ✔ Console App / Web App → **EXE**
- ✔ Class Library → **DLL**



### 4. How Does a DLL Run?

Command:

```bash
dotnet run
```

Explanation:

* `dotnet.exe` acts as a **container**
* Reads `.csproj`
* Loads DLL
* Starts execution

✔ DLL runs **inside dotnet runtime**



### 5. MVC Application Structure

Command:

```bash
dotnet new mvc
```

### Generated Folder Structure

```
Controllers/
Models/
Views/
wwwroot/
Program.cs
appsettings.json
```

### MVC Pattern

* **Model** → Data + business logic
* **View** → UI (HTML / Razor)
* **Controller** → Handles HTTP requests

✔ Suitable for **traditional enterprise applications**


### 6. Problems With MVC for Beginners

* Too many folders initially
* Hard to see request–response flow
* Learning becomes mechanical

👉 Solution: **Minimal API**



### 7. Minimal Web Application

Command:

```bash
dotnet new web
```

### Structure

```
Program.cs
appsettings.json
```

- ✔ Minimal
- ✔ Lightweight
- ✔ Easy to understand



### 8. Program.cs – Heart of Minimal API

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to Transflower");

app.Run();
```

### Line-by-Line Meaning

| Code          | Purpose          |
| ------------- | ---------------- |
| CreateBuilder | Configure app    |
| Build         | Assemble app     |
| MapGet        | Map HTTP request |
| Run           | Start web server |



### 9. HTTP Request Mapping

```csharp
app.MapGet("/about", () => "About Us");
```

Meaning:

* Browser requests `/about`
* Lambda function executes
* Response returned to browser

✔ Request → Handler → Response



### 10. Web Server: Kestrel

* Built-in .NET web server
* Starts automatically with `dotnet run`
* Listens on a port (example: 5075)

Flow:

```
Browser → Kestrel → Application → Response
```


### 11. Running and Stopping the App

### Run:

```bash
dotnet run
```

### Stop:

```bash
CTRL + C
```


### 12. Build vs Run

### Build

```bash
dotnet build
```

* Compiles C# → IL
* Output:

```
bin/Debug/netX/
```

### Run

```bash
dotnet run
```

* Builds + executes

✔ Warnings allowed
❌ Errors stop build

---

### 13. Solution & Project Structure

### Create Solution

```bash
dotnet new sln
```

### Add Project to Solution

```bash
dotnet add Portal/Portal.csproj
```

✔ Industry-standard organization
✔ Supports multiple projects

---

### 14. .NET Platform Capabilities

Same core knowledge can build:

| Platform | Application |
| -------- | ----------- |
| ASP.NET  | Web         |
| WPF      | Windows     |
| MAUI     | Mobile      |
| Blazor   | Web UI      |
| Console  | Utilities   |

👉 **Learn core once, apply everywhere**

 

### 15. Key Exam & Interview Points

* DLL cannot execute independently
* `dotnet.exe` hosts applications
* Minimal API = function-based request mapping
* MVC = structured enterprise pattern
* Kestrel = built-in web server
* `Program.cs` = entry point

 

### 16. Learning Strategy (Mentor Advice)

1. Start with **Minimal API**
2. Understand **HTTP mapping**
3. Move to **MVC**
4. Add **Services & Libraries**
5. Build real projects

 
### ✅ Summary

* .NET is **platform + runtime + tools**
* Minimal API helps understand fundamentals
* MVC is for scalable applications
* CLI knowledge increases productivity

# ASP.NET Core Web Applications

## Student Notes (Minimal API → MVC → Enterprise Thinking)
### 1. Running a Test Web Application
### Command-Line Execution

```bash
dotnet run
```

* `dotnet.exe` runs the project
* Starts the **ASP.NET Core application**
* Automatically opens the browser (if configured)
* Application listens on a **port number** (e.g., 5009, 5075)

### Browser Output

* `/` → Default page
* `/about` → About page (if mapped)

### 2. What Happens When the App Runs?

* ASP.NET Core app starts
* **Kestrel Web Server** begins listening
* Incoming HTTP requests are handled
* Output is sent back to the browser

- ✔ No UI logic is in the browser
- ✔ Browser only displays the response


### 3. Application Object & Lifetime

* ASP.NET Core creates **one application object**
* This object lives **for the entire application lifetime**

### Important Concept

> This is called a **Singleton-like lifetime**

* One instance
* Used globally
* Manages request pipeline

### 4. Entry Point: `Main()` Method

Earlier (.NET Framework style):

```csharp
public class Program
{
    public static void Main(string[] args)
    {
    }
}
```

Now (.NET 6+ / .NET 8):

* `Main()` is **implicit**
* Code is written directly in `Program.cs`

- ✔ Entry point still exists
- ✔ It is just hidden for simplicity


### 5. Build vs Run

### Build Command

```bash
dotnet build
```

* Compiles application
* Generates output in:

```
bin/Debug/net8.0/
```

### Run Command

```bash
dotnet run
```

* Build + Execute
* Starts web server

### 6. Creating a Web Application

### Minimal Web App

```bash
dotnet new web
```

* Very lightweight
* Minimal files
* Best for learning fundamentals

### Web App (With UI Features)

```bash
dotnet new webapp
```

* Razor Pages support
* Static files
* Middleware pipeline configured

---

### 7. Application Configuration Phases

ASP.NET Core app has **two major phases**:

#### 1️⃣ Service Configuration

```csharp
builder.Services.AddXyz();
```

* Dependency Injection
* Authentication (JWT, Cookies)
* Authorization
* Database context
* Caching

✔ Happens **before app runs**

#### 2️⃣ Middleware Configuration

```csharp
app.UseXyz();
```

* Defines HTTP pipeline
* Intercepts requests and responses

### 8. HTTP Middleware Pipeline (Very Important)

#### What Is Middleware?

* A component that:

  * Receives request
  * Performs some work
  * Passes request to next middleware

#### Common Middlewares

* Authentication
* Authorization
* Session
* Caching
* Logging

#### Flow

```
Request → Middleware Pipeline → Handler
Handler → Middleware Pipeline → Response
```

✔ You can intercept **before and after** request processing

### 9. Request Mapping

#### Map HTTP Requests

```csharp
app.MapGet("/", () => "Hello World");
```

* URL → Handler function
* Handler returns response

✔ This is request–response mapping

### 10. Static Files & UI Resources

#### wwwroot Folder

Used for **static content**:

* HTML
* CSS
* JavaScript
* Bootstrap
* Tailwind

```csharp
app.UseStaticFiles();
```

✔ Required for UI rendering

### 11. Razor Pages

#### Razor Pages Mapping

```csharp
app.MapRazorPages();
```

* Server-side pages
* Files:

  * `Index.cshtml`
  * `Privacy.cshtml`

✔ Similar to ASPX, but modern


### 12. Evolution of Microsoft UI Technologies

| Technology      | Platform       |
| --------------- | -------------- |
| WinForms        | Windows        |
| WebForms (ASPX) | Web            |
| MVC             | Web            |
| Razor Pages     | Web            |
| Blazor          | Web            |
| WPF (XAML)      | Windows        |
| MAUI            | Cross-platform |


### 13. MVC Framework

#### Create MVC App

```bash
dotnet new mvc
```

#### Folder Structure

```
Controllers/
Models/
Views/
```

#### MVC Purpose

* Structured development
* Large-scale applications
* Enterprise readiness

✔ Suitable for applications with **thousands of pages**


### 14. MVC Controller Basics

#### Controller Example

```csharp
public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

* Action methods handle requests
* Return `ViewResult`
* Automatically finds `.cshtml` file


### 15. Routing in MVC

* URL maps to:

```
Controller → Action → View
```

Example:

```
/Products/Index
```

✔ Routing is managed by MVC framework


### 16. MVC vs Minimal API

| Minimal API     | MVC              |
| --------------- | ---------------- |
| Lightweight     | Structured       |
| Function-based  | Pattern-based    |
| Faster learning | Enterprise scale |
| Small apps      | Large apps       |

✔ Both are correct
✔ Choice depends on **project size**


### 17. Architectural Patterns

* MVC – Web
* MVVM – Desktop (WPF)
* MVP – UI-heavy apps

✔ Pattern selection depends on **application type**

### 18. Layered Architecture (Industry Standard)

Typical enterprise solution:

```
Controllers
Services
Repositories
Interfaces
Models
```

✔ Improves:

* Maintainability
* Testability
* Scalability


### 19. Dependency Injection (Core Concept)

* Interfaces define behavior
* Implementations provide logic
* Injected at runtime

- ✔ Reduces tight coupling
- ✔ Enables unit testing

### 20. Data Layer & Practice Resources

* SQL scripts:

  * Table creation
  * Insert queries
* Sample databases
* JSON results
* API responses

✔ Use these for hands-on practice

### 21. Power Platform (Low-Code / No-Code)

#### Microsoft Power Platform

* Power Apps
* Power Automate
* Power BI
* SharePoint

- ✔ Business-focused
- ✔ Minimal coding
- ✔ Built on top of Microsoft ecosystem

### 22. Developer vs Platform Roles

| Developer    | Platform User     |
| ------------ | ----------------- |
| Writes code  | Uses visual tools |
| ASP.NET, C#  | Power Apps        |
| Full control | Rapid development |

✔ Both are valid career paths
 

### 23. Enterprise Domains

* ERP
* CRM
* BI
* Workflow Automation
* Content Management

✔ Domain knowledge + tech = strong profile

 

### 24. Final Learning Advice

* Learn **Minimal API first**
* Then MVC
* Then layered architecture
* Focus on **Dependency Injection**
* Practice real controllers (Products, Cart, Orders)

 

## ✅ Session Summary

* ASP.NET Core uses middleware pipeline
* Single application object lifecycle
* MVC is for large systems
* Minimal API builds fundamentals
* Power Platform is business-focused

