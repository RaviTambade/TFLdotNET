# 🧪 Lab Assignment

## **Garbage Collection & Dispose Pattern – HR Solution**

---

## 🎯 Lab Objective

To understand how **.NET manages memory automatically** and how developers must **explicitly release unmanaged resources** using:

* Garbage Collector
* `IDisposable`
* `Dispose()` pattern
* `using` statement

within an **HR Employee Management system**.

---

## 🧠 Concepts Covered

| Concept                        | Purpose                                        |
| ------------------------------ | ---------------------------------------------- |
| Garbage Collection             | Automatic memory cleanup                       |
| Managed vs Unmanaged resources | CLR responsibility vs developer responsibility |
| `IDisposable`                  | Manual cleanup                                 |
| Dispose Pattern                | Safe and predictable release                   |
| `using`                        | Automatic dispose                              |

---

## 🏗️ Solution Structure

```
HRMemoryLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
└── Services
    └── EmployeeFileLogger.cs
```

---

## 🧱 Step 1: Employee Domain Model (Managed Object)

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
    }
}
```

> ✅ This class uses **managed memory only** and is handled completely by **Garbage Collector**.

---

## 🧠 Step 2: Service Using Unmanaged Resource (File Handle)

📄 **Services/EmployeeFileLogger.cs**

```csharp
using HR.Models;
using System;
using System.IO;

namespace HR.Services
{
    public class EmployeeFileLogger : IDisposable
    {
        private StreamWriter _writer;
        private bool _disposed = false;

        public EmployeeFileLogger(string filePath)
        {
            _writer = new StreamWriter(filePath, append: true);
        }

        public void Log(Employee emp)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EmployeeFileLogger));

            _writer.WriteLine($"{emp.Id},{emp.Name},{emp.Department},{emp.Salary}");
        }

        // Dispose pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writer?.Dispose(); // release unmanaged resource wrapper
                }
                _disposed = true;
            }
        }

        // Finalizer (safety net)
        ~EmployeeFileLogger()
        {
            Dispose(false);
        }
    }
}
```

---

## 🚀 Step 3: Program Execution (GC + Dispose in Action)

📄 **Program.cs**

```csharp
using HR.Models;
using HR.Services;
using System;

class Program
{
    static void Main()
    {
        CreateEmployees();

        // Force GC (only for learning, not recommended in production)
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Garbage Collection completed.");
    }

    static void CreateEmployees()
    {
        Employee emp = new Employee
        {
            Id = 101,
            Name = "Amit",
            Department = "IT",
            Salary = 50000
        };

        // using ensures Dispose is called
        using (EmployeeFileLogger logger = new EmployeeFileLogger("employees.log"))
        {
            logger.Log(emp);
        }

        Console.WriteLine("Employee logged successfully.");
    }
}
```

---

## 📄 Output (`employees.log`)

```
101,Amit,IT,50000
```

---

## 🧠 HR Analogy Mapping

| Technical Concept  | HR Meaning                   |
| ------------------ | ---------------------------- |
| Garbage Collector  | Office housekeeping staff    |
| Managed memory     | Digital employee records     |
| Unmanaged resource | Physical file cabinet        |
| Dispose            | Locking and closing cabinets |
| using              | Auto-cleanup policy          |

---

## 🧪 Student Tasks (Hands-On)

### ✅ Task 1

Add a **Console log** inside destructor to see when GC runs

### ✅ Task 2

Modify `EmployeeFileLogger` to write **DateTime**

### ✅ Task 3

Remove `using` and observe file lock behavior

### ✅ Task 4

Explain difference between:

```csharp
GC.Collect();
Dispose();
```

---

## ❓ Interview Questions

1. Does GC free unmanaged resources?
2. When is finalizer executed?
3. Why call `GC.SuppressFinalize()`?
4. What happens if Dispose is not called?

---

## 🧩 Mentor Notes (For You)

This lab:

* Builds **low-level understanding** (rare but powerful)
* Separates **CLR responsibilities vs developer responsibilities**
* Prepares students for:

  * File handling
  * Database connections
  * Network streams
  * High-load server applications

