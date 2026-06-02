## 🎯 Lab Title

**Building a Clean, SOLID-Compliant HR Web API using ASP.NET Core**

# 🧪 Lab 10: ASP.NET Core Web API using Clean Architecture & SOLID (HR Domain)
Below is a **complete Lab Assignment** designed for **ASP.NET Core Web API using Clean Architecture + SOLID**, mapped to your **HR domain** and **continuing all previous labs**.

## 🎯 Lab Objective

By the end of this lab, students will be able to:

* Build an **ASP.NET Core Web API**
* Apply **Clean Architecture layering**
* Enforce **SOLID principles**
* Use **Dependency Injection**
* Use **Repository Pattern**
* Design **RESTful HR APIs**
* Write **enterprise-ready backend code**

## 🧠 Prerequisites

Students must have completed:

* Lab 4 → OOP & Inheritance
* Lab 5 → Interfaces
* Lab 6 → Dependency Injection
* Lab 7 → Repository Pattern
* Lab 8 → Clean Architecture
* Lab 9 → SOLID Principles

## 🏗 Target Architecture (HR Domain)

```
Client (Postman / Browser)
        |
        ↓
HR.API (Controllers)
        |
        ↓
HR.Application (Use Cases)
        |
        ↓
HR.Domain (Entities & Rules)
        ↑
HR.Infrastructure (Repositories)
```

📌 **Dependency Rule**

> Outer layers depend on inner layers
> Inner layers depend on nothing

## 📁 Step 1: Create Solution & Projects

### Create Solution

```cmd
dotnet new sln -n HR.WebApi.Clean
mkdir src
```

### Create Projects

```cmd
dotnet new classlib -n HR.Domain
dotnet new classlib -n HR.Application
dotnet new classlib -n HR.Infrastructure
dotnet new webapi   -n HR.API
```

## ➕ Step 2: Add Projects to Solution

```cmd
dotnet sln add src\HR.Domain\HR.Domain.csproj
dotnet sln add src\HR.Application\HR.Application.csproj
dotnet sln add src\HR.Infrastructure\HR.Infrastructure.csproj
dotnet sln add src\HR.API\HR.API.csproj
```

## 🔗 Step 3: Configure Project References (VERY IMPORTANT)

```cmd
dotnet add src\HR.Application\HR.Application.csproj reference src\HR.Domain\HR.Domain.csproj
dotnet add src\HR.Infrastructure\HR.Infrastructure.csproj reference src\HR.Application\HR.Application.csproj
dotnet add src\HR.API\HR.API.csproj reference src\HR.Application\HR.Application.csproj
dotnet add src\HR.API\HR.API.csproj reference src\HR.Infrastructure\HR.Infrastructure.csproj
```

📌 **Domain layer has NO references**

## 🧩 Step 4: Domain Layer (HR.Domain)
### Responsibilities

* Entities
* Business rules
* No frameworks

### Required Classes

* `Employee` (abstract)
* `SalesEmployee`
* `SalesManager`

📌 Reuse and refactor code from **Lab 4**

## 🧩 Step 5: Application Layer (HR.Application)

### Responsibilities

* Use cases
* Interfaces
* Business workflows

### Required Interfaces

```csharp
IEmployeeRepository
IPayrollService
```

### Required Services

* `EmployeeService`
* `PayrollService`

📌 **SOLID applied**

* SRP → Services are focused
* DIP → Services depend on interfaces

## 🧩 Step 6: Infrastructure Layer (HR.Infrastructure)

### Responsibilities

* Data access
* External systems
* Implement interfaces

### Repository Implementation

```text
InMemoryEmployeeRepository
```

📌 Later replaceable with:

* EF Core
* SQL Server
* MongoDB

(No API or service code change)

## 🧩 Step 7: Presentation Layer (HR.API)

### Responsibilities

* HTTP
* Routing
* Model binding
* DI wiring

### Create Controller

📄 `Controllers/EmployeesController.cs`

```csharp
[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Hire(Employee employee)
    {
        _service.Hire(employee);
        return Ok("Employee hired");
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetEmployees());
    }
}
```

📌 **Controller depends on Application layer only**

## 🧩 Step 8: Dependency Injection Configuration

📄 `Program.cs`

```csharp
builder.Services.AddSingleton<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<PayrollService>();
```

📌 **DIP enforced**

* API does NOT know concrete repository

## ▶ Step 9: Run & Test API

```cmd
dotnet build
dotnet run --project src\HR.API
```

Test using:

* Browser
* Swagger
* Postman

## 🔗 Sample API Endpoints

| Method | Endpoint         | Description    |
| ------ | ---------------- | -------------- |
| POST   | `/api/employees` | Hire employee  |
| GET    | `/api/employees` | List employees |

## 🧠 SOLID Mapping (Explicit)

| Principle | Where Applied                         |
| --------- | ------------------------------------- |
| SRP       | Controllers / Services / Repositories |
| OCP       | New employee types                    |
| LSP       | Employee polymorphism                 |
| ISP       | Role-based interfaces                 |
| DIP       | DI container                          |

## 📝 Lab Assignment Tasks (Mandatory)

### 🧪 Task 1

Add:

```
GET /api/employees/{id}
```

### 🧪 Task 2

Add:

```
PayrollController
GET /api/payroll/{id}
```

### 🧪 Task 3

Replace `InMemoryEmployeeRepository` with `FileEmployeeRepository`.

📌 No API code change allowed.

### 🧪 Task 4

Add **DTOs** and mapping layer.

### 🧪 Task 5 (Thinking Task)

Explain:

> How Clean Architecture + SOLID helps when migrating from Monolith to Microservices?

## 🎓 Mentor Note (Transflower Learning)

> “ASP.NET Core is just a delivery mechanism.
> Architecture is what makes software last.”

Students who finish this lab are:

* Web API ready
* Architecture aware
* Industry aligned
* Framework independent
