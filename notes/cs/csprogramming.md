
## C# Memory Management, Arrays, Methods and Object References

**Don't just learn C# syntax. Understand how data is represented, passed, stored and processed during program execution.**

This session continues from the previous learning material, Your First C# Program – Looking Behind the Curtain.Our objective is to develop a deeper understanding of C# types, memory behavior, arrays, method parameters and object references before moving into advanced OOP and .NET application development.

## 1. Let's begin with a question

**Mentor**: Dear students, consider this statement.

```
int age = 25;
```

What exactly happens when this statement executes?

**Student**: An integer variable called age is created and 25 is assigned to it.

**Mentor**: Correct. But I want you to think one level deeper.

- What is a variable?
- What is a data type?
- Where is the value stored?
- How does the runtime understand this value?
- What happens when we pass this variable to a method?

These questions take us towards understanding memory management.

## 2. Understanding Value Types and Reference Types

Let's compare two examples.

Example A – Value Type

```
int x = 100;
int y = x;
y = 200;
Console.WriteLine(x);
Console.WriteLine(y);
```

Output:

```
100
200
```

Why?  Because assigning `x` to `y` copies the integer value.

```
       x                 y
   +-------+         +-------+
   |  100  |         |  100  |
   +-------+         +-------+
                         |
                     y = 200
                         |
                         ▼
                     +-------+
                     |  200  |
                     +-------+
```

Changing `y` does not change `x`.

Example B – Reference Type

```
int[] a = { 10, 20, 30 };
int[] b = a;
b[0] = 999;
Console.WriteLine(a[0]);
```

Output:

```
999
```

Why? Because both variables refer to the same array object.

```
       a                 b
       |                 |
       +--------+--------+
                |
                ▼
        +------------------+
        | 10 | 20 | 30     |
        +------------------+
                |
           b[0] = 999
                |
                ▼
        +------------------+
        | 999 | 20 | 30    |
        +------------------+
```

**Mentor Insight**: Assignment of a reference type copies the reference, not the underlying object.

**Note**: Value types are not necessarily always stored on the stack, and reference types are not simply synonymous with heap variables. Their storage depends on execution context.

## 3. Understanding the `new` Keyword

Consider:

```
Student student = new Student();
```

Let's break this statement down.

```
Student
   |
   ▼
Data type

student
   |
   ▼
Reference variable

new
   |
   ▼
Object creation

Student()
   |
   ▼
Constructor invocation
```

Conceptually:

```
       Stack / Execution Context
       -------------------------
             student
                |
                | reference
                ▼
       Managed Object Storage
       -------------------------
           Student Object
          +--------------+
          | Name         |
          | Age          |
          | Email        |
          +--------------+
```

The diagram is conceptual; actual storage and object layout are managed by the .NET runtime.

## 4. Arrays – Working with Multiple Values

**Mentor**: Suppose we want to maintain marks of 100 students. Should we create 100 individual variables? No!  We use arrays.

```
int[] marks = { 90, 85, 70, 95, 80 };
```

Array visualization:

```
          marks
            |
            ▼
     +------+------+------+------+------+
     |  90  |  85  |  70  |  95  |  80  |
     +------+------+------+------+------+
        0      1      2      3      4
```

Accessing array elements:

```
Console.WriteLine(marks[0]);
Console.WriteLine(marks[2]);
```

Output:

```
90
70
```

### Practical exercise – Calculate total marks

```
int[] marks = { 90, 85, 70, 95, 80 };

int total = 0;

foreach (int mark in marks)
{
    total += mark;
}

Console.WriteLine($"Total Marks: {total}");
```

Output:

```
Total Marks: 420
```

## 5. Methods – Understanding Parameter Passing

Now let's understand something important. Consider:

```
static void ChangeValue(int number)
{
    number = 500;
}

int x = 100;
ChangeValue(x);
Console.WriteLine(x);
```

Output:

```
100
```

Why?  Because C# passes the value of the integer argument by value by default.

```
Main()
  |
  | x = 100
  |
  ▼
ChangeValue(number)
  |
  | number = 100 (copy)
  |
  ▼
number = 500
```

The original variable remains unchanged.

### What happens with reference types?

```
static void ChangeArray(int[] values)
{
    values[0] = 999;
}

int[] numbers = { 10, 20, 30 };
ChangeArray(numbers);
Console.WriteLine(numbers[0]);
```

Output:

```
999
```

Why? The reference is copied into the parameter, but both references point to the same array. An important distinction:

```
static void ReplaceArray(int[] values)
{
    values = new int[] { 100, 200, 300 };
}
```

This does not replace the caller's array reference. To replace the caller's reference, you need `ref`.

## 6. Understanding `ref` and `out`

Let's revisit these keywords from the previous lesson.

Using ref:

```
static void Update(ref int number)
{
    number = 500;
}

int x = 100;
Update(ref x);
Console.WriteLine(x);
```

Output:

```
500
```

Using out:

```
static void Calculate( int a, int b, out int sum)
{
    sum = a + b;
}

Calculate(10, 20, out int result);
Console.WriteLine(result);
```

Output:

```
30
```

The distinction:

| Concept                  | Meaning                                        |
| ------------------------ | ---------------------------------------------- |
| Value parameter          | Receives a copy                                |
| Reference-type parameter | Receives a copy of the object reference        |
| `ref`                    | Passes the caller's variable by reference      |
| `out`                    | Produces a value through a reference parameter |

## 7. Practical Lab – Student Result Processing

Let's combine today's concepts. Problem statement: Create a C# console application to process student examination results.

Requirements:

1. Accept student name.
2. Accept marks in five subjects.
3. Store marks in an array.
4. Calculate total marks.
5. Calculate percentage.
6. Determine pass/fail status.
7. Display the result.

```
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Student Name: ");
        string name = Console.ReadLine() ?? "";

        int[] marks = new int[5];

        int total = 0;

        for (int i = 0; i < marks.Length; i++)
        {
            Console.Write($"Enter Subject {i + 1} Marks: ");

            if (!int.TryParse(Console.ReadLine(), out marks[i]) || marks[i] < 0 || marks[i] > 100)
            {
                Console.WriteLine("Invalid marks.");
                return;
            }

            total += marks[i];
        }

        double percentage = total / 5.0;

        Console.WriteLine("\n--- Student Result ---");

        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Total: {total}");
        Console.WriteLine($"Percentage: {percentage}%");

        if (Array.Exists(marks, mark => mark < 35))
        {
            Console.WriteLine("Result: Fail");
        }
        else
        {
            Console.WriteLine("Result: Pass");
        }
    }
}
```

## 8. Connect Today's Concepts to .NET Development

Why are we learning these fundamentals? Because the same concepts appear everywhere in enterprise application development.

```
              C# Fundamentals
                     |
          +----------+----------+
          |          |          |
        Types      Arrays     Methods
          |          |          |
          ▼          ▼          ▼
       Objects   Collections  Services
          |          |          |
          +----------+----------+
                     |
                     ▼
              ASP.NET Core
                     |
               Web API / MVC
                     |
                     ▼
              Entity Framework
                     |
                     ▼
                  MySQL
```

For example, an ASP.NET Core API may receive:

```
public class StudentRequest
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int[] Marks { get; set; } = [];
}
```

Here we are applying classes, properties, reference types and arrays to represent real application data.


### 🌸 Ravi Sir's closing message

Dear students, When you write a program, 
- don't just ask what output it produces. 
- Ask what happens inside the machine.
- When you assign a variable, understand copying.
- When you create an object, understand references. 
- When you pass parameters, understand how data travels between methods.
-  When you work with arrays, understand how collections organize data. A programmer writes instructions.

 A .NET developer understands the execution model behind those instructions. That is how we build strong engineering foundations at Transflower.