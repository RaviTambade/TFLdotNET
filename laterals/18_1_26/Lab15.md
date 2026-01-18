Below is a **complete, mentor-style Lab Assignment** focused on
👉 **JSON Serialization & Deserialization**
using the **HR Employee Solution domain**, written in the **same format and depth** as your earlier labs.

This lab prepares students for **Web API, REST, Microservices, and data exchange** concepts.

---

# 🧪 Lab Assignment

## **JSON Serialization & Deserialization – HR Employee Solution**

---

## 🎯 Lab Objective

To understand how **HR domain objects** are:

* Converted into **JSON** (Serialization)
* Reconstructed back into **C# objects** (Deserialization)
* Stored, transferred, and consumed across applications

using **System.Text.Json**.

---

## 🧠 Concepts Covered

| Concept                | Purpose               |
| ---------------------- | --------------------- |
| Serialization          | Convert object → JSON |
| Deserialization        | Convert JSON → object |
| DTO mindset            | Data exchange format  |
| File-based persistence | In-memory → disk      |

---

## 🏗️ Solution Structure

```
HRJsonLab
│
├── Program.cs
│
├── Models
│   └── Employee.cs
│
└── Services
    └── EmployeeJsonService.cs
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

## 🧠 Step 2: JSON Service Layer

📄 **Services/EmployeeJsonService.cs**

```csharp
using HR.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HR.Services
{
    public class EmployeeJsonService
    {
        private readonly string _filePath = "employees.json";

        public void SerializeEmployees(List<Employee> employees)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(_filePath, json);
        }

        public List<Employee> DeserializeEmployees()
        {
            if (!File.Exists(_filePath))
                return new List<Employee>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Employee>>(json);
        }
    }
}
```

---

## 🚀 Step 3: Program Execution (End-to-End Flow)

📄 **Program.cs**

```csharp
using HR.Models;
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
            new Employee { Id=103, Name="Ravi", Department="IT", Salary=70000, Experience=6, IsPermanent=true }
        };

        EmployeeJsonService jsonService = new EmployeeJsonService();

        // Serialize to JSON
        jsonService.SerializeEmployees(employees);
        Console.WriteLine("Employees serialized to JSON file.");

        // Deserialize from JSON
        var loadedEmployees = jsonService.DeserializeEmployees();
        Console.WriteLine("\nEmployees loaded from JSON:");

        loadedEmployees.ForEach(e => Console.WriteLine(e));
    }
}
```

---

## 📄 Generated JSON Output (`employees.json`)

```json
[
  {
    "Id": 101,
    "Name": "Amit",
    "Department": "IT",
    "Salary": 50000,
    "Experience": 3,
    "IsPermanent": true
  },
  {
    "Id": 102,
    "Name": "Neha",
    "Department": "HR",
    "Salary": 40000,
    "Experience": 2,
    "IsPermanent": false
  }
]
```

---

## 🧠 HR Analogy Mapping

| Technical Concept | HR Meaning                              |
| ----------------- | --------------------------------------- |
| Serialization     | Employee file converted to digital form |
| JSON              | Standard HR data exchange format        |
| Deserialization   | Rebuilding employee records             |
| File storage      | Local HR archive                        |

---

## 🧪 Student Tasks (Hands-On)

### ✅ Task 1

Add `DateOfJoining` to `Employee` and serialize it

### ✅ Task 2

Create method:

```csharp
Employee GetEmployeeById(int id)
```

(from deserialized data)

### ✅ Task 3

Handle **null / missing JSON file** gracefully

### ✅ Task 4

Use `JsonPropertyName` to rename JSON fields

---

## ❓ Interview Questions

1. Difference between `System.Text.Json` and `Newtonsoft.Json`?
2. What happens if JSON property names don’t match class fields?
3. What is DTO and why JSON uses it?
4. Can private fields be serialized?

---

## 🧩 Mentor Notes (For You)

This lab:

* Directly connects to:

  * Web API request/response
  * Microservices communication
  * Event-driven architecture
* Prepares students for:

  * Swagger
  * REST contracts
  * API gateways

---
