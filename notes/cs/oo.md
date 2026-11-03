## C# Properties, Access Modifiers, Static Members and Object Collaboration

From Individual Objects → Controlled Access → Shared Members → Collaborating Objects

Previous session: We created a `Date` class and learned class, object, state, behavior, constructor, `this`, encapsulation and domain modeling.

Today's challenge: Can we design multiple classes that collaborate to solve a real-world problem?

## C# Properties, Access Modifiers, Static Members and Object Collaboration

### 1. Start with a Real-World Scenario

Dear students,

Imagine we are building a Student Management System for Transflower.

We have 100 students. Each student has:

- Student ID
- Name
- Marks
- Course

Now think about these questions:

1. Should every part of the application directly modify student marks?
2. Should every student have a separate copy of the total student count?
3. How can a Student object communicate with a Course object?
4. How can we protect internal data?

Today we will answer these questions using C#.

### 2. Understanding Properties

In the previous session, we created fields:

```
private int marks;
```

We learned that private fields protect internal state. But how can outside code read the marks?

We use properties.

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

Understand the property:

```
public int Marks { get; private set; }
```

```
             Marks Property
                   |
             +-----+-----+
             |           |
             v           v
            get         set
             |           |
        Public Read   Private Write
             |           |
             v           v
       Outside code   Class only
```

Outside code can read:

```
Console.WriteLine(student.Marks);
```

But it cannot directly write:

```
student.Marks = 95; // Compile-time error
```

**Mentor:** Why do we need this restriction?

**Student:** Sir, otherwise any part of the application could change student marks without validation.

**Mentor:** Correct. Controlled access is an important design principle.

### 3. Properties with Validation

Suppose marks must always be between 0 and 100. We can provide a controlled method.

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
                "Marks must be between 0 and 100.");
        }

        Marks = marks;
    }
}
```

Now the object protects its own business rules.

```
       External Application
                |
                v
          UpdateMarks()
                |
                v
        Validate marks
                |
          +-----+-----+
          |           |
        Valid       Invalid
          |           |
          v           v
       Update       Reject
       state        change
```

**Remember:** Properties provide controlled access. Methods are useful when an operation needs validation or business logic.

### 4. Access Modifiers – Who Can Access What?

C# provides access modifiers to control visibility. Consider a company building.

```
Company
  |
  +-- Public Reception
  |
  +-- Employee Area
  |
  +-- Manager Office
  |
  +-- Private Locker
```

Different people have different access permissions. Similarly, C# provides different levels of access.

| Modifier             | Meaning                                                 |
| -------------------- | ------------------------------------------------------- |
| `public`             | Accessible wherever the containing type is accessible   |
| `private`            | Accessible only inside the containing type              |
| `protected`          | Accessible inside the containing type and derived types |
| `internal`           | Accessible within the same assembly                     |
| `protected internal` | Same assembly OR derived types                          |
| `private protected`  | Same assembly AND derived types                         |

For now, focus on these four:

```
public       → Everyone with access to the type
private      → Current class
protected    → Current class + derived classes
internal     → Same assembly
```

Example:

```
public class Employee
{
    public int Id;
    private decimal salary;
    protected string department;
    internal string OfficeLocation;
}
```

A derived class can access protected members:

```
public class Manager : Employee
{
    public void DisplayDepartment()
    {
        Console.WriteLine(department);
    }
}
```

But it cannot directly access the private salary field.

**Mentor question:** Why should salary not be public?

**Student:** Because salary is sensitive business information and should be accessed through controlled operations.

### 5. Static Members – Class-Level Data

Now imagine Transflower has 100 students. We want to maintain the total number of Student objects created. Should every student maintain a separate total count? No. The count belongs to the class, not to an individual student.

This is where `static` becomes useful.

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

Object creation:

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

Visualize:

```
                Student Class
                      |
             Static TotalStudents
                      |
                      3
                      |
          +-----------+-----------+
          |           |           |
          v           v           v
       Student 1   Student 2   Student 3
          |           |           |
       Id: 101     Id: 102     Id: 103
       Amit        Sneha       Rahul
```

Each object has its own instance data. All objects share the same static member.

### 6. Instance vs Static

This is an important distinction.

| Instance Member                        | Static Member                     |
| -------------------------------------- | --------------------------------- |
| Belongs to an object                   | Belongs to the type               |
| Each object has its own instance state | Shared at type level              |
| Access through an object               | Usually access through class name |
| Created as part of an instance         | Does not require an instance      |

Example:

```
Student s1 = new Student(101, "Amit");

Console.WriteLine(s1.Name); 
Console.WriteLine(Student.TotalStudents);
```

Think carefully:

```
Student.Name
```

is not the same as:

```
Student.TotalStudents
```

The first is instance data; the second is shared class-level data.

### 7. Static Methods

A static method can be called without creating an object.

For example:

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

Why? Because `Add()` does not need the state of a particular Calculator object. A static method cannot directly access instance members without an object reference.

### 8. Object Collaboration – The Real Power of OOP

Now comes the most important topic. Real applications rarely contain only one class. Consider a Student Management System. We may have:

```
Student
Course
Enrollment
Mentor
Assessment
Result
```

These objects must collaborate. Let's start with two classes.

**Course class**

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

**Student class**

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
        Console.WriteLine($"{Name} enrolled in {course.Title}");
    }
}
```

Now let's make objects collaborate.

```
Course course = new Course(1, "AI Enabled Fullstack Development");
Student student = new Student(101, "Amit");
student.Enroll(course);
```

Output:

```
Amit enrolled in AI Enabled Fullstack Development
```

Understand the relationship:

```
              Student Object
                    |
                    |
               Enroll(course)
                    |
                    v
               Course Object
                    |
                    v
       AI Enabled Fullstack Development
```

The Student object receives a Course object and interacts with it. This is object collaboration.

### 9. Complete Execution Flow

```
              Program.cs
                  |
          +-------+-------+
          |               |
          v               v
      Create Course   Create Student
          |               |
          v               v
      Course Object   Student Object
          |               |
          +-------+-------+
                  |
                  v
           student.Enroll()
                  |
                  v
          Access Course data
                  |
                  v
            Display message
```

**Mentor:** Is the Student class responsible for implementing all Course behavior?

**Student:** No, sir. The Course object owns its own information and behavior.

**Mentor:** Exactly! Each class should have clear responsibilities.

### 10. Practical Lab – Student and Course Management

Create a console application.

```
dotnet new console -n StudentCourseApp
cd StudentCourseApp
dotnet run
```

Implement the following classes:

**Student**

- Id
- Name
- TotalStudents (static)
- Enroll(Course course)
- Display()

**Course**

- CourseId
- Title
- Duration
- Display()

**Program**

- Create two Course objects.
- Create three Student objects.
- Enroll students in courses.
- Display student details.
- Display the total number of students.

Expected output:

```
Student: Amit
Course : C# Programming

Student: Sneha
Course : ASP.NET Core

Student: Rahul
Course : C# Programming

Total Students: 3
```

### 11. Engineering Thinking – What Did We Learn?

| Concept              | Engineering purpose                      |
| -------------------- | ---------------------------------------- |
| Properties           | Controlled access to state               |
| Access modifiers     | Visibility and encapsulation             |
| Static members       | Shared type-level functionality or state |
| Instance members     | Object-specific state and behavior       |
| Object collaboration | Multiple objects working together        |
| Class responsibility | Organizing application behavior          |

### 12. Mentor Assessment

1. What is the difference between a field and a property?
2. Why do we use `private set`?
3. What is the difference between `private` and `protected`?
4. What is the difference between `internal` and `public`?
5. What is a static variable?
6. Can a static method directly access an instance variable?
7. What is object collaboration?
8. Why should a Student object not own all Course behavior?
9. What happens when multiple objects access the same static property?
10. How can object collaboration help us design ASP.NET Core applications?


### Ravi Sir's Closing Thought

Dear students,

A beginner writes classes.

An intermediate developer creates objects.

An experienced developer understands how objects collaborate.

When you build a real application, don't ask only:

"How many classes should I create?"

Ask:

- What responsibility does each class own?
- Which object needs to communicate with another?
- Which data should remain private?
- Which operations should be shared?
- Which behavior belongs to the object?

**Remember: An enterprise application is not a collection of classes. It is a collaboration of well-designed objects.**

— Ravi Sir, Transflower Mentor
