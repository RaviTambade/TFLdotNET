## C# as .NET Programming Language

- C ++ Heritage : 	Namespaces, Pointers (in unsafe code),	Unsigned types, etc.
- Increased Productivity : 	Short Learning curve
- C# is a type safe Object Oriented Programming Language
- C# is case sensitive
- Interoperability

## Structure of C# Program


```
using System;
// A “Hello World!” program in C#
public class HelloWorld
{ public static void Main ()
  {
            Console.WriteLine (“Hello, World”);

  }
}

```

## Passing Command Line Arguments


```
using System;

/* Invoke exe using console */

public class HelloWorld
{
          public static void Main (string [] args)
          {
                Console.WriteLine (“parameter count = {0}”, args.Length);
                Console.WriteLine (“Hello {0}”, args [0]);
                Console.ReadLine ();

          }
}



```


## Execution of .NET Application

- C# code is compiled by CSC.exe (C# compiler) into assembly as Managed code.
- Managed code is targeted to Common Language Runtime of .NET Framework
- Common Language Runtime converts MSIL code into Platform dependent executable code (native code) for targeted operating System.
- Application is executed by Operating System as Process.


## C# Types

- A C# Program is a collection of types
	Structure, Classes, Enumerations, Interfaces, Delegates, Events
- C# provides a set of predefined types
	e.g. int, byte, char, string, object, etc.
Custom types can be created.
- All data and code is defined within a type.
	No global variables, no global function.


- Types can be instantiated and used by
	Calling methods, setters and getters, etc.
- Types can be converted from one type to another.
- Types are organized into namespaces, files, and assemblies.
- Types are arranged in hierarchy.

In .NET Types are of two categories

- Value Type
	
Directly contain data on Stack.	

	Primitives: 		int num; float speed;
	Enumerations: 	enum State {off, on}
	Structures: 		struct Point {int x, y ;}


- Reference Types

Contain reference to actual instance on managed Heap.

	Root		Object
	String		string
	Classes	class Line: Shape{ }
	Interfaces	interface IDrawble {….}
	Arrays		string [] names = new string[10];
	Delegates	delegate void operation ();
 


## Type Conversion
- Implicit Conversion
	No information loss
	Occur automatically
- Explicit Conversion
	Require a cast
	May not succeed
	Information (precision) might be lost


```
    int x=543454;
    long y=x;				//implicit conversion
    short z=(short)x;  		//explicit conversion
    double d=1.3454545345;
    float f= (float) d;		//explicit conversion
    long l= (long) d		// explicit conversion

```

## Constants and read only variables
```

// This example illustrates the use of constant data and readonly fields.

using System;
using System.Text;

namespace ConstData
{
 
  class MyMathClass
  {
    public static readonly double PI;
    static MyMathClass()
    { PI = 3.14; }
  }
 
  class Program
  {
    static void Main(string [] args)
    {
      Console.WriteLine ("***** Fun with Const *****\n");
      Console.WriteLine ("The value of PI is: {0}", MyMathClass.PI);

      // Error! Can't change a constant!
      // MyMathClass.PI = 3.1444;

      LocalConstStringVariable ();
    }

    static void LocalConstStringVariable()
    {
      	// A local constant data point.
      	const string fixedStr = "Fixed string Data";
      	Console.WriteLine(fixedStr);

      	// Error!
      	//fixedStr = "This will not work!";
    }
  }
}


```

## Enumerations

Enumerations are user defined data Type which consist of a set of named integer constants.
enum Weekdays { Mon, Tue, Wed, Thu, Fri, Sat}

Each member starts from zero by default and is incremented by 1 for each next member.
Using Enumeration Types
```
Weekdays day=Weekdays.Mon;
Console.WriteLine(“{0}”, day);    //Displays Mon

```

## Structures
```
public struct Point
{	public int x;
    public int y;
}



```

## Arrays

Declare
	int [] marks;
Allocate
	int [] marks= new int [9];
Initialize
	int [] marks=new int [] {1, 2, 3, 4, 5, 6, 7, 9};
	int [] marks={1,2,3,4,5,6,7,8,9};
Access and assign
	Marks2[i] = marks[i];
Enumerate
	foreach (int i in marks) {Console.WriteLine (i); }

## Params Keyword
It defines a method that can accept a variable number of arguments.
```
static void ViewNames (params string [] names)
{
 	Console.WriteLine (“Names: {0}, {1}, {2}”, 
                    names [0], names [1], names [2]);
}

public static void Main (string [] args)
{     
  	ViewNames(“Nitin”, “Nilesh”, “Shrinivas”);
}


```


## ref and out parameters

```
void static Swap (ref int n1, ref int n2)
{
int temp =n1; n1=n2; n2=temp;
}

void static Calculate (float radius, out float area, out float circum)
{
	Area=3.14f * radius * radius;
	Circum=2*3.14f * radius;
}

public static void Main ()
{
    int x=10, y=20;
    Swap (ref x, ref y);
    float area, circum;
    Calculate (5, out area, out circum);
}


```

##Exeuction Process of .net Application

<img src="/images/CSharp/Execution.jpg"/>