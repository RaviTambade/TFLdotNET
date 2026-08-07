# Inheritance

> **"Good morning, future software engineers! Today we are going to enter the HR department of a software company. Imagine that the company has thousands of employees — developers, managers, testers, sales executives, architects. They all have salaries, but their salary calculations are not necessarily the same. How would we model this in an object-oriented way?"**

This simple business problem will help us understand some very important C# concepts:

```text
Employee
   │
   ├── Constructor Overloading
   │
   ├── Method Overriding
   │
   ├── Method Shadowing
   │
   ├── Sealed Class
   │
   ├── Concrete Class
   │
   └── Abstract Class
```


# 🏢 The Story of an Employee

Imagine a company called **Transflower Software**. Every employee has:

```text
Employee
 │
 ├── Basic Salary
 ├── HRA
 └── DA
```

A Manager has everything an Employee has, plus:

```text
Manager
 │
 ├── Basic Salary
 ├── HRA
 ├── DA
 └── Incentive
```

So we can model this using inheritance.

```text
                 Employee
                    │
                    │ IS-A
                    ▼
                 Manager
```

The statement:

> **A Manager IS-A Employee**

makes inheritance meaningful.



# Base Class – Employee

Let's create our base class.

```csharp
public class Employee
{
    private double basicSal;
    private double hra;
    private double da;

    public Employee()
    {
        basicSal = 5000;
        hra = 1200;
        da = 700;
    }

    public Employee(
        double basicSal,
        double hra,
        double da)
    {
        this.basicSal = basicSal;
        this.hra = hra;
        this.da = da;
    }

    public virtual double CalculateSalary()
    {
        return basicSal + hra + da;
    }

    public override string ToString()
    {
        return
            "Basic Salary = " + basicSal +
            ", HRA = " + hra +
            ", DA = " + da;
    }
}
```

The class represents the common employee information.

```text
Employee
+-------------------------+
| basicSal                |
| hra                     |
| da                      |
+-------------------------+
| CalculateSalary()       |
| ToString()              |
+-------------------------+
```

# 🔄 Constructor Overloading

Look carefully. We have two constructors:

```csharp
public Employee()
{
    basicSal = 5000;
    hra = 1200;
    da = 700;
}
```

and:

```csharp
public Employee(
    double basicSal,
    double hra,
    double da)
{
    this.basicSal = basicSal;
    this.hra = hra;
    this.da = da;
}
```

Both have the same name:

```text
Employee()
Employee(double, double, double)
```

but different parameter lists. This is called:

# 🔄 Constructor Overloading

> **Constructor overloading means providing multiple constructors with different parameter lists in the same class.**

Usage:

```csharp
Employee emp1 = new Employee();

Employee emp2 =
    new Employee(10000, 3000, 2000);
```

The compiler decides which constructor to call based on the arguments.

```text
new Employee()
      │
      ▼
Employee()


new Employee(10000, 3000, 2000)
      │
      ▼
Employee(double, double, double)
```

This is an example of **compile-time polymorphism**.

# 💰 CalculateSalary()

Our Employee has:

```csharp
public virtual double CalculateSalary()
{
    return basicSal + hra + da;
}
```

The important keyword here is:

```csharp
virtual
```

It means:

> **"Derived classes are allowed to provide their own implementation of this method."**

So:

```text
Employee
   │
   └── virtual CalculateSalary()
             │
             ▼
       "You may override me."
```

# 👨‍💼 Creating the Manager

Now let's create a specialized Employee.

```csharp
public class Manager : Employee
{
    private double incentive;

    public Manager() : base()
    {
        incentive = 0;
    }

    public Manager(
        double basicSal,
        double hra,
        double da,
        double incentive)
        : base(basicSal, hra, da)
    {
        this.incentive = incentive;
    }

    public double CalculateIncentives()
    {
        return incentive * 2;
    }

    public override double CalculateSalary()
    {
        return base.CalculateSalary()
               + CalculateIncentives();
    }

    public override string ToString()
    {
        return base.ToString() +
               ", Incentive = " + incentive;
    }
}
```

Now our hierarchy becomes:

```text
                 Employee
                    │
          ┌─────────┴─────────┐
          │                   │
      Salary              Common Data
          │
          ▼
       Manager
          │
          └── Incentive
```


# 🏗️ What Does `base()` Mean?

Look at:

```csharp
public Manager() : base()
```

This tells C#:

> **"Before constructing the Manager part, execute the Employee constructor."**

Think of object construction as building a house.

```text
Create Manager
      │
      ▼
Construct Employee part
      │
      ▼
Construct Manager part
      │
      ▼
Manager object ready
```

So:

```csharp
Manager mgr = new Manager();
```

causes:

```text
Manager()
   │
   ▼
base()
   │
   ▼
Employee()
   │
   ▼
Manager constructor body
```

# 🧬 Parameterized Base Constructor

Now consider:

```csharp
public Manager(
    double basicSal,
    double hra,
    double da,
    double incentive)
    : base(basicSal, hra, da)
{
    this.incentive = incentive;
}
```

The:

```csharp
: base(...)
```

calls the base class constructor.

For example:

```csharp
Manager mgr =
    new Manager(
        20000,
        5000,
        3000,
        4000);
```

Construction becomes:

```text
Manager(...)
      │
      ▼
Employee(
  20000,
  5000,
  3000
)
      │
      ▼
Manager incentive = 4000
```

# 🎭 Method Overriding

Now we reach one of the most important OOP concepts.

Employee says:

```csharp
public virtual double CalculateSalary()
{
    return basicSal + hra + da;
}
```

Manager says:

```csharp
public override double CalculateSalary()
{
    return base.CalculateSalary()
           + CalculateIncentives();
}
```

The keyword:

```csharp
override
```

means:

> **"I am replacing the inherited implementation with a specialized implementation."**

This is **method overriding**.


# 🔥 Why Override?

An Employee salary may be:

```text
Basic Salary
     +
HRA
     +
DA
```

A Manager salary may be:

```text
Basic Salary
     +
HRA
     +
DA
     +
Incentive
```

Same operation:

```text
CalculateSalary()
```

Different behavior.

```text
Employee
   │
   └── CalculateSalary()
          │
          ▼
     Basic + HRA + DA


Manager
   │
   └── CalculateSalary()
          │
          ▼
     Basic + HRA + DA + Incentive
```

That is polymorphism.

# Runtime Polymorphism

Now look at this:

```csharp
Employee emp = new Manager();

double salary =
    emp.CalculateSalary();
```

The reference type is:

```text
Employee
```

but the actual object is:

```text
Manager
```

Therefore the overridden Manager implementation executes.

```text
Employee reference
       │
       ▼
Manager object
       │
       ▼
CalculateSalary()
       │
       ▼
Manager implementation
```

This is called:

# Runtime Polymorphism

Because the implementation is determined based on the **actual object at runtime**.


# Shadowing – Hiding Instead of Overriding

Now let's imagine a different situation.

Suppose `SalesEmployee` wants its own `CalculateSalary()`.

Instead of overriding:

```csharp
public override double CalculateSalary()
```

it writes:

```csharp
public new double CalculateSalary()
```

The keyword:

```csharp
new
```

means:

> **"Hide the inherited member. I am not overriding it."**

Example:

```csharp
class Employee
{
    public virtual double CalculateSalary()
    {
        return 5000;
    }
}
```

Derived class:

```csharp
class SalesEmployee : Employee
{
    public new double CalculateSalary()
    {
        return 10000;
    }
}
```

Now:

```csharp
SalesEmployee employee =
    new SalesEmployee();

Console.WriteLine(
    employee.CalculateSalary()
);
```

calls:

```text
SalesEmployee.CalculateSalary()
```

because the reference is `SalesEmployee`.



# Shadowing vs Overriding

This distinction is extremely important.

### Overriding

```csharp
public override double CalculateSalary()
```

### Shadowing

```csharp
public new double CalculateSalary()
```

Compare them:

```text
                 Employee
                    │
          CalculateSalary()
                    │
          ┌─────────┴─────────┐
          ▼                   ▼
       override               new
          │                   │
          ▼                   ▼
      Replace             Hide
     behavior             behavior
```



# The Interview Trap

Consider:

```csharp
Employee emp =
    new SalesEmployee();

double salary =
    emp.CalculateSalary();
```

If `SalesEmployee` uses:

```csharp
public new double CalculateSalary()
```

the **base implementation** is called through the `Employee` reference. Why? Because shadowing is not runtime overriding. Conceptually:

```text
Employee reference
       │
       ▼
Employee.CalculateSalary()
```

Whereas with:

```csharp
public override double CalculateSalary()
```

the runtime dispatches to:

```text
Employee reference
       │
       ▼
Actual object = SalesEmployee
       │
       ▼
SalesEmployee.CalculateSalary()
```

This is why `override` and `new` must not be confused.



# Sealed Class – "No More Children!"

Now imagine the company says:

> "This class is complete. Nobody is allowed to inherit from it."

C# provides:

```csharp
sealed
```

Example:

```csharp
public sealed class SinglyList
{
    public void Add()
    {
        // Add record
    }
}
```

Now this is illegal:

```csharp
public class StringSinglyList : SinglyList
{
}
```

The compiler reports an error because:

```text
SinglyList
    │
    X
    │
StringSinglyList
```

Inheritance is blocked.



# 🔐 Why Use a Sealed Class?

A class might be sealed because:

* Its behavior must not be changed through inheritance.
* The designer wants to prevent extension.
* The class has invariants that subclasses could violate.
* The implementation is intended to remain fixed.

For example:

```csharp
public sealed class SecurityToken
{
    // Controlled implementation
}
```

The important idea is:

> **A sealed class can be instantiated, but it cannot be inherited.**



# Concrete Class

Now let's understand another term. A **concrete class** is a normal class from which objects can be created.

Example:

```csharp
public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Person()
    {
    }

    public Person(
        string firstName,
        string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
```

We can create objects:

```csharp
Person p1 = new Person();

Person p2 =
    new Person("Amit", "Patil");
```

Therefore:

```text
Person
  │
  ├── Object 1
  ├── Object 2
  └── Object 3
```

The class is **instantiable**.



# Abstract Class – "Do Not Create Me Directly"

Now imagine the HR department says:

> "Employee is a general concept. We don't want to create a generic Employee. Every employee must have a specific role."

We can represent this using:

```csharp
abstract
```

Example:

```csharp
public abstract class Employee
{
    public decimal BasicSalary { get; set; }

    public decimal Hra { get; set; }

    public decimal Da { get; set; }

    public virtual double CalculateSalary()
    {
        return (double)(
            BasicSalary + Hra + Da);
    }

    public abstract double CalculateBonus();
}
```

Now this is illegal:

```csharp
Employee emp =
    new Employee();
```

Because an abstract class cannot be instantiated directly.



# Abstract Class as a Blueprint

Think of an abstract class as a **partially completed blueprint**.

```text
              ABSTRACT CLASS
                    Employee
                       │
          ┌────────────┼────────────┐
          │            │            │
       State       Common Code    Contract
          │            │            │
          ▼            ▼            ▼
      Salary       Methods      Abstract Method
```

It can contain:

```text
Abstract Class
   │
   ├── Fields
   ├── Properties
   ├── Constructors
   ├── Concrete Methods
   ├── Virtual Methods
   └── Abstract Methods
```

This is important:

> **An abstract class does not have to contain only abstract methods.**

It can contain fully implemented methods as well.



# Abstract Method

An abstract method is a method declaration without an implementation.

Example:

```csharp
public abstract double CalculateBonus();
```

There is no method body.

The abstract class is saying:

> **"Every concrete child must provide its own implementation."**



# Manager Implements the Abstract Contract

```csharp
public class Manager : Employee
{
    public override double CalculateBonus()
    {
        return (double)(BasicSalary * 0.20m);
    }
}
```

Now:

```text
              Employee
           abstract class
                │
                │
        CalculateBonus()
          MUST IMPLEMENT
                │
                ▼
             Manager
                │
                ▼
         CalculateBonus()
             = 20%
```


# Using an Abstract Class

We cannot do:

```csharp
Employee emp =
    new Employee();     // ❌
```

But we can do:

```csharp
Employee emp =
    new Manager();      // ✅
```

Why?

Because:

```text
Employee
   ▲
   │
   │ IS-A
   │
Manager
```

A Manager is an Employee.

Now:

```csharp
double salary =
    emp.CalculateSalary();

double bonus =
    emp.CalculateBonus();
```

The reference is:

```text
Employee
```

The actual object is:

```text
Manager
```

So the Manager implementations execute.



# 🧠 Abstract Class vs Concrete Class

| Feature                      | Concrete Class | Abstract Class |
| ---------------------------- | -------------- | -------------- |
| Create object directly       | ✅              | ❌              |
| Can have fields              | ✅              | ✅              |
| Can have properties          | ✅              | ✅              |
| Can have constructors        | ✅              | ✅              |
| Can have implemented methods | ✅              | ✅              |
| Can have abstract methods    | ❌              | ✅              |
| Can be inherited             | ✅              | ✅              |

Think:

```text
Concrete Class
     │
     └── "You can create me."


Abstract Class
     │
     └── "You can inherit me,
           but don't create me directly."
```



# Abstract vs Virtual

Students often confuse these two.

### Virtual Method

```csharp
public virtual double CalculateSalary()
{
    return BasicSalary + Hra + Da;
}
```

It says:

> **"Here is a default implementation. You may override it."**

### Abstract Method

```csharp
public abstract double CalculateBonus();
```

It says:

> **"I am providing no implementation. The concrete child must provide one."**

Compare:

```text
virtual
   │
   ├── Has implementation
   └── Override is optional


abstract
   │
   ├── No implementation
   └── Override is mandatory
```



# Complete Employee Design

Let's combine everything.

```csharp
public abstract class Employee
{
    protected double BasicSalary;
    protected double Hra;
    protected double Da;

    protected Employee()
    {
        BasicSalary = 5000;
        Hra = 1200;
        Da = 700;
    }

    protected Employee(
        double basicSalary,
        double hra,
        double da)
    {
        BasicSalary = basicSalary;
        Hra = hra;
        Da = da;
    }

    public virtual double CalculateSalary()
    {
        return BasicSalary + Hra + Da;
    }

    public abstract double CalculateBonus();
}
```

Manager:

```csharp
public class Manager : Employee
{
    private double incentive;

    public Manager()
        : base()
    {
        incentive = 0;
    }

    public Manager(
        double basicSalary,
        double hra,
        double da,
        double incentive)
        : base(basicSalary, hra, da)
    {
        this.incentive = incentive;
    }

    public double CalculateIncentives()
    {
        return incentive * 2;
    }

    public override double CalculateSalary()
    {
        return base.CalculateSalary()
             + CalculateIncentives();
    }

    public override double CalculateBonus()
    {
        return BasicSalary * 0.20;
    }
}
```

Now:

```csharp
static void Main()
{
    Employee emp =
        new Manager(
            20000,
            5000,
            3000,
            4000);

    double salary =
        emp.CalculateSalary();

    double bonus =
        emp.CalculateBonus();

    Console.WriteLine(
        $"Salary = {salary}");

    Console.WriteLine(
        $"Bonus = {bonus}");
}
```

The architecture is:

```text
                 Employee
              <<abstract>>
                    │
       ┌────────────┼────────────┐
       │            │            │
     State       virtual      abstract
       │         method        method
       │            │            │
       ▼            ▼            ▼
   Salary      Calculate()   CalculateBonus()
                    │            │
                    └──────┬─────┘
                           ▼
                        Manager
                           │
                    override methods
```



# The Complete Concept Map

All the concepts we discussed are connected.

```text
                         OOP
                          │
                          ▼
                    Inheritance
                          │
             ┌────────────┼────────────┐
             ▼            ▼            ▼
        Overloading    Overriding    Shadowing
             │            │            │
             ▼            ▼            ▼
       Compile-time   Runtime      Hiding
       polymorphism  polymorphism  using new
                          │
                          ▼
                   virtual + override
```

And class types:

```text
                       CLASS
                         │
            ┌────────────┼────────────┐
            ▼            ▼            ▼
        Concrete      Abstract      Sealed
            │            │            │
            ▼            ▼            ▼
       Can create     Cannot       Cannot
       objects        instantiate  inherit
                      directly
```



# Interview Revision Table

| Concept                 | Keyword         | Meaning                                           |
| ----------------------- | --------------- | ------------------------------------------------- |
| Constructor Overloading | —               | Multiple constructors with different parameters   |
| Method Overloading      | —               | Same method name, different parameter lists       |
| Method Overriding       | `override`      | Derived class replaces virtual/abstract behavior  |
| Virtual Method          | `virtual`       | Provides default behavior that can be overridden  |
| Abstract Method         | `abstract`      | Declares required behavior without implementation |
| Shadowing               | `new`           | Hides an inherited member                         |
| Sealed Class            | `sealed`        | Prevents inheritance                              |
| Concrete Class          | —               | Can be instantiated                               |
| Abstract Class          | `abstract`      | Cannot be instantiated directly                   |
| Base Constructor        | `base(...)`     | Calls parent constructor                          |
| Base Implementation     | `base.Method()` | Calls parent implementation                       |



# 🌟 Mentor's Golden Wisdom

> **"Students, don't memorize `virtual`, `override`, `new`, `abstract`, and `sealed` as isolated keywords. Understand the question each keyword answers."**

```text
virtual
↓
"Can my child change this behavior?"

override
↓
"I am changing the inherited behavior."

new
↓
"I am hiding the inherited member."

abstract
↓
"Every concrete child MUST provide this behavior."

sealed
↓
"No class is allowed to inherit from me."

base
↓
"Go to my parent implementation."
```

And remember the most important distinction:

```text
                    Inheritance
                         │
             ┌───────────┴───────────┐
             ▼                       ▼
        Override                  Shadow
        override                    new
             │                       │
             ▼                       ▼
      Runtime dispatch          Member hiding
```

> **"Good OOP design is not about using inheritance everywhere. First ask whether the relationship is genuinely `IS-A`. If it is, inheritance may be appropriate. If you simply want to reuse functionality, consider composition. As your applications become larger, this distinction separates code that merely works from code that is maintainable."**