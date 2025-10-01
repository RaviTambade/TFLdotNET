# Object-Oriented Programming in C\#

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

### The Tale of the Singleton — The One and Only in the World of Objects

Imagine a kingdom where there must be exactly one **King** at any time. No more, no less. If there were two kings, confusion would reign, laws would conflict, and chaos would spread.

In software, sometimes you want the same kind of order — only **one single instance** of a class should ever exist. That’s the magic behind the **Singleton Pattern**.


### What Is Singleton?

The **Singleton** is a special class designed to make sure **only one object** of itself can ever be created throughout the whole lifetime of the program. It’s like the King of your digital kingdom — unique, alone, and always accessible.

Why do we need this? Imagine:

* A **Configuration Manager** that reads app settings — you want just one source of truth.
* A **Logger** that records events — you want all logs going to the same place.
* A **Database Connection Pool** — one manager controlling all connections.

### The Four Sacred Rules of Singleton

To keep this promise of uniqueness, Singleton classes follow these sacred rules:

1. **The Private Constructor**

The constructor is the guarded gate — it’s private and parameterless. No outsiders can sneak in to create new instances. This stops anyone from calling `new Singleton()` from outside.

Because no one else can create new ones, it also prevents subclassing. Why? Because if subclasses could create new instances, the “only one” rule would break down.

2. **The Sealed Class**

The class is sealed — like a royal decree that no one can override or extend it. This is not absolutely required, but it makes sure no one can trick the system by making subclasses and breaking the rule.

3. **The Static Instance**

Inside the class, there’s a **static variable** holding the one and only instance. It’s the throne — the unique seat reserved for the singleton object.

4. **The Public Accessor**

The class exposes a **public static property or method** that allows everyone in the kingdom to access the one singleton instance. If the throne is empty, this accessor creates the king; if the king already sits, it simply points to him.

 

### The Singleton in C# — The Bank Manager Example 🏦

"Imagine a bank. There’s only **one Bank Manager** responsible for authorizing all big transactions, creating accounts, and approving loans.
No matter how many tellers or accounts exist, there is only **one manager** — that’s the Singleton pattern in code.

```csharp
public sealed class BankManager
{
    // 3. Holds the one and only instance of BankManager
    private static BankManager? _instance = null;

    // 1. Private constructor - ensures no one else can create a manager
    private BankManager()
    {
        // Initialize resources, e.g., bank policies
    }

    // 4. Public accessor to get the single BankManager
    public static BankManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BankManager();
            }
            return _instance;
        }
    }

    // Example behavior
    public void ApproveLoan(decimal amount)
    {
        Console.WriteLine($"Bank Manager approved a loan of {amount}.");
    }
}
```

 

### Using the Singleton

```csharp
class Program
{
    static void Main()
    {
        BankManager manager1 = BankManager.Instance;
        BankManager manager2 = BankManager.Instance;

        manager1.ApproveLoan(50000);

        // Are both references pointing to the same instance?
        Console.WriteLine(Object.ReferenceEquals(manager1, manager2)); // True
    }
}
```

💡 **Key Takeaways:**

* There is **only one Bank Manager** in the system — no duplicates.
* Everyone in the bank (tellers, accounts, systems) **shares the same manager instance**.
* The Singleton ensures **controlled access** and consistent behavior across the bank.
 

### Why Does This Matter?

Without the Singleton, you might accidentally create many kings, causing conflicting rules and confusion. But with Singleton, you have:

* **Controlled access** to one and only instance.
* **Lazy initialization** — the king only appears when first needed.
* **Global access** without scattering the instance everywhere.



## Nullable Types in C#

"Think about a **database** or a real-world banking system. Sometimes, a field **may not have a value yet**:

* A new bank account may **not have a credit limit** assigned.
* A loan application may **not have been approved**, so the approval date is unknown.
* A user may **not have filled optional information** like middle name or a second contact number.

In C#, **value types** like `int`, `double`, `bool` **cannot hold null** by default. If you try to assign `null` to an `int`, you’ll get a compile-time error.

This is where **nullable types** come in. They allow **value types to also hold null**, representing “no value” or “undefined.”

---

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

---

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

---

💡 **Mentor Tip:**
“Think of nullable types as **placeholders for optional values**. Whenever you fetch data from a database, or have computations where a value may not exist yet, nullable types give you a **safe and expressive way to represent ‘unknown’** without breaking your program.”


## 🏦 Partial Classes – Teamwork in Banking Code

"Imagine you are building a **Bank Account Management system**.

Two developers are working on the same `BankAccount` class:

1. **Developer A** focuses on the **core data and properties** like account number, balance, and customer name.
2. **Developer B** focuses on **business logic** like deposit, withdraw, and applying interest.

Rather than one developer writing a huge class, C# allows us to **split the class into multiple files** using the **`partial` keyword**.

At compile-time, the compiler **merges all parts** into a single class. This way, both developers can work **simultaneously without conflicts**.
 

### Banking Example in C#

**BankAccount.Data.cs** (developer A — properties/data)

```csharp
public partial class BankAccount
{
    public string AccountNumber { get; set; }
    public string CustomerName { get; set; }
    public decimal Balance { get; set; }
}
```

**BankAccount.Logic.cs** (developer B — business logic)

```csharp
public partial class BankAccount
{
    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"{CustomerName} deposited {amount}. New Balance: {Balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"{CustomerName} withdrew {amount}. New Balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Insufficient funds!");
        }
    }

    public void ApplyInterest(decimal rate)
    {
        Balance += Balance * rate;
        Console.WriteLine($"Interest applied to {CustomerName}'s account. New Balance: {Balance}");
    }
}
```

 

### Using the Partial Class

```csharp
class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount
        {
            AccountNumber = "ACC123",
            CustomerName = "Ravi Tambade",
            Balance = 1000
        };

        account.Deposit(500);          // Works seamlessly
        account.Withdraw(200);         // Works seamlessly
        account.ApplyInterest(0.05m);  // Works seamlessly
    }
}
```

 

### 💡 Mentor Notes

1. **Why use partial classes?**

   * When multiple developers work on **different concerns** of the same class.
   * Helps organize **design vs logic**, **data vs behavior**, or **auto-generated vs custom code**.

2. **Compilation:**

   * All `.cs` files with the same partial class are **merged into one class** at compile-time.

3. **Banking analogy:**

   * Think of `BankAccount` as a **team effort**:

     * One person sets up the **account data**
     * Another writes the **banking operations**
   * Together, they form a **complete functional account** for the bank.

 
### The Invisible Contract: Interfaces in C\#

Imagine you’re signing a contract with someone — it clearly defines what each side promises to do. That’s exactly what **Interfaces** do in programming.


### What Is an Interface?

An **Interface** is like an invisible handshake or a contract between:

* **Providers** — who promise to deliver certain services or behaviors.
* **Consumers** — who rely on those promises without worrying about how they’re fulfilled.


### Why Are Interfaces Important?

Interfaces let us write software that’s:

* **Loosely Coupled** — Consumers and Providers don’t tightly depend on each other’s internal details.
* **Highly Cohesive** — Each piece focuses on doing its job well, nothing extra.

In other words, interfaces are the blueprint for building flexible, maintainable, and scalable software systems.


### 💳 Interfaces – A Bank’s Contract

"Imagine a bank with different types of accounts: **Savings Account**, **Current Account**, and maybe even **Loan Account**.

Every account **must support certain actions**: deposit money, withdraw money, and check balance.

But **how** these actions work can be different for each account type:

* Savings accounts might apply interest on deposit.
* Current accounts might allow overdrafts.
* Loan accounts might not allow withdrawals at all.

The **bank doesn’t care** about the internal details — it just knows: *any account must follow the contract*."

In C#, this contract is called an **interface**.


### How This Looks in Banking C#

```csharp
// The contract – every bank account must implement these actions
public interface IAccount
{
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    decimal CheckBalance();
}

// Savings Account implementing the contract
public class SavingsAccount : IAccount
{
    private decimal _balance = 0;
    private decimal _interestRate = 0.05m;

    public void Deposit(decimal amount)
    {
        _balance += amount + (amount * _interestRate);
        Console.WriteLine($"SavingsAccount: Deposited {amount} + interest. Balance: {_balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;
            Console.WriteLine($"SavingsAccount: Withdrew {amount}. Balance: {_balance}");
        }
        else
        {
            Console.WriteLine("SavingsAccount: Insufficient funds!");
        }
    }

    public decimal CheckBalance() => _balance;
}

// Current Account implementing the same contract
public class CurrentAccount : IAccount
{
    private decimal _balance = 0;
    private decimal _overdraftLimit = 1000;

    public void Deposit(decimal amount)
    {
        _balance += amount;
        Console.WriteLine($"CurrentAccount: Deposited {amount}. Balance: {_balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (_balance + _overdraftLimit >= amount)
        {
            _balance -= amount;
            Console.WriteLine($"CurrentAccount: Withdrew {amount}. Balance: {_balance}");
        }
        else
        {
            Console.WriteLine("CurrentAccount: Exceeds overdraft limit!");
        }
    }

    public decimal CheckBalance() => _balance;
}
```


### Using the Interface

```csharp
class Program
{
    static void Main()
    {
        // The bank system can work with any account type through IAccount
        IAccount account = new SavingsAccount();
        account.Deposit(1000);
        account.Withdraw(200);

        account = new CurrentAccount();
        account.Deposit(500);
        account.Withdraw(1200); // Uses overdraft
    }
}
```


### 💡 Mentor Notes

1. **Interface = Contract**: The bank ensures that every account type implements the required actions.
2. **Decoupling**: The bank system interacts with `IAccount` and **doesn’t care** about the specific account type.
3. **Flexibility**: You can add new account types in the future (like `LoanAccount`) **without changing the existing code**.
4. **Polymorphism + Interface**: The same code (`Deposit`, `Withdraw`) works differently depending on the account type.

 

### Final Thoughts for the Apprentice

Interfaces are the **contract** that bind your software parts together cleanly and predictably. They enable the elegant dance of collaboration between classes — letting you swap, improve, or extend implementations without breaking everything else.

## 🏦 Explicit Interface Implementation – Handling Name Conflicts

"Imagine you are in a bank working with **Transactions**.

* Every transaction can have **Order Details** (like which product or service is purchased).
* Every transaction also has **Customer Details** (like who performed the transaction).

Now, suppose both **Order Details** and **Customer Details** require a method called `ShowDetails()`.

💡 Problem: How does the `Transaction` class handle **two methods with the same name**?

The answer is **Explicit Interface Implementation** — we fully qualify which method belongs to which interface. This way, there’s **no ambiguity**.



### Banking Example in C#

```csharp
// Interface for Order Details
public interface IOrderDetails
{
    void ShowDetails();
}

// Interface for Customer Details
public interface ICustomerDetails
{
    void ShowDetails();
}

// Transaction class implements both interfaces explicitly
public class Transaction : IOrderDetails, ICustomerDetails
{
    // Implementation specific to order details
    void IOrderDetails.ShowDetails()
    {
        Console.WriteLine("Showing Order Details: Transaction #12345, Product: Savings Account Subscription");
    }

    // Implementation specific to customer details
    void ICustomerDetails.ShowDetails()
    {
        Console.WriteLine("Showing Customer Details: Customer Name: Ravi Tambade, Account: 987654321");
    }
}
```

### Using Explicit Interface Implementation

```csharp
class Program
{
    static void Main()
    {
        Transaction txn = new Transaction();

        // Accessing Order Details
        IOrderDetails order = txn;
        order.ShowDetails(); 
        // Output: Showing Order Details: Transaction #12345, Product: Savings Account Subscription

        // Accessing Customer Details
        ICustomerDetails customer = txn;
        customer.ShowDetails();
        // Output: Showing Customer Details: Customer Name: Ravi Tambade, Account: 987654321
    }
}
```

### 💡 Mentor Notes

1. **Why explicit implementation?**

   * When multiple interfaces have the **same method signature**, explicit implementation avoids conflicts.

2. **Access restriction:**

   * Explicitly implemented methods **cannot be called directly** via the class instance (`txn.ShowDetails()` will not compile).
   * You must **cast to the interface type** to access the method.

3. **Banking analogy:**

   * Think of it as having **two separate managers** in the bank:

     * One handles **order processing**
     * Another handles **customer verification**
   * Even though both have a method called `ShowDetails()`, they work **independently**.

 
### The World of Collections in C#: Organizing Chaos with Grace

Let me take you into a day in the life of a software developer. You wake up and look at your tasks — emails to check, logs to scan, messages to process, and maybe even some errors to handle. All these are **collections of data** — grouped and ready to be handled.

In C#, **collections** are containers that help you manage, organize, and operate on groups of objects in elegant ways.

But behind the scenes, how do these collections work so well together? The secret lies in **interfaces** — contracts that ensure every collection behaves in a predictable way.

### Building Blocks: Standard .NET Interfaces That Power Collections

Let’s walk through the main players of this well-orchestrated collection world.

### 🔁 `IEnumerator<T>` – The Tour Guide

Think of `IEnumerator` as a **tour guide**. It knows how to walk through a collection **one item at a time**.

```csharp
public interface IEnumerator
{
    bool MoveNext();
    object Current { get; }
    void Reset();
}
```

It helps power loops like this:

```csharp
while (enumerator.MoveNext())
{
    var item = enumerator.Current;
    // process item
}
```

### 🔄 `IEnumerable<T>` – The Collection You Can Walk Through

This is the **walkable collection**. Any class implementing `IEnumerable` promises: “You can use `foreach` on me.”

```csharp
public interface IEnumerable
{
    IEnumerator GetEnumerator();
}
```

Example:

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };
foreach (var n in numbers)
{
    Console.WriteLine(n); // Thanks to IEnumerable!
}
```


### 📏 `ICollection<T>` – The Measured Bag

`ICollection` builds on `IEnumerable`. It adds **size**, **add/remove**, and **sync** capabilities.

It says: “I’m a real collection — you can count me, add to me, and ask about my contents.”

```csharp
public interface ICollection<T> : IEnumerable<T>
{
    int Count { get; }
    void Add(T item);
    bool Remove(T item);
}
```


### 📚 `IList<T>` – The Indexed Library

`IList` is like a **bookshelf** — every item is accessible **by position**.

```csharp
IList<string> books = new List<string>();
books.Add("C# for Beginners");
Console.WriteLine(books[0]); // Access by index!
```


### 🧠 `IComparable<T>` – The Self-Aware Item

This interface is for **individual objects** that know how to compare themselves with others.

```csharp
public interface IComparable<T>
{
    int CompareTo(T other);
}
```

Think of a **Student** object knowing how to compare its score with another student.


### 🧑‍⚖️ `IComparer<T>` – The Judge

Unlike `IComparable`, the `IComparer` is a **third-party judge** — it compares two objects *externally*.

```csharp
public interface IComparer<T>
{
    int Compare(T x, T y);
}
```

Imagine a **custom sorter** that compares people by age, name, or height, depending on the situation.


### 🗝️ `IDictionary<K, V>` – The Lookup Table

`IDictionary` is like a **phonebook** — a set of **key-value pairs**.

```csharp
Dictionary<string, string> countryCodes = new Dictionary<string, string>
{
    { "IN", "India" },
    { "US", "United States" }
};

Console.WriteLine(countryCodes["IN"]); // Output: India
```

Each item in `IDictionary` is accessed using a unique key — quick and efficient.

 ### 💡 Why Use These Interfaces?

These standard interfaces bring:

* **Flexibility** – you can switch between `List`, `LinkedList`, `Array`, etc., without changing your consumer code.
* **Extensibility** – you can create your own collections that play well with `foreach`, `Sort()`, and LINQ.
* **Interoperability** – any class using these interfaces can plug into the wider .NET ecosystem.

 ### 📦 A Real-World Glimpse: Custom Collection Example

```csharp
class MyCustomCollection : IEnumerable<int>
{
    private int[] data = { 10, 20, 30 };

    public IEnumerator<int> GetEnumerator()
    {
        foreach (var item in data)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
```

Now you can use your custom collection like this:

```csharp
var myData = new MyCustomCollection();
foreach (int num in myData)
{
    Console.WriteLine(num);
}
```

### 🧭 Final Mentor's Note

These interfaces are the **foundation stones** of the rich .NET collections framework. Think of them like the agreements and blueprints that let many kinds of collections work the same way, whether they’re built into .NET or custom-made by you.

In software architecture, interfaces don’t just enforce structure — they enable **freedom through consistency**.

 


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
 

 
#### Implementing IEnumerable Interface
```
public class Team:IEnumerable
{
    private player [] players;

    public Team ()
    {
    Players= new Player [3];
    Players[0] = new Player(“Sachin”, 40000);
    Players[1] = new Player(“Rahul”, 35000);
    Players[2] = new Player(“Mahindra”, 34000);
    }

    public IEnumertor GetEnumerator ()
    {
        Return players.GetEnumerator();
    }
}

public static void Main()
{
    Team India = new Team();
    foreach(Player c in India)
    {
        Console.WriteLine (c.Name, c.Runs);
    }
}
```

#### Implementing ICollection Interface

```
public class Team:ICollection
{
    private Players [] players;
    public Team() {……..}


    //Implementing Count Property of ICollection Interface
    public int Count {
      get {
        return players.Count ;
      }
    }
    // other implementation of Team
}

//Main Function
public static void Main()
{
    Team India = new Team ();
    foreach (Player c in India)
    {
    Console.WriteLine (c.Name, c.Runs);
    }
}
```

#### Implementing IComparable Interface

```
public class Player:IComparable
{

    int IComparable.CompareTo(object obj)
    {
        Player temp= (Player) obj;
        if (this. Runs > temp.Runs)
          return 1;
        if (this. Runs < temp.Runs)
          return -1;
        else 
          return 0;
    }
}

public static void Main()
{
    Team India = new Team();
    // add five players with Runs
    Arary.Sort(India);
    // display sorted Array
}
```
