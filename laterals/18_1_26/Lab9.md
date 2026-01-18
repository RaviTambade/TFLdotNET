

# 🧪 Lab 9: SOLID Principles Mapped to HR Domain
This is the **right sequencing**. After Clean Architecture, **SOLID** becomes crystal clear.

Below is **🧪 Lab 9**, designed as a **direct continuation** of your HR Clean Architecture labs and written in **Transflower mentor style**
## 🎯 Lab Title

**Applying SOLID Design Principles in an HR Management System**

---

## 🎯 Lab Objective

By the end of this lab, students will:

* Understand **each SOLID principle practically**
* Identify **bad design vs good design**
* Apply SOLID in HR domain classes
* Write **maintainable, extensible, testable code**
* Be interview-ready with **real examples**

---

## 🧠 What is SOLID?

> **SOLID** is a set of **5 design principles** that help software **grow without breaking**.

```
S – Single Responsibility
O – Open / Closed
L – Liskov Substitution
I – Interface Segregation
D – Dependency Inversion
```

---

## 🏗 HR Domain Context (Reminder)

```
Employee
 ├── SalesEmployee
 ├── SalesManager
 └── HRManager
```

---

# 🔴 S — Single Responsibility Principle (SRP)

> **A class should have only ONE reason to change**

---

## ❌ Bad Design

```csharp
public class EmployeeManager
{
    public void ComputeSalary(Employee emp) { }
    public void SaveToDatabase(Employee emp) { }
    public void SendEmail(Employee emp) { }
}
```

❌ Too many responsibilities

---

## ✅ Good Design (HR Domain)

```csharp
EmployeeService      → Business logic
EmployeeRepository   → Data access
NotificationService  → Communication
```

📌 **Mapping**

* Salary → Payroll concern
* Storage → Repository
* Email → Notification service

---

## 🧪 Student Task (SRP)

Split `EmployeeManager` into:

* `PayrollService`
* `EmployeeRepository`
* `NotificationService`

---

# 🔴 O — Open / Closed Principle (OCP)

> **Open for extension, closed for modification**

---

## ❌ Bad Design

```csharp
if(emp is SalesEmployee)
    salary = ...
else if(emp is SalesManager)
    salary = ...
```

❌ Code breaks when new role is added

---

## ✅ Good Design (Polymorphism)

```csharp
public abstract class Employee
{
    public abstract float ComputePay();
}
```

📌 New roles → new classes
📌 Existing code → unchanged

---

## 🧪 Student Task (OCP)

Add:

```
ContractEmployee : Employee
```

Without changing any salary logic.

---

# 🔴 L — Liskov Substitution Principle (LSP)

> **Derived classes must be substitutable for base classes**

---

## ❌ Violation Example

```csharp
public class Intern : Employee
{
    public override float ComputePay()
    {
        throw new Exception("Interns not paid");
    }
}
```

❌ Breaks expectations

---

## ✅ Correct Design

```csharp
public class Intern : Employee
{
    public override float ComputePay()
    {
        return 0;
    }
}
```

📌 Program continues safely

---

## 🧪 Student Task (LSP)

Verify:

```csharp
List<Employee> employees;
```

Works for **all employee types**.

---

# 🔴 I — Interface Segregation Principle (ISP)

> **Many small interfaces are better than one big interface**

---

## ❌ Bad Interface

```csharp
public interface IEmployeeActions
{
    void DoWork();
    void ApproveLeave();
    void ConductInterview();
    void Train();
}
```

❌ Forces unnecessary implementation

---

## ✅ Good Design (HR Domain)

```csharp
ITrainer
IInterviewPanel
ILeaveApprover
```

📌 Implement only what is needed

---

## 🧪 Student Task (ISP)

Refactor a large interface into **role-based interfaces**.

---

# 🔴 D — Dependency Inversion Principle (DIP)

> **Depend on abstractions, not concretions**

---

## ❌ Bad Design

```csharp
EmployeeService service = new EmployeeService(
    new InMemoryEmployeeRepository()
);
```

❌ High-level depends on low-level

---

## ✅ Good Design

```csharp
IEmployeeRepository repo = new InMemoryEmployeeRepository();
EmployeeService service = new EmployeeService(repo);
```

📌 Interfaces in Application layer
📌 Implementations in Infrastructure

---

## 🧪 Student Task (DIP)

Replace:

```
InMemoryEmployeeRepository
```

with:

```
FileEmployeeRepository
```

Without touching service code.

---

## 🧠 SOLID Summary Table (HR Mapping)

| Principle | HR Example                    |
| --------- | ----------------------------- |
| SRP       | Payroll ≠ Storage ≠ Email     |
| OCP       | Add new employee types safely |
| LSP       | Any Employee works everywhere |
| ISP       | Role-based HR capabilities    |
| DIP       | Services depend on interfaces |

---

## 📝 Final Lab Assignment (Mandatory)

### 🧪 Task 1

Create a diagram mapping **SOLID → HR classes**

---

### 🧪 Task 2

Identify **2 SOLID violations** in your earlier labs and fix them.

---

### 🧪 Task 3

Write **one interview explanation** per SOLID principle using HR example.

---

### 🧪 Task 4 (Thinking Task)

Answer:

> Which SOLID principle is most critical for Microservices and why?

---

## 🎓 Mentor Note (Transflower Learning)

> “Frameworks come and go.
> SOLID principles decide whether your code survives.”

If students **master this lab**, they are:

* Architecture-ready
* Framework-independent
* Industry-aligned

 
