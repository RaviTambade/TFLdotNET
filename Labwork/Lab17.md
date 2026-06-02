# 🧪 Lab Assignment

## **Delegates & Events – HR Solution**

---

## 🎯 Lab Objective

To understand how **HR system components communicate** using:

* Delegates (method references)
* Events (publisher–subscriber pattern)
* Event-driven design

within an **Employee Management System**.

---

## 🧠 Concepts Covered

| Concept              | Purpose                      |
| -------------------- | ---------------------------- |
| Delegate             | Reference to a method        |
| Multicast delegate   | Call multiple methods        |
| Event                | Controlled delegate exposure |
| Publisher–Subscriber | Loose coupling               |

---

## 🏗️ Solution Structure

```
HREventDelegateLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
└── Services
    ├── HRService.cs
    └── NotificationService.cs
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
        public double Salary;

        public override string ToString()
        {
            return $"{Id} | {Name} | {Salary}";
        }
    }
}
```

---

## 🧠 Step 2: Define Delegate & Event (Publisher)

📄 **Services/HRService.cs**

```csharp
using HR.Models;
using System;

namespace HR.Services
{
    // Delegate declaration
    public delegate void EmployeeAddedHandler(Employee emp);

    public class HRService
    {
        // Event based on delegate
        public event EmployeeAddedHandler EmployeeAdded;

        public void AddEmployee(Employee emp)
        {
            Console.WriteLine($"Employee Added: {emp}");

            // Raise event
            EmployeeAdded?.Invoke(emp);
        }
    }
}
```

---

## 🧠 Step 3: Subscriber Services (Event Handlers)

📄 **Services/NotificationService.cs**

```csharp
using HR.Models;
using System;

namespace HR.Services
{
    public class NotificationService
    {
        public void SendEmail(Employee emp)
        {
            Console.WriteLine($"📧 Email sent to HR for new employee: {emp.Name}");
        }

        public void SendSMS(Employee emp)
        {
            Console.WriteLine($"📱 SMS alert: Employee {emp.Name} onboarded");
        }
    }
}
```

---

## 🚀 Step 4: Program Execution (Wiring Events)

📄 **Program.cs**

```csharp
using HR.Models;
using HR.Services;

class Program
{
    static void Main()
    {
        HRService hrService = new HRService();
        NotificationService notifier = new NotificationService();

        // Subscribe to event
        hrService.EmployeeAdded += notifier.SendEmail;
        hrService.EmployeeAdded += notifier.SendSMS;

        Employee emp = new Employee
        {
            Id = 101,
            Name = "Amit",
            Salary = 50000
        };

        hrService.AddEmployee(emp);
    }
}
```

---

## 🔍 Expected Output

```
Employee Added: 101 | Amit | 50000
📧 Email sent to HR for new employee: Amit
📱 SMS alert: Employee Amit onboarded
```

---

## 🧠 HR Analogy Mapping

| Technical Concept | HR Meaning               |
| ----------------- | ------------------------ |
| Delegate          | Contact list of handlers |
| Event             | Official HR announcement |
| Publisher         | HR department            |
| Subscriber        | Email / SMS systems      |

---

## 🧪 Student Tasks (Hands-On)

### ✅ Task 1

Add another event:

```csharp
EmployeeSalaryUpdated
```

### ✅ Task 2

Unsubscribe SMS notification after first call

### ✅ Task 3

Convert delegate to:

```csharp
EventHandler<Employee>
```

### ✅ Task 4

Raise event only if salary > 40000

---

## ❓ Interview Questions

1. Why are events safer than public delegates?
2. Can events have multiple subscribers?
3. Difference between delegate and event?
4. What happens if no one subscribes to an event?

---

## 🧩 Mentor Notes (For You)

This lab:

* Introduces **event-driven thinking**
* Maps directly to:

  * UI events
  * Domain events
  * Message brokers (RabbitMQ, Kafka)
* Prepares students for:

  * Observer pattern
  * Clean Architecture domain events

