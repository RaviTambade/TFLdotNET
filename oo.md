# Object Orientation in C#

Object-Oriented Programming is a programming model that works on a principle that revolves around objects rather than action or logic. It allows the users to create objects based on the requirement and then create methods to operate upon those objects.

Working on these objects to obtain the desired result is the goal of object-oriented programming.


## Object
	A real world entity which has well defined structure and behavior.
	
### Characteristics of an Object are:
- State
- Behavior
- Identity
- Responsibility
		
### Pillars of Object Orientation

- Abstraction
- Encapsulation
- Inheritance 
- Typing, Concurrency, Hierarchy, Persistence

### Abstraction

Getting essential characteristic of a System depending on the perspective of on Observer.
Abstraction is a process of identifying the key aspects of System and ignoring the rest
Only domain expertise can do right abstraction.

### Abstraction of Person Object

	- Useful for social survey
	- Useful for healthcare industry
	- Useful for Employment Information

### Encapsulation
- Hiding complexity of a System.
- Encapsulation is a process of compartmentalizing the element of an abstraction that constitute its structure and behavior.
	- Servers to separate interface of an abstraction and its implementation.
- User is aware only about its interface: any changes to implementation does not affect the user.

### Inheritance
- Classification helps in handing complexity.
- Factoring out common elements in a set of entities into a general entity and then making it more and more specific.
- Hierarchy is ranking or ordering of abstraction.
- Code and Data Reusability in System using is a relationship.

### Typing
Typing is the enforcement of the entity such that objects of different types may not be interchanges, or at the most, they may be interchanged only in restricted ways.

### Concurrency
Different objects responding simultaneously.

### Persistence
Persistence of an object through which its existence transcends time and or space.


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



### Singleton
The singleton pattern is one of the best-known patterns in software engineering. Essentially, a singleton is a class which only allows a single instance of itself to be created, and usually gives simple access to that instance

All these implementations share four common characteristics, however:

- A single constructor, which is private and parameterless. This prevents other classes from instantiating it (which would be a violation of the pattern). Note that it also prevents subclassing - if a singleton can be subclassed once, it can be subclassed twice, and if each of those subclasses can create an instance, the pattern is violated. The factory pattern can be used if you need a single instance of a base type, but the exact type isn't known until runtime.
- The class is sealed. This is unnecessary, strictly speaking, due to the above point, but may help the JIT to optimise things more.
- A static variable which holds a reference to the single created instance, if any.
- A public static means of getting the reference to the single created instance, creating one if necessary.

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

## Inheritance & Polymorphism
 
  - Provides code reusability and extensibility.
  - Inheritance is a property of class hierarchy whereby each derived class inherits attributes and methods of its base class.


- Manager class  is extended class of an Employee class.
- Employee class is bydefault extended from Object class.
- Object Class is mother of all classes .NET.

- Polymorphism is achieved using virtual methods and inheritance.
- virtual keyword is used to define a method in base class 
- override keyword is used in derived class.

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

    Manager mgr=new Manager ();
    double bonus=mgr. CalaculateBonus ();
    double Salary=mgr. CalculateSalary ();
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

- Interfaces are used for defining a Contract between Consumers and Providers in Software
- Consumer can consume instances wihtout know more details about implementations.
- Providers can implement standard interface methods into sub classes.
- Intefaces help us to build software using loosely coupled, highly cohesive architecture

For example:Text Editor uses Spellchecker as interfaces.
EnglishSpellChecker and FrenchSpellChecker are implementing contract defined by SpellChecker interface.

```

//Contract Code
public interface ISpellChecker
{ 
  ArrayList CheckSpelling (string word) ;
}


//Provider Code

public class EnglishSpellChecker:ISpellChecker
{
  ArrayList CheckSpelling (string word) 
  {
    // return possible spelling suggestions
  }
}

public class FrenchSpellChecker:ISpellChecker
{ 
  ArrayList CheckSpelling (string word) 
  {
    // return possible spelling suggestions
  }
}

//Consumer Code
class TextEditor
{
  public static void Main()
  { 
      ISpellChecker checker= new EnglishSpellChecker ();
      ArrayList words=checker. CheckSpelling (“Flower”);
      …
  }
}
```

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
