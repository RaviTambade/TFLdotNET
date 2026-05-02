Below is a **hands-on Lab Assignment** in the **same HR Solution format and mentor-style flow** you’ve been using, focused on:

👉 **IComparable**
👉 **IComparer**
👉 **List<T> of objects**

This lab is **industry-aligned**, builds **collection thinking**, and prepares students for **sorting, ranking, and reporting** scenarios in real HR systems.

---

# 🧪 Lab Assignment: `IComparable`, `IComparer` & `List<Employee>`

### **HR Domain – Employee Sorting & Ranking**

---

## 🎯 Lab Objective

To understand how **lists of domain objects** can be:

* Stored
* Compared
* Sorted using **business rules**

using **C# interfaces**:

* `IComparable<T>`
* `IComparer<T>`

---

## 🧠 Concepts Covered

| Concept          | Purpose                   |
| ---------------- | ------------------------- |
| `List<T>`        | Store multiple employees  |
| `IComparable<T>` | Default object comparison |
| `IComparer<T>`   | Custom comparison logic   |
| `Sort()`         | Sort domain objects       |

---

## 🏗️ Solution Structure

```
HRComparableLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
└── Comparers
    ├── SalaryComparer.cs
    └── NameComparer.cs
```

---

## 🧱 Step 1: Create Employee Model (IComparable)

📄 **Models/Employee.cs**

```csharp
namespace HR.Models
{
    public class Employee : IComparable<Employee>
    {
        public int Id;
        public string Name;
        public double Salary;
        public int Experience;

        // Default comparison: by Id
        public int CompareTo(Employee other)
        {
            if (other == null) return 1;
            return this.Id.CompareTo(other.Id);
        }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Salary} | {Experience} yrs";
        }
    }
}
```

---

## 🧠 Step 2: Custom Comparer – Salary Based

📄 **Comparers/SalaryComparer.cs**

```csharp
using HR.Models;
using System.Collections.Generic;

namespace HR.Comparers
{
    public class SalaryComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.Salary.CompareTo(y.Salary);
        }
    }
}
```

---

## 🧠 Step 3: Custom Comparer – Name Based

📄 **Comparers/NameComparer.cs**

```csharp
using HR.Models;
using System.Collections.Generic;

namespace HR.Comparers
{
    public class NameComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
```

---

## 🚀 Step 4: Program Execution Using List<Employee>

📄 **Program.cs**

```csharp
using HR.Models;
using HR.Comparers;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 103, Name = "Ravi", Salary = 60000, Experience = 5 },
            new Employee { Id = 101, Name = "Amit", Salary = 45000, Experience = 2 },
            new Employee { Id = 104, Name = "Sneha", Salary = 70000, Experience = 7 },
            new Employee { Id = 102, Name = "Neha", Salary = 50000, Experience = 3 }
        };

        Console.WriteLine("Default Sort (By Id)");
        employees.Sort();
        Display(employees);

        Console.WriteLine("\nSort By Salary");
        employees.Sort(new SalaryComparer());
        Display(employees);

        Console.WriteLine("\nSort By Name");
        employees.Sort(new NameComparer());
        Display(employees);
    }

    static void Display(List<Employee> employees)
    {
        foreach (var emp in employees)
        {
            Console.WriteLine(emp);
        }
    }
}
```

---

## 🔍 Expected Output (Order May Vary)

```
Default Sort (By Id)
101 | Amit | 45000 | 2 yrs
102 | Neha | 50000 | 3 yrs
103 | Ravi | 60000 | 5 yrs
104 | Sneha | 70000 | 7 yrs

Sort By Salary
101 | Amit | 45000 | 2 yrs
102 | Neha | 50000 | 3 yrs
103 | Ravi | 60000 | 5 yrs
104 | Sneha | 70000 | 7 yrs

Sort By Name
101 | Amit | 45000 | 2 yrs
102 | Neha | 50000 | 3 yrs
103 | Ravi | 60000 | 5 yrs
104 | Sneha | 70000 | 7 yrs
```

---

## 🧠 HR-World Analogy

| Technical Concept | HR Meaning                   |
| ----------------- | ---------------------------- |
| `List<Employee>`  | Employee master data         |
| `IComparable`     | Company default sorting rule |
| `IComparer`       | Department-specific policies |
| `Sort()`          | HR report generation         |

---

## 🧪 Student Tasks (Assignments)

### ✅ Task 1

Implement **ExperienceComparer**
Sort employees by **experience descending**

### ✅ Task 2

Change default `CompareTo()` to sort by **Name**

### ✅ Task 3

Create a method:

```csharp
List<Employee> GetTopPaidEmployees(List<Employee> list, int count)
```

### ✅ Task 4

Use **LINQ OrderBy** instead of `IComparer`

---

## ❓ Interview Questions

1. Difference between `IComparable` and `IComparer`?
2. Why should `CompareTo()` return `int`?
3. When should we avoid modifying `CompareTo()`?
4. Can one class have multiple `IComparer` implementations?

---

## 🧩 Mentor Notes (For You)

This lab:

* Bridges **collections → interfaces → business rules**
* Prepares students for **ranking engines**
* Leads naturally into:

  * **LINQ**
  * **Repository Pattern**
  * **Clean Architecture**

