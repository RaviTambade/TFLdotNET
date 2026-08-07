# `System.Object` – The Mother of All Classes in .NET

> **"Good morning, future software engineers! Today I want to introduce you to someone very special. You have already learned inheritance. You have created `Employee`, `Manager`, `Customer`, `Product` and many other classes. But have you ever wondered — who is the parent of all these classes? Who gave your objects common abilities like `ToString()`, `Equals()`, and `GetHashCode()`?"**

> **"There is one common ancestor standing at the top of the .NET family tree — `System.Object`."**

# Imagine a Huge Software Family

Let us imagine a large family.

At the top of the family is the **grandparent**.

```text
                         System.Object
                              │
             ┌────────────────┼────────────────┐
             │                │                │
          Employee         Customer         Product
             │
             ▼
          Manager
             │
             ▼
       SalesManager
```

Now I ask you:

> **"Who gave `Employee` the `ToString()` method?"**

Did we write it?

```csharp
class Employee
{
}
```

No.

Did we inherit it from somewhere?

**Yes!**

It ultimately comes from:

```text
System.Object
```


# What is `System.Object`?

`System.Object` is the **root class of the .NET type system**.

In C#, you can write:

```csharp
public class Employee
{
}
```

But conceptually, the compiler treats it as:

```csharp
public class Employee : System.Object
{
}
```

We normally don't write the inheritance explicitly because C# provides it automatically for classes.

So remember:

```text
Your Class
    │
    ▼
System.Object
```


# Why Do We Call It the "Mother of All Classes"?

Because every class ultimately inherits from `System.Object`.

For example:

```csharp
public class Employee
{
}

public class Manager : Employee
{
}
```

The hierarchy becomes:

```text
System.Object
      │
      ▼
   Employee
      │
      ▼
   Manager
```

Therefore:

```text
Manager
   ↓
Employee
   ↓
System.Object
```

Manager indirectly inherits all the object-level behavior.


# A Real-World Story

Imagine a company.

Every employee is different.

```text
Employee
 ├── Ravi
 ├── Amit
 ├── Seeta
 └── Rahul
```

Their jobs may be different.

One may be:

```text
Developer
Manager
Tester
Architect
HR
Accountant
```

But the company gives every employee some common facilities:

```text
Employee ID
Identity
Access Card
Department
Basic Information
```

Similarly, .NET gives every object some common capabilities.

Those capabilities come from:

```text
System.Object
```


# The Five Important Object Methods

As a mentor, I want you to remember five important methods.

```text
                  System.Object
                       │
       ┌───────────────┼────────────────┐
       │               │                │
       ▼               ▼                ▼
   GetType()        Equals()     ReferenceEquals()
       │
       ├───────────────┐
       ▼               ▼
   ToString()     GetHashCode()
```

Now let's meet each one.


# 🔎 1. `GetType()` – "Who Are You?"

Imagine you meet someone at a railway station.

You ask:

> **"Who are you?"**

The object has an answer.

In C#:

```csharp
GetType()
```

Example:

```csharp
Employee emp = new Employee();

Console.WriteLine(emp.GetType());
```

Output:

```text
Employee
```

So:

```text
Object
  │
  ▼
GetType()
  │
  ▼
"What type are you?"
```



# `GetType()` and Polymorphism

Now things become interesting.

Look at this:

```csharp
Employee emp = new Manager();
```

What is the reference type?

```text
Employee
```

What is the actual object?

```text
Manager
```

Now:

```csharp
Console.WriteLine(emp.GetType());
```

What will it print?

```text
Manager
```

Because `GetType()` tells us the **actual runtime type**.

```text
Employee reference
       │
       ▼
+------------------+
| Manager Object   |
+------------------+
       │
       ▼
   GetType()
       │
       ▼
     Manager
```

This is extremely useful when understanding **runtime polymorphism**.


# 2. `Equals()` – "Are You Equal?"

Now imagine two employees.

```text
Employee 1
ID = 101
Name = Ravi

Employee 2
ID = 101
Name = Ravi
```

You ask:

> "Are these two employees equal?"

In C# we can use:

```csharp
emp1.Equals(emp2)
```

Example:

```csharp
Employee emp1 = new Employee();
Employee emp2 = new Employee();

Console.WriteLine(
    emp1.Equals(emp2)
);
```

The result depends on the type's equality implementation.

For a normal class that hasn't overridden `Equals()`, the default behavior is based on object identity.

But a class can override `Equals()` to define business/value equality.


# Equality Is a Business Decision

Suppose:

```text
Customer A
CustomerId = 101
Name = Ravi

Customer B
CustomerId = 101
Name = Ravi
```

From a business perspective, perhaps they represent the same customer.

But technically:

```text
Customer A ≠ Customer B
```

because they may be two separate objects.

Therefore, we have to distinguish:

```text
Object Identity
       vs
Object Equality
```

This distinction becomes very important when working with:

* Collections
* Entity Framework
* LINQ
* `HashSet<T>`
* `Dictionary<TKey,TValue>`


#  3. `ReferenceEquals()` – "Are You the Same Object?"

Now I want to ask a different question.

Not:

> "Are you equal?"

Instead:

> **"Are you literally the same object in memory?"**

That's the job of:

```csharp
Object.ReferenceEquals()
```

Example:

```csharp
Employee emp1 = new Employee();

Employee emp2 = emp1;

Console.WriteLine(
    Object.ReferenceEquals(emp1, emp2)
);
```

Output:

```text
True
```

Why?

Because both variables point to the same object.

```text
emp1 ──────────────┐
                   ▼
              +---------+
              | Employee|
              | Object  |
              +---------+
                   ▲
                   │
emp2 ──────────────┘
```

One object.

Two references.


# Two Different Objects

Now:

```csharp
Employee emp1 = new Employee();
Employee emp2 = new Employee();

Console.WriteLine(
    Object.ReferenceEquals(emp1, emp2)
);
```

Output:

```text
False
```

Because:

```text
emp1 ───► Employee Object A

emp2 ───► Employee Object B
```

Two different objects.


# `Equals()` vs `ReferenceEquals()`

This is a very common interview question.

Think of two questions.

### Question 1

> "Do these objects consider themselves equal?"

```csharp
Equals()
```

### Question 2

> "Are these two references pointing to the exact same object?"

```csharp
ReferenceEquals()
```

Remember:

```text
Equals()
    ↓
Logical / defined equality

ReferenceEquals()
    ↓
Same object identity
```


# 4. `ToString()` – "Describe Yourself"

Now imagine I introduce you to someone and ask:

> "Tell me something about yourself."

Objects can answer a similar question.

We call:

```csharp
ToString()
```

Example:

```csharp
Employee emp = new Employee();

Console.WriteLine(
    emp.ToString()
);
```

The default implementation generally gives the object's type name.

For example:

```text
Employee
```

But we can make the object more descriptive.


# Overriding `ToString()`

Suppose our Employee has:

```csharp
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public override string ToString()
    {
        return $"Id = {Id}, Name = {Name}";
    }
}
```

Now:

```csharp
Employee emp = new Employee
{
    Id = 101,
    Name = "Ravi"
};

Console.WriteLine(emp.ToString());
```

Output:

```text
Id = 101, Name = Ravi
```

Now the object can describe itself.


# Why Is `ToString()` Useful?

Imagine debugging a large application. Without overriding:

```text
Employee
```

With overriding:

```text
Id = 101, Name = Ravi
```

Much more useful!.That is why developers commonly override `ToString()` for domain objects.


# 5. `GetHashCode()` – "Give Me Your Hash Identity"

Now we reach another important method:

```csharp
GetHashCode()
```

Example:

```csharp
Employee emp = new Employee();

int hash =
    emp.GetHashCode();

Console.WriteLine(hash);
```

It returns an integer representing the object's hash code. But why do we need this?

# 🗄️ Hash-Based Collections

Think about a huge library. You have:

```text
1,000,000 books
```

If you search every book one by one:

```text
Book 1
Book 2
Book 3
...
Book 1,000,000
```

that can be inefficient. A hash-based data structure uses a hash to help locate an item quickly. Conceptually:

```text
Object
   │
   ▼
GetHashCode()
   │
   ▼
Hash Value
   │
   ▼
Hash Bucket
   │
   ▼
Lookup
```

This is why hash codes are important for collections such as:

```text
Dictionary<TKey,TValue>
HashSet<T>
```


# Important Rule of `GetHashCode()`

There is one rule you must remember. 
If:

```csharp
a.Equals(b)
```

returns:

```text
true
```

then:

```csharp
a.GetHashCode()
```

and:

```csharp
b.GetHashCode()
```

must return the same hash code.

In simple terms:

```text
Equal Objects
      │
      ▼
Same Hash Code
```

But the reverse is **not** necessarily true. Two different objects can have the same hash code. This is called a:

# Hash Collision

```text
Object A ──► Hash 100
Object B ──► Hash 100
```

Same hash does not automatically mean same object.



# Let's Put the Five Together

Suppose:

```csharp
Employee emp =
    new Employee();
```

We can ask five questions.

### 1. Who are you?

```csharp
emp.GetType();
```

### 2. Are you equal to another object?

```csharp
emp.Equals(other);
```

### 3. Are you literally the same object?

```csharp
Object.ReferenceEquals(
    emp,
    other);
```

### 4. Describe yourself.

```csharp
emp.ToString();
```

### 5. What is your hash code?

```csharp
emp.GetHashCode();
```

So remember:

```text
                   Object
                     │
        ┌────────────┼────────────┐
        │            │            │
      WHO?        EQUAL?        SAME?
        │            │            │
   GetType()      Equals()   ReferenceEquals()
        │
        ├───────────────┐
        │               │
      DESCRIBE?       HASH?
        │               │
   ToString()     GetHashCode()
```



# Real-World Bank Account Example

Let's make this practical.

```csharp
public class BankAccount
{
    public int AccountNumber { get; set; }
    public string CustomerName { get; set; }
    public decimal Balance { get; set; }
    public override string ToString()
    {
        return
            $"Account = {AccountNumber}, " +
            $"Customer = {CustomerName}, " +
            $"Balance = {Balance}";
    }
}
```

Create an account:

```csharp
BankAccount account =
    new BankAccount
    {
        AccountNumber = 101,
        CustomerName = "Ravi",
        Balance = 50000
    };
```

Now ask the object:

```csharp
Console.WriteLine(
    account.GetType()
);
```

It answers:

```text
BankAccount
```

Ask:

```csharp
Console.WriteLine(
    account.ToString()
);
```

It answers:

```text
Account = 101, Customer = Ravi, Balance = 50000
```

Ask:

```csharp
Console.WriteLine(
    account.GetHashCode()
);
```

It gives its hash code. And:

```csharp
Console.WriteLine(
    Object.ReferenceEquals(
        account,
        account
    )
);
```

returns:

```text
True
```

Because both references point to the same object.

# 🌳 The Bigger .NET Type Family

There is another important point. When we say:

> **"Object is the mother of all classes."**

we are talking about the root of the .NET type hierarchy. For normal classes:

```text
System.Object
      │
      ├── Employee
      │      │
      │      └── Manager
      │
      ├── Customer
      │
      ├── Product
      │
      └── BankAccount
```

But value types have an intermediate parent:

```text
System.Object
      │
      ▼
System.ValueType
      │
      ├── Int32
      ├── Double
      ├── Boolean
      └── Decimal
```

So a simplified type hierarchy looks like:

```text
                         System.Object
                              │
                ┌─────────────┴─────────────┐
                │                           │
         Reference Types               Value Types
                │                           │
                │                    System.ValueType
                │                           │
                │                 ┌─────────┼─────────┐
                │                 │         │         │
                │                int     double     bool
                │
                ├── Employee
                │      │
                │      └── Manager
                │
                ├── Customer
                │
                └── Product
```


# 📦 What Does `object` Mean in C#?

C# provides a keyword:

```csharp
object
```

which represents:

```csharp
System.Object
```

These are equivalent:

```csharp
object value;
```

and:

```csharp
System.Object value;
```

Therefore:

```csharp
object value;

value = 10;
value = "Hello";
value = true;
value = 10.5;
```

An `object` reference can participate in the .NET type system for many different kinds of values.


# 📦 Object and Boxing

Now something interesting happens with value types. Consider:

```csharp
int number = 100;
object obj = number;
```

The integer is a value type. When assigned to an `object`, it is **boxed**.

```text
int number = 100
       │
       │ Boxing
       ▼
+----------------+
| Object         |
| boxed int=100  |
+----------------+
```

To retrieve the integer:

```csharp
int number =
    (int)obj;
```

This is called:

```text
Unboxing
```

So:

```text
Value Type
    │
    │ Boxing
    ▼
 object
    │
    │ Unboxing
    ▼
Value Type
```

This is another important topic that naturally follows from understanding `System.Object`.


# Interview Questions

### Question 1

**What is the base class of a C# class?**

```text
System.Object
```


###  Question 2

**What does `GetType()` return?**

> The runtime type of the object.



### Question 3

**What is `ToString()` used for?**

> To provide a string representation of an object.


### Question 4

**What is `ReferenceEquals()` used for?**

> To determine whether two references refer to the exact same object.



### ❓ Question 5

**Why is `GetHashCode()` important?**

> It is used by hash-based collections such as `Dictionary` and `HashSet`.



### ❓ Question 6

**Can two different objects have the same hash code?**

Yes.

```text
Object A ──► Hash 100
Object B ──► Hash 100
```

This is a **hash collision**.


# Mentor's Memory Trick

Don't memorize five methods separately. Remember a conversation with an object. 
I meet an object and ask:

```text
👨 Mentor:
"Who are you?"

🤖 Object:
GetType()


👨 Mentor:
"Are you equal to this object?"

🤖 Object:
Equals()


👨 Mentor:
"Are you literally the same object?"

🤖 Object:
ReferenceEquals()


👨 Mentor:
"Describe yourself."

🤖 Object:
ToString()


👨 Mentor:
"What hash identity do you have?"

🤖 Object:
GetHashCode()
```

That's it!


# Mentor's Golden Wisdom

> **"Students, `System.Object` is not just another class in the .NET library. It is one of the foundations of the .NET type system. Every time you create a class, every time you call `ToString()`, every time you compare objects, every time a `Dictionary` calculates a hash, the influence of `System.Object` is somewhere underneath."**

Keep this picture in your mind:

```text
                       🌳 System.Object
                              │
             ┌────────────────┼────────────────┐
             │                │                │
          GetType()        Equals()       ReferenceEquals()
             │                │                │
             └────────────┬───┴────────────────┘
                          │
                   ┌──────┴──────┐
                   ▼             ▼
               ToString()   GetHashCode()
                          │
                          ▼
                    .NET Objects
```

> **"When you understand the root of the tree, the branches become easier to understand. Learn `System.Object` well, and concepts such as inheritance, polymorphism, equality, hashing, collections, boxing, and runtime type information start connecting into one big picture."**

# Final Takeaway

```text
                 SYSTEM.OBJECT
                       │
                       ▼
              Common Object Behavior
                       │
        ┌──────────────┼──────────────┐
        ▼              ▼              ▼
   GetType()        Equals()    ReferenceEquals()
        │
        ├──────────────┐
        ▼              ▼
   ToString()     GetHashCode()
        │              │
        ▼              ▼
  Human-readable    Hash-based
   representation   collections
```

> **"Don't just remember that `Object` is the mother of all classes. Remember what this mother gives every object: identity, equality, representation, type information, and hashing."**