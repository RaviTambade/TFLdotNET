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

## Exeuction Process of .net Application

<img src="/images/CSharp/Execution.jpg"/>



## .NET Core Concpets


### Common Language Runtime (CLR): 

CLR is the heart of the .NET framework and it does 4 primary important things:
1.	Garbage collection
2.	CAS (Code Access Security)
3.	CV (Code Verification)
4.	IL to Native translation.

### Common Type System (CTS): - 
CTS ensure that data types defined in two different languages get compiled to a common data type. This is useful because there may be situations when we want code in one language to be called in other language. We can see practical demonstration of CTS by creating same application in C# and VB.Net and then compare the IL code of both applications. Here the data type of both IL code is same.
 
### Common Language Specification (CLS):
CLS is a subset of CTS. CLS is a set of rules or guidelines. When any programming language adheres to these set of rules it can be consumed by any .Net language.
e.g. 	Lang must be object oriented, each object must be allocated on heap,
               Exception handling supported. 
Also each data type of the language should be converted into CLR understandable types by the Lang compiler. 

All types understandable by CLR forms CTS (common type system) which includes: 
System.Byte, System.Int16, System.UInt16, 
System.Int32, System.UInt32, System.Int64,         
System.UInt64, System.Char, System.Boolean, etc.

### Assembly Loader:

When a .NET application runs, CLR starts to bind with the version of an assembly that the application was built with. It uses the following steps to resolve an assembly reference:
1)   Examine the Configuration Files
2)   Check for Previously Referenced Assemblies
3)   Check the Global Assembly Cache
4)   Locate the Assembly through Codebases or Probing


### MSIL Code Verification and Just In Time compilation (JIT)

When .NET code is compiled, the output it produces is an intermediate language (MSIL) code that requires a runtime to execute the IL. So during assembly load at runtime, it first validates the metadata then examines the IL code against this metadata to see that the code is type safe. When MSIL code is executed, it is compiled Just-in-Time and converted into a platform-specific code that's called native code.
Thus any code that can be converted to native code is valid code. Code that can't be converted to native code due to unrecognized instructions is called Invalid code. During JIT compilation the code is verified to see if it is type-safe or not.

### Garbage Collection
 “Garbage” consists of objects created during a program’s execution on the managed heap that are no longer accessible by the program. Their memory can be reclaimed and reused with no adverse effects.
The garbage collector is a mechanism which identifies garbage on the managed heap and makes its memory available for reuse. This eliminates the need for the programmer to manually delete objects which are no longer required for program execution. This reuse of memory helps reduce the amount of total memory that a program needs to run.

