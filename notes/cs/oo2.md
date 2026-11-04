# SOLID Principles in C# – Writing Maintainable and Extensible Software


### Software Design Principles


**Learning Journey**: From Object-Oriented Programming → Good Design → SOLID → Enterprise Architecture

## 1. Connect with the previous session

Dear students,

In our previous sessions, we learned:

1. Classes and Objects
2. Encapsulation and Properties
3. Access Modifiers
4. Static and Instance Members
5. Object Collaboration
6. Interfaces and Abstract Classes
7. Polymorphism
8. Dependency Injection

We learned how multiple objects collaborate to solve business problems. Now I want to ask you an important question. Does using classes, interfaces and Dependency Injection automatically mean that we have designed good software? Not necessarily! You can write an application using 100 classes, 20 interfaces and Dependency Injection and still create a poorly designed system. Why? Because good software design is not about the number of classes. It is about assigning responsibilities correctly, managing dependencies and making the application easier to change. Today, we will explore five important software design principles known as SOLID.

# 2. What is SOLID?

SOLID is an acronym representing five object-oriented software design principles.

| Letter | Principle                       | Meaning                                                    |
| ------ | ------------------------------- | ---------------------------------------------------------- |
| S      | Single Responsibility Principle | One class should have one responsibility                   |
| O      | Open/Closed Principle           | Open for extension, closed for modification                |
| L      | Liskov Substitution Principle   | Derived types should be substitutable for their base types |
| I      | Interface Segregation Principle | Prefer smaller, focused interfaces                         |
| D      | Dependency Inversion Principle  | Depend on abstractions, not concrete implementations       |

### Visualize SOLID

```
                 SOLID PRINCIPLES
                        |
       +----------------+----------------+
       |                |                |
       v                v                v
      SRP              OCP              LSP
   One clear       Extend without     Substitution
 Responsibility   unnecessary change   correctness
       |                |                |
       +----------------+----------------+
                        |
                  +-----+-----+
                  |           |
                  v           v
                 ISP         DIP
              Focused      Depend on
              Interfaces  Abstractions
```

Let's understand each principle through a real-world Transflower Student Management System.

# 3. S – Single Responsibility Principle (SRP)

One Class – One Responsibility

### Start with a problem

Imagine we are developing a Student Management System.

Initially, we create one class:

```
public class Student
{
    public int Id { get; set; }

    public string Name { get; set; }

    public void CalculateResult()
    {
        Console.WriteLine("Calculating result...");
    }

    public void SaveToDatabase()
    {
        Console.WriteLine("Saving student...");
    }

    public void SendEmail()
    {
        Console.WriteLine("Sending email...");
    }

    public void GenerateReport()
    {
        Console.WriteLine("Generating report...");
    }
}
```

**Mentor asks:** What responsibilities does this class have?

**Student:**

- Student data management
- Result calculation
- Database operations
- Email communication
- Report generation

**Mentor:**

Exactly! We have created a class that knows too many things. Imagine the database technology changes from MySQL to SQL Server. Do we need to modify the Student class? Yes. Imagine the email provider changes. Do we need to modify the Student class again? Yes. This creates unnecessary coupling.

### What does SRP suggest?

A class should have one reason to change. This does not necessarily mean one method per class. It means one cohesive responsibility.

### Improved design

```
          Student Management System
                     |
        +------------+------------+
        |            |            |
        v            v            v
     Student    ResultService  StudentRepository
        |            |            |
        v            v            v
    Student Data  Calculation   Database Access
                     |
                     v
              EmailService
                     |
                     v
               Email Delivery
```

Let's implement it.

Student.cs

```
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }
}
```

ResultService.cs

```
public class ResultService
{
    public string CalculateGrade(Student student)
    {
        if (student.Marks >= 75)
            return "Distinction";

        if (student.Marks >= 60)
            return "First Class";

        if (student.Marks >= 40)
            return "Pass";

        return "Fail";
    }
}
```

StudentRepository.cs

```
public class StudentRepository
{
    public void Save(Student student)
    {
        Console.WriteLine($"Saving student: {student.Name}");
    }
}
```

EmailService.cs

```
public class EmailService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}
```

Now each class has a cohesive responsibility.

### Mentor wisdom

> A class should have one clear reason to change.

# 4. O – Open/Closed Principle (OCP)

Extend Behavior Without Rewriting Existing Logic

### Start with a business scenario

Transflower currently supports Email notifications. Tomorrow management asks for SMS notifications. Next month they request WhatsApp notifications. Consider this implementation:

```
public class NotificationService
{
    public void Send(string type, string message)
    {
        if (type == "Email")
        {
            Console.WriteLine("Sending Email");
        }
        else if (type == "SMS")
        {
            Console.WriteLine("Sending SMS");
        }
        else if (type == "WhatsApp")
        {
            Console.WriteLine("Sending WhatsApp");
        }
    }
}
```

What happens when a new notification provider is introduced? We must modify this class every time. As the number of providers increases, the conditional logic becomes difficult to maintain.

### What does OCP suggest?

Software entities should be:

- Open for extension.
- Closed for modification.

We should be able to introduce new implementations without repeatedly changing stable business logic.

### Improved design using interfaces

INotification.cs

```
public interface INotification
{
    void Send(string message);
}
```

EmailNotification.cs

```
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}
```

SmsNotification.cs

```
public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("SMS: " + message);
    }
}
```

WhatsAppNotification.cs

```
public class WhatsAppNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("WhatsApp: " + message);
    }
}
```

NotificationService.cs

```
public class NotificationService
{
    private readonly INotification _notification;

    public NotificationService(INotification notification)
    {
        _notification = notification;
    }

    public void Notify(string message)
    {
        _notification.Send(message);
    }
}
```

Now we can add a new notification provider without changing NotificationService.

### Architecture

```
                NotificationService
                        |
                        v
                  INotification
                  <<interface>>
                        |
          +-------------+-------------+
          |             |             |
          v             v             v
        Email          SMS         WhatsApp
     Notification  Notification  Notification
          |             |             |
          v             v             v
       Email API     SMS API      WhatsApp API
```

**Mentor asks**: If management asks for Telegram notifications, what should we do?

**Student**: Create a TelegramNotification class implementing INotification.

**Mentor**: Excellent! We extend behavior without modifying the existing service.

# 5. L – Liskov Substitution Principle (LSP)

Inheritance Must Preserve Expected Behavior. This principle is slightly more challenging. Let's understand it through a real-world example. Imagine we have a Bird class.

```
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Bird is flying");
    }
}
```

Now create two derived classes.

```
public class Sparrow : Bird
{
    public override void Fly()
    {
        Console.WriteLine("Sparrow is flying");
    }
}
```

```
public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotSupportedException("Penguins cannot fly");
    }
}
```

Now consider this code:

```
public void MakeBirdFly(Bird bird)
{
    bird.Fly();
}
```

Usage:

```
MakeBirdFly(new Sparrow());
MakeBirdFly(new Penguin());
```

The first works. The second throws an exception. What is wrong with our design? We assumed that every Bird can fly. But that assumption is not valid for all birds.

**Mentor asks**: Is inheritance always appropriate simply because two objects belong to the same category?

**Student**: No, sir. The derived class must preserve the expected behavior of the base class.

Exactly!

### Improved design

Separate the flying capability from the general Bird concept.

```
public abstract class Bird
{
    public abstract void Eat();
}
```

```
public interface IFlyable
{
    void Fly();
}
```

Sparrow:

```
public class Sparrow : Bird, IFlyable
{
    public override void Eat()
    {
        Console.WriteLine("Sparrow eats seeds");
    }

    public void Fly()
    {
        Console.WriteLine("Sparrow is flying");
    }
}
```

Penguin:

```
public class Penguin : Bird
{
    public override void Eat()
    {
        Console.WriteLine("Penguin eats fish");
    }
}
```

### Architecture

```
                  Bird
              <<abstract>>
                    |
             +------+------+
             |             |
             v             v
          Sparrow        Penguin
             |
             |
          IFlyable
             |
             v
            Fly()
```

Now we don't force Penguin to implement an operation it cannot support.

### Understand LSP

If a method accepts a base type, an appropriate derived type should be usable without breaking the method's expected behavior. LSP is about preserving behavioral contracts, not merely satisfying the compiler.

# 6. I – Interface Segregation Principle (ISP)

Small, Focused Interfaces . Let's imagine a company has different employee roles.

- Developer writes code.
- Tester tests software.
- Manager manages a team.

Now someone creates this interface:

```
public interface IEmployee
{
    void Develop();
    void Test();
    void Manage();
    void ConductInterview();
}
```

Every employee must implement every method. But should a Tester be forced to implement Manage()? Should a Manager be forced to implement Develop()?  No!  This is an example of a poorly designed interface.

### What does ISP suggest?

Clients should not be forced to depend on methods they do not use. Let's split the large interface into smaller contracts.

```
public interface IDeveloper
{
    void Develop();
}
```

```
public interface ITester
{
    void Test();
}
```

```
public interface IManager
{
    void Manage();
}
```

Now implement only the capabilities required.

```
public class SoftwareDeveloper : IDeveloper
{
    public void Develop()
    {
        Console.WriteLine("Writing C# code");
    }
}
```

```
public class QAEngineer : ITester
{
    public void Test()
    {
        Console.WriteLine("Testing application");
    }
}
```

```
public class ProjectManager : IManager
{
    public void Manage()
    {
        Console.WriteLine("Managing project");
    }
}
```

### Visualize

Before ISP – Fat Interface

```
     IEmployee
         |
   +-----+------+
   |     |      |
 Develop Test  Manage
```

\<text color="secondary" size="xs">Every implementing class is forced to support unrelated operations.\</text>\
\<divider color="subtle"/>\
After ISP – Segregated Interfaces

```
   IDeveloper    ITester    IManager
       |            |          |
       v            v          v
    Developer    Tester     Manager
```

Each class implements only the capabilities it needs.

**Mentor wisdom**: A good interface should represent a meaningful capability, not a collection of unrelated operations.

# 7. D – Dependency Inversion Principle (DIP)

Depend on Abstractions, Not Concrete Implementations. This principle connects directly to our previous session on Dependency Injection. Let's revisit the notification example.

### Without DIP

```
public class EnrollmentService
{
    private EmailNotification _email = new EmailNotification();

    public void Enroll(string studentName)
    {
        Console.WriteLine($"{studentName} enrolled" );
        _email.Send("Enrollment successful");
    }
}
```

What is the problem? EnrollmentService directly depends on EmailNotification. If we change the notification mechanism, we need to modify EnrollmentService.

### With DIP

Create an abstraction.

```
public interface INotification
{
    void Send(string message);
}
```

Implement it.

```
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine(
            "Email: " + message
        );
    }
}
```

Modify the service.

```
public class EnrollmentService
{
    private readonly INotification _notification;

    public EnrollmentService(INotification notification)
    {
        _notification = notification;
    }

    public void Enroll(string studentName)
    {
        Console.WriteLine($"{studentName} enrolled");

        _notification.Send("Enrollment successful");
    }
}
```

Now the high-level business service depends on an abstraction.

### Visualize DIP

```
             WITHOUT DIP

        EnrollmentService
                |
                v
        EmailNotification
                |
                v
             Email API


              WITH DIP

        EnrollmentService
                |
                v
          INotification
          <<interface>>
                ^
                |
        EmailNotification
                |
                v
             Email API
```

Notice the difference. The business logic no longer directly depends on a concrete notification provider.

### Dependency Injection in .NET 10

Inside a console application, we can use the .NET dependency injection abstractions and hosting packages. For ASP.NET Core, the built-in container is already available.

Example:

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<INotification,EmailNotification>();
builder.Services.AddScoped<EnrollmentService>();

var app = builder.Build();

app.MapGet("/enroll", (EnrollmentService service) =>
{
    service.Enroll("Amit");
    return Results.Ok("Enrollment completed");
});

app.Run();
```

ASP.NET Core resolves the dependencies when it creates the service needed by the endpoint.

**Mentor explains:**

Dependency Inversion is a design principle.

Dependency Injection is one technique used to implement that principle.

They are related, but they are not the same thing.

# 8. Understand All Five SOLID Principles Together

Let's connect everything.

```
                SOLID DESIGN
                     |
       +-------------+-------------+
       |             |             |
       v             v             v
      SRP           OCP           LSP
       |             |             |
   Clear          Extension      Safe
Responsibility    without       Substitution
                  unnecessary
                  modification
       |             |             |
       +-------------+-------------+
                     |
                +----+----+
                |         |
                v         v
               ISP       DIP
                |         |
             Focused   Abstract
             Contracts Dependencies
                |         |
                +----+----+
                     |
                     v
           Maintainable Software
                     |
                     v
            Extensible Architecture
```

| Principle | Question to ask yourself                                   |
| --------- | ---------------------------------------------------------- |
| SRP       | Does this class have too many reasons to change?           |
| OCP       | Can I add a new behavior without modifying stable code?    |
| LSP       | Can I replace a base type with its derived type safely?    |
| ISP       | Am I forcing classes to implement methods they don't need? |
| DIP       | Am I depending directly on concrete implementations?       |

# 9. Complete Practical Lab – Transflower Enrollment System

Enterprise Design Challenge

Now let's apply all five principles together.

### Business requirement

Build an Enrollment Management System.

When a student enrolls:

1. Student information must be validated.
2. Enrollment details must be saved.
3. A confirmation notification must be sent.
4. A result report must be generated when required.

### Suggested project structure

```
TransflowerEnrollment/
    |
    |-- Models/
    |     |
    |     |-- Student.cs
    |     |-- Course.cs
    |
    |-- Interfaces/
    |     |
    |     |-- IStudentRepository.cs
    |     |-- INotification.cs
    |     |-- IReportGenerator.cs
    |
    |-- Services/
    |     |
    |     |-- EnrollmentService.cs
    |     |-- ResultService.cs
    |
    |-- Infrastructure/
    |     |
    |     |-- MySqlStudentRepository.cs
    |     |-- EmailNotification.cs
    |     |-- PdfReportGenerator.cs
    |
    |-- Program.cs
```

### Suggested contracts

```
public interface IStudentRepository
{
    void Save(Student student);
}
```

```
public interface INotification
{
    void Send(string message);
}
```

```
public interface IReportGenerator
{
    void Generate(Student student);
}
```

### EnrollmentService

```
public class EnrollmentService
{
    private readonly IStudentRepository _repository;
    private readonly INotification _notification;

    public EnrollmentService(IStudentRepository repository,INotification notification)
    {
        _repository = repository;
        _notification = notification;
    }

    public void Enroll(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        _repository.Save(student);

        _notification.Send($"Welcome {student.Name}!"
        );
    }
}
```

### Student assessment

Ask students to identify where each principle is applied.

| Design decision                                              | SOLID principle |
| ------------------------------------------------------------ | --------------- |
| Student and EnrollmentService are separate                   | SRP             |
| New notification providers implement INotification           | OCP             |
| Implementations preserve the expected contract               | LSP             |
| Separate interfaces for repository, notification and reports | ISP             |
| EnrollmentService receives abstractions through constructor  | DIP             |

Note that these principles are design guidelines, not automatic guarantees. We must still evaluate actual responsibilities, behavioral contracts and dependency boundaries.

# 10. Practical Engineering Exercises

Ask students to solve these problems independently.

**Exercise 1 – SRP**

Create a `Student` class that currently:

- Stores student information.
- Calculates grades.
- Saves records.
- Sends notifications.

Refactor it into cohesive classes.

**Exercise 2 – OCP**

Create a discount calculation system supporting:

- Regular Customer
- Premium Customer
- Student Customer

Introduce a new discount type without changing the existing calculator.

**Exercise 3 – LSP**

Design a Shape hierarchy with:

- Rectangle
- Square
- Circle

Ensure derived types preserve the expected behavior of the abstraction. Discuss why independently setting rectangle width and height can violate expected behavior if Square inherits Rectangle.

**Exercise 4 – ISP*

Create a Machine interface containing:

- Print()
- Scan()
- Fax()

Now design separate interfaces so that a simple printer is not forced to implement scanning and faxing.

**Exercise 5 – DIP**

Create a payment processing service supporting:

- UPI
- Credit Card
- Net Banking

Use interfaces and constructor injection.

 

# 11. Connect SOLID with Real ASP.NET Core Architecture

Dear students, now think about the applications we will build in the industry. Consider an ASP.NET Core application.

```
                React Frontend
                      |
                      v
              ASP.NET Core API
                      |
                      v
                  Controller
                      |
                      v
                Application
                   Service
                      |
             +--------+--------+
             |                 |
             v                 v
       Repository         Notification
             |                 |
             v                 v
          MySQL             Email API
```

SOLID principles influence how we design these components.

- Controllers handle HTTP concerns.
- Services coordinate business operations.
- Repositories handle persistence.
- Interfaces define dependency contracts.
- Dependency Injection connects implementations.

This is the foundation for maintainable enterprise applications.

It is also an important starting point for understanding Clean Architecture, Hexagonal Architecture and modular monoliths.

# 13. Ravi Sir's Engineering Challenge

Before our next session, I want every student to perform one important activity. Take an existing project that you have developed.

It could be:

- Student Management System
- Library Management System
- Banking Application
- Insurance Application
- E-commerce Application

Inspect your classes.

Ask yourself:

1. Does one class perform too many responsibilities?
2. Are there large conditional statements selecting concrete implementations?
3. Are derived classes breaking the expectations of their base classes?
4. Are interfaces unnecessarily large?
5. Are high-level services creating their own dependencies?

Identify at least five design improvements.

Don't simply memorize SOLID definitions. Learn to recognize design problems in actual code.

# Ravi Sir's Closing Thought

- A good programmer can write working code.
- A good developer can organize classes and objects.
- A good software engineer can design a system that remains understandable and maintainable as requirements change.

SOLID is not about writing more interfaces or creating more classes. It is about making the right design decisions.

## Understand the problem. Design the responsibility. Then write the code.

— Ravi Tambade, Chief Mentor, Transflower Learning

  