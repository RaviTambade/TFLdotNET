# 🧪 Lab Assignment: C# Keywords (`ref`, `out`, `params`, `static`)

### **HR Domain – Employee Management**

## 🎯 Lab Objective

To understand and apply important **C# method-related keywords** using a **realistic HR domain**, focusing on:

* Memory behavior
* Method parameter passing
* Reusability
* Shared state using static members

## 🧠 Concepts Covered

| Keyword  | Purpose                                  |
| -------- | ---------------------------------------- |
| `ref`    | Pass variable **by reference**           |
| `out`    | Return **multiple values**               |
| `params` | Accept **variable number of arguments**  |
| `static` | Share data & behavior across all objects |

## 🏗️ Solution Structure

```
HRKeywordLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
└── Services
    └── HRService.cs
```

## 🧱 Step 1: Create Employee Model

📄 **Models/Employee.cs**

```csharp
namespace HR.Models
{
    public class Employee
    {
        public int Id;
        public string Name;
        public double Salary;
    }
}
```

## 🧠 Step 2: HR Service (Keywords Implementation)

📄 **Services/HRService.cs**

```csharp
using HR.Models;

namespace HR.Services
{
    public class HRService
    {
        // static field – shared across all employees
        public static int TotalEmployees = 0;

        // static method
        public static void AddEmployee()
        {
            TotalEmployees++;
        }

        // ref keyword
        public void IncrementSalary(ref double salary)
        {
            salary += 5000;
        }

        // out keyword
        public void CalculateBonus(double salary, out double bonus)
        {
            bonus = salary * 0.10;
        }

        // params keyword
        public double CalculateAverageSalary(params double[] salaries)
        {
            double total = 0;
            foreach (double s in salaries)
            {
                total += s;
            }
            return total / salaries.Length;
        }
    }
}
```

## 🚀 Step 3: Program Execution

📄 **Program.cs**

```csharp
using HR.Models;
using HR.Services;

class Program
{
    static void Main()
    {
        HRService service = new HRService();

        // static usage
        HRService.AddEmployee();
        HRService.AddEmployee();
        Console.WriteLine($"Total Employees: {HRService.TotalEmployees}");

        // Employee object
        Employee emp = new Employee
        {
            Id = 101,
            Name = "Amit",
            Salary = 40000
        };

        // ref keyword demo
        service.IncrementSalary(ref emp.Salary);
        Console.WriteLine($"Updated Salary: {emp.Salary}");

        // out keyword demo
        service.CalculateBonus(emp.Salary, out double bonus);
        Console.WriteLine($"Bonus: {bonus}");

        // params keyword demo
        double avgSalary = service.CalculateAverageSalary(
            30000, 40000, 50000, 60000
        );
        Console.WriteLine($"Average Salary: {avgSalary}");
    }
}
```

## 🔍 Expected Output

```
Total Employees: 2
Updated Salary: 45000
Bonus: 4500
Average Salary: 45000
```

## 🧠 Concept Mapping (HR Analogy)

| Keyword  | HR Analogy                                       |
| -------- | ------------------------------------------------ |
| `ref`    | HR **modifies salary directly** in employee file |
| `out`    | HR **calculates bonus** and returns it           |
| `params` | HR processes **any number of salaries**          |
| `static` | Company-wide **employee counter**                |

## 🧪 Student Tasks

### ✅ Task 1

Add a method:

```csharp
static double CalculateTax(double salary)
```

### ✅ Task 2

Use `ref` to **update employee designation**

### ✅ Task 3

Use `out` to return:

* PF
* Bonus

### ✅ Task 4

Use `params` to calculate:

* Maximum salary
* Minimum salary

## ❓ Interview Questions

1. Why must `ref` variables be initialized?
2. Difference between `ref` and `out`?
3. Why is `static` memory shared?
4. Can a static method access non-static fields?

## 🧩 Mentor Notes (For You)

This lab:

* Reinforces **memory & method behavior**
* Prepares students for **real-world debugging**
* Acts as a **bridge to OOP + DI concepts**
* Fits perfectly before **Interfaces & SOLID**
