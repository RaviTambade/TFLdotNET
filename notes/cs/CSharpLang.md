## From C# Syntax to Object-Oriented Programming (OOP)


Today's objective: Understand how C# transforms programming logic into reusable software components.

## 1. Let's begin with a real-world question

**Mentor**: Dear students, suppose Transflower wants to develop a Student Management System.

What information do we need to maintain about a student?

**Student 1**: Student name, age and email.

**Student 2**: Course name, fees and enrollment date.

**Mentor**: Excellent! Now, if we have 100 students, should we create 100 separate sets of variables?

Imagine writing:

```
string student1Name = "Rahul";
int student1Age = 21;

string student2Name = "Pragati";
int student2Age = 22;

string student3Name = "Sahil";
int student3Age = 20;
```

What happens when we have 10,000 students? We need a better approach. That approach is called Object-Oriented Programming.

## 2. Understand the concept of Class and Object

Think about a building architect. An architect prepares a blueprint. Using that blueprint, construction workers create multiple buildings.

Similarly:

- Class = Blueprint
- Object = Actual building
- Properties = Characteristics
- Methods = Behaviors


Let's translate this into C#.

```
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Study()
    {
        Console.WriteLine($"{Name} is studying.");
    }
}
```

Creating objects:

```
Student s1 = new Student();

s1.Name = "Rahul";
s1.Age = 21;

Student s2 = new Student();

s2.Name = "Pragati";
s2.Age = 22;

s1.Study();
s2.Study();
```

Understand the relationship:

```
             Student Class
                  |
          Blueprint / Template
                  |
          +-------+-------+
          |               |
          ▼               ▼
      Student s1      Student s2
          |               |
       Rahul           Pragati
       Age 21          Age 22
          |               |
        Study()         Study()
```

## 3. Four Pillars of OOP

Now let's understand the four fundamental principles.


1. **Encapsulation** : Protect data and control how it is accessed.

Example: BankAccount balance should not be modified directly.


2. **Inheritance** : Reuse and extend the functionality of an existing class.

Example: SavingsAccount inherits from BankAccount.


3. **Polymorphism** : One common interface, multiple implementations.

Example: Different payment methods implement Pay().

4. **Abstraction** :Expose essential functionality while hiding implementation details.

Example: A customer clicks Pay without knowing the payment gateway's internal processing.

## 4. Practical Lab 1 – Encapsulation

Problem statement: Develop a simple bank account that protects its balance.

```
public class BankAccount
{
    private decimal balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
        }
    }

    public decimal GetBalance()
    {
        return balance;
    }
}
```

Usage:

```
BankAccount account = new BankAccount();

account.Deposit(5000);
account.Deposit(2000);

Console.WriteLine(account.GetBalance());
```

Output:

```
7000
```

**Mentor asks**: Why did we make balance private?

Because we don't want external code to arbitrarily change the account balance. This is encapsulation.

## 5. Practical Lab 2 – Inheritance

Suppose we have different types of employees.

```
             Employee
                |
       +--------+--------+
       |                 |
    Developer         Tester
       |                 |
    WriteCode()      TestSoftware()
```

C# implementation:

```
public class Employee
{
    public string Name { get; set; }

    public void Login()
    {
        Console.WriteLine($"{Name} logged in.");
    }
}

public class Developer : Employee
{
    public void WriteCode()
    {
        Console.WriteLine("Developer is writing code.");
    }
}

public class Tester : Employee
{
    public void TestSoftware()
    {
        Console.WriteLine("Tester is testing software.");
    }
}
```

Usage:

```
Developer dev = new Developer();

dev.Name = "Rahul";

dev.Login();
dev.WriteCode();
```

The Developer inherits the common functionality of Employee.

## 6. Practical Lab 3 – Polymorphism

Suppose an e-commerce application supports multiple payment methods.

```
public class Payment
{
    public virtual void Pay(decimal amount)
    {
        Console.WriteLine("Processing payment");
    }
}

public class UpiPayment : Payment
{
    public override void Pay(decimal amount)
    {
        Console.WriteLine($"UPI payment: {amount}");
    }
}

public class CardPayment : Payment
{
    public override void Pay(decimal amount)
    {
        Console.WriteLine($"Card payment: {amount}");
    }
}
```

Usage:

```
Payment payment;

payment = new UpiPayment();
payment.Pay(1500);

payment = new CardPayment();
payment.Pay(2500);
```

Output:

```
UPI payment: 1500
Card payment: 2500
```

Same method call, different behavior. That is runtime polymorphism.

## 7. Practical Lab 4 – Abstraction

Let's introduce an interface.

```
public interface IPayment
{
    void Pay(decimal amount);
}
```

Implementations:

```
public class UpiPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("UPI payment completed.");
    }
}

public class CardPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("Card payment completed.");
    }
}
```

Usage:

```
IPayment payment = new UpiPayment();

payment.Pay(500);
```

The calling code depends on the payment contract rather than a specific implementation.

## 8. Connect OOP with ASP.NET Core

Now comes the important question.

**Mentor**: Why are we learning OOP?

Because ASP.NET Core applications are built using classes, objects, interfaces and dependency relationships.

Consider an insurance application.

```
             ASP.NET Core Application
                       |
              +--------+--------+
              |                 |
          Controller         Services
              |                 |
         PolicyController  PolicyService
                                |
                         IPolicyRepository
                                |
                         PolicyRepository
                                |
                             MySQL
```

Example:

```
public interface IPolicyRepository
{
    void SavePolicy();
}

public class PolicyRepository : IPolicyRepository
{
    public void SavePolicy()
    {
        Console.WriteLine("Policy saved in database.");
    }
}
```

A service can consume the interface through constructor injection:

```
public class PolicyService
{
    private readonly IPolicyRepository repository;

    public PolicyService(IPolicyRepository repository)
    {
        this.repository = repository;
    }

    public void CreatePolicy()
    {
        repository.SavePolicy();
    }
}
```

This is where OOP connects with Dependency Injection and application architecture.

## 9. Today's Student Assignment

Hands-on lab

Build a small Student Management application using C#.

Requirements:

1. Create a `Student` class.
2. Add Name, Age and Email properties.
3. Add a constructor.
4. Create a `DisplayDetails()` method.
5. Create at least three Student objects.
6. Create a `Course` class.
7. Establish a relationship between Student and Course.
8. Use a List to maintain multiple students.

Expected conceptual model:

```
           Student Management
                   |
          +--------+--------+
          |                 |
        Student            Course
          |                 |
       Name               Title
       Age                Fees
       Email              Duration
          |
     DisplayDetails()
```

### 🌱 Ravi Sir's closing message

Dear students, 

- Learning syntax makes you a programmer. 
- Understanding objects makes you an application developer. 
- Understanding relationships between objects makes you a software designer.
- And understanding why those relationships exist makes you an architect.

**Remember**: First understand the problem. Then identify the objects. Define their responsibilities. Finally, write the code.

That is the Transflower way of learning software engineering.