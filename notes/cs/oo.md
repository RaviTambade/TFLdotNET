# C# Properties, Access Modifiers, Static Members and Object Collaboration

**Concepts + Coding + Design**

**Learning Journey**: From Individual Objects → Controlled Access → Shared Members → Collaborating Objects

## 1. Connect with the previous session

Dear students, In our previous session, we created a `Date` class and explored the fundamental concepts of Object-Oriented Programming.

We understood:

- Class and Object
- State and Behavior
- Fields and Methods
- Constructors
- `this` keyword
- Encapsulation
- Domain Modeling

We learned that a class is a blueprint and an object is an instance of that blueprint.

Today, we are going one step further.

**Our challenge**: Can multiple objects collaborate to solve a real-world business problem?

Imagine Transflower is building a Student Management System. We have 100 students, multiple courses, mentors and assessments. Each student has:

- Student ID
- Name
- Marks
- Course

Let's think like software engineers.

Mentor asks:

1. Should every part of our application directly modify student marks?
2. Should every student maintain a separate copy of the total student count?
3. How can a Student object communicate with a Course object?
4. How can we protect internal data?
5. How can we organize responsibilities across multiple classes?

Today, we will answer these questions using C#.

## 2. Understanding Properties – Controlled Access to Object State

Let's revisit our previous implementation. We created a field:

```
public class Student
{
    private int marks;
}
```

**Mentor asks**: If `marks` is private, how can another class read its value?

**Student**: Sir, we need some mechanism to access the data without exposing the field directly.

Exactly! That mechanism is called a Property.

### What is a property?

A property provides a controlled way to read or modify the state of an object.

```
public class Student
{
    public int StudentId { get; private set; }

    public string Name { get; private set; }

    public int Marks { get; private set; }

    public Student(int id, string name, int marks)
    {
        StudentId = id;
        Name = name;
        Marks = marks;
    }
}
```

Let's understand:

```
public int Marks { get; private set; }
```

ASCII visualization:

```
                  Student Object
                        |
                        |
                  Marks Property
                        |
                 +------+------+
                 |             |
                 v             v
                get           set
                 |             |
                 v             v
            Public Read    Private Write
                 |             |
                 v             v
          External Code    Student Class
```

Outside code can read:

```
Console.WriteLine(student.Marks);
```

But cannot directly modify:

```
student.Marks = 95;  // Compile-time error
```

Why?

Because the setter is private.

### Mentor–Student conversation

**Mentor**: Why should we restrict direct modification of marks?

**Student**: Sir, otherwise any part of the application could change student marks without validation.

**Mentor**: Correct! Imagine an examination system where marks can be modified by any user. What would happen to the integrity of examination results?

**Student**: The system would become unreliable.

**Mentor**: Exactly. Encapsulation is not just hiding data. It is protecting business rules.

## 3. Properties with Validation – Protecting Business Rules

Suppose marks must always be between 0 and 100. Can we allow this?

```
student.Marks = 150;
```

Obviously not! We need controlled modification.

```
public class Student
{
    public int Marks { get; private set; }

    public void UpdateMarks(int marks)
    {
        if (marks < 0 || marks > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(marks),
                "Marks must be between 0 and 100."
            );
        }

        Marks = marks;
    }
}
```

Execution flow:

```
       External Application
                |
                v
          UpdateMarks()
                |
                v
         Validate Marks
                |
          +-----+-----+
          |           |
          v           v
        Valid       Invalid
          |           |
          v           v
      Update State   Reject
          |           |
          v           v
       Success      Exception
```

Usage:

```
student.UpdateMarks(85);

Console.WriteLine(student.Marks);
```

Output:

```
85
```

**Mentor's observation**:

A property provides **controlled access**.

A method provides an opportunity to execute business logic before changing the state.

### Important distinction

| Property                 | Method                              |
| ------------------------ | ----------------------------------- |
| Represents object data   | Represents an operation             |
| Used to read/write state | Used to perform behavior            |
| Usually concise access   | Can contain validation and workflow |
| Example: `Marks`         | Example: `UpdateMarks()`            |

## 4. Understanding Access Modifiers – Who Can Access What?

Imagine a professional company building. Not everyone can enter every room.

```
              COMPANY BUILDING
                     |
        +------------+------------+
        |            |            |
        v            v            v
    Reception    Employee     Manager
                   Area         Office
        |            |            |
        +------------+------------+
                     |
                     v
               Private Locker
```

Similarly, C# provides access modifiers to control the visibility of classes and their members.

### Access modifier comparison

| Modifier             | Accessibility                              |
| -------------------- | ------------------------------------------ |
| `public`             | Wherever the containing type is accessible |
| `private`            | Within the containing type                 |
| `protected`          | Within the type and derived types          |
| `internal`           | Within the same assembly                   |
| `protected internal` | Same assembly OR derived types             |
| `private protected`  | Same assembly AND derived types            |

For today's practical exercises, concentrate on four important modifiers.

```
public
  |
  +-- Accessible to permitted external callers

private
  |
  +-- Accessible inside current type

protected
  |
  +-- Accessible inside current type
  +-- Accessible inside derived types

internal
  |
  +-- Accessible within same assembly
```

### Practical example

```
public class Employee
{
    public int Id;
    private decimal salary;
    protected string department;
    internal string OfficeLocation;
}
```

Now create a derived class.

```
public class Manager : Employee
{
    public void DisplayDepartment()
    {
        Console.WriteLine(department);
    }
}
```

The Manager can access `department` because it is protected. But it cannot directly access `salary`.

### Mentor question

**Mentor**: Why shouldn't salary be publicly accessible?

**Student**: Because salary is sensitive business information.

**Mentor**: Correct. But remember, a private field alone is not a complete security system. Real applications also need authentication, authorization and proper data access controls.

## 5. Static Members – Data Shared Across Objects

Now let's consider another real-world problem. Transflower has 100 students. We want to maintain the total number of Student objects created. Should every student maintain a separate copy of the total count? Think about it.

```
Student 1 → TotalStudents = 1

Student 2 → TotalStudents = 2

Student 3 → TotalStudents = 3
```

This is not what we want. The count belongs to the Student class, not to an individual student. This is where `static` becomes useful.

### Static property

```
public class Student
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public static int TotalStudents { get; private set; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;

        TotalStudents++;
    }
}
```

Create objects:

```
Student s1 = new Student(101, "Amit");
Student s2 = new Student(102, "Sneha");
Student s3 = new Student(103, "Rahul");

Console.WriteLine(Student.TotalStudents);
```

Output:

```
3
```

### Visualize static versus instance data

```
                STUDENT CLASS
              +------------------+
              |                  |
              | static           |
              | TotalStudents=3  |
              |                  |
              +--------+---------+
                       |
           Shared class-level data
                       |
          +------------+------------+
          |            |            |
          v            v            v
      +--------+   +--------+   +--------+
      |   s1   |   |   s2   |   |   s3   |
      +--------+   +--------+   +--------+
      | Id=101 |   | Id=102 |   | Id=103 |
      | Amit   |   | Sneha  |   | Rahul  |
      +--------+   +--------+   +--------+
```

Notice:

- Every Student object has its own ID.
- Every Student object has its own Name.
- All Student objects share the same static TotalStudents property.

Mentor explains:

Instance data belongs to an object. 

Static data belongs to the type.

**One technical point**: this counter represents the number of objects constructed during the lifetime of the process, not necessarily the number of currently enrolled students. For a production system, student enrollment counts should normally come from persistent application data.

## 6. Instance Members vs Static Members

This distinction is fundamental.

| Instance Member                   | Static Member                    |
| --------------------------------- | -------------------------------- |
| Belongs to an object              | Belongs to a type                |
| Each object has its own state     | Shared at type level             |
| Accessed through object reference | Accessed through type name       |
| Requires an object                | Does not require an object       |
| Example: `student.Name`           | Example: `Student.TotalStudents` |

Example:

```
Student s1 = new Student(101, "Amit");

Console.WriteLine(s1.Name);

Console.WriteLine(Student.TotalStudents);
```

Visualize:

```
          Student Class
                |
        +-------+-------+
        |               |
        v               v
   Static Member   Instance Members
        |               |
        v               v
 TotalStudents      Student Object
                         |
                    +----+----+
                    |         |
                    v         v
                   Id       Name
```

## 7. Static Methods – Behavior Without Object State

Consider a Calculator. Do we need to create a Calculator object just to perform addition?

Not necessarily.

```
public class Calculator
{
    public static int Add(int a, int b)
    {
        return a + b;
    }
}
```

Usage:

```
int result = Calculator.Add(10, 20);

Console.WriteLine(result);
```

Output:

```
30
```

Why static?

Because addition does not require the state of a particular Calculator object.

### Static method execution

```
       Calculator Class
              |
              v
         Static Add()
              |
          +---+---+
          |       |
          v       v
        int a   int b
          |       |
          +---+---+
              |
              v
          a + b
              |
              v
          Return 30
```

Important engineering rule:

A static method cannot directly access instance members because it does not automatically have an instance reference.

For example:

```
public class Student
{
    public string Name { get; set; }

    public static void Display()
    {
        // Console.WriteLine(Name); // Compile-time error
    }
}
```

It can access instance members if an object is explicitly passed to it.

## 8. Object Collaboration – The Real Power of OOP

Now we arrive at the most important concept of today's session. Real-world applications are not built using only one class. Consider our Student Management System.

```
          Student Management System
                     |
        +------------+------------+
        |            |            |
        v            v            v
      Student      Course       Mentor
        |            |            |
        v            v            v
    Assessment   Enrollment     Guidance
        |
        v
      Result
```

Each class has a specific responsibility. But how do they communicate? Through object collaboration.

### Step 1: Create Course class

```
public class Course
{
    public int CourseId { get; private set; }

    public string Title { get; private set; }

    public Course(int id, string title)
    {
        CourseId = id;
        Title = title;
    }

    public void Display()
    {
        Console.WriteLine($"Course: {Title}");
    }
}
```

### Step 2: Create Student class

```
public class Student
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Enroll(Course course)
    {
        Console.WriteLine( $"{Name} enrolled in {course.Title}" );
    }
}
```

### Step 3: Make objects collaborate

```
Course course = new Course(1,"AI Enabled Fullstack Development");
Student student = new Student(101, "Amit");
student.Enroll(course);
```

Output:

```
Amit enrolled in AI Enabled Fullstack Development
```

### Understand the collaboration

```
       PROGRAM.CS
           |
     +-----+------+
     |            |
     v            v
 Student Object  Course Object
     |            |
     |            |
     |            +----------------+
     |                             |
     v                             v
 Enroll(course) ------------> Course.Title
     |
     v
 Display Enrollment Message
```

The Student object receives a reference to a Course object and uses its public interface.This is object collaboration.

**Mentor asks**: Does the Student class need to know how a Course is internally implemented?

**Student**: No, sir. It only needs to know what functionality the Course exposes.

**Mentor**: Excellent! That is the beginning of programming against abstractions and designing loosely coupled systems.

## 9. Go Deeper – Object Relationships

When two objects collaborate, they may have different types of relationships. Let's understand three common relationships.

### A. Association

A Student interacts with a Mentor.

```
+-----------+          +-----------+
|  Student  |----------|  Mentor   |
+-----------+          +-----------+
       |
       | communicates with
       |
```

Both objects can exist independently.

### B. Aggregation

A Department has multiple Teachers.

```
+------------+
| Department |
+------------+
      |
      | has
      |
  +---+---+
  |       |
  v       v
Teacher Teacher
```

The Teachers can exist independently of the Department.

### C. Composition

An Order contains OrderItems that are managed as part of that order.

```
+-------------+
|    Order    |
+-------------+
       |
       | owns
       v
+-------------+
| OrderItems  |
+-------------+
```

The Order owns the lifecycle of its items in the domain model.

**Mentor insight**: Object collaboration is not merely calling methods. It is understanding relationships, ownership, responsibilities and dependencies.

## 10. Complete Execution Flow

Let's connect all the concepts.

```
                   Program.cs
                       |
                       v
                Create Course
                       |
                       v
                 Course Object
                       |
                       |
                Create Students
                       |
              +--------+--------+
              |        |        |
              v        v        v
             s1       s2       s3
              |        |        |
              +--------+--------+
                       |
                       v
                student.Enroll()
                       |
                       v
                 Course Object
                       |
                       v
                Read Course.Title
                       |
                       v
                Display Result
                       |
                       v
              Student.TotalStudents
```

We have now connected:

- Classes
- Objects
- Properties
- Access modifiers
- Static members
- Instance members
- Methods
- Object references
- Object collaboration

This is the foundation of object-oriented application design.

## 11. Practical Lab – Student and Course Management

Hands-on C# Assignment

**Problem statement** : Build a console-based Student Course Management application.

### Project structure

```
StudentCourseApp/
    |
    |-- Program.cs
    |
    |-- Models/
    |      |
    |      |-- Student.cs
    |      |-- Course.cs
    |
    |-- Services/
           |
           |-- EnrollmentService.cs
```

### Student class

Responsibilities:

- Store Student ID.
- Store Student Name.
- Maintain TotalStudents using static property.
- Provide Display().
- Support controlled marks updates.

### Course class

Responsibilities:

- Store CourseId.
- Store Title.
- Store Duration.
- Provide Display().

### EnrollmentService class

Responsibilities:

- Accept Student and Course objects.
- Perform enrollment validation.
- Display enrollment details.

### Program.cs

Responsibilities:

- Create two Course objects.
- Create three Student objects.
- Enroll students into courses.
- Display student details.
- Display total student objects created.

### Expected output

```
==================================
     TRANSFLOWER STUDENT SYSTEM
==================================

Student: Amit
Course : C# Programming

Student: Sneha
Course : ASP.NET Core

Student: Rahul
Course : C# Programming

----------------------------------
Total Students Created: 3
----------------------------------
```

### Suggested implementation challenge

Ask students to implement the following method:

```
public void Enroll(Student student, Course course)
{
    // Validate student
    // Validate course
    // Perform enrollment
    // Display confirmation
}
```

Think about where the enrollment behavior belongs. Should it be inside Student? Course? Or a separate EnrollmentService?

There can be different valid designs depending on business requirements. For this lab, use `EnrollmentService` to explore separation of responsibilities.

## 12. Debugging and Engineering Experiments

Ask students to perform these experiments.

| Experiment                           | What to observe                              |
| ------------------------------------ | -------------------------------------------- |
| Change `private set` to `public set` | Anyone can modify the property               |
| Change `private` to `protected`      | Derived classes gain access                  |
| Change `static` to instance          | Each object gets its own counter             |
| Call static method using object      | Compiler warning for static access style     |
| Remove Course reference              | Student can no longer use that Course object |
| Pass `null` as Course                | Understand null validation                   |

For the last experiment, improve the enrollment method:

```
public void Enroll(Course course)
{
    ArgumentNullException.ThrowIfNull(course);

    Console.WriteLine($"{Name} enrolled in {course.Title}");
}
```

**Mentor's lesson**: Good developers don't just write code that works. They experiment with code to understand why it works.

  

## 13. Connect Today's OOP Learning with ASP.NET Core

Now let's connect classroom concepts with enterprise development. Suppose tomorrow you develop an ASP.NET Core Web API.

You may encounter:

```
             ASP.NET Core Web API
                      |
                      v
                Controller
                      |
                      v
                 Service
                      |
                      v
                 Repository
                      |
                      v
                  Database
```

Look carefully. These are not just files or classes.They are objects collaborating at runtime.

For example:

- Controller receives HTTP requests.
- Service implements business rules.
- Repository manages data access.
- Database stores persistent information.

Properties, access modifiers, interfaces, constructors and object references form the foundation of this architecture.

Later, when we learn Dependency Injection, we will understand how ASP.NET Core creates and connects these collaborating objects.

Today's OOP concepts are tomorrow's enterprise architecture fundamentals.

## 15. Ravi Sir's Closing Thought

Dear students,

A beginner writes classes.

An intermediate developer creates objects.

An experienced developer understands how objects collaborate.

An architect understands how collaborating objects form a maintainable system.

When you design a real application, don't ask only:

- How many classes should I create?
- How many methods should I write?

Instead, ask:

- What responsibility does each class own?
- Which data should remain private?
- Which operations need validation?
- Which members should be shared?
- Which objects need to communicate?
- How can I reduce unnecessary dependencies?

# Remember this principle

An enterprise application is not merely a collection of classes. It is a collaboration of well-designed objects, each having a clear responsibility.

## Design before Code. Fundamentals before Frameworks.

— Ravi Sir, Chief Mentor, Transflower Learning
