# 🧪 Lab Assignment

## **Async & Await – HR Solution**

 **`async` / `await`**
using the **HR Solution domain**, written in the **same structure and depth** as your previous labs.

This lab helps students clearly understand **non-blocking execution**, **I/O-bound operations**, and **real-world async usage** in HR systems (DB calls, APIs, file access).

## 🎯 Lab Objective

To understand how **asynchronous programming** improves application responsiveness by:

* Executing long-running HR operations without blocking
* Using `Task`, `async`, and `await`
* Simulating real-world HR workloads (DB / API / File access)


## 🧠 Concepts Covered

| Concept                | Purpose                             |
| ---------------------- | ----------------------------------- |
| `async`                | Marks a method as asynchronous      |
| `await`                | Suspends execution without blocking |
| `Task<T>`              | Represents async operation result   |
| Non-blocking execution | Better scalability                  |


## 🏗️ Solution Structure

```
HRAsyncLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
└── Services
    └── HROperationService.cs
```

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

        public override string ToString()
        {
            return $"{Id} | {Name} | {Department} | {Salary}";
        }
    }
}
```

## 🧠 Step 2: HR Service with Async Operations

📄 **Services/HROperationService.cs**

```csharp
using HR.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Services
{
    public class HROperationService
    {
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            // Simulate database/API delay
            await Task.Delay(2000);

            return new List<Employee>
            {
                new Employee { Id=101, Name="Amit", Department="IT", Salary=50000 },
                new Employee { Id=102, Name="Neha", Department="HR", Salary=40000 },
                new Employee { Id=103, Name="Ravi", Department="Finance", Salary=60000 }
            };
        }

        public async Task<double> CalculateBonusAsync(Employee emp)
        {
            await Task.Delay(1000); // simulate calculation
            return emp.Salary * 0.10;
        }

        public async Task SaveEmployeeAsync(Employee emp)
        {
            await Task.Delay(1500); // simulate DB save
            Console.WriteLine($"Employee saved asynchronously: {emp.Name}");
        }
    }
}
```

## 🚀 Step 3: Program Execution (Async Flow)

📄 **Program.cs**

```csharp
using HR.Models;
using HR.Services;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("HR System Started...\n");

        HROperationService service = new HROperationService();

        // Async call to fetch employees
        var employees = await service.GetEmployeesAsync();

        Console.WriteLine("Employees Loaded:");
        employees.ForEach(e => Console.WriteLine(e));

        Console.WriteLine("\nCalculating bonus asynchronously...");
        double bonus = await service.CalculateBonusAsync(employees[0]);
        Console.WriteLine($"Bonus for {employees[0].Name}: {bonus}");

        await service.SaveEmployeeAsync(employees[0]);

        Console.WriteLine("\nHR System Completed.");
    }
}
```

## 🔍 Expected Output (with delays)

```
HR System Started...

Employees Loaded:
101 | Amit | IT | 50000
102 | Neha | HR | 40000
103 | Ravi | Finance | 60000

Calculating bonus asynchronously...
Bonus for Amit: 5000
Employee saved asynchronously: Amit

HR System Completed.
```


## 🧠 HR Analogy Mapping

| Technical Concept | HR Meaning                      |
| ----------------- | ------------------------------- |
| Async method      | HR request sent                 |
| Await             | Waiting without blocking others |
| Task              | Work order                      |
| Delay             | DB / approval processing        |

## 🧪 Student Tasks (Hands-On)

### ✅ Task 1

Add async method:

```csharp
Task<Employee> GetEmployeeByIdAsync(int id)
```

### ✅ Task 2

Call **bonus calculation for all employees** using `Task.WhenAll`

### ✅ Task 3

Remove `await` and observe compiler warning

### ✅ Task 4

Convert one async method to **synchronous** and compare execution time



## ❓ Interview Questions

1. Difference between `Task` and `Thread`?
2. What happens if `await` is not used?
3. Can `Main()` be async?
4. Difference between CPU-bound and I/O-bound tasks?

---

## 🧩 Mentor Notes (For You)

This lab:

* Builds foundation for:

  * ASP.NET Core async controllers
  * EF Core async queries
  * Microservices & cloud scalability
* Connects naturally with:

  * RabbitMQ
  * gRPC
  * Background workers

