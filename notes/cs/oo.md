# Welcome to Object-Oriented Programming in C#

In the vast universe of programming, there’s a magical way of crafting software called **Object-Oriented Programming**, or OOP for short. Instead of thinking about programming as a list of steps or actions, OOP invites you to think like an architect, a designer — someone who builds digital worlds with objects that behave, interact, and live together.

### What Is an Object?

Think about the world around you. Everything you see — a car, a person, a smartphone — is an **object**. It has:

* **State** (the current condition or attributes it holds; e.g., a phone’s color, model)
* **Behavior** (what it can do; e.g., a phone can make calls, send messages)
* **Identity** (each object is unique, like your own phone versus a friend’s phone)
* **Responsibility** (what role it plays in the world; e.g., a phone’s job is communication)

In programming, an **object** is just like these real-world things — a bundle of data (state) and code (behavior) wrapped together.

### Why Object-Oriented?

Because it’s natural! We humans tend to think in terms of “things” and how they relate, rather than just raw steps. Imagine you’re building an app for a bookstore. Instead of just listing tasks like “list all books” or “add a new book,” OOP lets you create a **Book** object, an **Author** object, and maybe a **Customer** object — each with their own data and behavior.


### The Four Pillars of Object-Oriented Programming

OOP stands on four mighty pillars, each helping you build strong, clean, and reusable software.

#### 1. Abstraction: The Art of Focus

Abstraction is like looking through a camera lens and focusing on just what’s important. When you design a **Person** object, you don’t worry about every tiny detail; you pick what matters for your purpose.

* For a social survey, maybe age, gender, and location matter.
* For healthcare, it’s medical history and allergies.
* For employment, job title and experience.

This ability to hide unnecessary details and show only the relevant parts is **abstraction**.

#### 2. Encapsulation: The Protective Shield

Imagine your phone. You don’t open it up to fix the wiring when you want to call someone, right? You just use the interface — the touchscreen and apps. **Encapsulation** in C# hides the complex inner workings of objects and exposes only what is necessary.

In code, you wrap the data (variables) and the methods (functions) inside a class and control access using keywords like `private`, `public`, and `protected`. This way, users of your object don’t accidentally mess with the internals — they interact only with the safe, clean interface.

#### 3. Inheritance: Family Traits Passed Down

Inheritance is nature’s way of passing down traits, and in programming, it’s your shortcut to reuse code.

Imagine you have a **Vehicle** class — it has properties like `Speed` and methods like `Move()`. Now, a **Car** is a kind of Vehicle, so it inherits those properties and behaviors but also adds its own — maybe `NumberOfDoors`.

In C#, you write:

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

The `Car` automatically gets everything `Vehicle` has, plus its own extras. This saves you from rewriting the same code and helps you organize your program like a real-world family tree.

#### 4. Typing: Keeping Things in Their Place

Typing is the rulebook that says, “A Car is a Car, not a Banana.” It makes sure you don’t mix up different kinds of objects accidentally. In C#, the type system is strict, so you can’t assign a `Car` object to a variable expecting a `Book`. This helps catch mistakes early and keeps your code logical and safe.

### Bonus Concepts

* **Concurrency:** Imagine multiple objects doing their tasks at the same time — like a cashier scanning items while a manager checks inventory. C# supports this beautifully with threading and async programming.

* **Persistence:** Sometimes, you want objects to live beyond the running program — like saving a user’s profile to a database so it’s there when they come back tomorrow. That’s persistence, and it’s a crucial part of real-world apps.

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

## Using inbuilt(standard) interfaces Provided by .NET

The .NET provides standard interfaces for enumerating, comparing, and creating collections.
- <b>IEnumertor</b> Supports a simple iteration over collection
- <b>IEnumerable</b> Supports foreach semantics
- <b>ICollection</b> Defines size, enumerators and synchronization methods for all collections.
- <b>IList</b> Represents a collection of objects that could be accessed by index
- <b>IComaprable</b> Defines a generalized comparison method to create a type-specific 
comparison
- <b>IComparer</b> Exposes a method that compares two objects.
- <b>IDictionary</b> Represents a collection of Key and value pair

### Implementing ICloneable interface

```
class StackClass: ICloneable
{  
  int size; int [] sArr;

  public StackClass (int s) { size=s; sArr= new int [size]; }

  public object Clone(){
    StackClass s = new StackClass(this.size);
    this.sArr.CopyTo(s.sArr, 0);
    return s;
  }
}

public static void Main()
{
  StackClass stack1 = new StackClass (4);
  Stack1 [0] = 89;
    …..
  StackClass stack2 = (StackClass) stack1.Clone ();
}
```
 
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
