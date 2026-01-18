

---

# 🧪 Lab 5: Interface Inheritance in HR Domain

Below is **🧪 Lab Assignment – Interface Inheritance**, designed as a **continuation of Lab 4 (HR Domain)**.

This lab **does not repeat OOP basics** — it builds directly on your existing `Employee`, `SalesEmployee`, and `SalesManager` hierarchy.


## 🎯 Lab Title

**Extending HR System using Interface Inheritance & Multiple Behavior Contracts**

---

## 🎯 Lab Objective

By completing this lab, students will:

* Understand **interfaces as behavior contracts**
* Implement **interface inheritance**
* Apply **multiple interface implementation**
* Compare **abstract classes vs interfaces**
* Design flexible HR roles without modifying base classes
* Use interfaces for **loosely coupled design**

---

## 🧠 Prerequisite Knowledge

Students should already know:

* Abstract classes and inheritance (Lab 4)
* Method overriding
* Polymorphism
* HR domain class hierarchy

---

## 🏗 Design Requirement (Problem Statement)

Your HR system now needs to support **cross-cutting responsibilities**:

1. Some employees **get appraisals**
2. Some employees **receive bonuses**
3. Some employees **conduct interviews**
4. Some employees **can train juniors**

These behaviors:

* Do **not** belong to every employee
* Must be **plugged in without changing Employee base class**

👉 **Interfaces are the correct solution**

---

## 🧠 Conceptual Design

### Interfaces (Contracts)

```
IAppraisable
IBonusEligible
IInterviewPanel
ITrainer
```

### Interface Inheritance

```
IBonusEligible
    ↑
IManagerBenefits
```

---

## 📁 Step 1: Create Interfaces Folder

Inside your existing project:

```cmd
mkdir HR\Interfaces
```

---

## 🧩 Step 2: Define Base Interfaces

### 📄 `HR/Interfaces/IAppraisable.cs`

```csharp
namespace HR.Interfaces;

public interface IAppraisable
{
    void ConductAppraisal();
}
```

---

### 📄 `HR/Interfaces/IBonusEligible.cs`

```csharp
namespace HR.Interfaces;

public interface IBonusEligible
{
    float CalculateBonus();
}
```

---

### 📄 `HR/Interfaces/IInterviewPanel.cs`

```csharp
namespace HR.Interfaces;

public interface IInterviewPanel
{
    void TakeInterview();
}
```

---

### 📄 `HR/Interfaces/ITrainer.cs`

```csharp
namespace HR.Interfaces;

public interface ITrainer
{
    void Train();
}
```

---

## 🧩 Step 3: Create Interface Inheritance

### 📄 `HR/Interfaces/IManagerBenefits.cs`

```csharp
namespace HR.Interfaces;

public interface IManagerBenefits : IBonusEligible, IAppraisable
{
    void ApproveLeave();
}
```

📌 **Key Learning**

* Interfaces can **inherit from multiple interfaces**
* No implementation, only contracts

---

## 🧩 Step 4: Implement Interfaces in HR Classes

### ✅ Modify `SalesEmployee`

📌 Implements **single interface**

```csharp
using HR.Interfaces;

namespace HR;

public class SalesEmployee : Employee, IAppraisable
{
    public void ConductAppraisal()
    {
        Console.WriteLine("Sales Employee appraisal completed.");
    }
}
```

---

### ✅ Modify `SalesManager`

📌 Implements **interface inheritance + multiple interfaces**

```csharp
using HR.Interfaces;

namespace HR;

public class SalesManager : SalesEmployee, 
                            IManagerBenefits,
                            IInterviewPanel,
                            ITrainer
{
    public float CalculateBonus()
    {
        return Bonus;
    }

    public void ConductAppraisal()
    {
        Console.WriteLine("Manager appraisal completed.");
    }

    public void ApproveLeave()
    {
        Console.WriteLine("Leave approved by Sales Manager.");
    }

    public void TakeInterview()
    {
        Console.WriteLine("Sales Manager conducting interview.");
    }

    public void Train()
    {
        Console.WriteLine("Sales Manager training sales team.");
    }
}
```

---

## ▶ Step 5: Test Interface Polymorphism in `Program.cs`

```csharp
using HR;
using HR.Interfaces;

SalesManager manager = new SalesManager();

IAppraisable appraisable = manager;
appraisable.ConductAppraisal();

IBonusEligible bonusEligible = manager;
Console.WriteLine("Bonus: " + bonusEligible.CalculateBonus());

IInterviewPanel panel = manager;
panel.TakeInterview();

ITrainer trainer = manager;
trainer.Train();
```

---

## 🧠 Key Observations for Students

| Concept              | Observation                  |
| -------------------- | ---------------------------- |
| Multiple inheritance | Achieved via interfaces      |
| Loose coupling       | Code depends on interfaces   |
| Polymorphism         | Same object, different views |
| Clean design         | No Employee modification     |

---

## 🔍 Abstract Class vs Interface (Quick Recall)

| Feature                | Abstract Class | Interface |
| ---------------------- | -------------- | --------- |
| Fields                 | Yes            | No        |
| Constructors           | Yes            | No        |
| Multiple inheritance   | ❌              | ✅         |
| Default implementation | Yes            | ❌         |
| Use case               | "Is-a"         | "Can-do"  |

---

## 📝 Lab Assignment Tasks (Mandatory)

### 🧪 Task 1

Create a new interface:

```csharp
IAuditable
{
    void Audit();
}
```

Implement it in `SalesManager`.

---

### 🧪 Task 2

Create a new role:

```text
HRManager : Employee
```

Implement:

* `IManagerBenefits`
* `IInterviewPanel`

---

### 🧪 Task 3

Write a method:

```csharp
void ProcessAppraisal(IAppraisable emp)
```

Call it with different employee types.

---

### 🧪 Task 4 (Thinking Task)

Answer:

> Why interface inheritance is preferred over deep class inheritance in enterprise applications?

---

## 🎓 Mentor Note (Transflower Learning)

> “Abstract classes model **what you are**.
> Interfaces model **what you can do**.
> Enterprise systems survive because of interfaces.”


