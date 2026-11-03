## C# Object Relationships – Association, Aggregation, Composition and Inheritance

### 1. Start with a Real-World Story

Dear students, Imagine Transflower has a learning management system. We have:

- Students
- Mentors
- Courses
- Batches
- Assessments

Now consider these situations:

1. A Student enrolls in a Course.
2. A Mentor teaches multiple Students.
3. A Department contains multiple Courses.
4. An Order contains OrderItems.
5. A Manager is an Employee.

Are all these relationships the same? 

No!

Today we will understand four important object-oriented relationships.

### 2. Association – Objects Know Each Other

Association represents a relationship between independent objects.

Example:

A Student enrolls in a Course.

```text
       Student  ---------------->  Course
                    enrolls in
```

Both objects can exist independently. A Student can exist without a Course. A Course can exist without a Student.

Let's implement this in C#.

**Course.cs**

```csharp
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }

    public Course(int id, string title)
    {
        Id = id;
        Title = title;
    }
}
```

**Student.cs**

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

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

**Program.cs**

```csharp
Course course = new Course(101, "C# Programming");
Student student = new Student(1, "Amit");
student.Enroll(course);
```

Output:

```text
Amit enrolled in C# Programming
```

Notice: The Student receives a Course object. Neither object owns the lifetime of the other.

### 3. Aggregation – A Whole Has Parts, but Parts Can Exist Independently

Imagine a Department containing multiple Courses.

```text
          Department
               |
        Aggregates
               |
       +-------+-------+
       |       |       |
       v       v       v
     Course  Course  Course
       A       B       C
```

If the Department is closed, the Course objects may still exist or be moved to another Department. That is the important idea behind aggregation.

Let's implement it.

**Course.cs**

```csharp
public class Course
{
    public string Title { get; set; }

    public Course(string title)
    {
        Title = title;
    }
}
```

**Department.cs**

```csharp
using System.Collections.Generic;

public class Department
{
    public string Name { get; set; }
    public List<Course> Courses { get; private set; }

    public Department(string name, List<Course> courses)
    {
        Name = name;
        Courses = courses;
    }

    public void Display()
    {
        Console.WriteLine($"Department: {Name}");

        foreach (Course course in Courses)
        {
            Console.WriteLine(course.Title);
        }
    }
}
```

**Program.cs**

```csharp
Course c1 = new Course("C#");
Course c2 = new Course("ASP.NET Core");

List<Course> courses = new List<Course>{  c1, c2};
Department department = new Department("Software Development", courses);
department.Display();
```

The Department receives existing Course objects.

**Mentor question:** If we remove the Department object, must the Course objects automatically disappear?

**Student:** No, sir. The Course objects can exist independently.

### 4. Composition – Strong Ownership

Now imagine an Order containing OrderItems. Can an OrderItem exist meaningfully without its Order in this particular business design? We may decide that an OrderItem is owned by one Order and its lifecycle is controlled by that Order.

This is composition.

```text
             Order
               |
            Owns
               |
        +------+------+
        |      |      |
        v      v      v
      Item1  Item2  Item3
```

Let's implement it.

**OrderItem.cs**

```csharp
public class OrderItem
{
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(string productName, int quantity)
    {
        ProductName = productName;
        Quantity = quantity;
    }
}
```

**Order.cs**

```csharp
using System.Collections.Generic;

public class Order
{
    public int OrderId { get; private set; }
    private readonly List<OrderItem> items = new();

    public Order(int orderId)
    {
        OrderId = orderId;
    }

    public void AddItem(string productName, int quantity)
    {
        items.Add(new OrderItem(productName, quantity));
    }

    public void Display()
    {
        Console.WriteLine($"Order: {OrderId}");
        foreach (OrderItem item in items)
        {
            Console.WriteLine($"{item.ProductName} - {item.Quantity}");
        }
    }
}
```

**Program.cs**

```csharp
Order order = new Order(1001);

order.AddItem("Laptop", 1);
order.AddItem("Mouse", 2);

order.Display();
```

Here, Order creates and controls its OrderItems through its public operations.

Important distinction: C# does not automatically destroy child objects when a parent reference disappears. Composition is a domain ownership and lifecycle design decision, not an automatic garbage-collection rule.

### 5. Association vs Aggregation vs Composition

| Relationship | Meaning | Example |
|---|---|---|
| Association | Objects interact | Student enrolls in Course |
| Aggregation | Whole-part, independent lifetimes | Department has Courses |
| Composition | Strong ownership | Order owns OrderItems |

Visual memory aid:

```text
Association:
Student -------- Course

Aggregation:
Department ◇---- Course

Composition:
Order      ◆---- OrderItem
```

In UML notation:

- `◇` represents aggregation.
- `◆` represents composition.

### 6. Inheritance – An IS-A Relationship

Now consider:

```text
Employee
   |
   +-- Manager
   |
   +-- Developer
   |
   +-- Tester
```

A Manager is an Employee. A Developer is an Employee. A Tester is an Employee. This is an IS-A relationship. C# supports inheritance using the colon (`:`).

**Employee.cs**

```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Display()
    {
        Console.WriteLine($"{Id} - {Name}");
    }
}
```

**Developer.cs**

```csharp
public class Developer : Employee
{
    public string ProgrammingLanguage { get; set; }

    public void WriteCode()
    {
        Console.WriteLine(
            $"Developing using {ProgrammingLanguage}");
    }
}
```

**Program.cs**

```csharp
Developer developer = new Developer();

developer.Id = 101;
developer.Name = "Rahul";
developer.ProgrammingLanguage = "C#";

developer.Display();
developer.WriteCode();
```

Output:

```text
101 - Rahul
Developing using C#
```

The Developer inherits accessible members of Employee.

### 7. Method Overriding – Specialized Behavior

Suppose every employee works, but each employee type performs different work.We can use method overriding.

**Employee.cs**

```csharp
public class Employee
{
    public string Name { get; set; }

    public virtual void Work()
    {
        Console.WriteLine("Employee is working");
    }
}
```

**Developer.cs**

```csharp
public class Developer : Employee
{
    public override void Work()
    {
        Console.WriteLine("Developer is writing code");
    }
}
```

**Tester.cs**

```csharp
public class Tester : Employee
{
    public override void Work()
    {
        Console.WriteLine("Tester is testing software");
    }
}
```

**Program.cs**

```csharp
Employee employee1 = new Developer();
Employee employee2 = new Tester();

employee1.Work();
employee2.Work();
```

Output:

```text
Developer is writing code
Tester is testing software
```

This is runtime polymorphism.

### 8. Object Relationships in a Real Application

Let's combine our learning.

Consider an e-commerce application.

```text
                 E-Commerce System
                         |
          +--------------+--------------+
          |              |              |
          v              v              v
        Customer       Product         Order
                          |              |
                          |           Composition
                          |              |
                          |          +---+---+
                          |          |       |
                          v          v       v
                       Category   OrderItem OrderItem

Customer -------- places -------- Order
```

Possible relationships:

- Customer and Order: Association.
- Category and Product: Often modeled as aggregation or association, depending on ownership.
- Order and OrderItem: Composition.
- Employee and Manager: Inheritance, if Manager is modeled as a specialized Employee.

### 9. Important Design Thinking – Prefer Composition When Appropriate

Students often ask:"Sir, should we use inheritance everywhere?"

No!

Consider a Car. A Car has an Engine. It is not an Engine.

```text
Car HAS-A Engine
```

Therefore, composition is more appropriate than inheritance.

```csharp
public class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine started");
    }
}

public class Car
{
    private readonly Engine engine;

    public Car()
    {
        engine = new Engine();
    }

    public void StartCar()
    {
        engine.Start();
        Console.WriteLine("Car started");
    }
}
```

This demonstrates a HAS-A relationship.

Remember:

- IS-A → Inheritance.
- HAS-A → Object reference / composition or aggregation, depending on ownership.
- USES-A → Association or dependency.

### 10. Practical Lab – Design a Learning Management System

Build a small console application with the following classes:

**Student**
- StudentId
- Name
- Enroll(Course course)

**Course**
- CourseId
- Title

**Department**
- DepartmentId
- Name
- List of Courses

**Assessment**
- AssessmentId
- Title
- MaximumMarks

**OnlineAssessment**
- Inherits Assessment
- DurationInMinutes

**Order and OrderItem extension**
- Order owns its items.
- Add and display order items.

Your objective is to demonstrate all four relationships.

### 11. Student Assessment

Answer these questions:

1. What is association?
2. How is aggregation different from composition?
3. What does the UML diamond represent?
4. What is an IS-A relationship?
5. What is a HAS-A relationship?
6. Why should a Car not inherit from Engine?
7. What is method overriding?
8. What is the purpose of `virtual`?
9. What is the purpose of `override`?
10. When might composition be a better choice than inheritance?


### Ravi Sir's Closing Thought

Dear students, When you design software, don't begin by asking:

"Which class should inherit from which class?"

Start by understanding the real-world relationship.

- Is it an IS-A relationship?
- Is it a HAS-A relationship?
- Does one object own another?
- Can both objects live independently?

These questions lead to better software design.

**A good software engineer doesn't merely connect classes. A good software engineer models meaningful relationships between objects.**

— Ravi Sir, Transflower Mentor