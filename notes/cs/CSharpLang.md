# Introduction to C# and .NET

> "Good morning, everyone! Today, I want to introduce you to a programming language that’s not just popular but powerful and exciting — **C#**. You might hear it pronounced as 'See Sharp' — like a musical note that’s just a bit higher, sharper, and clearer. And trust me, this language is just as sharp for developers building modern applications.

> Now, imagine you’re building all kinds of software — websites that millions use, mobile apps on phones, or even those games you love playing. C# lets you do all that and more. It’s a language created by Microsoft, designed to work seamlessly with something called the **.NET Framework** — think of .NET as a big toolbox full of all the tools you need to build software quickly and reliably.

> What’s really cool about C# is that it borrows the best ideas from other languages you might have heard of — like **C++**, which is known for speed and power; **Java**, which is famous for being reliable across different systems; and even **VB.NET**, known for its simplicity. But C# doesn’t stop there. It adds its own superpowers — like easy ways to write asynchronous code so your programs can do many things at once without getting stuck, and something called LINQ, which makes working with data smooth and elegant.

> Here’s something you should know about C#: it’s a fully **object-oriented language**. That means it follows four important principles that help keep your code clean, reusable, and easy to maintain. These principles are:

> * **Abstraction:** Like focusing on the controls of a car — you don’t need to know how the engine works, just how to drive.
> * **Encapsulation:** Imagine putting all your car’s parts safely under the hood so no one can mess with them accidentally.
> * **Inheritance:** Like inheriting your family’s car, you get many features already built-in without starting from scratch.
> * **Polymorphism:** The same car can behave differently — sometimes sport mode, sometimes eco mode — all controlled by the driver.

> Now, let me show you a simple program — the classic *Hello, World!* — to give you a taste of C#. It looks like this:

```csharp
using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello, World!");
    }
}
```

> When you run this, it prints ‘Hello, World!’ on the console — that’s the command-line screen where programs talk to you without fancy buttons or graphics. It’s a great place to start because it helps you learn the basics of how code runs.


> Under the hood, here’s what happens: you write your C# code, then a special program called the **C# compiler** turns it into something called Intermediate Language — think of it as a universal language that .NET understands. Then the **Common Language Runtime (CLR)**, which is like the engine room of .NET, takes over and converts it into the actual instructions your computer’s operating system understands. And just like that, your program runs!

> So, why learn C#? Because it’s used everywhere — from web applications to Windows software, from mobile apps to cloud services. Learning C# today opens doors to careers in software development, game design, enterprise solutions, and much more.

> And the best part? As we go deeper into this course, you’ll discover the many features and tricks that make C# a language loved by beginners and experts alike.


> Ready to get started? Let’s dive in and write our first program together!"

 

## Structure of C# Program


```
using System;
// A “Hello World!” program in C#
public class Program
{ 
  public static void Main ()
  {
            Console.WriteLine ("Hello, World");
  }
}
```
In C# Console applicatino Program.cs file consist of Program class with Main as EntryPoint function code with Hello World output.

- A console application is an application that can be run in the command prompt. For any beginner on .NET or anyone who wants to learn C# Language or anyone who wants to become an expert in C# Language, building a console application is ideally the first step to learning the C# Language.
- The Console Applications contain a similar user interface to the Operating systems like MS-DOS, UNIX, etc.
- The Console Application is known as the CUI application because in this application we completely work with the CUI environment.
- These applications are similar to C or C++ applications.
- Console applications do not provide any GUI facilities like the Mouse Pointer, Colors, Buttons, Menu Bars, etc.


## Passing Command Line Arguments
Arguments that are passed by command line known as command line arguments. We can send arguments to the Main method while executing the code. The string args variable contains all the values passed from the command line.

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

<img src="/images/CSharp/Execution.jpg"/>




## C# Types
C# programs consist of one or more files. Each file contains zero or more namespaces. A namespace contains types such as classes, structs, interfaces, enumerations, and delegates, or other namespaces.

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

The process of converting the value of one type (int, float, double, etc.) to another type is known as type conversion.

In C#, there are two basic types of type conversion:

1. Implicit Type Conversions
2. Explicit Type Conversions

```
    int x=543454;
    long y=x;				//implicit conversion
    short z=(short)x;  		//explicit conversion
    double d=1.3454545345;
    float f= (float) d;		//explicit conversion
    long l= (long) d		// explicit conversion

```

### C# Type Conversion using Convert Class
In C#, we can use the Convert class to perform type conversion. The Convert class provides various methods to convert one type to another.

- ToBoolean()	converts a type to a Boolean value
- ToChar()	converts a type to a char type
- ToDouble()	converts a type to a double type
- ToInt16()	converts a type to a 16-bit int type
- ToString()	converts a type to a string


## Constants vs read only
The main difference between const and readonly keywords in C# is that const need to be defined at the time of assignment, while readonly field can be defined at runtime. Const's are implicitly static, while readonly values don't have to be static.
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
Console.WriteLine("{0}", day);    //Displays Mon

```

## Structures
In C#, a structure is a value type data type. It helps you to make a single variable hold related data of various data types. The struct keyword is used for creating a structure. Structures are used to represent a record

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
params is a special keyword that allows passing a variable number of parameters into a method. It allows a nice, flexible syntax that helps us specify: One or multiple parameters separated by commas.


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

- ref 
It is a keyword that we use in the C# language for passing the available arguments by their references. In simpler words, if we make any changes in the given argument, then this method reflects these changes in the variable whenever the control returns to the calling method. The parameter of ref does not at all pass the property.
- out 
It is a keyword that we use in the C# language to pass the available arguments to the methods as a type of reference. We generally use this keyword when any method returns various different values. The parameter of out does not at all pass the property.


```
void static Swap (ref int n1, ref int n2)
{
  int temp =n1; 
  n1=n2; 
  n2=temp;
}

void static Calculate (float radius, out float area, out float circum)
{
	  area=3.14f * radius * radius;
	  circum=2*3.14f * radius;
}

public static void Main ()
{
    int x=10, y=20;
    Swap (ref x, ref y);
    float area, circum;
    Calculate (5, out area, out circum);
}

```

