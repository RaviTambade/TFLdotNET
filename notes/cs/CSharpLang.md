### **The Journey Begins with C#**

"Today, I’ll be opening the gates to a world where logic meets creativity — welcome to the world of **C#** and **.NET**.

You know, the first time I heard the word *'See Sharp'*, I smiled. It sounded like music — and that’s not a coincidence. Because just like music can be soft, powerful, emotional, or energetic, C# allows us to build applications that *respond, react, and perform* — like a finely tuned instrument in the hands of a good developer."

### 🧰 **What Is C#? And What’s This .NET Thing?**

"Think of C# as your *language*, and .NET as your *playground*. C# was created by Microsoft — to make it easier for people like us to build software that's robust, fast, and scalable.

.NET is not just a framework — it's like a **magic toolbox**. Inside, you’ll find ready-made tools for building web apps, desktop apps, mobile apps, and even cloud-based services. With C# and .NET together, you can go from *idea* to *execution* — with elegance."




# 🌸 Transflower Mentor: Introduction to C# Syntax

Dear students,

If you are coming from **C, C++, Java, or JavaScript**, C# will look familiar.

That is good news.

You don't have to learn programming again.

You are learning a **new language for expressing the same fundamental programming ideas**:

```text
Variables
   ↓
Data Types
   ↓
Operators
   ↓
Conditions
   ↓
Loops
   ↓
Methods
   ↓
Classes
   ↓
Objects
   ↓
Collections
   ↓
.NET Applications
```

So today, let's learn **C# syntax by understanding what the code is trying to say**, rather than memorizing punctuation.

---

# 1️⃣ Your First C# Program

Let's start with the simplest program.

```csharp
Console.WriteLine("Hello, C#");
```

Output:

```text
Hello, C#
```

That's it.

In modern C#, you don't necessarily need to write:

```csharp
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, C#");
    }
}
```

Modern .NET supports **top-level statements**.

So beginners can start with:

```csharp
Console.WriteLine("Hello, C#");
```

Later, when learning classes and application entry points, we can understand what the compiler generates around this code.

---

# 2️⃣ C# Is Case-Sensitive

This is very important.

These are different identifiers:

```csharp
int age;
int Age;
int AGE;
```

C# treats them as three different names.

Similarly:

```csharp
Console.WriteLine();
```

is correct.

But:

```csharp
console.writeline();
```

is not.

So remember:

> 🌱 **C# is case-sensitive.**

---

# 3️⃣ Statements End With `;`

Most C# statements end with a semicolon.

```csharp
int age = 25;

Console.WriteLine(age);

age = 30;
```

Think of `;` as saying:

> **"The instruction ends here."**

For example:

```csharp
int age = 25;
```

means:

> Create an integer variable called `age` and initialize it with `25`.

---

# 4️⃣ Variables — Give Data a Name

Suppose I want to store a student's age.

```csharp
int age = 22;
```

Break it down:

```text
int
 ↓
Data type

age
 ↓
Variable name

=
 ↓
Assignment

22
 ↓
Value
```

So:

```csharp
int age = 22;
```

means:

> "Create an integer variable called `age` and put 22 into it."

---

# 5️⃣ Common C# Data Types

C# provides many built-in data types.

### Integer

```csharp
int age = 25;
```

### Decimal number

```csharp
double salary = 45000.50;
```

### More precise decimal calculations

```csharp
decimal premium = 12500.75m;
```

Notice the `m`.

For financial applications, `decimal` is commonly preferred for monetary calculations.

### Character

```csharp
char grade = 'A';
```

Notice:

```text
'A'
```

uses **single quotes**.

---

# 6️⃣ String

For text:

```csharp
string name = "Ravi";
```

Notice:

```text
"Ravi"
```

uses double quotes.

So:

```csharp
char letter = 'A';

string name = "Ravi";
```

Remember:

```text
'A'      → char
"Ravi"   → string
```

---

# 7️⃣ Boolean

For true/false values:

```csharp
bool isActive = true;
```

or:

```csharp
bool isLoggedIn = false;
```

This becomes extremely important when writing conditions.

---

# 8️⃣ Let's Create a Small Student Program

```csharp
string name = "Pragati";
int age = 21;
double percentage = 82.5;
bool isPlaced = false;

Console.WriteLine(name);
Console.WriteLine(age);
Console.WriteLine(percentage);
Console.WriteLine(isPlaced);
```

Output:

```text
Pragati
21
82.5
False
```

Now we are beginning to represent a real-world entity using variables.

---

# 9️⃣ String Interpolation — The C# Way

Suppose we want:

```text
Pragati is 21 years old.
```

We can write:

```csharp
string name = "Pragati";
int age = 21;

Console.WriteLine($"{name} is {age} years old.");
```

The `$` tells C#:

> "I want to embed variables inside this string."

For example:

```csharp
Console.WriteLine($"Name: {name}, Age: {age}");
```

Output:

```text
Name: Pragati, Age: 21
```

This is called **string interpolation**.

---

# 🔟 Constants

Sometimes a value should not change.

For example:

```csharp
const double PI = 3.14159;
```

Now:

```csharp
PI = 4.5;
```

is not allowed.

Think:

```text
variable
   ↓
value can change

const
   ↓
value cannot be reassigned
```

Example:

```csharp
const int MaxAttempts = 3;
```

---

# 1️⃣1️⃣ Operators

C# provides the familiar arithmetic operators.

```csharp
int a = 10;
int b = 3;

Console.WriteLine(a + b);
Console.WriteLine(a - b);
Console.WriteLine(a * b);
Console.WriteLine(a / b);
Console.WriteLine(a % b);
```

Output:

```text
13
7
30
3
1
```

Notice:

```csharp
10 / 3
```

with two integers produces:

```text
3
```

not:

```text
3.3333
```

If you want fractional division, use an appropriate floating-point or decimal type.

---

# 1️⃣2️⃣ Comparison Operators

We often need to compare values.

```csharp
int age = 22;

Console.WriteLine(age > 18);
Console.WriteLine(age == 22);
Console.WriteLine(age != 30);
```

The result is `true` or `false`.

Common comparison operators:

```text
>       greater than
<       less than
>=      greater than or equal
<=      less than or equal
==      equal
!=      not equal
```

Remember:

```csharp
=
```

means assignment.

While:

```csharp
==
```

means comparison.

This is a very common beginner mistake.

---

# 1️⃣3️⃣ Logical Operators

Now suppose:

> Student must be older than 18 **AND** percentage must be greater than 60.

We can write:

```csharp
int age = 21;
double percentage = 75;

bool eligible = age >= 18 && percentage >= 60;
```

Important operators:

```text
&&    AND
||    OR
!     NOT
```

Example:

```csharp
bool hasDegree = true;
bool hasExperience = false;

bool eligible = hasDegree || hasExperience;
```

---

# 1️⃣4️⃣ `if` Statement

Now we can make decisions.

```csharp
int age = 21;

if (age >= 18)
{
    Console.WriteLine("Eligible");
}
```

Think of it as:

```text
Condition
   ↓
Is age >= 18?
   ↓
 YES
   ↓
Execute block
```

---

# 1️⃣5️⃣ `if-else`

```csharp
int age = 16;

if (age >= 18)
{
    Console.WriteLine("Adult");
}
else
{
    Console.WriteLine("Minor");
}
```

This is the basic decision-making structure.

---

# 1️⃣6️⃣ Multiple Conditions

```csharp
int marks = 75;

if (marks >= 90)
{
    Console.WriteLine("Excellent");
}
else if (marks >= 75)
{
    Console.WriteLine("Very Good");
}
else if (marks >= 60)
{
    Console.WriteLine("Good");
}
else
{
    Console.WriteLine("Needs Improvement");
}
```

The flow is:

```text
marks
  ↓
>= 90 ?
  ↓ No
>= 75 ?
  ↓ Yes
Very Good
```

---

# 1️⃣7️⃣ `switch`

When we are choosing among multiple discrete options, `switch` can make the code cleaner.

```csharp
int day = 2;

switch (day)
{
    case 1:
        Console.WriteLine("Monday");
        break;

    case 2:
        Console.WriteLine("Tuesday");
        break;

    default:
        Console.WriteLine("Unknown day");
        break;
}
```

Modern C# also provides powerful **switch expressions**, which we can learn later.

---

# 1️⃣8️⃣ Loops — Repeat Work

Suppose I want to print numbers from 1 to 5.

### `for`

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
```

Output:

```text
1
2
3
4
5
```

Think:

```text
initialize
    ↓
condition
    ↓
execute
    ↓
increment
    ↓
condition
    ↓
...
```

---

# 1️⃣9️⃣ `while`

```csharp
int i = 1;

while (i <= 5)
{
    Console.WriteLine(i);
    i++;
}
```

Use `while` when the repetition is naturally expressed as:

> "Keep doing this while the condition is true."

---

# 2️⃣0️⃣ `foreach`

When working with collections, `foreach` is extremely useful.

```csharp
string[] names =
{
    "Ravi",
    "Pragati",
    "Sahil"
};

foreach (string name in names)
{
    Console.WriteLine(name);
}
```

Output:

```text
Ravi
Pragati
Sahil
```

The mental model is:

```text
Collection
    ↓
Take one item
    ↓
Process it
    ↓
Take next item
    ↓
...
```

---

# 2️⃣1️⃣ Methods — Give Your Code a Responsibility

Suppose we need to calculate the square of a number.

Instead of writing the logic everywhere:

```csharp
int result = 10 * 10;
```

we can create a method:

```csharp
static int Square(int number)
{
    return number * number;
}
```

Then:

```csharp
int result = Square(10);

Console.WriteLine(result);
```

Output:

```text
100
```

A method allows us to give a **name to a piece of behavior**.

---

# 2️⃣2️⃣ Method Anatomy

Look at:

```csharp
static int Square(int number)
{
    return number * number;
}
```

Break it down:

```text
static
  ↓
Modifier

int
  ↓
Return type

Square
  ↓
Method name

(int number)
  ↓
Parameter

return number * number;
  ↓
Result
```

This syntax will become very important when we start building classes.

---

# 2️⃣3️⃣ Creating a Class

Now we move toward OOP.

```csharp
public class Student
{
    public string Name;
    public int Age;
}
```

We have created a class called:

```text
Student
```

Think:

```text
Student
│
├── Name
└── Age
```

---

# 2️⃣4️⃣ Creating an Object

Now:

```csharp
Student student = new Student();
```

This creates an object.

Then:

```csharp
student.Name = "Ravi";
student.Age = 25;
```

We can access the object's members.

Conceptually:

```text
Student Class
      │
      ▼
 new Student()
      │
      ▼
Student Object
      │
 ┌────┴────┐
 ▼         ▼
Name      Age
Ravi       25
```

---

# 2️⃣5️⃣ Constructor

Instead of creating an empty object and assigning values separately:

```csharp
Student student = new Student();

student.Name = "Ravi";
student.Age = 25;
```

we can create a constructor:

```csharp
public class Student
{
    public string Name;
    public int Age;

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

Then:

```csharp
Student student = new Student("Ravi", 25);
```

Now the object is created with meaningful state.

---

# 2️⃣6️⃣ Properties — Important C# Syntax

In modern C#, instead of exposing fields directly:

```csharp
public string Name;
```

we commonly use properties:

```csharp
public string Name { get; set; }
public int Age { get; set; }
```

For example:

```csharp
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

Then:

```csharp
Student student = new Student();

student.Name = "Ravi";
student.Age = 25;
```

Properties are an important part of idiomatic C#.

---

# 2️⃣7️⃣ Nullable Reference Types

Modern C# also encourages developers to think about whether a reference can be `null`.

For example:

```csharp
string name = "Ravi";
```

means `name` is expected to contain a non-null string when nullable reference types are enabled.

If null is intentionally allowed:

```csharp
string? name = null;
```

This becomes particularly important in:

```text
ASP.NET Core
Entity Framework Core
DTOs
API request models
Database applications
```

---

# 2️⃣8️⃣ Namespaces

As applications grow, we need to organize classes.

For example:

```csharp
namespace TFL.Insurance.Models
{
    public class Customer
    {
    }
}
```

The namespace helps organize types and avoid naming conflicts.

You may later see:

```csharp
using TFL.Insurance.Models;
```

This allows you to refer to types from that namespace more conveniently.

Modern C# also supports **file-scoped namespaces**:

```csharp
namespace TFL.Insurance.Models;

public class Customer
{
}
```

---

# 2️⃣9️⃣ A Small Complete C# Program

Let's combine what we've learned.

```csharp
using System;

namespace TFLDemo;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Display()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
    }
}

Student student = new Student("Ravi", 25);

student.Display();
```

Output:

```text
Name: Ravi
Age: 25
```

Now look at how many concepts we have already used:

```text
using
namespace
class
property
constructor
object
method
string
int
string interpolation
```

And this is still a tiny application.

---

# 🧠 The C# Syntax Map

Dear students, don't memorize C# syntax randomly.

Build a mental map:

```text
                    C#
                     │
        ┌────────────┼────────────┐
        │            │            │
      Data         Logic         OOP
        │            │            │
   ┌────┴────┐   ┌───┴────┐   ┌───┴─────┐
   │         │   │        │   │         │
 int      string if      loops class    object
 double   bool   switch   for   method   constructor
 decimal  char            while property interface
```

Then these language features become building blocks rather than isolated syntax rules.

---

# 🌱 From C# Syntax to .NET Development

This is where your journey gets interesting.

You start with:

```text
C# Syntax
   ↓
Variables
   ↓
Conditions
   ↓
Loops
   ↓
Methods
   ↓
Classes & Objects
   ↓
OOP
   ↓
Collections
   ↓
Exception Handling
   ↓
Generics
   ↓
LINQ
   ↓
Async/Await
   ↓
.NET
   ↓
ASP.NET Core
   ↓
Web API
   ↓
Entity Framework Core
   ↓
Cloud / Microservices
```

So don't think:

> "I'm just learning C# syntax."

Think:

> **"I'm learning the language that I will use to build .NET software."**

---

# 🌸 Mentor's Final Advice

Dear students,

When learning C#, don't try to memorize hundreds of keywords.

Instead, repeatedly ask:

### What data do I have?

```csharp
int age = 25;
```

### What decision do I need to make?

```csharp
if (age >= 18)
```

### What work needs to be repeated?

```csharp
for (...)
```

### What behavior deserves a name?

```csharp
CalculatePremium()
```

### What real-world thing am I modeling?

```csharp
Customer
Policy
Claim
Product
Order
```

### What responsibility should this object own?

That final question is where **syntax becomes software design**.

> 🌱 **Learn the syntax. Understand the meaning. Practice the patterns. Then use those patterns to solve real business problems.**

That is the Transflower way of learning C#.

