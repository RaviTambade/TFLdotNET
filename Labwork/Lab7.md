
# 🧪 Lab 7: Repository Pattern using `IEmployeeRepository` (HR Domain)

> **Continuation of:**
> Lab 4 – OOP & Inheritance
> Lab 5 – Interface Inheritance
> Lab 6 – Dependency Injection

This lab introduces a **core enterprise pattern** used in **ASP.NET Core, Spring Boot, Clean Architecture, Microservices**.

## 🎯 Lab Title

**Separating Data Access using Repository Pattern in HR Domain**


## 🎯 Lab Objective

By the end of this lab, students will:

* Understand **why data access must be separated**
* Implement **Repository Pattern**
* Use **IEmployeeRepository** as abstraction
* Combine **Repository + DI + Interfaces**
* Prepare for **Database / ORM integration later**
* Write **testable and maintainable code**

## 🧠 Why Repository Pattern?

### ❌ Without Repository

```csharp
SalesEmployee emp = new SalesEmployee();
// data logic inside business code ❌
```

Problems:

* Business logic mixed with data access
* Hard to change database
* Hard to test
* Violates **Single Responsibility Principle**

### ✅ With Repository

```csharp
IEmployeeRepository repo;
repo.Add(employee);
```

Benefits:

* Business logic doesn’t know **how data is stored**
* Database can change without code rewrite
* Easy mocking for unit tests

## 🏗 Target Architecture (Big Picture)

```
Program
  |
  ↓
HRProcessor
  |
  ↓
IEmployeeService
  |
  ↓
IEmployeeRepository
  |
  ↓
InMemoryEmployeeRepository (today)
  |
  ↓
SQL / EF Core / Mongo (tomorrow)
```

## 📁 Step 1: Create Repository Folder

```cmd
mkdir HR\Repositories
```

## 🧩 Step 2: Define Repository Interface

### 📄 `HR/Repositories/IEmployeeRepository.cs`

```csharp
using HR;

namespace HR.Repositories;

public interface IEmployeeRepository
{
    void Add(Employee employee);
    void Update(Employee employee);
    Employee GetById(int id);
    List<Employee> GetAll();
}
```

📌 **Key Point**
Repository exposes **WHAT** operations, not **HOW**.

## 📁 Step 3: Implement In-Memory Repository

### 📄 `HR/Repositories/InMemoryEmployeeRepository.cs`

```csharp
using HR;

namespace HR.Repositories;

public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = new();

    public void Add(Employee employee)
    {
        _employees.Add(employee);
        Console.WriteLine("Employee added to repository.");
    }

    public void Update(Employee employee)
    {
        var emp = GetById(employee.Id);
        if (emp != null)
        {
            _employees.Remove(emp);
            _employees.Add(employee);
            Console.WriteLine("Employee updated.");
        }
    }

    public Employee GetById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }

    public List<Employee> GetAll()
    {
        return _employees;
    }
}
```

📌 **Teaching Insight**
Today → List
Tomorrow → SQL Server / EF Core
**No business code change required**

## 🔁 Step 4: Modify `EmployeeService` to Use Repository

### 📄 `HR/Services/EmployeeService.cs`

```csharp
using HR;
using HR.Repositories;

namespace HR.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    // Repository injected
    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public float GetSalary(Employee employee)
    {
        employee.DoWork();
        return employee.ComputePay();
    }

    public void PerformDuties(Employee employee)
    {
        employee.DoWork();
        Console.WriteLine("Employee duties completed.");
    }

    public void AddEmployee(Employee employee)
    {
        _repository.Add(employee);
    }

    public List<Employee> GetEmployees()
    {
        return _repository.GetAll();
    }
}
```

📌 **Important Learning**

* Service depends on **Repository interface**
* Concrete storage hidden

## 🔁 Step 5: Update `IEmployeeService`

### 📄 `HR/Services/IEmployeeService.cs`

```csharp
using HR;

namespace HR.Services;

public interface IEmployeeService
{
    float GetSalary(Employee employee);
    void PerformDuties(Employee employee);
    void AddEmployee(Employee employee);
    List<Employee> GetEmployees();
}
```

## ▶ Step 6: Wire Everything in `Program.cs` (Manual DI)

```csharp
using HR;
using HR.Repositories;
using HR.Services;

// Create repository
IEmployeeRepository repository = new InMemoryEmployeeRepository();

// Inject repository into service
IEmployeeService service = new EmployeeService(repository);

// Inject service into processor
HRProcessor processor = new HRProcessor(service);

// Create employees
Employee emp1 = new SalesEmployee { Id = 1 };
Employee emp2 = new SalesManager { Id = 2 };

// Save employees
service.AddEmployee(emp1);
service.AddEmployee(emp2);

// Process employees
foreach (var emp in service.GetEmployees())
{
    processor.Process(emp);
}
```

## 🧠 What Students Must Observe

* HRProcessor never touches data storage
* Repository can be replaced without impact
* Clear separation of concerns
* Industry-grade architecture achieved

## 🧠 Repository Pattern in One Line (Interview Ready)

> **Repository Pattern** abstracts data access and provides a clean collection-like interface to the domain layer.


## 📝 Lab Assignments (Mandatory)

### 🧪 Task 1

Create another repository:

```text
FileEmployeeRepository
```

Store employee data in a text file.


### 🧪 Task 2

Create:

```csharp
GetEmployeesByLocation(string location)
```

Add it to repository interface.


### 🧪 Task 3

Replace `InMemoryEmployeeRepository` with `MockEmployeeRepository`
(No code change in service or processor)


### 🧪 Task 4 (Thinking Task)

Answer:

> Why Repository Pattern is heavily used in Microservices and DDD?


## 🎓 Mentor Note

> “Services decide **what** to do.
> Repositories decide **where data lives**.
> This separation keeps systems alive for years.”

