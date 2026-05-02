 
# 🧪 Lab 6: Dependency Injection using Interfaces (HR Domain)
Below is **🧪 Lab 6**, designed as a **direct continuation of Lab 4 (OOP)** and **Lab 5 (Interface Inheritance)** in the **HR Domain**.

This lab introduces **Dependency Injection (DI)** — a **must-know industry concept** for .NET, Java Spring, and Node.js.
## 🎯 Lab Title

**Decoupling HR Business Logic using Dependency Injection**

---

## 🎯 Lab Objective

By the end of this lab, students will:

* Understand **tight coupling vs loose coupling**
* Apply **Dependency Injection using interfaces**
* Implement **Constructor Injection**
* Learn **why DI is mandatory in enterprise applications**
* Prepare for **ASP.NET Core / Spring Boot DI frameworks**

---

## 🧠 Prerequisites

Students should have completed:

* Lab 4: Inheritance & Polymorphism
* Lab 5: Interface Inheritance
* Knowledge of:

  * Interfaces
  * Polymorphism
  * HR Domain classes

---

## ❌ Problem with Current Design (Why DI?)

### Existing Situation

```csharp
SalesManager manager = new SalesManager();
manager.DoWork();
```

📌 **Problem**

* Object creation is **hardcoded**
* Business logic depends on **concrete classes**
* Difficult to:

  * Replace implementation
  * Test code
  * Extend behavior

👉 **Industry Solution:** Dependency Injection

---

## 🧠 DI Concept (Human Analogy)

> “Doctor does not manufacture medicines.
> Hospital injects medicines when needed.”

Same in software:

* Classes **should not create dependencies**
* Dependencies should be **provided (injected)**

---

## 🏗 Target Design (After DI)

```
Program
  |
  | injects
  ↓
HRService
  |
  | depends on
  ↓
IEmployeeService
```

---

## 📁 Step 1: Create Service Interfaces

Create folder:

```cmd
mkdir HR\Services
```

---

### 📄 `HR/Services/IEmployeeService.cs`

```csharp
using HR;

namespace HR.Services;

public interface IEmployeeService
{
    float GetSalary(Employee employee);
    void PerformDuties(Employee employee);
}
```

📌 **Why Interface?**

* Service logic must be **replaceable**
* Enables testing & flexibility

---

## 📁 Step 2: Implement Service Interface

### 📄 `HR/Services/EmployeeService.cs`

```csharp
using HR;

namespace HR.Services;

public class EmployeeService : IEmployeeService
{
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
}
```

📌 **Key Learning**

* Service depends on **Employee abstraction**
* No concrete role knowledge inside service

---

## 📁 Step 3: Create Consumer Class (HRProcessor)

### 📄 `HR/Services/HRProcessor.cs`

```csharp
using HR;

namespace HR.Services;

public class HRProcessor
{
    private readonly IEmployeeService _employeeService;

    // Constructor Injection
    public HRProcessor(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public void Process(Employee employee)
    {
        float salary = _employeeService.GetSalary(employee);
        Console.WriteLine(employee);
        Console.WriteLine("Final Salary: " + salary);
    }
}
```

📌 **Important**

* Dependency is injected via constructor
* HRProcessor does NOT create service

---

## ▶ Step 4: Inject Dependencies in `Program.cs`

### 📄 `Program.cs`

```csharp
using HR;
using HR.Services;

// Create dependency
IEmployeeService service = new EmployeeService();

// Inject dependency
HRProcessor processor = new HRProcessor(service);

// Create employees
Employee emp1 = new SalesEmployee();
Employee emp2 = new SalesManager();

// Process employees
processor.Process(emp1);
processor.Process(emp2);
```

---

## 🧠 Key Observation (Very Important)

| Without DI       | With DI            |
| ---------------- | ------------------ |
| `new` everywhere | `new` at one place |
| Tight coupling   | Loose coupling     |
| Hard to test     | Easy to mock       |
| Poor design      | Enterprise-ready   |

---

## 🧪 Step 5: Build and Run

```cmd
dotnet build
dotnet run
```

---

## 🧠 What Students Must Observe

* Same `HRProcessor`
* Same `EmployeeService`
* Different behaviors at runtime
* No code change required to support new employee types

👉 **This is Dependency Injection + Polymorphism**

---

## 🧠 Interview Gold Concepts (Explain in 1 Line)

* **Dependency Injection**

  > Passing dependency from outside instead of creating it internally

* **Constructor Injection**

  > Dependency provided through constructor

* **Why Interfaces?**

  > To depend on abstraction, not implementation

---

## 📝 Lab Assignments (Mandatory)

### 🧪 Task 1

Create a new service:

```csharp
IPayrollService
{
    void GeneratePayslip(Employee emp);
}
```

Inject it into `HRProcessor`.

---

### 🧪 Task 2

Create `MockEmployeeService`
Use it instead of `EmployeeService` in `Program.cs`.

📌 Purpose: **Unit testing simulation**

---

### 🧪 Task 3

Modify `HRProcessor` so it accepts **multiple services**.

---

### 🧪 Task 4 (Thinking Task)

Answer:

> Why DI is compulsory in Microservices and Cloud-native applications?

---

## 🎓 Mentor Note (Transflower Learning)

> “Frameworks like ASP.NET Core and Spring Boot
> are just **DI containers with HTTP support**.”

Master this lab and:

* ASP.NET Core DI becomes trivial
* Spring Boot `@Autowired` makes sense
* Clean Architecture becomes natural

---

## 🚀 Next Natural Labs (Your Choice)

1. Built-in DI Container (ASP.NET Core)
2. SOLID Principles (DIP, ISP)
3. Repository Pattern (IEmployeeRepository)
4. Unit Testing using Mocks
5. Mapping DI to **Spring Boot**

Just say **next** 👨‍🏫✨
