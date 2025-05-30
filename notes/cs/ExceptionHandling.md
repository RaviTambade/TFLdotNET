
## **“Why Even a Perfect Code Needs a Parachute”**

*Students, gather around. Let me tell you a little story — not about code, but about life. Because life and programming have a lot in common.*


### **Scene 1: The Confident Skydiver**

> *“Imagine a skydiver preparing for the perfect jump. Weather is clear, the instructor has trained well, the parachute is packed, and everything seems right. But — what if the parachute doesn’t open? What if something goes wrong mid-air?”*

Just like that skydiver, even our best-written programs can face sudden issues at runtime — things we didn’t expect. These are called **exceptions**.


### **What Are Exceptions?**

They are **unexpected problems** that occur when your program is running. Everything compiles fine, but *boom* — it crashes during execution.

Examples?

* A student forgets to enter a number → **`FormatException`**
* Dividing by zero → **`DivideByZeroException`**
* Trying to open a missing file → **`FileNotFoundException`**
* Stack memory runs out → **`StackOverflowException`**


### **Let’s Experiment**

Here’s a basic example:

```csharp
int a, b=0;
Console.WriteLine("My program starts");
try 
{
    a = 10 / b; // 💥 Runtime error here!
}
catch(Exception e)
{
    Console.WriteLine(e.Message); // 👨‍🚒 Handles the crash gracefully
}
Console.WriteLine("Remaining program");
```

> *“Instead of letting the program crash like a plane with no parachute, the `try-catch` block gives it a soft landing.”*


### **.NET Built-in Exception Classes**

.NET Framework already provides a rich hierarchy of exception classes. These are like pre-built first-aid kits.

Let’s look at a few:

| Category  | Common Exceptions                                                                                |
| --------- | ------------------------------------------------------------------------------------------------ |
| 🧠 Logic  | `DivideByZeroException`, `InvalidCastException`, `NullReferenceException`                        |
| 📚 Data   | `FormatException`, `IndexOutOfRangeException`, `ArgumentException`, `ArrayTypeMismatchException` |
| 💻 System | `OutOfMemoryException`, `StackOverflowException`, `InvalidOperationException`                    |


### **Custom Exception: Your Own First-Aid Kit**

> *“Sometimes, the default bandages don’t work. You need your own custom solution.”*

In real-world apps, you define your own exception classes for specific conditions.

Let’s see this:

```csharp
class StackFullException : ApplicationException
{
    public string Message;
    public StackFullException(string msg)
    {
        Message = msg;
    }
}
```

And how to use it:

```csharp
public static void Main(string[] args)
{
    StackClass stack1 = new StackClass();
    try
    {
        stack1.Push(54);
        stack1.Push(24);
        stack1.Push(53);
        stack1.Push(89); // 🧨 Too much!
    }
    catch(StackFullException s)
    {
        Console.WriteLine(s.Message); // 🙏 Gracefully informs the user
    }
}
```



### **Mentor’s Reflection**

> *“Writing a program without exception handling is like driving a car without brakes. You may be a great driver, but the world is unpredictable.”*

With `try`, `catch`, `finally`, and custom exceptions, we give our code the **resilience** it needs to survive in the real world — just like people do with patience and preparation.

### ✅ **Takeaway for Students**

1. **Try**: The risky zone.
2. **Catch**: The safety net.
3. **Finally**: Cleanup, no matter what.
4. **Throw**: When you want to raise your own flag.
5. **Custom Exceptions**: When your problem is unique.

---

### Homework Thought

> *“Write a small calculator that uses exception handling for invalid input, division by zero, and throws a custom exception if a result goes beyond 1000.”*

Let the students *not just learn* — but *feel* the need for exception handling.
