# Object Orientation in C#

Object
	A real world entity which has well defined structure and behavior.
	
### Characteristics of an Object are:
	State
	Behavior
	Identity
	Responsibility
		
### Pillars of Object Orientation
	Abstraction
	Encapsulation
	Inheritance 
	Typing, Concurrency, Hierarchy, Persistence

### Abstraction
Getting essential characteristic of a System depending on the perspective of on Observer.
Abstraction is a process of identifying the key aspects of System and ignoring the rest
Only domain expertise can do right abstraction.

### Abstraction of Person Object
	Useful for social survey
	Useful for healthcare industry
	Useful for Employment Information

### Encapsulation
Hiding complexity of a System.
Encapsulation is a process of compartmentalizing the element of an abstraction that constitute its structure and behavior.
	
	Servers to separate interface of an abstraction and its implementation.
	User is aware only about its interface: any changes to implementation does not affect the user.

### Inheritance
Classification helps in handing complexity.
Factoring out common elements in a set of entities into a general entity and then making it more and more specific.
Hierarchy is ranking or ordering of abstraction.
Code and Data Reusability in System using is a relationship.

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
    { empID=18; currPay=15000; }
    public Employee (int id, float basicSal)
    { empID=id; currPay= basicSal; }
    public ~Employee()
    { //DeInitializtion }
    public void GiveBonus(float amount)
    {      currPay += amount;    }
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


### Partial class

A class can be spread across multiple source files using the keyword partial.
All source files for the class definition are compiled as one file with all class members.
Access modifiers used for defining a class should be consistent across all files.

### Properties (smart fields)
Have two assessors:
Get	retrieves data member values.
Set	enables data members to be assigned
```
public int EmployeeID
{ get {return _id;}
  set {_id=value ;}
}


```

### Indexers (smart Array)

```
public class Books
{
 private string [] titles= new string [100];
 public string this [int index]
  {
get{ if (index <0 || index >=100)
			return 0;
		else
			return titles [index];
   }
set{
 	if (! index <0 || index >=100)
			return 0;
		else
			titles [index] =value;
   }
   }
}
public static void Main ()
{ Books mybooks=new Books ();
  Mybooks [3] =”Mogali in Jungle”;
}

```

### Singleton

```
public class OfficeBoy
 {      private static OfficeBoy _ref = null;
        private  int _val;
        private  OfficeBoy()   {   _val = 10;  }
        public  int Val {   get  {  return _val;  }
               	   set { _val = value; }
                       }
       public static OfficeBoy GetObject ()
        {    if (_ref == null)
               _ref = new OfficeBoy ();
             return _ref;
        }
 }
static void Main(string[] args)
 {    OfficeBoy sweeper, waiter;
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



### Arrays
```
int [  , ]  mtrx = new int [2, 3];
		Can initialize declaratively
int [ , ] mtrx = new int [2, 3] { {10, 20, 30}, {40, 50, 60} }


```

### Jagged Arrays
An Array of Array
```
int [ ]  [ ]  mtrxj = new  int [2] [];

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
  {   int? nullableInt = 10;
      double? nullableDouble = 3.14;
      bool? nullableBool = null;
      int?[] arrayOfNullableInts = new int?[10];
      // Define some local nullable types using Nullable<T>. 
      Nullable<int> nullableInt = 10;
      Nullable<double> nullableDouble = 3.14;
      Nullable<bool> nullableBool = null;
      Nullable<int> [] arrayOfNullableInts = new int?[10];
    }}}

```

## Inheritance
 
- Provides code reusability and extensibility.
- Inheritance is a property of class hierarchy whereby each derived class inherits attributes and methods of its base class.
Every Manager is Employee.
Every Wage Employee is Employee.

```
class Employee
{   public double CalculateSalary ()
       {return basic_sal + hra+ da ;}
}

class Manager: Employee
{
   public double CalculateIncentives ()
    {
     //code to calculate incentives
     Return incentives;
   }
}

static void Main ()
{
Manager mgr =new Manager ();
double Inc=mgr. CalculateIncentives ();
double sal=mgr. CalculateSalary ();
}


```

## Constructors in Inheritance

```

class Employee
{ public  Employee ()
    {Console.WriteLine (“in Default constructor”) ;}
  public Employee (int eid, ….)
     {Console.WriteLine (“in Parameterized constructor”) ;}
}

class Manager: Employee
{
 public Manager (): base ()   {……..}
 public Manager (int id): base (id,….)   {…..}
}

```


## Polymorphism

Ability of different objects to responds to the same message in different ways is called Polymorphism.
horse.Move();
car.Move();
aeroplane.Move(); 

#### Virtual and Override

Polymorphism is achieved using virtual methods and inheritance.
Virtual keyword is used to define a method in base class and override keyword is used in derived class.

```
class Employee
{ public virtual double CalculateSalary ()
     {return basic_sal+ hra + da ;}
}

class Manager: Employee
{
  public override double CalculateSalary ()
     {return (basic_sal+ hra + da + allowances);}
}

staic void Main ()
{
  Employee mgr= new Manager ();
  Double salary= mgr. CalculateSalary ();
  Console.WriteLine (salary);
}

```

### Shadowing

Hides the base class member in derived class by using keyword new.

```
class Employee
{public virtual double CalculateSalary ()
     {return basic_sal;}
}
class SalesEmployee:Employee
{ double sales, comm;
  public new double CalculateSalary ()
     {return basic_sal+ (sales * comm) ;}
}
static void Main ()
{ SalesEmployee sper= new SalesEmployee ();
  Double salary= sper.CalculateSalary ();
  Console.WriteLine (salary);
}



```

### Sealed class
Sealed class cannot be inherited

```
sealed class SinglyList
{
  public virtual double Add () 
  {// code to add a record in the linked list}
}
public class StringSinglyList:SinglyList
{
  public override double Add () 
  {  // code to add a record in the String linked list}
 }


```

### Concrete class vs. abstract classes
Concrete class
      Class describes the functionality of the objects that it can be used to instantiate.
Abstract class
Provides all of the characteristics of a concrete class except that it does not permit object instantiation.
An abstract class can contain abstract and non-abstract methods.
Abstract methods do not have implementation.

```
abstract class Employee
 {  public virtual double CalculateSalary();
     { return basic +hra + da ;}
    public abstract double CalculateBonus();
  }

 class Manager: Employee
  {   public override double CalculateSalary();
       { return basic + hra + da + allowances ;}
      public override double CalaculateBonus ()
       { return basic_sal * 0.20 ;}
  }

static void Main ()
{ 	Manager mgr=new Manager ();
  	double bonus=mgr. CalaculateBonus ();
  	double Salary=mgr. CalculateSalary ();
}



```


### Object class

Base class for all .NET classes

Object class methods

o	public bool Equals(object)
o	protected void Finalize()
o	public int GetHashCode()
o	public System.Type GetType()
o	protected object MemberwiseClone()
o	public string ToString()

Polymorphism using Object

Polymorphism using Object

The ability to perform an operation on an object without knowing the precise type of the object.

```
void Display (object o) 
{
Console.WriteLine (o.ToString ());
}
public static void Main ()
{  Display (34); 
   Display (“Transflower”);
   Display (4.453655);
   Display (new Employee (“Ravi”, “Tambade”);
}
```


### Interface Inheritance

For loosely coupled highly cohesive mechanism in Application.
An interface defines a Contract
Text Editor uses Spellchecker as interfaces.
EnglishSpellChecker and FrenchSpellChecker are implementing contract defined by SpellChecker interface.

```
interface ISpellChecker
{ ArrayList CheckSpelling (string word) ;}
class EnglishSpellChecker:ISpellChecker
{
  ArrayList CheckSpelling (string word) 
   {// return possible spelling suggestions}
}
class FrenchSpellChecker:ISpellChecker
{ 
  ArrayList CheckSpelling (string word) 
   {// return possible spelling suggestions}
}
class TextEditor
{
 public static void Main()
 {    ISpellChecker checker= new EnglishSpellChecker ();
ArrayList words=checker. CheckSpelling (“Flower”);
…
 }
}



```


### Explicit Interface Inheritance
```
interface IOrderDetails    { void ShowDetails() ;}
interface ICustomerDetails { void ShowDetails() ;}
class Transaction: IOrderDetails, ICustomerDetils
{
void IOrderDetails. ShowDetails()
{  // implementation for interface IOrderDetails ;}
void ICustomerDetails. ShowDetails()
{  // implementation for interface IOrderDetails ;}
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

## Abstract class vs. Interface





## Building cloned Objects

```
class StackClass: ICloneable
 {  int size; int [] sArr;
    public StackClass (int s) { size=s; sArr= new int [size]; }
    public object Clone()
    {  StackClass s = new StackClass(this.size);
this.sArr.CopyTo(s.sArr, 0);
return s;
    }
 }
public static void Main()
{ StackClass stack1 = new StackClass (4);
  Stack1 [0] = 89;
  …..
 StackClass stack2 = (StackClass) stack1.Clone ();
}


```


## Reflection

Reflection is the ability to examine the metadata in the assembly manifest at runtime.
Reflection is useful in following situations:
•	Need to access attributes in your programs metadata.
•	To examine and instantiate types in an assembly.
•	To build new types at runtime using classes in System.Reflection.Emit namespace.
•	To perform late binding, accessing methods on types created at runtime.

System. Type class
Type class provides access to metadata of any .NET Type.

System. Reflection namespace
Contains classes and interfaces that provide a managed view of loaded types, methods and fields
These types provide ability to dynamically create and invoke types.

## MethodInfo method;
string methodName;
object result = new object ();
object [] args = new object [] {1, 2};
Assembly asm = Assembly.LoadFile (@”c:/transflowerLib.dll”);
Type [] types= asm.GetTypes();
foreach (Type t in types)
{     method = t.GetMethod(methodName);
string typeName= t.FullName;
object obj= asm.CreateInstance(typeName);
 result = t.InvokeMember (method.Name, BindingFlags.Public |    
 BindingFlags.InvokeMethod | BindingFlags.Instance, null, obj, args);
break;
}string res = result.ToString();
Console.WriteLine (“Result is: {0}”, res);

