# 🧪 Lab 8: Clean Architecture Layers (HR Domain)

## 🎯 Lab Title

**Designing a Clean Architecture–based HR System**

## 🎯 Lab Objective

By the end of this lab, students will:

* Understand **Clean Architecture principles**
* Separate code into **clear layers**
* Enforce **dependency direction**
* Apply **interfaces + DI + Repository**
* Build **enterprise-ready architecture**
* Prepare for **ASP.NET Core / Microservices**

## 🧠 Why Clean Architecture?

### ❌ Typical Student Project

```
Controller → Service → DB → Models (mixed everywhere)
```

Problems:

* Tight coupling
* Hard to test
* Framework-dependent
* Difficult to maintain

### ✅ Clean Architecture Rule

> **Dependencies always point inward**

```
UI → Application → Domain
           ↑
      Infrastructure
```

## 🏗 Clean Architecture Layers (HR Domain)

```
┌──────────────────────────────┐
│ Presentation (Console/Web)   │
├──────────────────────────────┤
│ Application (Use Cases)      │
├──────────────────────────────┤
│ Domain (Entities & Rules)    │
├──────────────────────────────┤
│ Infrastructure (Data Access) │
└──────────────────────────────┘
```

## 📁 Step 1: Create Solution Structure

```cmd
dotnet new sln -n HR.CleanArchitecture
mkdir src
```

---

## 📁 Step 2: Create Projects (One per Layer)

```cmd
dotnet new classlib -n HR.Domain
dotnet new classlib -n HR.Application
dotnet new classlib -n HR.Infrastructure
dotnet new console  -n HR.Presentation
```


## ➕ Step 3: Add Projects to Solution

```cmd
dotnet sln add src\HR.Domain\HR.Domain.csproj
dotnet sln add src\HR.Application\HR.Application.csproj
dotnet sln add src\HR.Infrastructure\HR.Infrastructure.csproj
dotnet sln add src\HR.Presentation\HR.Presentation.csproj
```

## 🔗 Step 4: Set Dependency Rules (IMPORTANT)

```cmd
dotnet add src\HR.Application\HR.Application.csproj reference src\HR.Domain\HR.Domain.csproj
dotnet add src\HR.Infrastructure\HR.Infrastructure.csproj reference src\HR.Application\HR.Application.csproj
dotnet add src\HR.Presentation\HR.Presentation.csproj reference src\HR.Application\HR.Application.csproj
```

🚫 **Rule**

* Domain depends on NOTHING
* Infrastructure never referenced by Domain

## 🧩 Step 5: Domain Layer (Pure Business Rules)

### 📄 `HR.Domain/Entities/Employee.cs`

> Move your **Lab 4 HR classes here**

* Employee (abstract)
* SalesEmployee
* SalesManager

📌 **Allowed**

* C#
* OOP
* Business logic

🚫 **Not Allowed**

* Console
* Database
* Framework code

## 🧩 Step 6: Application Layer (Use Cases)

### 📄 `HR.Application/Interfaces/IEmployeeRepository.cs`

```csharp
using HR.Domain.Entities;

namespace HR.Application.Interfaces;

public interface IEmployeeRepository
{
    void Add(Employee employee);
    List<Employee> GetAll();
}
```

### 📄 `HR.Application/Services/EmployeeService.cs`

```csharp
using HR.Application.Interfaces;
using HR.Domain.Entities;

namespace HR.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public void Hire(Employee employee)
    {
        _repository.Add(employee);
    }

    public List<Employee> GetEmployees()
    {
        return _repository.GetAll();
    }
}
```

📌 **Application Layer**

* Contains **business workflows**
* Depends only on **interfaces**

## 🧩 Step 7: Infrastructure Layer (Data Access)

### 📄 `HR.Infrastructure/Repositories/InMemoryEmployeeRepository.cs`

```csharp
using HR.Application.Interfaces;
using HR.Domain.Entities;

namespace HR.Infrastructure.Repositories;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = new();

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

    public List<Employee> GetAll()
    {
        return _employees;
    }
}
```

📌 **Infrastructure**

* Implements interfaces
* Can be replaced by:

  * EF Core
  * SQL
  * MongoDB

## 🧩 Step 8: Presentation Layer (Console App)

### 📄 `Program.cs`

```csharp
using HR.Application.Services;
using HR.Infrastructure.Repositories;
using HR.Domain.Entities;

// Manual Dependency Injection
var repository = new InMemoryEmployeeRepository();
var service = new EmployeeService(repository);

// Domain objects
Employee emp1 = new SalesEmployee { Id = 1 };
Employee emp2 = new SalesManager { Id = 2 };

// Use cases
service.Hire(emp1);
service.Hire(emp2);

foreach (var emp in service.GetEmployees())
{
    emp.DoWork();
    Console.WriteLine(emp);
    Console.WriteLine("Salary: " + emp.ComputePay());
}
```

## 🧠 What Students Must Clearly Understand

| Layer          | Responsibility |
| -------------- | -------------- |
| Domain         | Business rules |
| Application    | Use cases      |
| Infrastructure | Data, IO       |
| Presentation   | Input/Output   |

## 🧠 Clean Architecture Golden Rules

1. Domain has **zero dependencies**
2. Interfaces live **inside**
3. Infrastructure is **replaceable**
4. Frameworks are **details**
5. Business logic survives framework change

## 📝 Lab Assignments (Mandatory)

### 🧪 Task 1

Add `IPayrollService` to Application layer.


### 🧪 Task 2

Replace `InMemoryEmployeeRepository` with `FileEmployeeRepository`.

### 🧪 Task 3

Create `EmployeeController` (simulate Web API).

### 🧪 Task 4 (Thinking Task)

Answer:

> Why Clean Architecture is preferred in long-running enterprise systems?


## 🎓 Mentor Note (Transflower Learning)

> “Clean Architecture is not about folders.
> It is about **thinking clearly** and **coding responsibly**.”

Once students understand this lab:

* ASP.NET Core architecture becomes obvious
* Spring Boot layers feel natural
* Microservices design makes sense
