# Unsafe Code & Pointers in C#

In normal C# development, **we live inside a safe world**. But sometimes, as engineers, we need to:

* Talk directly to memory
* Improve performance
* Interact with low-level systems (C/C++, drivers, embedded systems)

That’s where **unsafe code + pointers** come in.

Think of it like this:
# Unsafe Code & Pointers in C# – A Transflower Mentor Explanation

> *"Imagine you've spent years driving a modern car with automatic transmission, power steering, and safety sensors. One day, your mentor opens the car's hood and says, 'Today, you will understand how the engine really works.' That's exactly what happens when we enter the world of **unsafe code** in C#. We move beyond the comfort of managed code and begin working directly with computer memory."*


# The Safe World of C#

One of the greatest strengths of C# and the .NET platform is **memory safety**. Normally, the .NET runtime takes care of:

* Allocating memory
* Releasing unused memory
* Preventing invalid memory access
* Protecting applications from crashes caused by memory corruption

As developers, we simply write:

```csharp
int number = 10;
number = 20;
```

We focus on solving business problems rather than worrying about where variables are stored in memory.

# But Why Learn Unsafe Code?

As software engineers, there are situations where we need more control. Suppose you are building:

* A game engine
* An image processing application
* A hardware driver
* A robotics controller
* A networking library
* A C# application that communicates with a C/C++ library

In these cases, every millisecond matters. Sometimes we need to work directly with memory addresses instead of relying on managed references.

That's where **unsafe code** becomes useful.

# Safe World vs Unsafe World

```text
           SAFE C#
-----------------------------------
Variables
Objects
References
Garbage Collector
Memory Protection
Automatic Management
-----------------------------------
```

```text
          UNSAFE C#
-----------------------------------
Pointers
Memory Addresses
Direct RAM Access
Manual Control
Maximum Performance
-----------------------------------
```

Think of it this way:

> **Safe C# is like driving a car with automatic transmission.**

Everything is handled for you.

Whereas...

> **Unsafe C# is like driving a Formula One race car.**

You gain incredible control, but you must know exactly what you're doing.

# Creating the Project

Create a new console application.

```bash
dotnet new console -n UnsafePointerDemo
cd UnsafePointerDemo
```

Simple.

Nothing special yet.

# Enabling Unsafe Code

By default, C# prevents pointer operations. If you try writing pointer code immediately, the compiler will complain. Open the project file.

```xml
<PropertyGroup>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
</PropertyGroup>
```

This tells the compiler:

> **"I understand the risks. Allow unsafe operations."**

Without this setting:

```text
Compilation Failed
Unsafe code may only appear if compiling with /unsafe
```

# Understanding the Four Important Symbols

Before writing any code, understand these symbols.

| Symbol            | Meaning                                 | Easy Way to Remember          |
| ----------------- | --------------------------------------- | ----------------------------- |
| `unsafe`          | Allows pointer operations               | Unlocks low-level programming |
| `&`               | Returns the address of a variable       | "Where is it?"                |
| `*` (declaration) | Declares a pointer                      | "This stores an address."     |
| `*` (dereference) | Accesses the value stored at an address | "Go to this address."         |

These four ideas form the foundation of pointer programming.

# Our First Unsafe Program

```csharp
using System;

class Program
{
    static unsafe void Main(string[] args)
    {
        int number = 10;

        Console.WriteLine("Before Modification");
        Console.WriteLine("Value: " + number);

        Console.WriteLine("Address: " + (IntPtr)(&number));

        int* ptr = &number;

        *ptr = 50;

        Console.WriteLine("\nAfter Modification");
        Console.WriteLine("Value: " + number);
        Console.WriteLine("Value via Pointer: " + *ptr);

        Console.WriteLine("\nLearning Complete");
    }
}
```

# Understanding the Program Step by Step

## Step 1 – Creating a Variable

```csharp
int number = 10;
```

Imagine memory like rows of houses.

```text
Address        Value
---------------------
1000            10
```

The variable lives somewhere inside RAM.

## Step 2 – Getting Its Address

```csharp
&number
```

The ampersand (`&`) asks:

> **"Where does this variable live?"**

Suppose the answer is:

```text
1000
```

Now we know the location.

## Step 3 – Creating a Pointer

```csharp
int* ptr = &number;
```

Now:

```text
ptr
 │
 ▼
1000
```

The pointer doesn't store **10**.

It stores the **address** where **10** lives.

Think of it like writing someone's home address rather than carrying the person with you.

## Step 4 – Reading Through the Pointer

```csharp
*ptr
```

The star (`*`) means:

> **"Go to the stored address and read the value."**

```text
ptr
 │
 ▼
1000
 │
 ▼
10
```

The result is:

```text
10
```

## Step 5 – Writing Through the Pointer

```csharp
*ptr = 50;
```

Now we're not changing the pointer.

We're changing the memory.

```text
Before

Address     Value
1000        10
```

After

```text
Address     Value
1000        50
```

The original variable automatically changes because both refer to the same memory location.

# Visualizing the Entire Process

```text
number
   │
   ▼
+----------------+
| Address:1000   |
| Value:10       |
+----------------+

&
↓
1000
↓
ptr
 │
 ▼

+----------------+
| Address:1000   |
| Value:10       |
+----------------+

↓
*ptr = 50
↓
+----------------+
| Address:1000   |
| Value:50       |
+----------------+
↓
number == 50
```

# Expected Output

```text
Before Modification
Value: 10
Address: 0000007FFB9AF218

After Modification
Value: 50
Value via Pointer: 50

Learning Complete
```

The memory address will vary every time you run the program.

# Pointer vs Variable

| Variable       | Pointer               |
| -------------- | --------------------- |
| Stores a value | Stores an address     |
| Easy to use    | Requires care         |
| Safe           | Unsafe                |
| Managed by CLR | Managed by programmer |

Think of it like this:

```text
Variable
↓
"I know the value."
Pointer
↓
"I know where the value lives."
```

# Why Do Performance Engineers Love Pointers?

Imagine processing a 500 MB image. Without pointers:

```text
Read Pixel
↓
Copy Pixel
↓
Modify Pixel
↓
Store Pixel
```

With pointers:

```text
Go directly to memory
↓
Modify pixel
↓
Done
```

Fewer copies.Less overhead.Higher performance.

# Real-World Applications

## Game Engines

Every frame updates:

* Player position
* Physics
* Graphics
* Audio

Pointers reduce overhead.


## Image Processing
Millions of pixels are modified every second.Direct memory access is significantly faster.


## Video Processing
Applications like video editors manipulate large buffers directly.



## Networking
High-performance servers often use pointers to process incoming packets efficiently.


## Interoperability (Interop)

Many native libraries expect memory addresses instead of managed references.Examples include:

* Windows APIs
* Camera SDKs
* Printer SDKs
* Robotics libraries
* Scientific instruments


# When Should You Avoid Unsafe Code?

Business applications rarely benefit from pointer programming. Examples include:

* ASP.NET Core Web APIs
* MVC Applications
* Banking Software
* Insurance Systems
* ERP Applications
* CRM Systems

The CLR already manages memory efficiently.

Using unsafe code unnecessarily increases complexity and the risk of bugs.

# Mentor Insight

> **Imagine a librarian managing thousands of books. Normally, you ask the librarian for a book, and they safely bring it to you. That's managed C#. But in unsafe code, you're given the keys to the entire library. You can go directly to any shelf and pick any book yourself. It's faster, but if you're careless, you may disturb the entire arrangement.**
# Summary

| Concept  | Description                                       |
| -------- | ------------------------------------------------- |
| `unsafe` | Enables pointer programming                       |
| `&`      | Retrieves a variable's memory address             |
| `int*`   | Declares a pointer to an integer                  |
| `*ptr`   | Reads or modifies the value at the stored address |
| Pointer  | Stores an address, not a value                    |
| Memory   | The actual location where data resides            |
| CLR      | Normally manages memory safely                    |

# Final Takeaway

Understanding pointers isn't about writing everyday business applications differently—it's about understanding **how computers really work**.

Your learning journey looks like this:

```text
Variables
      ↓
References
      ↓
Managed Memory
      ↓
unsafe
      ↓
Pointers
      ↓
Direct Memory Access
      ↓
System-Level Programming
```

> **As a mentor, I tell every aspiring software engineer this:** *"You may never need pointers in most enterprise applications, but learning them deepens your understanding of memory, the CLR, and computer architecture. It's this knowledge that transforms a programmer into a well-rounded software engineer."*

#  Step 1: Create Your Console Application

We start simple.

```bash
dotnet new console -n UnsafePointerDemo
cd UnsafePointerDemo
```

#  Step 2: Enable “Unsafe Mode”

By default, C# protects you.We must explicitly tell it:

👉 “I know what I am doing. Allow me to access memory.”

Open `.csproj` file:

```xml
<PropertyGroup>
  <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
</PropertyGroup>
```

 Mentor Note:
Without this, pointer code will NOT compile.


# Step 3: Understanding the Idea Before Code

We will work with:

| Concept  | Meaning                         |
| -------- | ------------------------------- |
| `&`      | Get memory address              |
| `*`      | Access value at address         |
| `int*`   | Pointer to integer              |
| `unsafe` | Block that allows pointer usage |

#  Step 4: First Unsafe Program (Hands-on Learning)

Now let’s write real code.

```csharp
using System;

class Program
{
    static unsafe void Main(string[] args)
    {
        int number = 10;

        Console.WriteLine(" Before Modification");
        Console.WriteLine("Value: " + number);

        // Getting memory address
        Console.WriteLine("Address: " + (IntPtr)(&number));

        // Creating pointer
        int* ptr = &number;

        // Modify value using pointer
        *ptr = 50;

        Console.WriteLine("\n After Modification via Pointer");
        Console.WriteLine("Value: " + number);
        Console.WriteLine("Value via pointer: " + *ptr);

        Console.WriteLine("\n Learning Complete");
    }
}
```

# Step 5: What Actually Happened?

Let’s break it like a mentor:

### 1. Variable in memory

```
number = 10
```

Stored somewhere in RAM.


### 2. We took its address

```csharp
&number
```

Now we know WHERE it lives.

### 3. Pointer created

```csharp
int* ptr = &number;
```

👉 ptr now “points” to number


### 4. Direct memory update

```csharp
*ptr = 50;
```

We didn’t use variable name. We changed memory directly.


# Step 6: Key Learning Insight

> Pointer is not about variables.
> Pointer is about memory control.

# Step 7: When should you use this?

As a mentor, I want you to be clear:

### ✔ Use unsafe when:

* Working with performance-critical systems
* Interfacing with C/C++ libraries
* Graphics / game engines
* Embedded / hardware-level programming

### ❌ Avoid when:

* Business applications
* Web APIs
* CRUD applications


#  Final Mentor Message

In C#:

> You don’t need pointers for 95% of applications
> But understanding them makes you a **complete software engineer**