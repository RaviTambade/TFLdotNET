## IComparable , IComparer


### 🌅 Scene 1: A Real-Life Hiring Situation

*“Imagine you're the HR manager of your own startup. Three fresh graduates walk into your office — Vedant, Abhay, and Shubham. All excited. All dreaming. But you’ve got one position and you need to decide who fits best.”*

"Now tell me this honestly... how would you **compare** them?"

Students murmur: “Based on their salary expectations?”, “Based on their experience?”, “Maybe based on attitude?”

Exactly. And *that* is where our story begins.


### 📘 **Chapter 1: The Tale of IComparable – Self-Aware Objects**

Let me tell you about this very honest employee class — **Employee**.

This class knows how to **compare itself** with another employee.
It has a built-in nature — just like some students who always say:

> *"Sir, compare me with others based on my marks. I’ve worked hard, and that should speak for itself!"*

That’s what `IComparable` does.

```csharp
public class Employee : IComparable<Employee>
{
    public string Name { get; set; }
    public int Salary { get; set; }

    public int CompareTo(Employee other)
    {
        return Salary.CompareTo(other.Salary); // Sort by salary by default
    }
}
```

🔍 **Real-World Analogy**:
Think of `IComparable` like someone with a **natural ranking system** inside them.

"Compare me by salary — that’s my identity," the object says.

So, when you do:

```csharp
employees.Sort();
```

It just works — no questions asked.
Because each employee *knows* how to compare itself.


### 📘 **Chapter 2: The Tale of IComparer – External Judges**

But what if one day your CEO walks in and says:

> “Forget salary. Sort candidates by **name**. Or maybe by **age** tomorrow.”

Now what? You can’t rewrite your Employee class every time!
So, what do you do?

You **bring in a judge**. An external one. That’s `IComparer`.

```csharp
public class SalaryComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        return x.Salary.CompareTo(y.Salary); // Compare from outside
    }
}
```

Now the Employee doesn’t care about comparison anymore.
The **judge (comparer)** makes the decision.

Use it like this:

```csharp
employees.Sort(new SalaryComparer());
```

### 💡 **Mentor’s Tip**: Know When to Use What

| Scenario                                                     | Use `IComparable` | Use `IComparer` |
| ------------------------------------------------------------ | ----------------- | --------------- |
| You always sort by a natural field (like Age or Salary)      | ✅ Yes             | ❌ Not needed    |
| You want **multiple ways** to compare (by Name, Age, Salary) | ❌ Avoid           | ✅ Perfect       |
| You can’t change the original class (it’s from a library)    | ❌                 | ✅ Yes           |


### 🛠️ **HR Example Recap**

```csharp
// IComparable version
public class Employee : IComparable<Employee>
{
    public string Name { get; set; }
    public int Salary { get; set; }

    public int CompareTo(Employee other)
    {
        return Salary.CompareTo(other.Salary);
    }
}
```

```csharp
// IComparer version
public class NameComparer : IComparer<Employee>
{
    public int Compare(Employee x, Employee y)
    {
        return string.Compare(x.Name, y.Name);
    }
}
```

```csharp
// Usage in Main
employees.Sort(); // Uses CompareTo inside Employee
employees.Sort(new NameComparer()); // Uses external logic
```


### 💬 Mentor’s Final Words

> *“In life, sometimes we know how we should be judged — that’s `IComparable`.
> But sometimes, we let others judge us by different standards — that’s `IComparer`.*
> Choose wisely, young developers.”\*

👨‍🏫 *"So whether you’re comparing employees, products, or even marks in a leaderboard — always ask yourself: Should the class decide? Or should an external logic decide? That’s the whole game!"*

 
