# 🎓 Singleton Pattern in C# – The One and Only Object

> **"Good morning, future software engineers! Today, I want you to imagine a kingdom where thousands of people live, hundreds of soldiers protect it, and many ministers run different departments. But there is only one King. Why? Because if there were three Kings, three different decisions would be made, three different laws would be announced, and the kingdom would fall into chaos. In software, sometimes we face exactly the same problem. We need one — and only one — object. That is where the Singleton Pattern comes into the story."**

# 👑 The Story of the Kingdom

Imagine a kingdom.

There are:

* 👨‍🌾 Thousands of citizens
* ⚔️ Hundreds of soldiers
* 🏰 Many departments
* 👨‍💼 Many officers
* 📜 Many government services

But there is:

```text
                    KINGDOM
                       │
                       ▼
                     KING
                       │
          ┌────────────┼────────────┐
          ▼            ▼            ▼
       Citizens      Soldiers    Ministers
```

There is **only one King**.

Now imagine:

```text
                  KINGDOM
                     │
       ┌─────────────┼─────────────┐
       ▼             ▼             ▼
    King A         King B         King C
       │             │             │
     Rule A         Rule B        Rule C
```

King A says:

> "Tax is 10%."

King B says:

> "Tax is 20%."

King C says:

> "No tax."

What happens?

**Chaos!**

So the kingdom establishes one simple rule:

> **There must be only one King.**

Software sometimes needs the same rule.


# 📖 What Is Singleton?

The **Singleton Pattern** is a creational design pattern used when an application needs to ensure that a class has **one controlled instance** and provides a common access point to that instance.

Think of it as:

```text
                  Singleton Class
                         │
                         ▼
                   ┌───────────┐
                   │ Object #1 │
                   └───────────┘
                         ▲
             ┌───────────┼───────────┐
             │           │           │
          Client A    Client B    Client C
```

All clients use the **same object**.

They don't create separate objects.

# 🏦 The Bank Manager Story

Imagine a large bank.

The bank has:

```text
1000 Customers
100 Tellers
20 Branches
50 Loan Officers
```

But imagine there is one central `BankManager` responsible for a particular shared responsibility.

Without Singleton, developers might accidentally create:

```csharp
new BankManager();
new BankManager();
new BankManager();
new BankManager();
```

Now we have:

```text
          BankManager
          BankManager
          BankManager
          BankManager
```

Multiple managers.

Different objects may hold different state or configuration.

Instead, we want:

```text
                    BankManager
                         │
                    ONE INSTANCE
                         │
          ┌──────────────┼──────────────┐
          ▼              ▼              ▼
       Teller A       Teller B       Loan Officer
          │              │              │
          └──────────────┼──────────────┘
                         ▼
                  SAME OBJECT
```

That is the Singleton idea.


# 🔐 The Four Rules of Singleton

To protect our "one and only one" object, we need a few important mechanisms.

```text
Singleton
   │
   ├── Private Constructor
   │
   ├── Sealed Class
   │
   ├── Static Instance
   │
   └── Public Accessor
```

Let's understand each one through the story.


# 🚪 1. Private Constructor – Close the Gate

First, we close the door.

```csharp
private BankManager()
{
}
```

Now an outside class cannot do:

```csharp
BankManager manager = new BankManager();
```

The compiler prevents it.

```text
Outside World
      │
      │ new BankManager()
      ▼
 ┌─────────────┐
 │ Constructor │
 │   PRIVATE   │
 └──────┬──────┘
        │
        X
     ❌ BLOCKED
```

Think of the constructor as the **gate of the royal palace**.

```text
                 PALACE
                   │
                🚪 Gate
                   │
                PRIVATE
                   │
              BankManager
```

Nobody from outside can simply walk in and create another King.


# 🛡️ 2. Sealed Class – No Replacement King

We can declare:

```csharp
public sealed class BankManager
```

`sealed` prevents inheritance.

For example:

```csharp
class MyBankManager : BankManager
{
}
```

This is not allowed.

```text
              BankManager
                   │
                   X
                   │
           MyBankManager

              ❌ Not allowed
```

Think of `sealed` as a royal declaration:

> **"There will be no replacement King through inheritance."**

### Mentor Note

`sealed` is useful, but it is **not the core reason** Singleton works.

The fundamental Singleton mechanisms are:

```text
Private constructor
        +
One stored instance
        +
Controlled access
```

# 👑 3. Static Instance – The Royal Throne

Now we need a place to store the one object.

```csharp
private static BankManager? _instance = null;
```

Think about `_instance` as the **royal throne**.

There is only one throne.

```text
             BankManager Class
                    │
                    ▼
              _instance
                    │
                    ▼
              ┌───────────┐
              │ BankManager│
              │  Object #1 │
              └───────────┘
```

Because `_instance` is `static`, it belongs to the class rather than to individual objects.


# 🚪 4. Public Accessor – The Royal Entrance

Now people need a way to access the King.

We create:

```csharp
public static BankManager Instance
```

This becomes the official entrance.

The logic is:

```text
             Request Instance
                    │
                    ▼
           Is _instance null?
              /          \
            YES           NO
             │             │
             ▼             │
     Create Object         │
             │             │
             └──────┬──────┘
                    ▼
             Return Object
```

The first visitor creates the object.

Every visitor after that gets the **same object**.


# 🏦 Complete Singleton Example

```csharp
public sealed class BankManager
{
    // 3. Holds the single instance
    private static BankManager? _instance = null;

    // 1. Private constructor
    private BankManager()
    {
        Console.WriteLine("Bank Manager Created");
    }

    // 4. Public accessor
    public static BankManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BankManager();
            }

            return _instance;
        }
    }

    // Business behavior
    public void ApproveLoan(decimal amount)
    {
        Console.WriteLine(
            $"Bank Manager approved a loan of {amount}"
        );
    }
}
```


# 🚶 First Visitor

The first part of the application asks:

```csharp
BankManager manager1 = BankManager.Instance;
```

The Singleton checks:

```text
Is _instance null?
```

Yes!

So:

```csharp
_instance = new BankManager();
```

The object is created.

```text
              BankManager
                   │
                   ▼
             ┌───────────┐
             │ Object #1 │
             └───────────┘
```


# 🚶 Second Visitor

Another part of the application asks:

```csharp
BankManager manager2 = BankManager.Instance;
```

This time:

```text
Is _instance null?
```

No!

The object already exists.

So Singleton simply returns the existing object.

```text
             ┌───────────┐
             │ Object #1 │
             └───────────┘
                ▲     ▲
                │     │
                │     │
            manager1 manager2
```

Both references point to the same object.


# 🧪 Let's Test It

```csharp
class Program
{
    static void Main()
    {
        BankManager manager1 = BankManager.Instance;

        BankManager manager2 = BankManager.Instance;

        manager1.ApproveLoan(50000);

        Console.WriteLine(
            Object.ReferenceEquals(manager1, manager2)
        );
    }
}
```

Output:

```text
Bank Manager Created
Bank Manager approved a loan of 50000
True
```

Look carefully.

We requested the object twice:

```csharp
BankManager.Instance
BankManager.Instance
```

But the constructor executed only once.

And:

```csharp
Object.ReferenceEquals(manager1, manager2)
```

returns:

```text
True
```

Therefore:

```text
manager1 ──────────┐
                   │
                   ▼
              ┌───────────┐
              │BankManager│
              │ Object #1 │
              └───────────┘
                   ▲
                   │
manager2 ──────────┘
```

**Two references.**

**One object.**


# 🧠 Object Creation Without Singleton

Normally, we can do:

```csharp
BankManager manager1 = new BankManager();
BankManager manager2 = new BankManager();
BankManager manager3 = new BankManager();
```

Conceptually:

```text
manager1 ──► Object #1
manager2 ──► Object #2
manager3 ──► Object #3
```

Three references.Three objects.

# 🧠 Object Creation With Singleton

With Singleton:

```csharp
BankManager manager1 = BankManager.Instance;
BankManager manager2 = BankManager.Instance;
BankManager manager3 = BankManager.Instance;
```

We get:

```text
manager1 ──┐
           │
manager2 ──┼──► Object #1
           │
manager3 ──┘
```

Three references.

**One object.**

That is the heart of Singleton.


# 🌱 Why Do We Need Singleton?

Sometimes an application needs a single shared instance. Examples include:

### ⚙️ Configuration Manager

```text
              Configuration
                    │
             ONE SOURCE
                    │
       ┌────────────┼────────────┐
       ▼            ▼            ▼
    Module A     Module B     Module C
```

### 📝 Logger

```text
                  Logger
                    │
               ONE LOGGER
                    │
       ┌────────────┼────────────┐
       ▼            ▼            ▼
    Service A    Service B    Service C
```

### 🗃️ Shared Application Component

```text
                Shared Component
                       │
            ┌──────────┼──────────┐
            ▼          ▼          ▼
         Module A   Module B   Module C
```

The important question is always:

> **"Why should this component have exactly one instance?"**

# 🐢 Lazy Initialization

Look at our code:

```csharp
if (_instance == null)
{
    _instance = new BankManager();
}
```

The object is not created when the application starts. It is created **only when someone asks for it**.

This is called:

> **Lazy Initialization**

Think of a King who doesn't enter the palace until someone actually needs him.

```text
Application Starts
        │
        ▼
No BankManager created
        │
        │
        ▼
Someone requests Instance
        │
        ▼
Create BankManager
        │
        ▼
Return object
```

So Singleton can provide both:

```text
ONE INSTANCE
      +
LAZY CREATION
```

# 🌍 Singleton in a Real Application

Imagine an enterprise application.

```text
                    Application
                         │
       ┌─────────────────┼─────────────────┐
       ▼                 ▼                 ▼
    Customer          Product            Order
    Module             Module            Module
       │                 │                 │
       └─────────────────┼─────────────────┘
                         ▼
                    Shared Object
                         │
                         ▼
                    ONE INSTANCE
```

Every module can access the same controlled instance.

# ⚠️ But Here Comes the Mentor's Warning

Students sometimes hear:

> **"Singleton means one object, so Singleton is always good."**

No! A design pattern is not automatically good just because it is a design pattern.

Ask:

```text
Do I really need ONE instance?
          │
          ▼
        YES
          │
          ▼
Does sharing this object make sense?
          │
          ▼
        YES
          │
          ▼
      Consider
      Singleton
```

Otherwise:

```text
Needlessly using Singleton
          │
          ▼
Global State
          │
          ▼
Tight Coupling
          │
          ▼
Harder Testing
          │
          ▼
Maintenance Problems
```

# 💉 Singleton and Dependency Injection

Modern .NET applications often use **Dependency Injection** instead of manually writing Singleton logic.

For example:

```csharp
builder.Services.AddSingleton<BankManager>();
```

Now the .NET DI container manages the Singleton lifetime.

Conceptually:

```text
              ASP.NET Core
                   │
                   ▼
             DI Container
                   │
                   ▼
              BankManager
                   │
              ONE INSTANCE
                   │
       ┌───────────┼───────────┐
       ▼           ▼           ▼
   Controller A Controller B Service C
```

This is often cleaner than manually maintaining:

```csharp
private static BankManager? _instance;
```

The framework manages the lifetime for us.

# 🔄 Singleton Lifetime in DI

When we say:

```csharp
builder.Services.AddSingleton<MyService>();
```

we are asking the DI container to maintain a singleton lifetime for that service within the application's service-provider lifetime.

Compare the common DI lifetimes:

```text
AddSingleton
     │
     ▼
ONE INSTANCE
     │
     ▼
Shared for application/service-provider lifetime


AddScoped
     │
     ▼
ONE INSTANCE PER SCOPE
     │
     ▼
ASP.NET Core → typically one per HTTP request


AddTransient
     │
     ▼
NEW INSTANCE WHEN REQUESTED
```

This distinction is extremely important in ASP.NET Core.

# 🏗️ Singleton in Software Architecture

As a beginner, Singleton may look like:

```csharp
private static MyClass _instance;
```

But as a software engineer, you should think:

```text
                  Singleton
                      │
       ┌──────────────┼──────────────┐
       ▼              ▼              ▼
  Object Lifetime   Shared State   Controlled Access
       │              │              │
       └──────────────┼──────────────┘
                      ▼
               Application Design
```

The real question is not:

> "How can I create a Singleton?"

The real question is:

> **"What should be the lifetime and ownership of this object?"**

# 🎯 Singleton vs Normal Object

| Feature             | Normal Object        | Singleton                     |
| ------------------- | -------------------- | ----------------------------- |
| Number of instances | Many                 | One controlled instance       |
| Constructor         | Usually public       | Usually private               |
| Access              | `new`                | `Instance` / DI               |
| State               | Independent          | Shared                        |
| Creation            | Explicit             | Controlled                    |
| Lifetime            | Depends on ownership | Singleton lifetime            |
| Common use          | Business objects     | Shared application components |

# 🌟 The Singleton Ecosystem

```text
                       Singleton
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
          One Object   Shared Access   Lifetime
              │            │            │
              ▼            ▼            ▼
          Instance      Instance      DI Container
              │
              ▼
       ┌──────┼──────┐
       ▼      ▼      ▼
    Module A Module B Module C
       │      │      │
       └──────┼──────┘
              ▼
        SAME INSTANCE
```



# Mentor's Golden Wisdom

> **"Students, don't memorize Singleton as `private constructor + static variable + Instance property`. First understand the problem. Sometimes an application needs many objects. Sometimes it needs one shared object. When the business or technical requirement says there should be one controlled instance, Singleton becomes a candidate solution."**

> **"And remember one more thing: Singleton is not simply about creating one object. It is about controlling object creation, object lifetime, and access to shared state. In modern ASP.NET Core applications, you will often express this requirement through Dependency Injection and `AddSingleton()` rather than manually implementing the pattern."**

# 🏁 Final Takeaway

```text
              Singleton Pattern
                      │
                      ▼
              "ONE OBJECT"
                      │
          ┌───────────┼───────────┐
          ▼           ▼           ▼
       Private      Static       Public
     Constructor    Instance     Accessor
          │           │           │
          └───────────┼───────────┘
                      ▼
               ONE INSTANCE
                      │
          ┌───────────┼───────────┐
          ▼           ▼           ▼
       Client A    Client B    Client C
          │           │           │
          └───────────┼───────────┘
                      ▼
                SAME OBJECT
```

> **"As a Transflower mentor, I always tell my students: don't learn Singleton as a trick to prevent `new`. Learn it as a lesson in object lifetime and ownership. The moment you start asking, 'Who should create this object? Who should own it? How long should it live? Who should share it?' — you are no longer just writing code. You are beginning to think like a software architect."**
