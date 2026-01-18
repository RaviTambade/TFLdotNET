Below is a **complete, mentor-style Lab Assignment** in the **same HR Solution format** you’ve been following, covering:

👉 **partial class**
👉 **extension methods**
👉 **lambda expressions**
👉 **LINQ**

All concepts are taught **together** because this is how they appear in **real production HR systems**.

---

# 🧪 Lab Assignment

## **Modern C# Features using HR Solution Domain**

---

## 🎯 Lab Objective

To understand how **modern C# language features** help build:

* Clean domain models
* Readable business logic
* Reusable extensions
* Declarative data queries

using an **HR Employee Management System**.

---

## 🧠 Concepts Covered

| Feature            | Usage                                |
| ------------------ | ------------------------------------ |
| `partial class`    | Split large domain classes           |
| Extension Methods  | Add behavior without modifying class |
| Lambda Expressions | Inline logic                         |
| LINQ               | Query employee data                  |

---

## 🏗️ Solution Structure

```
HRModernCSharpLab
│
├── Program.cs
│
├── Models
│   ├── Employee.Basic.cs
│   └── Employee.HR.cs
│
├── Extensions
│   └── EmployeeExtensions.cs
│
└── Services
    └── HRAnalyticsService.cs
```

---

## 🧱 Step 1: Employee Model using `partial class`

📄 **Models/Employee.Basic.cs**

```csharp
namespace HR.Models
{
    public partial class Employee
    {
        public int Id;
        public string Name;
        public string Department;
        public double Salary;
    }
}
```

📄 **Models/Employee.HR.cs**

```csharp
namespace HR.Models
{
    public partial class Employee
    {
        public int Experience;
        public bool IsPermanent;

        public override string ToString()
        {
            return $"{Id} | {Name} | {Department} | {Salary} | {Experience} yrs";
        }
    }
}
```

---

## 🧠 Step 2: Extension Methods for Employee

📄 **Extensions/EmployeeExtensions.cs**

```csharp
using HR.Models;

namespace HR.Extensions
{
    public static class EmployeeExtensions
    {
        // Extension method
        public static bool IsSenior(this Employee emp)
        {
            return emp.Experience >= 5;
        }

        public static double AnnualSalary(this Employee emp)
        {
            return emp.Salary * 12;
        }
    }
}
```

---

## 🧠 Step 3: HR Analytics Service (LINQ + Lambda)

📄 **Services/HRAnalyticsService.cs**

```csharp
using HR.Models;
using System.Collections.Generic;
using System.Linq;

namespace HR.Services
{
    public class HRAnalyticsService
    {
        public List<Employee> GetPermanentEmployees(List<Employee> employees)
        {
            return employees
                .Where(e => e.IsPermanent)
                .ToList();
        }

        public List<Employee> GetHighSalaryEmployees(List<Employee> employees, double minSalary)
        {
            return employees
                .Where(e => e.Salary >= minSalary)
                .OrderByDescending(e => e.Salary)
                .ToList();
        }

        public double GetAverageSalary(List<Employee> employees)
        {
            return employees
                .Average(e => e.Salary);
        }
    }
}
```

---

## 🚀 Step 4: Program Execution (All Concepts Together)

📄 **Program.cs**

```csharp
using HR.Models;
using HR.Extensions;
using HR.Services;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Id=101, Name="Amit", Department="IT", Salary=50000, Experience=3, IsPermanent=true },
            new Employee { Id=102, Name="Neha", Department="HR", Salary=40000, Experience=2, IsPermanent=false },
            new Employee { Id=103, Name="Ravi", Department="IT", Salary=70000, Experience=6, IsPermanent=true },
            new Employee { Id=104, Name="Sneha", Department="Finance", Salary=65000, Experience=7, IsPermanent=true }
        };

        // Extension method usage
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.Name} Senior? {emp.IsSenior()} | Annual: {emp.AnnualSalary()}");
        }

        HRAnalyticsService service = new HRAnalyticsService();

        Console.WriteLine("\nPermanent Employees:");
        service.GetPermanentEmployees(employees)
               .ForEach(e => Console.WriteLine(e));

        Console.WriteLine("\nHigh Salary Employees:");
        service.GetHighSalaryEmployees(employees, 60000)
               .ForEach(e => Console.WriteLine(e));

        Console.WriteLine($"\nAverage Salary: {service.GetAverageSalary(employees)}");
    }
}
```

---

## 🔍 Expected Output (Sample)

```
Amit Senior? False | Annual: 600000
Neha Senior? False | Annual: 480000
Ravi Senior? True | Annual: 840000
Sneha Senior? True | Annual: 780000

Permanent Employees:
101 | Amit | IT | 50000 | 3 yrs
103 | Ravi | IT | 70000 | 6 yrs
104 | Sneha | Finance | 65000 | 7 yrs

High Salary Employees:
103 | Ravi | IT | 70000 | 6 yrs
104 | Sneha | Finance | 65000 | 7 yrs

Average Salary: 56250
```

---

## 🧠 HR Analogy Mapping

| C# Feature       | HR Interpretation                      |
| ---------------- | -------------------------------------- |
| `partial class`  | Employee info split across departments |
| Extension Method | Extra HR rules added later             |
| Lambda           | Quick decision logic                   |
| LINQ             | HR data analysis engine                |

---

## 🧪 Student Tasks (Hands-On)

### ✅ Task 1

Add an extension method:

```csharp
bool IsHighPerformer(this Employee emp)
```

### ✅ Task 2

Use LINQ to group employees by **Department**

### ✅ Task 3

Find **Top 2 highest paid employees**

### ✅ Task 4

Convert one LINQ query to **query syntax**

---

## ❓ Interview Questions

1. Why are extension methods static?
2. When should we use partial classes?
3. Difference between lambda and anonymous method?
4. Deferred execution in LINQ?

---

## 🧩 Mentor Notes (For You)

This lab:

* Moves students from **imperative → declarative**
* Prepares them for **clean architecture**
* Directly connects to:

  * Repository Pattern
  * Entity Framework Core
  * Microservices data pipelines

