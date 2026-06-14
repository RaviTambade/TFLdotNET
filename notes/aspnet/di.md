###  *"Dependency Injection – Building Flexible APIs Together"*

Good morning, students!

Today, I want you to **step into the shoes of a software architect** — someone who not only writes code but designs systems that last, evolve, and stand the test of time.

Now let me tell you a story — not of just code, but of **design wisdom**.

### 🏗️ Scene 1: The Construction Site (Your Web API Project)

Imagine you're building a grand hotel. You’re the architect, and every room (controller) needs access to services like **room service (data access)** and **security (logging)**.

But here’s the catch — you don’t want the room to *know* how to cook or secure itself. Instead, you want **specialists to be assigned** to these tasks.

That’s what **Dependency Injection (DI)** is all about.

It’s like saying:

> *“Let me define what I need, and I trust the system to provide the best person (object) for the job.”*


### 🔧 Step 1: **Define What You Need (Interfaces)**

Just like how an architect gives a **job description** instead of a person’s name — we define **interfaces**.

```csharp
public interface IDataService
{
    void GetData();
}

public interface ILoggingService
{
    void Log(string message);
}
```

Here, you’re not saying *how* to get data or log — you’re just **declaring your needs**.


### 🛠️ Step 2: **Bring in the Specialists (Concrete Classes)**

Now, some skilled professionals apply for the jobs — the ones who *know* how to do the work.

```csharp
public class DataService : IDataService
{
    public void GetData()
    {
        // Fetching data logic
    }
}

public class LoggingService : ILoggingService
{
    public void Log(string message)
    {
        // Logging logic
    }
}
```

These are your **real workers**, the ones who will fulfill the roles defined in the interfaces.


### 🏢 Step 3: **Register Workers with HR (DI Container)**

But how will your rooms (controllers) know who to call?

Well, here comes the **HR department** — `Startup.cs` or `Program.cs`.
You register your specialists here:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    // Register services
    services.AddSingleton<IDataService, DataService>();
    services.AddSingleton<ILoggingService, LoggingService>();
}
```

Now your system knows:

> “If anyone asks for an `IDataService`, send in the `DataService` guy.”

### 🚪 Step 4: **Assign the Services to the Rooms (Constructor Injection)**

Now, when a **room** (controller) is being prepared, the system automatically **injects** the right specialists.

```csharp
[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly ILoggingService _loggingService;

    public ValuesController(IDataService dataService, ILoggingService loggingService)
    {
        _dataService = dataService;
        _loggingService = loggingService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _dataService.GetData();
        _loggingService.Log("Data retrieved successfully.");
        return Ok("Data retrieved and logged.");
    }
}
```

Did you see that? The controller didn’t create or know anything about how the services work — it simply **used** them.


### 🎯 Step 5: **Why Is This Magical?**
Let me tell you what you just built:

* ✅ **Loose coupling** — Your controller doesn’t depend on concrete classes. You can swap `DataService` with `MockDataService` for testing.
* ✅ **Testability** — You can now **unit test** your controller by passing fake services.
* ✅ **Flexibility** — Your app is like LEGO — interchangeable parts.
* ✅ **Maintainability** — If your logging logic changes, just update `LoggingService` — no need to touch the controller.

### ⚙️ Step 6: **Lifetimes — Who Lives How Long?**

Think of lifetimes like **employee contracts**:

* **Singleton**: One permanent employee for the whole application.
* **Scoped**: Hired fresh for every guest (HTTP request).
* **Transient**: Hired every time a service is needed.

You choose based on the **job’s nature**:

```csharp
services.AddTransient<ILoggingService, LoggingService>();
services.AddScoped<IDataService, DataService>();
```

### 🎨 Step 7: **Fine-Tune as You Grow**

As your hotel expands — new services, guests, departments — **Dependency Injection becomes your operations manager**, coordinating everything behind the scenes.

Want to:

* Swap a new logging system?
* Add caching to data access?
* Create test doubles for unit testing?

No problem — because your architecture is ready.

### 🧠 Mentor’s Final Words

Remember, my friends:

> **"Writing code is easy. Designing code that survives change — that’s engineering."**

Dependency Injection helps you do that. It lets you write code that’s **modular, testable, and scalable**.

So the next time you build an ASP.NET Web API, don’t write tightly coupled code.
Let the **framework do the heavy lifting**. Let **Dependency Injection be your silent hero** — always working, always managing, always ready.
