# Object-Oriented Programming (OOPs)

*"Alright everyone, settle in… Today I’m not just teaching you syntax or code — I want to take you on a journey… into the world of Object-Oriented Programming. Or as we lovingly call it, OOP. Now close your eyes for a moment and imagine you’re not just writing code, but building a little digital universe…"* 🌍

### 🌟 The Magic of Objects

"You know, programming used to be about giving a computer a series of steps — like giving instructions to a robot. But that was tiring, repetitive, and hard to scale.

Then came a smarter way — **Object-Oriented Programming**.

OOP asks you to think like a designer or architect. Instead of just steps, you now build **things** — called *objects* — that live, breathe, and interact with each other in your digital world.

Imagine your phone. It has a **state** (brand, battery level), it has **behavior** (make a call, take a picture), and it has an **identity** (your phone, not your friend's). That’s exactly what we create in code: real-world-like objects!"

### 💡 Why Use Object-Oriented Programming?

"Because that’s how **we naturally think**.

Let’s say we’re building an app for a bookstore. Wouldn’t it be nice to have a `Book` object with a `Title`, `Author`, and a `Read()` method? Maybe an `Author` object who can `WriteBook()`. It feels real. It feels organized.

And once you enter this world, you’ll never want to go back."

### 🏛️ The Four Pillars of OOP – Your Foundation

"Like any strong building, OOP stands on **four powerful pillars**. These are your tools, your rules, and your compass."


#### 🧊 1. Abstraction – Seeing Only What Matters

"I’ll tell you a secret. In life — and in code — not everything matters all the time.

Let’s say you're designing a `Person` object. For a voting system, only age and citizenship matter. For a fitness app, weight and height matter.

This is **abstraction**. You zoom in only on what's important. You hide the rest. Clean, focused, elegant."

#### 🛡️ 2. Encapsulation – Hide the Wires, Use the Buttons

"Have you ever tried fixing your phone by opening it up? No, right?

You trust the **interface** — the buttons, the screen. You don't mess with the circuits inside.

In code, we do the same. We wrap the internals in a class and expose only what’s needed. That’s **encapsulation**.

Using `private`, `public`, and `protected`, we decide what the world can touch — and what it can’t."




### 👪 3. Inheritance – Like Parents, Like Children

"Think about a bank account. Every account has a `Balance`, and you can `Deposit()` or `Withdraw()` money, right?

Now imagine you want a **special account** called `SavingAccount` that gives you interest every month. You don’t want to rewrite everything from `Account` — you just want to **reuse** its features and add the interest behavior.

That’s **inheritance** in programming — children (derived classes) get traits from their parents (base classes).

```csharp
// Parent class
class Account
{
    public decimal Balance { get; set; }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount}. New Balance: {Balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount}. New Balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Insufficient funds!");
        }
    }
}

// Child class
class SavingAccount : Account
{
    public decimal InterestRate { get; set; } = 0.05m;

    public void ApplyInterest()
    {
        Balance += Balance * InterestRate;
        Console.WriteLine($"Interest applied. New Balance: {Balance}");
    }
}
```

Here’s the magic:

* `SavingAccount` **inherits** `Balance`, `Deposit()`, and `Withdraw()` from `Account`.
* We **don’t rewrite** the deposit or withdraw logic — we simply add `ApplyInterest()`.
* Just like kids inherit traits from parents, `SavingAccount` inherits account behaviors and then extends them with extra features.

So, whenever you create a `SavingAccount` object, it **already knows how to deposit and withdraw**, but now it can also **earn interest**. 🎉"


#### 🎭 4. Polymorphism – One Action, Different Results

"Imagine you walk into a bank and press the **‘ProcessTransaction’ button**.

* For a **Savings Account**, the bank calculates interest before completing the transaction.
* For a **Current Account**, it just updates the balance without interest.

Same action — **different behavior**. That’s **polymorphism** in programming.

In C#, polymorphism allows us to call the **same method** on different types of accounts, and each behaves in its own way.

```csharp
// Base class
class Account
{
    public decimal Balance { get; set; }

    // Virtual method can be overridden
    public virtual void ProcessTransaction(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Generic account: Deposited {amount}. Balance: {Balance}");
    }
}

// Derived class 1
class SavingAccount : Account
{
    public decimal InterestRate { get; set; } = 0.05m;

    public override void ProcessTransaction(decimal amount)
    {
        Balance += amount;
        Balance += Balance * InterestRate; // Apply interest
        Console.WriteLine($"Saving Account: Deposited {amount} + interest. Balance: {Balance}");
    }
}

// Derived class 2
class CurrentAccount : Account
{
    public override void ProcessTransaction(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Current Account: Deposited {amount}. Balance: {Balance}");
    }
}
```

"Now let’s see polymorphism in action:"

```csharp
Account acc1 = new SavingAccount();
Account acc2 = new CurrentAccount();

acc1.ProcessTransaction(1000); // Applies interest
acc2.ProcessTransaction(1000); // Just adds amount
```

Even though we called **the same method** `ProcessTransaction()`, the **output is different** depending on the account type.

* Savings account adds **interest** automatically.
* Current account just updates the balance.

🎉 That’s **polymorphism** — one interface, many behaviors. Just like a single bank button serving multiple account types differently!"


##  Why Inheritance Matters ?


* **Code reusability:** Write once, use many times.
* **Extensibility:** Easily add new types of Employees without breaking old code.
* **Flexibility:** Let objects behave in ways appropriate to their type, even when accessed through a general reference.

 
### Final Thoughts

Inheritance is the family inheritance of code traits, while polymorphism is the shape-shifting magic that lets objects act differently when asked to perform the same task.

Together, they form the backbone of Object-Oriented Design in C# — helping you write clean, efficient, and powerful programs.
 


```
  //Base Class
  public class Employee
  { 
    //Data Members  
    private double basic_sal;
    private double hra;
    private double da;

    //Member functions

    //Constructor overloading
    public Employee(){
          this.basic_sal=5000;
          this.hra=1200;
          this.da=700;
    }

    public Employee(double bsal, double hra, double da){
        this.basic_sal=bsal;
        this.hra=hra;
        this.da=da;
    }

    public virtual double CalculateSalary ()
    {
      return basic_sal + hra+ da;
    }

    pubic override string ToString(){
      return base.ToString() +
      "Basic Salary ="+ this.basic_sal
      "HRA ="+ this.hra
      "Daily Allowance ="+ this.da;
    }
  }

  //Derived Class
  public class Manager: Employee
  { 
    private double incentive;

    public Manager():base(){
      this.incentive=0;
    }

    public Manager(double bsal, double hra, double da, double incentive):
                  base(bsal, hra, da)  //Member Initialized List
    {
      this.incentive=incentive;
    }

    public double CalculateIncentives ()
    {
      //code to calculate incentives
      Return incentives*2;
    }
    
    //Method overriding
    public override double CalculateSalary ()
    {
      return basic_sal + hra+ da + CalculateIncentives();
    }

    pubic override string ToString(){
      return base.ToString() + "Incentive ="+ this.incentive;
    }
  }

  static void Main ()
  {
      Manager mgr =new Manager();
      double Inc=mgr.CalculateIncentives ();
      double sal=mgr.CalculateSalary ();
  }
```


### Shadowing

Hides the base class member in derived class by using keyword new.

```
class Employee
{
  public virtual double CalculateSalary ()
     {return basic_sal;}
  }
}

class SalesEmployee:Employee
{ 
  double sales, comm;
  public new double CalculateSalary ()
  {
    return basic_sal+ (sales * comm);
  }
}

static void Main ()
{ 
  SalesEmployee sper= new SalesEmployee ();
  Double salary= sper.CalculateSalary ();
  Console.WriteLine (salary);
}
```

### Sealed class, Concrete class vs. abstract classes

##### Sealed class

Sometimes while building Software Product, we do not want any other developer to extend class infuture. We use <b>Sealed</b> keyword while declaring class. This class cannot be inherited futher. It tried , compiler would show compile time error.

```
sealed class SinglyList
{
  public virtual double Add () 
  {
    // code to add a record in the linked list}
  }
}

public class StringSinglyList:SinglyList
  {
    public override double Add () 
    {
        // code to add a record in the String linked list
    }
 }
```

##### Concrete class</b>
    - It is the class from whom we can create more than one objects.

```
  public class Person{
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public Person() {

    }
    public Person(string fname, string lname){
      this.FirstName=fname;
      this.LastName=lname;
    }
  }
```

#####  Abstract class</b>
    - It is the class from which we can not create object. 
    - An abstract class can contain minimum one method abstract method
    - An Abstract method do not have implementation.
    - An Abstract class enforces overriding in thier sub classes (Derived Classes)
```
public abstract class Employee
 {  
    public virtual double CalculateSalary();
     {
       return basic +hra + da ;
      }
    
    public abstract double CalculateBonus();
  }


 public  class Manager: Employee
 {   
    public override double CalculateSalary();
    {
      return basic + hra + da + allowances;
    }

    public override double CalaculateBonus ()
    {
       return basic_sal * 0.20;
    }
  }

  static void Main ()
  { 	

    Employee emp=new Manager ();
    double bonus=emp.CalaculateBonus ();
    double Salary=emp.CalculateSalary ();
  }
```

### Object class

All the types in .NET are represented as objects and are derived from the Object class.
The Object class has five methods:
- GetType
  - 	Returns type of the object.
- Equals
  - Compares two object instances. Returns true if they are equal, otherwise false.
- ReferenceEquals
  - Compares two object instances. Returns true if both are same instances, otherwise false.
- ToString
  - Converts an instance to a string type.
- GetHashCode
  - 	Returns hashcode for an object.




### 🔐 The Pillars Together — A Strong House

"So you see — abstraction filters out noise, encapsulation protects the core, inheritance gives us reuse, and polymorphism gives us flexibility. Together, they create a system that’s clean, powerful, and future-proof."


### ✨ Bonus Thought: Concurrency & Persistence

"Now picture this — multiple objects in your app doing their work at the same time — a chatbot replying to a user while data gets saved in the background. That’s **concurrency**.

And what if your user logs in tomorrow and finds their settings remembered? That’s **persistence** — your objects lived beyond the session, stored safely in a database or a file.

C# handles both like a champ. You'll learn threading, async/await, file I/O, databases — all under this beautiful OOP umbrella."

 
### 🙋 Mentor's Final Words

"My dear students, OOP is not just a coding style — it’s a **mindset**.

Think in terms of **objects**.
Speak in terms of **roles and responsibilities**.
And build systems like **real architects**.

Once you master OOP, you don’t just write code — you **design** software. And that is the difference between a coder and a software engineer."



## Nullable Types in C#

"Think about a **database** or a real-world banking system. Sometimes, a field **may not have a value yet**:

* A new bank account may **not have a credit limit** assigned.
* A loan application may **not have been approved**, so the approval date is unknown.
* A user may **not have filled optional information** like middle name or a second contact number.

In C#, **value types** like `int`, `double`, `bool` **cannot hold null** by default. If you try to assign `null` to an `int`, you’ll get a compile-time error.

This is where **nullable types** come in. They allow **value types to also hold null**, representing “no value” or “undefined.”

### How Nullable Types Work in C#

```csharp
class DatabaseReader
{
    // Nullable fields
    public int? numericValue = null; // int? is shorthand for Nullable<int>
    public bool? boolValue = true;

    // Nullable return types
    public int? GetIntFromDatabase()   { return numericValue; }
    public bool? GetBoolFromDatabase()  { return boolValue; }
}

class Program
{
    static void Main(string[] args)
    {
        DatabaseReader dr = new DatabaseReader();

        // Reading a nullable int
        int? i = dr.GetIntFromDatabase();
        if (i.HasValue)
            Console.WriteLine("Value of 'i' is: {0}", i.Value);
        else
            Console.WriteLine("Value of 'i' is undefined.");

        // Reading a nullable bool
        bool? b = dr.GetBoolFromDatabase();

        // Using the null-coalescing operator to provide a default
        int? myData = dr.GetIntFromDatabase() ?? 100;
        Console.WriteLine("Value of myData: {0}", myData.Value);
    }
}
```

### Key Points ✅

1. `int?` or `Nullable<int>` allows an **integer to be null**.
2. You can check if it has a value using `.HasValue`.
3. Use `.Value` to get the actual value safely **after checking HasValue**.
4. The **null-coalescing operator `??`** provides a default if the value is null.

---

### Local Nullable Variables Example

```csharp
void LocalNullableVariables()
{
    int? nullableInt = 10;
    double? nullableDouble = 3.14;
    bool? nullableBool = null;
    int?[] arrayOfNullableInts = new int?[10];

    // Using Nullable<T> explicitly
    Nullable<int> explicitInt = 10;
    Nullable<double> explicitDouble = 3.14;
    Nullable<bool> explicitBool = null;
    Nullable<int>[] arrayOfExplicitNullableInts = new int?[10];
}
```

💡 **Mentor Tip:**
“Think of nullable types as **placeholders for optional values**. Whenever you fetch data from a database, or have computations where a value may not exist yet, nullable types give you a **safe and expressive way to represent ‘unknown’** without breaking your program.”




 



### Understanding `ICloneable` in C#: Creating Meaningful Duplicates

Imagine you’re a designer, and you’ve just created a perfect prototype of a product. Now, instead of building every new item from scratch, you simply **clone** the prototype and tweak it slightly. That’s the idea behind the `ICloneable` interface in C#.

It’s a way of saying:
🗣 *“I promise I can create a **copy** of myself.”*

### What Is `ICloneable`?

`ICloneable` is a **marker interface** in .NET that provides a standard way to **clone** objects — that is, to make a new object that’s a copy of the current one.

```csharp
public interface ICloneable
{
    object Clone();
}
```

That’s it! Just one method — `Clone()` — which returns a new object that's supposed to be a **copy** of the original.

### Real-Life Analogy: Photocopy Machine

Think of a class implementing `ICloneable` as a document that knows how to **go through the photocopy machine** and come out with an identical copy.

For example:

* A **Resume** object can be cloned to send to different companies with small changes.
* A **Shape** object in a graphics editor can be cloned when duplicating a design.


### Basic Example

```csharp
class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone(); // Shallow copy
    }
}
```

```csharp
class Program
{
    static void Main()
    {
        Person original = new Person { Name = "Amit", Age = 30 };
        Person clone = (Person)original.Clone();

        Console.WriteLine(clone.Name); // Output: Amit
        Console.WriteLine(clone.Age);  // Output: 30
    }
}
```

Here, `MemberwiseClone()` is a protected method from the `Object` class that creates a **shallow copy** — it copies field-by-field, but not deeply.


### The Shallow vs Deep Copy Issue

🔹 **Shallow Copy**: If the object contains references (like other objects), they still point to the same memory in the clone.
🔹 **Deep Copy**: You manually clone each referenced object so that the clone is truly independent.

```csharp
class Address
{
    public string City { get; set; }
}

class Employee : ICloneable
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public object Clone()
    {
        return new Employee
        {
            Name = this.Name,
            Address = new Address { City = this.Address.City } // Deep copy
        };
    }
}
```


### Caution for Mentors and Developers

Microsoft documentation doesn’t recommend using `ICloneable` in public APIs because:

* It **doesn’t specify** whether the clone is shallow or deep.
* The behavior can vary between implementations.

🔁 So in real-world practice, you might:

* Implement **custom clone methods** (`DeepCopy()`, `CopyFrom()`, etc.)
* Use **copy constructors**
* Or even serialization/deserialization for deep copying in complex scenarios


### Mentor’s Wrap-Up

> Cloning is not just about copying — it’s about doing it **correctly and predictably**.

Use `ICloneable` when:

* You're working internally on objects you fully control.
* You understand the implications of shallow/deep copying.
* You want a **common, simple cloning mechanism** across your object model.

For public-facing APIs, prefer **explicitly named methods** that clearly state the kind of copy being made.
 

 
