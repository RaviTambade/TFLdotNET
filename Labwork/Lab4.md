
# 🧪 Lab 4: Object-Oriented Programming with Inheritance & Polymorphism (HR Domain)

Below is **🧪 Lab 4**, focused on **Object-Oriented Programming (OOP) in C#** using your provided `HR` namespace code.
## 🎯 Lab Title

**Designing an Employee Hierarchy using Abstraction, Inheritance, and Polymorphism**



## 🎯 Lab Objective

By the end of this lab, students will be able to:

* Understand the **Object class** as the root of all classes
* Use **abstract classes and abstract methods**
* Implement **inheritance** and **method overriding**
* Apply **constructor chaining**
* Use **virtual and override keywords**
* Demonstrate **runtime polymorphism**
* Model a **real-world HR system**

## 🧠 Concepts Covered

* `Object` class members

  * `ToString()`
  * `Equals()`
  * `GetHashCode()`
  * `GetType()`
* Abstract Class
* Virtual vs Override
* Constructor chaining (`base`)
* Inheritance hierarchy
* Method overriding
* Polymorphism

## 🏗 System Design Overview (Think First)

```
Object
  |
Employee (abstract)
  |
SalesEmployee
  |
SalesManager
```

📌 **Real-world mapping**

* `Employee` → Base HR identity
* `SalesEmployee` → Employee with sales responsibility
* `SalesManager` → Senior role with bonus


## 📁 Step 1: Create a New Console Project

```cmd
dotnet new console -n HRApp
cd HRApp
```

📌 This will act as the **application runner**.

## 📁 Step 2: Create HR Folder and Classes

Inside the project folder:

```cmd
mkdir HR
```

Create files:

```cmd
HR\Employee.cs
HR\SalesEmployee.cs
HR\SalesManager.cs
```

## 🧩 Step 3: Implement `Employee` (Abstract Base Class)

📄 **HR/Employee.cs**

### Key Responsibilities

* Represents a **generic employee**
* Cannot be instantiated
* Forces child classes to implement `DoWork()`

### Key Learning Points

* `abstract class`
* Constructor chaining
* `virtual` method
* Overriding `ToString()`

👉 Paste the provided `Employee` code exactly as given.

📌 **Teaching Insight**

> Every class in C# ultimately inherits from `Object`.
> `Employee` customizes `ToString()` instead of using `Object.ToString()`.


## 🧩 Step 4: Implement `SalesEmployee` (Inheritance + Override)

📄 **HR/SalesEmployee.cs**

### Key Responsibilities

* Inherits from `Employee`
* Adds sales-related properties
* Overrides behavior

### Concepts Demonstrated

* `: Employee` (Inheritance)
* `base()` constructor call
* Overriding abstract & virtual methods

👉 Paste the provided `SalesEmployee` code.

📌 **Business Logic**

* Incentive is granted only if target is achieved
* Salary = Base salary + Incentive

## 🧩 Step 5: Implement `SalesManager` (Multi-level Inheritance)

📄 **HR/SalesManager.cs**

### Key Responsibilities

* Inherits from `SalesEmployee`
* Adds `Bonus`
* Enhances salary computation

### Concepts Demonstrated

* Multi-level inheritance
* Reusing base behavior
* Extending functionality

👉 Paste the provided `SalesManager` code.

📌 **Design Insight**

> Managers don’t rewrite logic — they **extend it**

## ▶ Step 6: Use Polymorphism in `Program.cs`

📄 **Program.cs**

```csharp
using HR;

Employee emp1 = new SalesEmployee(
    1, "Amit", "Sharma", "amit@company.com",
    "9999999999", "Mumbai",
    new DateTime(1995, 5, 12),
    1000, 5000, 20000,
    100000, 120000
);

Employee emp2 = new SalesManager(
    2, "Neha", "Patil", "neha@company.com",
    "8888888888", "Pune",
    new DateTime(1990, 3, 22),
    1500, 7000, 30000,
    200000, 250000, 10000
);

emp1.DoWork();
emp2.DoWork();

Console.WriteLine(emp1);
Console.WriteLine("Salary: " + emp1.ComputePay());

Console.WriteLine(emp2);
Console.WriteLine("Salary: " + emp2.ComputePay());
```

## 🧪 Step 7: Build and Run

```cmd
dotnet build
dotnet run
```

---

## 🧠 Expected Learning Output

Students will observe:

* Same method call → **different behavior**
* `Employee` reference holding different objects
* Runtime decision of `ComputePay()` and `DoWork()`

📌 **This is POLYMORPHISM**

## 🧠 Concept Mapping (Exam + Interview Ready)

| Concept              | Where Seen                          |
| -------------------- | ----------------------------------- |
| Abstraction          | `abstract class Employee`           |
| Inheritance          | `SalesEmployee : Employee`          |
| Polymorphism         | `Employee emp = new SalesManager()` |
| Constructor Chaining | `: base(...)`                       |
| Method Overriding    | `ComputePay()`                      |
| Object Class         | `ToString()`                        |

## 📝 Lab Assignments (Student Tasks)

1. Add a new class `HRManager` with:

   * Extra allowance
   * Custom `DoWork()`
2. Override `Equals()` in `Employee`
3. Use `GetType()` to print runtime type
4. Convert salary logic to use `decimal`

## 🎓 Mentor Note 

> “Frameworks change.
> OOP principles never change.
> Master this, and any language becomes easy.”


