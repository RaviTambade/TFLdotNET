# C# Interfaces, Abstract Classes, Polymorphism and Dependency Injection


**Design • Abstraction • Collaboration**

**Learning Journey**: From Object Collaboration → Abstraction → Polymorphism → Loose Coupling → Dependency Injection

## 1. Connect with our previous session

Dear students, In our previous sessions, we learned how to create classes and objects, protect object state using properties, control visibility using access modifiers, and make multiple objects collaborate. We built a Student Management System.

We had:

- Student
- Course
- EnrollmentService

Now imagine Transflower introduces new requirements. Our Student Management System should support multiple notification channels. Whenever a student enrolls in a course, the system should send a notification. Initially, we use email. Tomorrow, management asks for SMS. Next week, they ask for WhatsApp notifications. What should we do?

- Should we continuously modify our existing Student class?
- Should we write separate enrollment logic for every notification provider?

This is where software design becomes interesting. Today's challenge: How can we design software that accepts new behavior without unnecessarily changing existing code?

## 2. Start with a real-world analogy – Payment Systems

Imagine you are shopping online. You reach the payment page. You may choose:

- Credit Card
- UPI
- Net Banking
- Wallet

The shopping application doesn't need to understand the internal implementation of every payment provider. It simply needs a common contract: Every payment provider must support Pay().

```
                Shopping Application
                        |
                        |
                  MakePayment()
                        |
                        v
                Payment Interface
                   IPayment
                        |
          +-------------+-------------+
          |             |             |
          v             v             v
      CreditCard       UPI        NetBanking
       Payment       Payment       Payment
```

Notice something important. The application depends on a common contract, not on a specific payment implementation. This is the idea behind interfaces.

## 3. Understanding Interfaces in C\#

An interface defines a contract that implementing types must fulfill. It describes what an object can do without specifying all the implementation details.

### Step 1: Create an interface

```
public interface IPayment
{
    void Pay(decimal amount);
}
```

What does this mean? 
Any class implementing `IPayment` must provide the required `Pay()` implementation.

### Step 2: Implement the interface

```
public class UpiPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine(
            $"Paid {amount} using UPI"
        );
    }
}
```

Another implementation:

```
public class CardPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine(
            $"Paid {amount} using Credit Card"
        );
    }
}
```

### Step 3: Use the interface

```
IPayment payment = new UpiPayment();

payment.Pay(1500);
```

Output:

```
Paid 1500 using UPI
```

Now change only the object creation:

```
IPayment payment = new CardPayment();

payment.Pay(1500);
```

Output:

```
Paid 1500 using Credit Card
```

The calling code remains the same!

### Visualize the relationship

```
              IPayment
             <<interface>>
                  |
          +-------+-------+
          |               |
          v               v
      UpiPayment      CardPayment
          |               |
          v               v
        Pay()            Pay()
          |               |
          v               v
      UPI Logic       Card Logic
```

**Mentor asks**: What advantage did we get?

**Student**: Sir, we can introduce new payment implementations without rewriting the code that uses the payment contract.

**Mentor**: Exactly! This is one of the foundations of extensible software design.

## 4. Understanding Abstract Classes

Now let's consider another situation. Suppose every employee in an organization has:

- Employee ID
- Name
- Department

But different employees perform different types of work.

- Developer writes software.
- Tester tests software.
- Manager manages a team.

Can we create a common Employee abstraction?

Yes!

An abstract class provides a common base for related classes.

### Step 1: Create abstract Employee class

```
public abstract class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    protected Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Display()
    {
        Console.WriteLine($"{Id} - {Name}");
    }

    public abstract void Work();
}
```

Notice two types of methods:

```
public void Display()
```

This method has an implementation.

```
public abstract void Work();
```

This method declares a behavior that derived classes must implement.

### Step 2: Create derived classes

```
public class Developer : Employee
{
    public Developer(int id, string name)
        : base(id, name)
    {
    }

    public override void Work()
    {
        Console.WriteLine("Developing software");
    }
}
```

```
public class Tester : Employee
{
    public Tester(int id, string name)
        : base(id, name)
    {
    }

    public override void Work()
    {
        Console.WriteLine("Testing software");
    }
}
```

### Step 3: Execute

```
Employee emp1 = new Developer(101, "Amit");
Employee emp2 = new Tester(102, "Sneha");

emp1.Work();
emp2.Work();
```

Output:

```
Developing software
Testing software
```

### Architecture visualization

```
                Employee
             <<abstract>>
                   |
          +--------+--------+
          |                 |
          v                 v
      Developer           Tester
          |                 |
          v                 v
       Develop()          Test()
```

**Mentor explains**: An abstract class represents a common foundation for related types.

It can contain:

- Fields
- Properties
- Constructors
- Concrete methods
- Abstract methods

You cannot directly instantiate an abstract class.

```
// Employee emp = new Employee(1, "Amit");
// Compile-time error
```

## 5. Interface vs Abstract Class

Students often ask: "Sir, if both interfaces and abstract classes provide abstraction, why do we need both?"

Excellent question!

Let's compare.

| Interface                                 | Abstract Class                          |
| ----------------------------------------- | --------------------------------------- |
| Defines a contract                        | Provides a common base                  |
| A class can implement multiple interfaces | A class can inherit only one class      |
| Cannot have instance fields               | Can have instance fields                |
| Cannot have instance constructors         | Can have constructors                   |
| Can have default implementations          | Can have concrete implementations       |
| Useful for capabilities                   | Useful for shared identity and behavior |

### Visual understanding

```
       INTERFACE                    ABSTRACT CLASS
           |                              |
           v                              v
    Defines a contract              Defines a base
           |                              |
           v                              v
   What should you do?          What do related objects
                                have in common?
```

Example:

A Developer is an Employee. But a Developer can also be:

- Billable
- Trainable
- Reportable

These capabilities can be represented through interfaces.

```
public interface ITrainable
{
    void Train();
}

public interface IReportable
{
    void GenerateReport();
}
```

A Developer can implement both.

```
public class Developer : Employee, ITrainable, IReportable
{
    public Developer(int id, string name): base(id, name)
    {
    }

    public override void Work()
    {
        Console.WriteLine("Developing software");
    }

    public void Train()
    {
        Console.WriteLine("Training junior developers");
    }

    public void GenerateReport()
    {
        Console.WriteLine("Generating development report");
    }
}
```

This demonstrates how inheritance and interfaces complement one another.

## 6. Polymorphism – One Interface, Multiple Behaviors

Let's understand another important OOP concept. The word polymorphism means:

Many forms.

Imagine you have a common command:

```
animal.MakeSound();
```

Different animals respond differently.

```
                 Animal
                    |
               MakeSound()
                    |
          +---------+---------+
          |         |         |
          v         v         v
         Dog       Cat       Cow
          |         |         |
          v         v         v
         Bark      Meow      Moo
```

The same method call produces different behavior depending on the actual object.

### C# example

```
public abstract class Animal
{
    public abstract void MakeSound();
}
```

```
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Dog barks");
    }
}
```

```
public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Cat meows");
    }
}
```

Usage:

```
Animal animal1 = new Dog();
Animal animal2 = new Cat();

animal1.MakeSound();
animal2.MakeSound();
```

Output:

```
Dog barks
Cat meows
```

### What happened?

The reference type is `Animal`. But the actual objects are `Dog` and `Cat`. At runtime, the appropriate overridden method is executed. This is runtime polymorphism.

**Mentor asks**: Why is polymorphism useful?

**Student**: Sir, we can write common code that works with different implementations.

Exactly!

## 7. Object Collaboration Using Interfaces

Now let's return to our Transflower Student Management System. We want to send enrollment notifications through different channels. Let's design it.

### Step 1: Notification contract

```
public interface INotification
{
    void Send(string message);
}
```

### Step 2: Email implementation

```
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}
```

### Step 3: SMS implementation

```
public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
}
```

### Step 4: Enrollment service

```
public class EnrollmentService
{
    private readonly INotification _notification;

    public EnrollmentService(INotification notification)
    {
        _notification = notification;
    }

    public void Enroll(string studentName, string courseName)
    {
        Console.WriteLine($"{studentName} enrolled in {courseName}");
        _notification.Send($"Welcome {studentName} to {courseName}");
    }
}
```

Look carefully at this constructor:

```
public EnrollmentService(INotification notification)
```

We are not accepting `EmailNotification` or `SmsNotification`. We are accepting the interface.

Why?

Because the service should not be tightly coupled to a specific notification provider.

### Step 5: Program.cs

```
INotification notification = new EmailNotification();

EnrollmentService service = new EnrollmentService(notification);

service.Enroll("Amit","AI Enabled Fullstack Development");
```

Output:

```
Amit enrolled in AI Enabled Fullstack Development

Email sent: Welcome Amit to AI Enabled Fullstack Development
```

Now replace:

```
INotification notification = new SmsNotification();
```

The EnrollmentService implementation remains unchanged.

### Complete architecture

```
              Program.cs
                  |
                  v
           EnrollmentService
                  |
                  | Depends on
                  v
            INotification
             <<interface>>
                  |
          +-------+-------+
          |               |
          v               v
    EmailNotification  SmsNotification
          |               |
          v               v
       Email API        SMS API
```

This is an important design improvement.

## 8. Understanding Dependency Injection

Now comes the next engineering concept. In the previous example, we manually created an object:

```
INotification notification = new EmailNotification();
EnrollmentService service = new EnrollmentService(notification);
```

We created the dependency and passed it to the service. This is called Dependency Injection. Dependency Injection is a technique in which an object receives the dependencies it needs from an external source rather than creating those dependencies itself. Let's understand the difference.

### Without Dependency Injection

```
public class EnrollmentService
{
    private EmailNotification _notification =new EmailNotification();
}
```

Problem? EnrollmentService is tightly coupled to EmailNotification. Switching to SMS requires modifying the service.

### With Dependency Injection

```
public class EnrollmentService
{
    private readonly INotification _notification;

    public EnrollmentService(INotification notification)
    {
        _notification = notification;
    }
}
```

Now the service depends on an abstraction. Different implementations can be supplied from outside.

### Visual comparison


#### Withoug Dependency Injection

```
+-----------------------------+
|      EnrollmentService      |
|                             |
| Creates EmailNotification   |
|         internally          |
+--------------+--------------+
               |
               | creates
               v
+-----------------------------+
|     EmailNotification       |
+-----------------------------+

TIGHT COUPLING
The service controls its own dependency creation.
EnrollmentService is directly tied to EmailNotification.

```

#### With Dependency Injection

```
+--------------------------------------+
|      DI Container / Composition Root |
|                                      |
| Selects and creates implementation   |
+-------------------+------------------+
                    |
                    | injects through constructor
                    v
+--------------------------------------+
|          EnrollmentService           |
|                                      |
| Receives INotification               |
| through its constructor              |
+-------------------+------------------+
                    |
                    | depends on abstraction
                    v
+--------------------------------------+
|            INotification             |
+-------------------+------------------+
                    ^
                    |
          implements the interface
                    |
       +------------+------------+
       |            |            |
       v            v            v
+------------+ +------------+ +-------------+
|   Email    | |    SMS     | |  WhatsApp   |
|Notification| |Notification| |Notification |
+------------+ +------------+ +-------------+

LOOSE COUPLING
Dependency creation and implementation selection are handled externally.
EnrollmentService depends on INotification, not on a concrete class.
```


### Key Difference

| Without Dependency Injection                                         | With Dependency Injection                                                |
| -------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| `EnrollmentService` creates `EmailNotification` internally.          | The DI container creates and supplies an implementation.                 |
| Depends directly on a concrete class.                                | Depends on the `INotification` abstraction.                              |
| Changing the notification provider may require changing the service. | The implementation can be changed through configuration or registration. |
| Tightly coupled design.                                              | Loosely coupled design.                                                  |


**Mentor's Key Point** :Dependency Injection does not mean that an object has no dependencies. It means that the object does not take responsibility for creating those dependencies.

EnrollmentService still needs a notification service. It simply receives that dependency from outside, usually through constructor
injection.


## 9. ASP.NET Core – Built-in Dependency Injection

One of the powerful features of ASP.NET Core is its built-in dependency injection container. Let's see how we can register our notification service.

Inside `Program.cs`:

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<INotification, EmailNotification>();
builder.Services.AddScoped<EnrollmentService>();

var app = builder.Build();

app.MapGet("/", () => "Transflower Enrollment API");

app.Run();
```

What does this line mean?

```
builder.Services.AddScoped<INotification, EmailNotification>();
```

Whenever the DI container needs an `INotification` dependency, it can provide an `EmailNotification` instance according to the scoped lifetime. The container also resolves constructor dependencies when creating `EnrollmentService`.

### Dependency Injection execution flow

```
             ASP.NET Core
                  |
                  v
             Program.cs
                  |
                  v
            Register Services
                  |
                  v
          builder.Services
                  |
                  v
           DI Container
                  |
                  v
          Create Controller
                  |
                  v
       Resolve Constructor Dependencies
                  |
                  v
          EnrollmentService
                  |
                  v
          INotification
                  |
                  v
          EmailNotification
```

### Understand service lifetimes

| Lifetime  | Meaning                                 | Example                       |
| --------- | --------------------------------------- | ----------------------------- |
| Transient | New instance each time resolved         | Lightweight stateless service |
| Scoped    | One instance per DI scope               | Typical request-level service |
| Singleton | One instance for the container lifetime | Shared thread-safe service    |

Remember:

A singleton service should not directly capture a scoped dependency. Also, singleton services must be designed for concurrent access.

## 10. Complete Practical Lab – Notification Management System

Hands-on Assignment

Let's build a practical console application using all today's concepts.

### Project structure

```
NotificationApp/
    |
    |-- Program.cs
    |
    |-- Interfaces/
    |      |
    |      |-- INotification.cs
    |
    |-- Services/
           |
           |-- EmailNotification.cs
           |-- SmsNotification.cs
           |-- WhatsAppNotification.cs
           |-- EnrollmentService.cs
```

### Student requirements

1. Create `INotification`.
2. Implement EmailNotification.
3. Implement SmsNotification.
4. Implement WhatsAppNotification.
5. Create EnrollmentService.
6. Inject INotification through the constructor.
7. Execute enrollment using each notification provider.
8. Compare the output.

### Expected output

```
====================================
 TRANSFLOWER ENROLLMENT MANAGEMENT
====================================

Student: Amit
Course : C# Programming

Email Notification Sent!

Student: Sneha
Course : ASP.NET Core

SMS Notification Sent!

Student: Rahul
Course : AI Enabled Fullstack Development

WhatsApp Notification Sent!
```

### Additional challenge

Implement another interface:

```
public interface ILogger
{
    void Log(string message);
}
```

Implement:

- ConsoleLogger
- FileLogger

Now modify EnrollmentService to use both `INotification` and `ILogger`.

Think about constructor injection with multiple dependencies.

## 11. Engineering Thinking – Connect the Concepts

| Concept               | Engineering purpose                       |
| --------------------- | ----------------------------------------- |
| Interface             | Define contracts                          |
| Abstract class        | Share common structure and behavior       |
| Inheritance           | Reuse and specialize behavior             |
| Polymorphism          | Support multiple implementations          |
| Dependency Injection  | Supply dependencies externally            |
| Loose coupling        | Reduce direct implementation dependencies |
| Constructor Injection | Make dependencies explicit                |

### The evolution of our design

```
       Classes and Objects
                |
                v
        Object Collaboration
                |
                v
             Interfaces
                |
                v
          Polymorphism
                |
                v
        Dependency Injection
                |
                v
         Loose Coupling
                |
                v
       Maintainable Software
` 

## 13. Ravi Sir's Closing Thought

Dear students,

Let's revisit our journey. In the beginning, we learned how to create objects. Then we learned how to protect their state. Today, we learned how to make objects communicate through contracts and how to introduce new implementations without tightly coupling our code. 

This is the evolution of an engineer's thinking.

A beginner asks: How do I write this class?

A developer asks: How do these classes communicate?

An experienced engineer asks: How can I design this system so that tomorrow's changes do not break today's functionality?

# Remember this principle

Depend on abstractions, not concrete implementations.

Fundamentals build confidence. Design builds engineering capability.

— Ravi Sir, Chief Mentor, Transflower Learning