
# Your First C# Program — Looking Behind the Curtain
  Let's continue the **Transflower Learning journey** from basic C# syntax into the runtime, types, arrays, parameters, and the ideas that prepare you for .NET application development.

Dear students, Yesterday we were learning **C# syntax**. Today, let's ask a more important question:**When I write C# code, what actually happens when I press Run?**

A professional developer should not only know:

```csharp
Console.WriteLine("Hello, World!");
```

but should also have a mental model of what happens **behind that line**. Let's begin.


# 👨‍💻 1. Your First C# Program — Hello, World!

Create a simple console application:

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

Output:

```text
Hello, World!
```

It looks very small. But there is a lot happening behind this tiny program.Let's understand it line by line.

---

# 🧩 2. Understanding the Program

## `using System`

```csharp
using System;
```

`System` is a namespace containing many fundamental .NET types.For example:

```csharp
Console
String
DateTime
Math
Object
```

Because we have:

```csharp
using System;
```

we can write:

```csharp
Console.WriteLine();
```

instead of:

```csharp
System.Console.WriteLine();
```

Think of `using` as telling the compiler: "I will be using types from this namespace frequently."

---

# 🏠 3. The `Program` Class

```csharp
public class Program
{
}
```

We are defining a class called:

```text
Program
```

Remember our previous lesson: **A class is a blueprint.** Here the class acts as the container for our application entry point.


# 🚪 4. The `Main()` Method

Inside the class:

```csharp
public static void Main()
{
    Console.WriteLine("Hello, World!");
}
```

The traditional console application's entry point is:

```text
Main()
```

Think of it as the **front door of the application**. When the application starts, execution begins from its entry point. You may also see:

```csharp
public static void Main(string[] args)
{
}
```

We'll understand `args` shortly.

---

# 🧠 Mentor Question

You might remember our earlier discussion about Java: Why is Java's `main()` static? C# has a similar concept.

```csharp
public static void Main()
```

The `static` keyword means the method belongs to the type rather than to an instance of that type.So the runtime doesn't need to create a `Program` object just to locate the traditional entry point.


# ⚙️ 5. What Happens Behind the Curtain?

Now let's look at the journey:

```text
C# Source Code
      │
      ▼
C# Compiler
      │
      ▼
.NET Assembly
      │
      ▼
IL / CIL
      │
      ▼
CLR
      │
      ▼
JIT Compiler
      │
      ▼
Native Machine Code
      │
      ▼
CPU
```

This is the mental model I want you to carry as a .NET developer.


# 🏗️ 6. C# Compiler

Historically, you may hear about:

```text
csc.exe
```

which is the C# compiler. Modern .NET development normally happens through the **.NET SDK and `dotnet` CLI**, which invokes the appropriate compiler/build tooling. For example:

```bash
dotnet new console
```

creates a console application. Then:

```bash
dotnet build
```

builds it.

And:

```bash
dotnet run
```

runs it.

So instead of focusing only on the old `csc.exe` terminology, understand the modern developer workflow:

```text
C# Source
    ↓
dotnet build
    ↓
Compiler
    ↓
Assembly
```



# 🧠 7. What Does the Compiler Produce?

The C# compiler does not normally translate your source code directly into CPU instructions. It produces an assembly containing:

```text
IL / CIL
Metadata
Other assembly information
```

IL means:

> **Intermediate Language**

You may also hear:

```text
CIL
MSIL
IL
```

in .NET discussions. Think of IL as a language understood by the .NET execution environment.

---

# ⚙️ 8. Enter the CLR

Now comes:

```text
CLR
```

The **Common Language Runtime**.

The CLR provides the execution environment for managed .NET applications. It is responsible for many important runtime services, including:

```text
Memory management
Garbage collection
Exception handling
Type safety
Threading/runtime services
JIT compilation
```

So don't think:

> "CLR is simply another compiler."

Instead:

> **CLR is the runtime environment in which managed .NET code executes.**



# 🚀 9. JIT Compilation

At runtime, .NET uses a **JIT compiler** to compile IL into native machine code suitable for execution. Conceptually:

```text
IL
 ↓
JIT
 ↓
Native Machine Code
 ↓
CPU
```

This is why the simplified statement:  "C# gets converted directly into machine code" 
is not the best mental model.

A better model is:

```text
C#
 ↓
IL
 ↓
Runtime/JIT
 ↓
Native code
```


# 💻 10. What Is a Console Application?

Now let's come back to something practical.What is a console application?Think about the old:

```text
DOS Prompt
```

or today's:

```text
Command Prompt
PowerShell
Terminal
```

A console application communicates primarily through text. For example:

```csharp
Console.WriteLine("Enter your name:");

string name = Console.ReadLine();

Console.WriteLine($"Welcome {name}");
```

You type:

```text
Ravi
```

and the application responds:

```text
Welcome Ravi
```

 

# 🌱 Why Do We Start With Console Applications?

Because I don't want you to worry about:

```text
HTML
CSS
Browser
Buttons
HTTP
Controllers
Database
Authentication
```

yet.

I want you to focus on:

```text
Problem
  ↓
Logic
  ↓
Algorithm
  ↓
C# Code
  ↓
Output
```

This creates strong programming fundamentals. Later the same logic will move into:

```text
ASP.NET Core
Web API
MVC
Worker Services
Microservices
Cloud applications
```


# 🧭 11. Command-Line Arguments

Now let's make our program slightly more interesting.

```csharp
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Parameter count = {args.Length}");

        if (args.Length > 0)
        {
            Console.WriteLine($"Hello {args[0]}");
        }
    }
}
```

Suppose we run:

```bash
HelloWorld.exe Ravi
```

The application receives:

```text
args[0] = "Ravi"
```

Therefore:

```text
Hello Ravi
```


# 🧠 Mentor Insight — What Is `args`?

Look carefully:

```csharp
string[] args
```

This means:

```text
string[]
   ↓
Array of strings

args
   ↓
Variable name
```

So command-line arguments are simply a collection of strings passed to the application. For example:

```bash
MyApp Ravi Pune 25
```

could result conceptually in:

```text
args[0] → Ravi
args[1] → Pune
args[2] → 25
```

Notice something interesting:

Even `25` arrives as text:

```text
"25"
```

If you need an integer, you must convert it.

 
# 🧱 12. Understanding C# Types

Now we arrive at one of the most important topics in C#:

> **Types**

C# is a strongly typed language.

When you write:

```csharp
int age = 25;
```

you are telling the compiler:

> "Age is an integer."

Similarly:

```csharp
string name = "Ravi";
```

means:

> "Name is a string."

 

# 🔹 13. Value Types

Common value types include:

```text
int
long
float
double
decimal
bool
char
struct
enum
```

For example:

```csharp
int age = 25;
double salary = 50000.50;
bool active = true;
char grade = 'A';
```

Value types conceptually represent the value directly. But students, be careful with a common oversimplification: "All value types are always stored on the stack." That's not universally true. Where a value lives depends on its context. For example, a value-type field inside an object is part of that object and therefore lives as part of the object's storage. So remember the stronger concept:

> **Value type vs reference type describes the semantics of how values are represented and accessed; it is not simply a rule that says stack vs heap.**

# 🔹 14. Reference Types

Common reference types include:

```text
class
interface
array
delegate
string
```

For example:

```csharp
string name = "Ravi";

int[] marks = { 90, 80, 70 };
```

A reference-type variable normally holds a reference to an object.

Conceptually:

```text
marks
  │
  ▼
┌───────────────┐
│ 90 │ 80 │ 70  │
└───────────────┘
```

This distinction becomes extremely important when we study:

```text
Classes
Objects
Inheritance
Garbage Collection
Collections
Dependency Injection
Entity Framework Core
```


# 🧩 15. `struct` and `enum`

C# also allows you to create your own value types.

### Enum

```csharp
enum Weekday
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
```

Now:

```csharp
Weekday today = Weekday.Monday;
```

Instead of remembering magic numbers:

```text
1
2
3
```

we use meaningful names.


# 📍 16. Struct

A `struct` is a user-defined value type.

```csharp
struct Point
{
    public int X;
    public int Y;
}
```

We can use it:

```csharp
Point p;

p.X = 10;
p.Y = 20;
```

Structs are useful for small values that naturally behave as values. Examples in .NET include types such as:

```text
DateTime
TimeSpan
Guid
```

which are structs.


# 🔄 17. Type Conversion

Suppose:

```csharp
int x = 100;
long y = x;
```

This is an **implicit conversion**. Why? Because an `int` can safely fit within a `long`. Conceptually:

```text
int
 ↓
long
```


# ⚠️ 18. Explicit Conversion

Now:

```csharp
double d = 3.14;

float f = (float)d;
```

We explicitly tell C#: "I know I am converting this value."

The syntax:

```csharp
(float)d
```

is called a **cast**.

Be careful:

```text
double
  ↓
float
```

can potentially lose precision. So casting isn't merely syntax—it is a decision.


# 🔢 19. String to Number Conversion

Suppose:

```csharp
string value = "123";
```
 
This is text. We cannot simply treat it as an integer. We can convert it:

```csharp
int number = Convert.ToInt32(value);
```

Now:

```text
"123"
  ↓
Convert
  ↓
123
```

Another common approach is:

```csharp
int number = int.Parse(value);
```

And when input may be invalid, safer APIs such as:

```csharp
int.TryParse(value, out int number);
```

are often preferable.

---

# 🛡️ 20. `TryParse` — A Professional Habit

Suppose the user enters:

```text
hello
```

and you execute:

```csharp
int.Parse("hello");
```

an exception occurs.

Instead:

```csharp
string input = Console.ReadLine();

if (int.TryParse(input, out int age))
{
    Console.WriteLine($"Age = {age}");
}
else
{
    Console.WriteLine("Please enter a valid number.");
}
```

Now the application handles invalid input gracefully. This is an important transition:  **Beginner code assumes input is correct. Professional code validates assumptions.** ---

# 🔐 21. `const` vs `readonly`

Now let's discuss something students often confuse.

### `const`

```csharp
const int MaxAttempts = 3;
```

A constant cannot be reassigned. It represents a compile-time constant. Think:

```text
Maximum attempts allowed
= 3
```

and that value is part of the program's fixed definition.

---

# 🔒 22. `readonly`

Now:

```csharp
readonly int id;
```

A readonly field cannot be reassigned after its initialization is complete. For instance:

```csharp
public class Customer
{
    private readonly int id;

    public Customer(int id)
    {
        this.id = id;
    }
}
```

Here the ID can be supplied at runtime:

```csharp
Customer customer = new Customer(101);
```

but the field cannot later be reassigned. So remember:

```text
const
 ↓
Compile-time constant

readonly
 ↓
Can be initialized using runtime information,
but cannot later be reassigned
```


# 🧭 23. Arrays

Now let's store multiple values.

```csharp
int[] marks = new int[] { 90, 80, 70 };
```

Conceptually:

```text
marks
  │
  ▼
┌────┬────┬────┐
│ 90 │ 80 │ 70 │
└────┴────┴────┘
  0    1    2
```

We can access:

```csharp
Console.WriteLine(marks[0]);
```

Output:

```text
90
```

Remember:

> **Array indexing starts from zero.**


# 🔁 24. `foreach`

Instead of manually accessing every element:

```csharp
foreach (int mark in marks)
{
    Console.WriteLine(mark);
}
```

Output:

```text
90
80
70
```

This is one of the most readable ways to iterate over a collection.

 
# 📦 25. `params`

Suppose we want a method that accepts any number of names.

```csharp
static void ViewNames(params string[] names)
{
    foreach (string name in names)
    {
        Console.WriteLine(name);
    }
}
```

Now we can call:

```csharp
ViewNames("Ravi", "Pragati", "Sahil");
```

or:

```csharp
ViewNames("Ravi", "Pragati");
```

The method can accept a variable number of arguments.

Think:

```text
params
  ↓
"Give me zero or more arguments of this type."
```


# 🔄 26. `ref` Parameters

Now something more interesting. Consider:

```csharp
static void Swap(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}
```

Call it:

```csharp
int x = 10;
int y = 20;

Swap(ref x, ref y);
```

After the call:

```text
x = 20
y = 10
```

Why?

Because `ref` allows the method to operate on the caller's variable rather than receiving only a copy of its value. Notice:

```csharp
Swap(ref x, ref y);
```

The caller must also explicitly use:

```text
ref
```

---

# 📤 27. `out` Parameters

Now consider:

```csharp
static void Calculate(
    double radius,
    out double area,
    out double perimeter)
{
    area = Math.PI * radius * radius;
    perimeter = 2 * Math.PI * radius;
}
```

We can call:

```csharp
Calculate(5, out double area, out double perimeter);

Console.WriteLine(area);
Console.WriteLine(perimeter);
```

The `out` parameters allow a method to produce additional results.

The important rule is:

> **An `out` parameter must be assigned by the method before the method returns.**


# 🧠 28. `ref` vs `out`
 
Students often memorize these. Instead, understand the intention:

| `ref`                                       | `out`                                  |
| ------------------------------------------- | -------------------------------------- |
| Caller passes an existing variable          | Method can produce the value           |
| Variable must normally be initialized first | Variable need not be initialized first |
| Method can read and modify it               | Method must assign it before returning |

For example:

```csharp
int x = 10;
Modify(ref x);
```

versus:

```csharp
Calculate(5, out double area);
```


# 🌸 29. A Small C# Exercise

Now let's combine today's concepts. Build a console application that asks for:

```text
First Name
Last Name
Age
Basic Salary
```

Then calculate:

```text
Gross Salary
```

For example:

```text
First Name: Ravi
Last Name: Tambade
Age: 25
Basic Salary: 50000
```

Output:

```text
Name: Ravi Tambade
Age: 25
Basic Salary: 50000
```

Don't worry about making it perfect. The purpose is to practice:

```text
Variables
Input
Output
Conversion
Methods
Conditions
```

---

# 🚀 30. Where Are We Going?

Dear students, look at the journey. We started with:

```csharp
Console.WriteLine("Hello, World!");
```

Then we discovered:

```text
C# Source Code
      ↓
Compiler
      ↓
IL
      ↓
CLR
      ↓
JIT
      ↓
Machine Code
```

Then:

```text
Variables
   ↓
Types
   ↓
Conversion
   ↓
Constants
   ↓
Arrays
   ↓
Methods
   ↓
ref / out
```

And now we are ready for the next major step:

```text
                 C#
                  │
          ┌───────┴────────┐
          ▼                ▼
      Language           OOP
       Features            │
          │                ▼
          │        Classes & Objects
          │                │
          └────────┬───────┘
                   ▼
                .NET
                   │
                   ▼
            Application Design
                   │
                   ▼
             ASP.NET Core
```


# 🌱 Transflower Mentor Advice

Don't rush through these concepts. When you learn:

```csharp
int x = 10;
```

ask:

> What is `int`?

When you learn:

```csharp
Student s = new Student();
```

ask:

> What is `Student`?
> What is `s`?
> What does `new` do?
> Where is the object?
> What does the reference represent?

When you learn:

```csharp
ref int x
```

ask:

> What exactly is being passed to the method?

When you learn:

```csharp
string[] args
```

ask:

> Why is it an array?
> Where did those values come from?

This habit of asking **"what is really happening?"** will take you much further than memorizing syntax.

> 🌸 **Syntax makes you a C# programmer. Understanding execution, types, objects, and responsibilities is what starts making you a .NET developer.** 