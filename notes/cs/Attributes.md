# **Attributes**

## 🎙️ *Mentor Begins...*

> *"When you walk into a library, how do you know which book is fiction, which one is science, or which is meant for kids? You don’t open every page — you just look at the **labels**."*
>
> *In the same way, when we build applications, we need to **label our code elements** with **extra information** so others — or even the program itself — can **understand how to treat them**. That, my dear students, is what **attributes** are all about in C#."*

## 🧾 **What are Attributes?**

> *Attributes are like metadata stickers we put on classes, methods, or properties to tell the .NET runtime (or other developers) something about how they should behave.*

They allow us to **attach additional information** — not just for documentation, but to **drive dynamic behaviors at runtime**. Think of them as secret tags that make your code smart.

```csharp
[Serializable]
class Person
{
    public string? Name { get; set; }
    public sbyte? Age {  get; set; }
}
```

> *Here, the `[Serializable]` tag is like a badge that says: “Hey .NET, this class can be saved and restored as binary data!”*

## 🎯 **Why Use Attributes? A Real-World Perspective**

Let me tell you **why attributes matter** — and not just in exams!

### 🧠 1. **Metadata for Smarter Code**

> *You can tag a method to say “Only admins can use this,” or a class to say “This needs to be logged.” These tags aren’t just labels — they change how the system behaves.*

### 📖 2. **Declarative Programming**

> *No need to write 10 lines of code to configure something. Just declare it with an attribute — clean, readable, and powerful.*

### 🔧 3. **Extensibility & Framework Magic**

> *Frameworks like ASP.NET, Entity Framework, and xUnit love attributes. They look for these tags to know what to do.*

* `[HttpGet]` in ASP.NET
* `[Key]` in EF Core
* `[Fact]` in xUnit

> *Behind the scenes, your attribute is telling the framework exactly what this piece of code is meant for.*


## 📌 **Built-in Attributes Example: Serializable**

```csharp
[Serializable]
class Person
{
    public string? Name { get; set; }
    public sbyte? Age { get; set; }
}
```

> *This is like saying: “Dear .NET, please allow me to save this object to disk and restore it later.”*

## 🔨 **Let’s Create Our Own Attribute: Custom Annotations**

> *Sometimes, you want to define your own rules. Like marking who’s allowed to access what. Let’s build our own attribute for permission checks.*

### 👷‍♂️ **Step 1: Define the Attribute**

```csharp
namespace TFL.Annotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PermissionRequiredAttribute : Attribute
{
    public string Permission { get; }

    public PermissionRequiredAttribute(string permission)
    {
        Permission = permission;
    }
}
```

> *This attribute says: “This class requires certain permissions.” You can even apply it multiple times.*


### 🛡️ **Step 2: Use It on a Class**

```csharp
using TFL.Annotations;
namespace TFL.Security;

[PermissionRequired("administrator")]
[PermissionRequired("manager")]
public class Credentials : ICredentials
{
    public string[] GetCredentials()
    {
        throw new NotImplementedException();
    }
}
```

> *Now the class is tagged — just like those library books. Any tool or logic can look at these tags and enforce rules.*

## 🔍 **Step 3: Reflection – Reading Tags at Runtime**

> *Reflection is like a scanner — it can read these labels even while the program is running.*

```csharp
Type t = typeof(Credentials);

IEnumerable<string> permissions = 
    t.GetCustomAttributes(typeof(PermissionRequiredAttribute), true)
     .Cast<PermissionRequiredAttribute>()
     .Select(x => x.Permission);

foreach (string permission in permissions)
{
    Console.WriteLine(permission);
}
```

> *And just like that — we’ve built a dynamic, extensible permission system, all by tagging and reading attributes!*


## ✨ **Where Are Attributes Used in Real Life?**

> *You’ve already seen them in action, maybe without realizing it.*

* **\[Obsolete]** – Warns developers not to use deprecated code.
* **\[Required], \[MaxLength]** – Used in data validation.
* **\[Route], \[Authorize]** – Used in web APIs.
* **\[TestMethod], \[Fact]** – Used in unit testing.

> *These small labels create **huge behavioral impact** — and that’s the magic of attributes.*


## 🧠 **Mentor's Insight: Attributes + AOP = Superpowers**

> *Ever wanted to add logging, validation, or security **without cluttering business logic**? That’s where Attributes shine — they make it possible to use **Aspect-Oriented Programming (AOP)**, keeping your core logic clean and concerns separate.*


## 🏁 **Conclusion: A Small Tag, A Big Change**

> *In life, sometimes a badge defines behavior — "Doctor", "Engineer", "Student". In code, Attributes are those badges.*

> *They are **lightweight**, **powerful**, and **everywhere**. Learn to use them, create them, and read them — because mastering Attributes means mastering flexible, modern, and maintainable C# code.*
