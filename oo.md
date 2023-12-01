Object Orientation in C#

Object
	A real world entity which has well defined structure and behavior.
	
Characteristics of an Object are:
	State
	Behavior
	Identity
	Responsibility
		
Pillars of Object Orientation
	Abstraction
	Encapsulation
	Inheritance 
	Typing, Concurrency, Hierarchy, Persistence

Abstraction
Getting essential characteristic of a System depending on the perspective of on Observer.
Abstraction is a process of identifying the key aspects of System and ignoring the rest
Only domain expertise can do right abstraction.

Abstraction of Person Object
	Useful for social survey
	Useful for healthcare industry
	Useful for Employment Information

Encapsulation
Hiding complexity of a System.
Encapsulation is a process of compartmentalizing the element of an abstraction that constitute its structure and behavior.
	
	Servers to separate interface of an abstraction and its implementation.
	User is aware only about its interface: any changes to implementation does not affect the user.

Inheritance
Classification helps in handing complexity.
Factoring out common elements in a set of entities into a general entity and then making it more and more specific.
Hierarchy is ranking or ordering of abstraction.
Code and Data Reusability in System using is a relationship.

Typing
Typing is the enforcement of the entity such that objects of different types may not be interchanges, or at the most, they may be interchanged only in restricted ways.

Concurrency
Different objects responding simultaneously.

Persistence
Persistence of an object through which its existence transcends time and or space.


Namespace and Class

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

