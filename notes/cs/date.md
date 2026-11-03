## Thinking in Objects – Modeling Real-World Concepts Using a C# Date Class

### 1. Today's Learning Objective

Dear students, Today I want you to think beyond writing C# statements. Imagine that you are developing an insurance application. You need to manage:

- Customer date of birth
- Policy start date
- Policy expiry date
- Premium payment date
- Claim registration date

Would you create three separate integer variables everywhere?

```csharp
int day;
int month;
int year;
```

Or would you design a reusable object called `Date`? Today, we will learn how software engineers make this decision.

### 2. Start with a Real-World Concept

Consider this date:

```text
12/10/2025
```

What information does it contain?

```text
Date
 |
 +-- Day   = 12
 +-- Month = 10
 +-- Year  = 2025
```

What operations can we perform?

```text
Date
 |
 +-- Show()
 +-- IsLeapYear()
 +-- NextDay()
 +-- PreviousDay()
 +-- CompareTo()
```

**Mentor:** What do you observe?

**Student:** Sir, a date contains information and can perform operations.

**Mentor:** Exactly! In object-oriented programming, we combine these two things.

> An object combines state and behavior.

### 3. Class – The Blueprint

Let's create our first class.

```csharp
public class Date
{
}
```

Think about a building architect. An architect creates one blueprint, but builders can construct many houses using that blueprint.

```text
             Date Class
           (Blueprint)
                |
        +-------+-------+
        |               |
        v               v
      today          birthday
        |               |
   12/10/2025       05/07/2003
```

A class defines the structure. An object is an actual runtime instance.

### 4. Instance Fields – Representing State

A date needs three pieces of information.

```csharp
public class Date
{
    private int day;
    private int month;
    private int year;
}
```

These are instance fields. Each object owns its own state.

```text
Date Class
    |
    +---- today
    |       +-- day = 12
    |       +-- month = 10
    |       +-- year = 2025
    |
    +---- birthday
            +-- day = 5
            +-- month = 7
            +-- year = 2003
```

**Mentor:** If both objects belong to the same class, must their values be identical?

**Student:** No, sir. Each object maintains its own state.

### 5. Constructor – Initializing an Object

How do we initialize the object?

We use a constructor.

```csharp
public class Date
{
    private int day;
    private int month;
    private int year;

    public Date(int d, int m, int y)
    {
        this.day = d;
        this.month = m;
        this.year = y;
    }
}
```

Object creation:

```csharp
Date today = new Date(12, 10, 2025);
Date birthday = new Date(5, 7, 2003);
```

Understand the execution sequence:

```text
new Date(12, 10, 2025)
          |
          v
   Object creation
          |
          v
    Constructor call
          |
          v
    Initialize fields
          |
          v
    Date object ready
```

The constructor establishes the initial state of an object.

### 6. Understanding `this`

Look at this statement:

```csharp
this.day = d;
```

There are two different things:

```text
this.day
    |
    +-- Field belonging to current object

d
    |
    +-- Constructor parameter
```

The keyword `this` refers to the current object.

**Mentor analogy:**

Imagine a classroom with 30 students. The teacher says, "Write your name." Every student understands: "I should write my own name." Similarly, `this.day` means the day belonging to this particular object.

### 7. Methods – Giving Behavior to Objects

Let's teach our Date object how to display itself.

```csharp
public void Show()
{
    Console.WriteLine($"{this.day}/{this.month}/{this.year}");
}
```

Calling the method:

```csharp
today.Show();
birthday.Show();
```

Output:

```text
12/10/2025
5/7/2003
```

Now our object has both state and behavior.

```text
            Date Object
                 |
          +------+------+
          |             |
          v             v
        STATE        BEHAVIOR
          |             |
    day/month/year    Show()
```

### 8. Encapsulation – Protecting Internal State

Consider this statement:

```csharp
date.day = 99;
```

Would this be a valid date?

No!

We don't want external code to manipulate internal state without control. This is called encapsulation. Encapsulation means protecting an object's internal state and exposing appropriate ways to interact with it. Think about a washing machine. You use its controls rather than directly manipulating its motor. Similarly, we should provide controlled access to our objects.

### 9. Properties – The C# Way

C# provides properties for controlled access.

```csharp
public class Date
{
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }
}
```

What does this mean?

```csharp
public int Day { get; private set; }
```

External code can read:

```csharp
Console.WriteLine(today.Day);
```

But external code cannot directly assign:

```csharp
today.Day = 99; // Not allowed
```

The class controls how its state changes.

### 10. Give the Object More Responsibilities

Let's add leap-year behavior.

```csharp
public bool IsLeapYear()
{
    return Year % 400 == 0 ||(Year % 4 == 0 && Year % 100 != 0);
}
```

Now the object can perform an operation related to its own state. We can also override `ToString()`.

```csharp
public override string ToString()
{
    return $"{Day}/{Month}/{Year}";
}
```

Usage:

```csharp
Console.WriteLine(today);
```

Output:

```text
12/10/2025
```

`ToString()` provides a textual representation of an object.

### 11. Complete C# Example

Create a Console Application:

```bash
dotnet new console -n DateDemo
cd DateDemo
dotnet run
```

Replace the program content with:

```csharp
using System;

public class Date
{
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    public void Show()
    {
        Console.WriteLine($"{Day}/{Month}/{Year}");
    }

    public bool IsLeapYear()
    {
        return Year % 400 == 0 ||
               (Year % 4 == 0 && Year % 100 != 0);
    }

    public override string ToString()
    {
        return $"{Day}/{Month}/{Year}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Date today = new Date(12, 10, 2025);
        Date birthday = new Date(5, 7, 2003);

        today.Show();
        birthday.Show();

        Console.WriteLine(today);
        Console.WriteLine(
            $"Is leap year: {today.IsLeapYear()}");
    }
}
```

**Important engineering observation:** This example demonstrates encapsulation, but it does not yet validate whether the supplied day, month and year form a real calendar date. We will treat validation as an improvement exercise.

### 12. From Date Class to Enterprise Domain Modeling

Today we created:

```text
Date
```

Tomorrow, we may create:

```text
Student
Customer
Employee
Product
Order
Policy
Claim
Payment
```

Consider an insurance application:

```text
             Insurance Application
                       |
          +------------+------------+
          |            |            |
          v            v            v
       Customer      Policy       Payment
          |            |            |
       BirthDate    StartDate    PaymentDate
                       |
                    EndDate
```

A domain model represents important concepts from the business domain.

For example:

```csharp
public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Display()
    {
        Console.WriteLine($"{Id} - {Name}");
    }
}
```

### 13. Connection with ASP.NET Core

The same object-oriented thinking becomes useful when building enterprise applications.

```text
             Browser / Client
                    |
                    v
               Controller
                    |
                    v
                 Service
                    |
                    v
               Domain Model
                    |
                    v
                Repository
                    |
                    v
                 Database
```

For example, a policy domain may contain:

```text
Policy
 |
 +-- PolicyNumber
 +-- Customer
 +-- StartDate
 +-- EndDate
 +-- Premium
 |
 +-- CalculatePremium()
 +-- IsActive()
 +-- Renew()
```

We are no longer merely writing C# syntax. We are identifying business concepts, their data and their responsibilities.

### 14. Hands-on Student Assignment

**Problem Statement: Design a Student Class**

Create a `Student` class with:

- StudentId
- Name
- DateOfBirth
- Marks

Implement:

- A constructor to initialize the student.
- A method to display student details.
- A method to calculate the result.
- Properties with controlled access.

Expected object creation:

```csharp
Student student =
    new Student(101, "Amit", 78);
```

Expected output:

```text
Student Id : 101
Name       : Amit
Marks      : 78
Result     : Pass
```

**Extension challenge:** Introduce a `Date` object as the student's date of birth instead of storing the date as a string.

### 15. Mentor Assessment

Answer these questions:

1. What is the difference between a class and an object?
2. Why does each object have its own state?
3. What is the responsibility of a constructor?
4. What does `this` represent?
5. Why do we declare fields as private?
6. What is the difference between a field and a property?
7. Why do we override `ToString()`?
8. What is domain modeling?
9. Why should business behavior belong to meaningful domain objects?
10. How can these concepts help us design an ASP.NET Core application?


### Ravi Sir's Closing Thought

Dear students,

Don't underestimate a small class like:

```csharp
public class Date
```

It contains the seeds of enterprise software design.

```text
Class
  |
  v
Object
  |
  v
State
  |
  v
Behavior
  |
  v
Encapsulation
  |
  v
Domain Modeling
  |
  v
Enterprise Application
```

A good developer doesn't merely know how to create objects.

A good developer understands:

- What objects should exist?
- What data should each object own?
- What behavior belongs to each object?
- What should be exposed?
- What should be protected?
- How should objects collaborate?

**Remember: C# programming starts with syntax, but software engineering starts with modeling the problem.**

— Ravi Sir, Transflower Mentor