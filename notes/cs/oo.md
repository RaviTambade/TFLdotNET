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


#### 👪 3. Inheritance – Like Parents, Like Children

"Let me ask you — do you have traits like your parents? Eyes, habits, maybe even their sense of humor?

That’s **inheritance** in code.

Let’s say you create a `Vehicle` class. It has `Speed` and a `Move()` method. Now, you build a `Car` class that inherits all of that and adds `NumberOfDoors`.

```csharp
class Vehicle
{
    public int Speed { get; set; }
    public void Move() => Console.WriteLine("Moving...");
}

class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
}
```

The `Car` doesn’t need to reinvent the wheel (pun intended 😄). It reuses and builds on what's already there."


#### 🎭 4. Polymorphism – One Interface, Many Behaviors

"Think about a `RemoteControl`. You use the same button to power on a TV, AC, or music system — but each responds differently.

That’s **polymorphism**.

In C#, it means you can call the same method name — like `Speak()` — and each class gives its own behavior."

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("Some sound");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Bark!");
}

class Cat : Animal
{
    public override void Speak() => Console.WriteLine("Meow!");
}
```

"Now when I write:

```csharp
Animal a1 = new Dog();
Animal a2 = new Cat();
a1.Speak(); // Bark!
a2.Speak(); // Meow!
```

I get different outputs, even though I used the same method. That’s the power of polymorphism."


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

 
### Wrapping Up with a Simple C# Example

Let’s see how all this fits into a small, friendly example:

```csharp
// Abstraction and Encapsulation: We hide details, show essentials
class Person
{
    // State: properties
    public string Name { get; set; }
    public int Age { get; set; }

    // Behavior: methods
    public void Introduce() => Console.WriteLine($"Hi, I'm {Name} and I'm {Age} years old.");
}

// Inheritance: Student is a Person with extra info
class Student : Person
{
    public string School { get; set; }
    
    public void Study() => Console.WriteLine($"{Name} is studying at {School}.");
}

class Program
{
    static void Main()
    {
        Student student = new Student { Name = "Tejas", Age = 25, School = "Open University" };
        student.Introduce();  // inherited behavior
        student.Study();      // student-specific behavior
    }
}
```

Here, you see abstraction by exposing only necessary info, encapsulation by grouping data and behavior, inheritance with `Student` from `Person`, and typing enforced by C#.

 

### Why Teach This Way?

By telling stories around objects, their roles, and their interactions, your learners will feel like architects, not just coders. They’ll think in models, designs, and relationships — and that’s the heart of software craftsmanship.


### Namespace and Class
Namespace is a collection .NET Types such as structure, class, interfaces, etc.

```
namespace  EmployeeApp
{
   public class Employee
  {
    private  string empName;
    private  int empID;
    private  float currPay;
    private   int empAge;
    private  string empSSN;
    private static string companyName;
   
    public Employee ()
    { 
      empID=18; 
      currPay=15000; 
    }
    
    public Employee (int id, float basicSal)
    { 
      empID=id; currPay= basicSal; }
      public ~Employee()
      { 
        //DeInitializtion 
      }

      public void GiveBonus(float amount)
      {
        currPay += amount;   
      }
      
      public void DisplayStats()
      { 
          Console.WriteLine("Name: {0}", empName);
          Console.WriteLine("ID:   {0}", empID);
          Console.WriteLine("Age:  {0}", empAge);
          Console.WriteLine("SSN:  {0}", empSSN);
          Console.WriteLine("Pay:  {0}", currPay);
      }
 }
}
```


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


### The Singleton in C# — A Royal Code Example

```csharp
public sealed class King
{
    // 3. The static variable holding the one instance
    private static King? _instance = null;

    // 1. The private constructor - gatekeeper of the throne
    private King()
    {
        // Initialize any resources here
    }

    // 4. The public static accessor to get the King instance
    public static King Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new King();
            }
            return _instance;
        }
    }

    // Example behavior
    public void Rule()
    {
        Console.WriteLine("The King rules the kingdom.");
    }
}
```

### Using the Singleton

```csharp
class Program
{
    static void Main()
    {
        King king1 = King.Instance;
        King king2 = King.Instance;

        king1.Rule();

        Console.WriteLine(Object.ReferenceEquals(king1, king2)); // True — both refer to the same King
    }
}
```

### Why Does This Matter?

Without the Singleton, you might accidentally create many kings, causing conflicting rules and confusion. But with Singleton, you have:

* **Controlled access** to one and only instance.
* **Lazy initialization** — the king only appears when first needed.
* **Global access** without scattering the instance everywhere.


### Final Thoughts for the Apprentice

The Singleton pattern is like appointing a wise monarch for your application — unique, reliable, and easy to find. It’s a classic tool in your design toolbox for managing shared resources carefully.

In your coding journey, you’ll find many patterns like this — each a story about how to tame complexity and build clear, maintainable systems.


```
public class OfficeBoy
 {     
    private static OfficeBoy _ref = null;
    private  int _val;
    private  OfficeBoy()
    {
        _val = 10;  
    }
    
    public  int Val 
    { 
      get {  return _val;  }
      set { _val = value; }
                       }
      public static OfficeBoy GetObject ()
      {    if (_ref == null)
              _ref = new OfficeBoy ();
             return _ref;
      }
 }

static void Main(string[] args)
 { 
    OfficeBoy sweeper, waiter;
    string s1; float f1;
    
    sweeper = OfficeBoy.GetObject(); waiter = OfficeBoy.GetObject();
    sweeper.Val = 60;
    
    Console.WriteLine("Sweeper Value : {0}", sweeper.Val);
    Console.WriteLine("Waiter Value  : {0}", waiter.Val);
    
    s1 = sweeper.Val.ToString();
    f1 = (float)sweeper.Val;
    
    sweeper.Val = int.Parse(s1);
    sweeper.Val = Convert.ToInt32(s1);    
 }

```


### Nullable Types
```
  class  DatabaseReader
  {
      public int? numericValue = null;
      public bool? boolValue = true;
      public int? GetIntFromDatabase()     { return numericValue; }
      public bool? GetBoolFromDatabase()   { return boolValue; }
  }
    
  public static void Main (string[] args)
    {
        DatabaseReader  dr = new DatabaseReader();
        int? i = dr.GetIntFromDatabase();
        if (i.HasValue)
          Console.WriteLine("Value of 'i' is: {0}", i.Value);
        else
          Console.WriteLine("Value of 'i' is undefined.");
        bool? b = dr.GetBoolFromDatabase();
        int? myData = dr.GetIntFromDatabase() ?? 100;
        Console.WriteLine("Value of myData: {0}", myData.Value);
    }

    static void LocalNullableVariables ()
      {     int? nullableInt = 10;
            double? nullableDouble = 3.14;
            bool? nullableBool = null;
            int?[] arrayOfNullableInts = new int?[10];
            
            // Define some local nullable types using Nullable<T>. 
            
            Nullable<int> nullableInt = 10;
            Nullable<double> nullableDouble = 3.14;
            Nullable<bool> nullableBool = null;
            Nullable<int> [] arrayOfNullableInts = new int?[10];
      }
    }
  }
```

### The Family Saga of Inheritance and Polymorphism in C#

Imagine a big family where children inherit traits, habits, and skills from their parents — that’s the heart of **Inheritance** in programming. It helps you reuse code and extend your software without rewriting everything from scratch.

### What Is Inheritance?

Inheritance is like a family tree in the world of classes.

* There’s a **base class** — the parent, like an **Employee**.
* Then you have **derived classes** — the children, like **Manager**, who inherit everything the parent has but also add their own special traits.

```plaintext
Object (root of all)
   └─ Employee (inherits from Object)
         └─ Manager (inherits from Employee)
```

In .NET, **every class** implicitly inherits from a special class called `Object` — the mother of all classes. This means even the simplest class you create has some basic behaviors already, like the ability to print itself (`ToString()`), check equality, or get a hash code.

### Why Inheritance?

Because it saves time and effort:

* You write common properties and methods once in `Employee`.
* `Manager` automatically gets those without extra code.
* You extend `Manager` with new things only managers have, like managing teams.


### What About Polymorphism?

Polymorphism means “many shapes” — and in programming, it means the same method can behave differently depending on the object calling it.

Imagine you have a method called `Work()`.

* For a generic **Employee**, `Work()` might say: “I’m doing general work.”
* For a **Manager**, `Work()` could say: “I’m managing my team.”

### How Does C# Support Polymorphism?

By using the magic keywords:

* **`virtual`** in the base class tells C#: “Hey, this method can be changed by the children.”
* **`override`** in the derived class says: “I’m changing the behavior of this method to fit me better.”

### A Simple C# Family Example

```csharp
class Employee
{
    public string Name { get; set; }

    // Virtual method: base version can be overridden
    public virtual void Work()
    {
        Console.WriteLine($"{Name} is doing general work.");
    }
}

class Manager : Employee
{
    // Override Work to customize for Manager
    public override void Work()
    {
        Console.WriteLine($"{Name} is managing the team.");
    }
}

class Program
{
    static void Main()
    {
        Employee emp = new Employee { Name = "Tejas" };
        Manager mgr = new Manager { Name = "Anita" };

        emp.Work();  // Output: Tejas is doing general work.
        mgr.Work();  // Output: Anita is managing the team.

        // Polymorphism in action: treating Manager as Employee reference
        Employee polymorphicEmp = new Manager { Name = "Raj" };
        polymorphicEmp.Work();  // Output: Raj is managing the team.
    }
}
```

### Why This Matters

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


### Partial class

Consider a situation, Where two developers are working on same class but they want to add 
design code and business logic or event handling logic as seperate  C# files. This class needed to be  spread across multiple source file. In such situaation ,we can use partial keword

```
// window.design.cs file

 public partial class Window{
  
    private int width ;
    private int height;
    private Color Background
 }


// window.eventdrivenlogic.cs file

 public partial class Window{
  
    public void OnLoad() {     }
    public void onPaint() {     }
    public void OnMouseMove() {   }
   
 }
```

AT the time of compilation , all source files(.cs files) for the class definition Window are merged  into  as single class Window with all members. Access modifiers used for defining a class should be consistent across all files.

## Interfaces

In C# an interface is similar to abstract class. In interface all methods are bydefault abstract.
```
public interface IShape{
  //method without body
  void Draw();
}
```

We can implment interface into more than one classes. Line and Circle classes are implementing IShape interface which is defined above.

```
  //Interface inheritance
  public class Line:IShape{

    public void Draw() {
      //Line drawing logic

    }
  }

  public class Circle:IShape{

    public void Draw() {
      //Circle drawing logic
      
    }
  }
```

### The Invisible Contract: Understanding Interfaces in C\#

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


### Real-World Analogy: The SpellChecker Contract

Think of a **Text Editor** app that needs to check spelling. It doesn’t care how the spell check is done, it just knows it must call certain methods to check words.

* The **Text Editor** is the Consumer.
* The **SpellChecker interface** is the contract defining those methods.
* **EnglishSpellChecker** and **FrenchSpellChecker** are Providers who implement this contract in their own way.

  

### How Does This Look in C#?

```csharp
// The contract - interface defining what a SpellChecker must do
public interface ISpellChecker
{
    bool CheckSpelling(string word);
}

// English Spell Checker implementing the contract
public class EnglishSpellChecker : ISpellChecker
{
    public bool CheckSpelling(string word)
    {
        // English spelling check logic here
        return word == "hello" || word == "world";
    }
}

// French Spell Checker implementing the contract
public class FrenchSpellChecker : ISpellChecker
{
    public bool CheckSpelling(string word)
    {
        // French spelling check logic here
        return word == "bonjour" || word == "monde";
    }
}

// Text Editor uses any spell checker through the interface
public class TextEditor
{
    private readonly ISpellChecker _spellChecker;

    public TextEditor(ISpellChecker spellChecker)
    {
        _spellChecker = spellChecker;
    }

    public void CheckWord(string word)
    {
        bool correct = _spellChecker.CheckSpelling(word);
        Console.WriteLine(correct ? $"{word} is spelled correctly." : $"{word} is misspelled.");
    }
}

class Program
{
    static void Main()
    {
        // Using English Spell Checker
        TextEditor editor = new TextEditor(new EnglishSpellChecker());
        editor.CheckWord("hello");   // Output: hello is spelled correctly.
        editor.CheckWord("bonjour"); // Output: bonjour is misspelled.

        // Switching to French Spell Checker without changing TextEditor code
        editor = new TextEditor(new FrenchSpellChecker());
        editor.CheckWord("bonjour"); // Output: bonjour is spelled correctly.
        editor.CheckWord("hello");   // Output: hello is misspelled.
    }
}
```
 

### Why This Matters

* The **Text Editor** doesn’t care *how* spelling is checked.
* It just calls `CheckSpelling` as promised by the `ISpellChecker` interface.
* You can add new spell checkers anytime (say, SpanishSpellChecker) without changing the editor’s code.

 

### Final Thoughts for the Apprentice

Interfaces are the **contract** that bind your software parts together cleanly and predictably. They enable the elegant dance of collaboration between classes — letting you swap, improve, or extend implementations without breaking everything else.

 
### Explicit Interface Inheritance
A class can implement more than one interfaces. These interfaces may contain a member function  with the same name(signature). In such cases, we should take care of implementing both method using fully qualified names as shown below:
```
public interface IOrderDetails    { void ShowDetails();}
public interface ICustomerDetails { void ShowDetails();}

public class Transaction: IOrderDetails, ICustomerDetils
{
    
    void IOrderDetails.ShowDetails()
    {  
      // implementation for interface IOrderDetails
    }
    void ICustomerDetails.ShowDetails()
    {  
      // implementation for interface IOrderDetails
    }
}

public static void Main()
{
    Transaction obj = new Transaction();
    IOrderDetails od = obj;
    od.ShowDetails();
    ICustomerDetails cd = obj;
    cd.ShowDetails();
}
```



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
