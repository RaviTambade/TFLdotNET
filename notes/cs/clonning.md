# Cloning– Creating a New Object from an Existing Object

> **"Good morning, future software engineers! Today, imagine that you have designed a perfect house. You want to build another house with exactly the same design. Would you redraw every wall, window, door, and room from the beginning? Probably not. You would take the existing design and create a copy. In software, we have the same requirement. Sometimes we already have a perfectly configured object and want to create another object with the same state. This is called **Cloning**."**

# The Story of the Prototype House

Imagine an architect creates a prototype house:

```text
                  HOUSE PROTOTYPE
                       │
        ┌──────────────┼──────────────┐
        ▼              ▼              ▼
      Rooms          Doors         Windows
        │              │              │
        └──────────────┼──────────────┘
                       ▼
                  Perfect House
```

Now the builder wants:

```text
House 1
House 2
House 3
House 4
```

Instead of designing everything again:

```text
Prototype
   │
   ├── Clone → House 1
   ├── Clone → House 2
   ├── Clone → House 3
   └── Clone → House 4
```

That's the basic idea of **object cloning**.

# What Is Cloning?

Cloning means:

> **Creating a new object based on the state of an existing object.**

Suppose we have:

```csharp
Person original = new Person
{
    Name = "Amit",
    Age = 30
};
```

We want:

```text
Original
┌─────────────────┐
│ Name = Amit     │
│ Age  = 30       │
└─────────────────┘
          │
          │ Clone
          ▼
Clone
┌─────────────────┐
│ Name = Amit     │
│ Age  = 30       │
└─────────────────┘
```

But there is an important question:

> **Are the two objects really independent?**

That question leads us to **shallow copy and deep copy**.


# `ICloneable` – The Cloning Contract

.NET provides:

```csharp
ICloneable
```

It defines a single method:

```csharp
public interface ICloneable
{
    object Clone();
}
```

The meaning is simple:

> **"I promise that I can create a copy of myself."**

Think of it as a contract:

```text
              ICloneable
                   │
                   │ Clone()
                   ▼
          "Create another object
             based on me"
```


# The Photocopy Machine Story

Imagine a document.

You put it into a photocopier:

```text
Original Document
       │
       ▼
   Photocopier
       │
       ▼
Copied Document
```

The copied document contains the same visible information.

Object cloning works similarly:

```text
Original Object
       │
       ▼
     Clone()
       │
       ▼
New Object
```

But software has an additional complication. Objects can contain **references to other objects**. That's where the story becomes interesting.


# Basic Cloning Example

Let's start with a simple object.

```csharp
class Person : ICloneable
{
    public string Name { get; set; }

    public int Age { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
```

Now:

```csharp
Person original = new Person
{
    Name = "Amit",
    Age = 30
};

Person clone = (Person)original.Clone();
```

We now have:

```text
              Person
                │
        ┌───────┴────────┐
        ▼                ▼
    Original            Clone
        │                │
   Name = Amit       Name = Amit
   Age  = 30         Age  = 30
```

The clone is a **different object**.

We can verify that:

```csharp
Console.WriteLine(
    Object.ReferenceEquals(original, clone)
);
```

Output:

```text
False
```

Why?

Because:

```text
original ───────► Object A

clone ──────────► Object B
```

They contain similar data, but they are different objects.


# What Is `MemberwiseClone()`?

`MemberwiseClone()` is a protected method provided by `System.Object`. It creates a **shallow copy** of the current object.

Conceptually:

```text
Original Object
       │
       │ MemberwiseClone()
       ▼
New Object
```

It copies the fields of the object. But there is an important rule:

> **Reference-type fields are copied as references, not as new objects.**

This is the heart of shallow copying.


# Shallow Copy

Imagine:

```csharp
class Person
{
    public string Name { get; set; }

    public Address Address { get; set; }
}
```

And:

```csharp
class Address
{
    public string City { get; set; }
}
```

Now:

```csharp
Person original = new Person
{
    Name = "Amit",

    Address = new Address
    {
        City = "Pune"
    }
};
```

Memory looks conceptually like:

```text
original
   │
   ▼
┌─────────────────┐
│ Person          │
│ Name = Amit     │
│ Address ────────┼────────┐
└─────────────────┘        │
                           ▼
                    ┌──────────────┐
                    │ Address      │
                    │ City = Pune  │
                    └──────────────┘
```

Now we clone:

```csharp
Person clone =
    (Person)original.MemberwiseClone();
```

The result is:

```text
original ───────┐
                │
                ▼
          ┌──────────────┐
          │ Address      │
          │ City = Pune  │
          └──────────────┘
                ▲
                │
clone ──────────┘
```

**Both objects point to the same Address object.**

That is a **shallow copy**.



# The Shallow Copy Trap

Suppose:

```csharp
clone.Address.City = "Mumbai";
```

What happens?

Because both objects share the same Address:

```text
original ───────┐
                │
                ▼
          Address
          City = Mumbai
                ▲
                │
clone ──────────┘
```

Now:

```csharp
Console.WriteLine(original.Address.City);
```

prints:

```text
Mumbai
```

Even though we changed:

```csharp
clone.Address.City
```

Why?

Because the clone and original share the same referenced object.



# The Golden Rule of Shallow Copy

Remember this:

```text
VALUE TYPE
   │
   ▼
Actual value copied
```

But:

```text
REFERENCE TYPE
   │
   ▼
Reference copied
   │
   ▼
Same referenced object
```

For example:

```text
Original
┌──────────────────┐
│ Age = 30         │──────► Value copied
│ Address ─────────┼──────┐
└──────────────────┘      │
                          ▼
                     Address Object
                          ▲
┌──────────────────┐      │
│ Age = 30         │──────┘
│ Address ─────────┼──────┐
└──────────────────┘      │
Clone                     │
                          └── Same Object
```


# Deep Copy

Now imagine that we don't want the clone and original to share the Address.

We want:

```text
Original
   │
   ▼
Person A
   │
   ▼
Address A
City = Pune


Clone
   │
   ▼
Person B
   │
   ▼
Address B
City = Pune
```

There are now **two Address objects**.

That is a **deep copy**.


# Deep Copy Example

```csharp
class Address
{
    public string City { get; set; }
}
```

Our Employee:

```csharp
class Employee
{
    public string Name { get; set; }

    public Address Address { get; set; }

    public Employee DeepCopy()
    {
        return new Employee
        {
            Name = this.Name,

            Address = new Address
            {
                City = this.Address.City
            }
        };
    }
}
```

Now:

```csharp
Employee original = new Employee
{
    Name = "Amit",

    Address = new Address
    {
        City = "Pune"
    }
};

Employee clone = original.DeepCopy();
```

Memory looks like:

```text
             Original
                │
                ▼
          ┌────────────┐
          │ Employee   │
          │ Amit       │
          └─────┬──────┘
                │
                ▼
          ┌────────────┐
          │ Address A  │
          │ Pune       │
          └────────────┘


              Clone
                │
                ▼
          ┌────────────┐
          │ Employee   │
          │ Amit       │
          └─────┬──────┘
                │
                ▼
          ┌────────────┐
          │ Address B  │
          │ Pune       │
          └────────────┘
```

Now changing:

```csharp
clone.Address.City = "Mumbai";
```

doesn't affect:

```csharp
original.Address.City
```

because they are independent objects.



# Shallow Copy vs Deep Copy

| Feature                    | Shallow Copy    | Deep Copy          |
| -------------------------- | --------------- | ------------------ |
| New top-level object       | ✅             | ✅                 |
| Value fields copied        | ✅             | ✅                 |
| Reference fields           | Same reference  | New object         |
| Independent nested objects | ❌             | ✅                 |
| Easier to implement        | ✅             | Usually more work  |
| Memory usage               | Lower          | Higher              |

Think:

```text
SHALLOW

Original ──────► Address ◄────── Clone
```

versus:

```text
DEEP

Original ──────► Address A

Clone ─────────► Address B
```

# 🧪 Comparing the Objects

Suppose:

```csharp
Person original = new Person
{
    Name = "Amit",
    Age = 30
};

Person clone = (Person)original.Clone();
```

This:

```csharp
Object.ReferenceEquals(original, clone)
```

returns:

```text
False
```

because they are separate objects.

But with a shallow copy containing an Address:

```csharp
Object.ReferenceEquals(
    original.Address,
    clone.Address
)
```

returns:

```text
True
```

That tells us:

```text
Person objects
     ↓
Different

Address objects
     ↓
Same
```

# 🧩 Cloning an Employee

Let's create a more realistic example.

```csharp
class Employee : ICloneable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Address Address { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
```

Usage:

```csharp
Employee original = new Employee
{
    Id = 101,

    Name = "Amit",

    Address = new Address
    {
        City = "Pune"
    }
};

Employee clone =
    (Employee)original.Clone();
```

This is a **shallow clone**.


# 🧬 Deep Clone of Employee

If we want a completely independent object:

```csharp
public Employee DeepCopy()
{
    return new Employee
    {
        Id = this.Id,

        Name = this.Name,

        Address = new Address
        {
            City = this.Address.City
        }
    };
}
```

Now:

```csharp
Employee clone = original.DeepCopy();
```

We have:

```text
Original
   │
   ├── Id
   ├── Name
   └── Address A

Clone
   │
   ├── Id
   ├── Name
   └── Address B
```

# 🧰 Different Ways to Clone Objects

There isn't only one way to create a copy.

### 1. `MemberwiseClone()`

Good for shallow copying:

```csharp
return MemberwiseClone();
```

### 2. Copy Constructor

A very explicit approach:

```csharp
public Employee(Employee other)
{
    Id = other.Id;

    Name = other.Name;

    Address = new Address
    {
        City = other.Address.City
    };
}
```

Usage:

```csharp
Employee clone =
    new Employee(original);
```

This clearly communicates:

> "Create a new Employee based on this Employee."


### 3. Explicit Copy Method

For example:

```csharp
public Employee DeepCopy()
{
    return new Employee
    {
        Id = Id,
        Name = Name,

        Address = new Address
        {
            City = Address.City
        }
    };
}
```

This has an important advantage:

**The method name tells us what it does.**

# ⚠️ Why `ICloneable` Can Be Confusing

At first glance:

```csharp
public interface ICloneable
{
    object Clone();
}
```

looks wonderful. But there is a problem. The interface doesn't tell us:

```text
Is Clone() shallow?
       OR
Is Clone() deep?
```

Both implementations technically satisfy the interface.

```text
ICloneable
    │
    └── Clone()
          │
     ┌────┴─────┐
     ▼          ▼
 Shallow       Deep
```

The caller cannot determine the copy semantics merely from the interface. That's why `ICloneable` is generally **not recommended for public APIs where the cloning semantics need to be clear**.



# Mentor's Better Approach

Instead of:

```csharp
object Clone();
```

we can write:

```csharp
Employee ShallowCopy()
```

or:

```csharp
Employee DeepCopy()
```

Now the intention is obvious.

```text
Employee
   │
   ├── ShallowCopy()
   │
   └── DeepCopy()
```

No guessing.


# 🏭 Cloning and the Prototype Pattern

Now we reach an important design-pattern connection. Suppose creating an object is expensive.

For example:

```text
Create Report
     │
     ├── Load configuration
     ├── Load templates
     ├── Load images
     ├── Configure formatting
     └── Prepare data
```

Creating this object repeatedly may be expensive. Instead:

```text
               Prototype
                  │
       ┌──────────┼──────────┐
       ▼          ▼          ▼
     Clone       Clone      Clone
       │          │          │
       ▼          ▼          ▼
   Report 1    Report 2   Report 3
```

This idea is called the **Prototype Design Pattern**.

The key idea:

> **Create a prototype once, then create new objects by copying the prototype.**


# 🏗️ Cloning in Enterprise Applications

Consider an e-commerce application.

We have:

```text
Product
   │
   ├── Name
   ├── Price
   ├── Category
   └── Configuration
```

Suppose the application wants to create product variants.

```text
                    Product Prototype
                           │
            ┌──────────────┼──────────────┐
            ▼              ▼              ▼
         Clone           Clone          Clone
            │              │              │
            ▼              ▼              ▼
       Product A       Product B       Product C
```

Each clone can then be customized.


# 🧠 Cloning vs Assignment

This is a very important distinction. Consider:

```csharp
Person p1 = new Person();
Person p2 = p1;
```

Did we clone the object?

**No.**

Both variables refer to the same object.

```text
p1 ───────┐
          ▼
       Person
          ▲
          │
p2 ───────┘
```

But:

```csharp
Person p2 = (Person)p1.Clone();
```

creates another object.

```text
p1 ──────► Person A
p2 ──────► Person B
```

So remember:

```text
Assignment
   ↓
Copy reference

Cloning
   ↓
Create another object
```


# 🔥 The Big Picture

```text
                         OBJECT
                           │
                    "I need another one"
                           │
                           ▼
                         CLONE
                           │
               ┌───────────┴───────────┐
               ▼                       ▼
         SHALLOW COPY              DEEP COPY
               │                       │
               ▼                       ▼
       References shared        References duplicated
               │                       │
               ▼                       ▼
          Faster/simple             Independent
```

And the .NET contract:

```text
                       ICloneable
                            │
                            ▼
                         Clone()
                            │
                 ┌──────────┴──────────┐
                 ▼                     ▼
             Shallow                 Deep
              Copy                   Copy
```


# 🌟 Mentor's Golden Wisdom

> **"Students, cloning is not simply copying data. The real question is: what exactly are you copying? If your object contains only value data, cloning may be simple. But once your object contains references to other objects, you must understand the difference between copying a reference and copying the referenced object."**

> **"Remember this picture: shallow copy creates a new house but may keep the same furniture. Deep copy creates a new house with its own furniture. That is the difference between copying the outer object and copying the entire object graph."**

# 🏁 Final Takeaway

```text
                    CLONING
                       │
                       ▼
             Existing Object
                       │
                       ▼
                    Copy
                       │
             ┌─────────┴─────────┐
             ▼                   ▼
       SHALLOW COPY          DEEP COPY
             │                   │
             ▼                   ▼
      New top-level        New top-level
         object               object
             │                   │
             ▼                   ▼
   Same nested references  New nested objects
             │                   │
             ▼                   ▼
       Shared state        Independent state
```

Remember the interview-ready version:

```text
ICloneable
    ↓
Clone()
    ↓
Creates a copy

MemberwiseClone()
    ↓
Shallow copy

Shallow Copy
    ↓
Reference objects are shared

Deep Copy
    ↓
Referenced objects are copied too

Assignment
    ↓
Copies reference, NOT object

Copy Constructor / DeepCopy()
    ↓
Often clearer for application code
```

> **"As a Transflower mentor, I want you to remember one final principle: when designing a copy operation, don't make the developer guess. A method called `DeepCopy()` communicates much more than a mysterious `Clone()`. Good software is not only about making things work — it is about making their behavior clear, predictable, and safe."**