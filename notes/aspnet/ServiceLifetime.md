
# *Understanding Service Lifetimes – Like Hiring Employees in an Organization*

> *“Let me tell you a story. Imagine you are building a company. And every time your company receives a new project (HTTP Request), you need a team to work on it. But now comes the twist — you don’t always need the same type of employee behavior for every team member. Some people work temporarily, some stay for one project, and some are always there — loyal and permanent. Just like this, services in ASP.NET Core have lifetimes.”*

### 🧑‍🔧 Scene 1: The Temporary Worker — `AddTransient`

> “Every time you need someone — you hire a fresh face. No memory of past work, and they’re gone after they’re done.”

That’s what **`AddTransient`** does.

```csharp
builder.Services.AddTransient<ILeaveService, LeaveService>();
```

* Every time the system needs `ILeaveService`, a **new object** of `LeaveService` is created.
* This works best for **lightweight, stateless services** — like logging, calculators, or simple validators.

> 🔁 *"Even in the same HTTP request, if you need it twice, you'll get two different objects!"*

### 🧑‍💼 Scene 2: The Project-Based Hire — `AddScoped`

> “Now imagine you bring someone in and say: ‘You’ll stay with this project from start to end. But once the project ends, you’re done.’ That’s **scoped**.”

```csharp
builder.Services.AddScoped<IPayrollService, PayrollService>();
```

* One `PayrollService` instance is created **per HTTP request**.
* If multiple controllers or classes within that **same request** ask for it, they all get the same instance.
* But when a new HTTP request comes in, a **new instance** is created.

> 🔁 *"Scoped is perfect when you want to share data across different layers during one HTTP operation — like when processing user input, saving to the database, and generating a response."*


### 🧓 Scene 3: The Veteran Employee — `AddSingleton`

> “Now think about the core HR manager — once they’re in, they stay for the company’s lifetime. You ask them anything at any time — they’ll respond the same way, because they’re **always there**.”

```csharp
builder.Services.AddSingleton<IHRService, HRService>();
```

* One instance of `HRService` is created the **first time it’s requested**.
* That **same instance** is reused **forever** for all future requests.

> 🧠 *"Singletons are useful for services that maintain shared state, configuration, or caching across requests. But beware — they can lead to unintended data sharing in multi-threaded apps if not handled carefully!"*

### 🧭 Putting It All Together — Quick Summary

| Lifetime  | Instance Created | Scope                             | Best Use Case                          |
| --------- | ---------------- | --------------------------------- | -------------------------------------- |
| Transient | Every request    | Even within the same HTTP call    | Stateless operations like utilities    |
| Scoped    | Per HTTP request | Same instance throughout one call | Per-request DB context, business logic |
| Singleton | Once             | Shared across all requests        | Config, caching, app-wide constants    |

### 💡 Mentor’s Tip

> “Don’t overthink which one to use. Start with `Scoped` — it fits most scenarios in web applications. Use `Transient` when objects are lightweight and stateless. Use `Singleton` only when you're sure it's safe to share state across all users and requests.”

### 🔧 Final Example: From Your `Program.cs`

```csharp
builder.Services.AddTransient<ILeaveService, LeaveService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddSingleton<IHRService, HRService>();
```

> *"This simple configuration line determines how often your application will create, reuse, or share the service instances. It's like designing your own resource strategy for performance, memory, and correctness."*

