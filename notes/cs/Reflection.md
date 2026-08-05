# Reflection in C# – Looking Inside the Soul of Your Application

> **"Good morning, future software engineers! Today, we're going to learn one of the most fascinating features of the .NET platform. Imagine having a magical mirror that doesn't show your face—it shows the internal structure of your software. It tells you every class, every method, every property, every constructor, and even every attribute, without opening the source code. That magical mirror is called Reflection."**



##  The Software Detective

Imagine you are a detective. Not an ordinary detective. A **Software Detective**. One day, your company receives a DLL file.

```
Accounting.dll
```

Your manager says,

> **"Ravi, we lost the source code. We only have this DLL. Can you tell me what classes it contains?"**

Can you? Without Reflection... No. With Reflection... Absolutely! Reflection allows us to inspect everything inside a compiled assembly.

---

##  What Does Reflection Mean?

The word **Reflection** comes from a mirror. A mirror reflects your image. Reflection in .NET reflects your application's structure.

```text
          Mirror

     +-------------+
     |             |
     |    You      |
     |             |
     +-------------+
            ▲
            |
      Reflection

----------------------------

       Application

     +-----------------+
     | Classes         |
     | Methods         |
     | Properties      |
     | Constructors    |
     +-----------------+
            ▲
            |
       Reflection API
```

Instead of seeing your face... Reflection lets your program see itself.

 
##  What is Reflection?

Reflection is the ability of a program to:

* Inspect itself
* Discover metadata
* Create objects dynamically
* Invoke methods dynamically
* Read attributes
* Load assemblies at runtime

Think of it as

> **Runtime Self-Inspection**

 
##  Reflection Works on Metadata

Remember our discussion about **Attributes**? The compiler stores metadata inside every assembly.

```text
Program.cs
      │
Compiler
      │
Produces

MyApplication.dll
      │
Contains Classes, Methods , Properties, Fields, Attributes, Constructors
      │
Reflection Reads Everything
```

Reflection doesn't read source code. It reads compiled metadata.


##  School Student Analogy

Imagine your school maintains student records.

```text
Student
Name
Roll Number
Age
Class
Subjects
Attendance
```

Instead of opening every student's notebook, the Principal simply opens the school database. Similarly,
Reflection reads the metadata database inside the assembly.

 
## Reflection in Action

A very simple example.

```csharp
int number = 42;
Type type = number.GetType();
Console.WriteLine(type);
```

Output

```
System.Int32
```

What happened?

```text
Variable
↓
GetType()
↓
Reflection
↓
Metadata
↓
System.Int32
```

Reflection told us

> "This object belongs to System.Int32."

## Looking Inside a DLL

Suppose another company gives us

```
Accounting.dll
```

We don't have

```
Accounting.cs
```

Can we still inspect it? Yes.

```csharp
Assembly assembly =
Assembly.LoadFile(@"c:\Accounting.dll");
```

Now Reflection has loaded the assembly.
 

## Inside the Assembly

Imagine the DLL as a building.

```text
Accounting.dll

+----------------------+
| Employee             |
| Invoice              |
| Payment              |
| Customer             |
| TaxCalculator        |
+----------------------+
```

Reflection becomes the building inspector. It can open every room.
 

## Type – The Heart of Reflection

The most important class is

```
Type
```

Think of Type as an identity card.

```text
Calculator
↓
Type Object
↓

Knows Methods, Properties, Constructors, Fields, Events, Attributes
```

Everything begins with a Type object.
 

## Creating Objects Dynamically

Normally we write

```csharp
Calculator calculator =
new Calculator();
```

But Reflection allows

```csharp
Activator.CreateInstance(type);
```

Notice something. No

```
new Calculator()
```

Reflection creates it dynamically.

 
# Mentor Analogy

Imagine a hotel. Normally, you know Room 305. You directly walk there. Reflection is different. You ask the receptionist.

> "Give me any available Deluxe Room."

The receptionist decides. Reflection creates objects dynamically.

 
## Reading Properties

Suppose the class contains

```csharp
public double Number
{
    get;
    set;
}
```

Reflection can discover it.

```csharp
PropertyInfo property =
type.GetProperty("Number");
```

Now Reflection knows everything.

```text
Property
↓
Name
↓
Type
↓
Getter
↓
Setter
```


## Setting Property Values

Normally

```csharp
calculator.Number = 10;
```

Reflection

```csharp
property.SetValue(obj,10);
```

No compile-time knowledge required.


## Calling Methods Dynamically

Suppose the DLL contains

```csharp
public double Add(double value)
```

Reflection finds it.

```csharp
MethodInfo method =
type.GetMethod("Add");
```

And executes it.

```csharp
method.Invoke(obj,new object[]{5});
```

Without writing

```csharp
calculator.Add(5);
```

Amazing!


## Reflection Workflow

```text
             DLL
              │
       Load Assembly
              │
              ▼
          Assembly
              │
              ▼
            Type
              │
      +-------+-------+
      |       |       |
      ▼       ▼       ▼
 Properties Methods Constructors
      │       │       │
      ▼       ▼       ▼
 Read   Invoke   Create Objects
```


## Reflection Architecture

```text
               Application
                     │
                     ▼
            System.Reflection
                     │
   +---------+---------+---------+
   ▼         ▼         ▼
Assembly    Type    Activator
                │
        +-------+-------+
        ▼               ▼
PropertyInfo      MethodInfo
        │               │
        ▼               ▼
 Read Property     Invoke Method
```


## Real World Uses

Students often ask,

> **"Sir, where is Reflection actually used?"**

Almost everywhere.


### 1️⃣ Dependency Injection

When ASP.NET Core starts

```text
Program.cs
↓
builder.Services
↓
Reflection Scans
↓
Controllers
↓
Services
↓
Repositories
```

Many DI libraries automatically discover implementations using Reflection.


### 2️⃣ ASP.NET Core Routing

When you write

```csharp
[HttpGet]
```

Reflection scans every controller.

```text
Controller
↓
Reflection
↓
Reads Attributes
↓
Creates Route Table
```


### 3️⃣ Entity Framework Core

Suppose we write

```csharp
public class Product
{
    public int Id

    public string Name
}
```

Entity Framework scans the class. Using Reflection. It automatically creates mappings.
### 4️⃣ Swagger

Swagger doesn't know your APIs. Reflection discovers them.

```text
Controllers
↓
Reflection
↓
Methods
↓
Attributes
↓
Swagger UI
```

Documentation appears automatically.



### 5️⃣ Unit Testing

Frameworks like xUnit search for

```csharp
[Fact]
```

How?,  Reflection.

```text
Assembly
↓
Reflection
↓
Find Methods
↓
Execute Tests
```



# Reflection + Attributes

These two friends always work together.

```text
Developer
↓
Writes

[Authorize]
↓
Compiler Stores Metadata
↓
Reflection Reads Metadata
↓
Framework Applies Security
```

Without Reflection, Attributes are just labels. Reflection gives them life.


## Reflection.Emit

Reflection reads code. Reflection.Emit creates code. Imagine this.

```text
Reflection
↓
Read Existing Classes

----------------------------

Reflection.Emit
↓
Generate New Classes
↓
Generate New Methods
↓
Generate New Assemblies
```

It's like moving from reading books...to writing books.


## Reflection Performance
 
Reflection is powerful. But... It comes with a price.

```text
Normal Method Call
↓
Very Fast
----------------------

Reflection
↓
Search Metadata
↓
Locate Method
↓
Invoke
↓
Slower
```

Therefore, Reflection should not be used unnecessarily inside tight loops.

 
## Reflection Pros and Cons

| Advantages            | Disadvantages             |
| --------------------- | ------------------------- |
| Dynamic Programming   | Slower than direct access |
| Plugin Architecture   | Harder to debug           |
| Reads Metadata        | Complex code              |
| Framework Development | Security concerns         |
| Runtime Discovery     | Higher maintenance        |

 

## Reflection Ecosystem

```text
                 Reflection
                      │
      +---------------+---------------+
      ▼                               ▼
 Read Metadata                 Create Objects
      │                               │
      ▼                               ▼
 Read Attributes              Invoke Methods
      │                               │
      ▼                               ▼
 Discover Types          Build Frameworks
      │
      ▼

ASP.NET Core
Entity Framework
Swagger
xUnit
Dependency Injection
AutoMapper
Plugin Systems
```


## Reflection vs Traditional Programming

```text
Traditional Programming

Known Class
↓
Compile Time
↓
Direct Method Call
↓
Fast


Reflection

Unknown Class
↓
Runtime Discovery
↓
Dynamic Method Call
↓
Flexible
```


## Mentor's Architecture Perspective

As a beginner... Reflection looks like

```csharp
GetType()
GetMethod()
Invoke()
```

As an experienced software architect... Reflection becomes

```text
Runtime Type Discovery
Dynamic Object Creation
Framework Development
Plugin Architecture
Dependency Injection
ORM Mapping
Automatic API Discovery
Metadata Processing
```

Reflection is one of the pillars upon which the .NET ecosystem is built.

## Mentor's Golden Wisdom

> **"Reflection is like giving your application X-ray vision. Instead of seeing the outside of a program, it lets you see its internal bones—its classes, methods, properties, constructors, and attributes. Frameworks like ASP.NET Core, Entity Framework, Swagger, xUnit, and Dependency Injection all rely on this X-ray vision to discover your code and make intelligent decisions at runtime."**


## Final Takeaway

```text
                Source Code
                     │
                 Compiler
                     │
             MyApplication.dll
                     │
       +-----------------------------+
       |         Metadata            |
       |-----------------------------|
       | Classes                     |
       | Interfaces                  |
       | Methods                     |
       | Properties                  |
       | Constructors                |
       | Fields                      |
       | Attributes                  |
       +-----------------------------+
                     ▲
                     │
             System.Reflection
                     │
                     ▼
      Discover → Inspect → Create → Invoke
```

> **"As a Transflower mentor, I always tell my students: Reflection is not something you use every day to write business logic. Instead, it is the foundation that empowers frameworks to write less code for you. Whenever ASP.NET Core automatically discovers your controllers, Entity Framework maps your models, Swagger generates API documentation, or xUnit finds your test methods, remember that an invisible detective named Reflection is working quietly behind the scenes."**