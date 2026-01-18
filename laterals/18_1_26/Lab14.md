Below is a **complete, industry-aligned Lab Assignment** combining
👉 **LINQ** + **Repository Pattern**
using the **HR Solution Domain**, written in the **same mentor-driven, step-by-step format** you’ve been following.

This lab is a **bridge lab** between **collections/LINQ** and **Clean Architecture + EF Core**.

---

# 🧪 Lab Assignment

## **LINQ + Repository Pattern – HR Solution**

---

## 🎯 Lab Objective

To design a **clean, testable HR data access layer** using:

* `List<Employee>` as in-memory data store
* **Repository Pattern**
* **LINQ for querying business data**

---

## 🧠 Concepts Covered

| Concept                | Purpose              |
| ---------------------- | -------------------- |
| Repository Pattern     | Abstract data access |
| LINQ                   | Query domain objects |
| Interface-based design | Loose coupling       |
| Separation of concerns | Clean code           |

---

## 🏗️ Solution Structure

```
HRRepositoryLinqLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
├── Repositories
│   ├── IEmployeeRepository.cs
│   └── EmployeeRepository.cs
│
└── Services
    └── HRReportService.cs
```

---

## 🧱 Step 1: Employee Domain Model

📄 **Models/Employee.cs**

```csharp
namespace HR.Models
{
    public class Employee
    {
        public int Id;
        public string Name;
        public string Department;
        public double Salary;
        public int Experience;
        public bool IsPermanent;

        public override string ToString()
        {
            return $"{Id} | {Name} | {Department} | {Salary} | {Experience} yrs | Permanent: {IsPermanent}";
        }
    }
}
```

---

## 🧠 Step 2: Repository Interface

📄 **Repositories/IEmployeeRepository.cs**

```csharp
using HR.Models;
using System.Collections.Generic;

namespace HR.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
    }
}
```

---

## 🧠 Step 3: Repository Implementation (LINQ inside)

📄 **Repositories/EmployeeRepository.cs**

```csharp
using HR.Models;
using System.Collections.Generic;
using System.Linq;

namespace HR.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees = new List<Employee>
        {
            new Employee { Id=101, Name="Amit", Department="IT", Salary=50000, Experience=3, IsPermanent=true },
            new Employee { Id=102, Name="Neha", Department="HR", Salary=40000, Experience=2, IsPermanent=false },
            new Employee { Id=103, Name="Ravi", Department="IT", Salary=70000, Experience=6, IsPermanent=true },
            new Employee { Id=104, Name="Sneha", Department="Finance", Salary=65000, Experience=7, IsPermanent=true }
        };

        public List<Employee> GetAll()
        {
            return _employees;
        }

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }
    }
}
```

---

## 🧠 Step 4: HR Service Layer (LINQ on Repository)

📄 **Services/HRReportService.cs**

```csharp
using HR.Models;
using HR.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HR.Services
{
    public class HRReportService
    {
        private readonly IEmployeeRepository _repository;

        public HRReportService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<Employee> GetPermanentEmployees()
        {
            return _repository.GetAll()
                              .Where(e => e.IsPermanent)
                              .ToList();
        }

        public List<Employee> GetEmployeesByDepartment(string department)
        {
            return _repository.GetAll()
                              .Where(e => e.Department == department)
                              .ToList();
        }

        public List<Employee> GetTopPaidEmployees(int count)
        {
            return _repository.GetAll()
                              .OrderByDescending(e => e.Salary)
                              .Take(count)
                              .ToList();
        }

        public double GetAverageSalary()
        {
            return _repository.GetAll()
                              .Average(e => e.Salary);
        }
    }
}
```

---

## 🚀 Step 5: Program Execution (Wiring Everything)

📄 **Program.cs**

```csharp
using HR.Repositories;
using HR.Services;

class Program
{
    static void Main()
    {
        IEmployeeRepository repository = new EmployeeRepository();
        HRReportService service = new HRReportService(repository);

        Console.WriteLine("Permanent Employees:");
        service.GetPermanentEmployees()
               .ForEach(e => Console.WriteLine(e));

        Console.WriteLine("\nIT Department Employees:");
        service.GetEmployeesByDepartment("IT")
               .ForEach(e => Console.WriteLine(e));

        Console.WriteLine("\nTop 2 Paid Employees:");
        service.GetTopPaidEmployees(2)
               .ForEach(e => Console.WriteLine(e));

        Console.WriteLine($"\nAverage Salary: {service.GetAverageSalary()}");
    }
}
```

---

## 🔍 Expected Output (Sample)

```
Permanent Employees:
101 | Amit | IT | 50000 | 3 yrs | Permanent: True
103 | Ravi | IT | 70000 | 6 yrs | Permanent: True
104 | Sneha | Finance | 65000 | 7 yrs | Permanent: True

IT Department Employees:
101 | Amit | IT | 50000 | 3 yrs | Permanent: True
103 | Ravi | IT | 70000 | 6 yrs | Permanent: True

Top 2 Paid Employees:
103 | Ravi | IT | 70000 | 6 yrs | Permanent: True
104 | Sneha | Finance | 65000 | 7 yrs | Permanent: True

Average Salary: 56250
```

---

## 🧠 HR Analogy Mapping

| Technical Layer | HR Meaning                 |
| --------------- | -------------------------- |
| Repository      | Employee master database   |
| LINQ            | HR analytics engine        |
| Service         | HR policy & reporting      |
| Interface       | Contract between HR & data |

---

## 🧪 Student Tasks (Assignments)

### ✅ Task 1

Add method:

```csharp
List<Employee> GetEmployeesWithExperience(int minYears)
```

### ✅ Task 2

Use **GroupBy** to generate **Department-wise count**

### ✅ Task 3

Add `UpdateSalary(int id, double amount)` in repository

### ✅ Task 4

Replace `List<Employee>` with **In-Memory DB Simulation**

---

## ❓ Interview Questions

1. Why should LINQ not be exposed directly to UI?
2. Where should LINQ logic live – Repository or Service?
3. How does Repository Pattern help unit testing?
4. Difference between `IEnumerable` and `IQueryable`?

---

## 🧩 Mentor Notes (For You)

This lab:

* Solidifies **domain + abstraction thinking**
* Prepares students for:

  * EF Core
  * Dapper
  * Clean Architecture
* Naturally extends to **Dependency Injection**

