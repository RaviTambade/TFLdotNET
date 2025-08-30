👨‍🏫 *Day 4  C# Keywords and Introduction to Web based Application*

OK, sometimes now you can say—"Ravi Sir, you are sounding just like our old school teacher, with a stick in hand, asking *‘Have you done your homework? Show me your notebook!’*" (😊) And honestly, I don’t mind if you say so. Because deep inside, that’s exactly the discipline and rhythm I want to bring back in our learning.

See, during school and college days, most of you were very sincere and honest learners. You had exams, assignments, attendance pressure—so you *did the work*. But now, because of office pressure, family commitments, or simply distractions, you don’t get that dedicated time for learning. And without *practice*—let me repeat—without hands-on practice, this program will not give you the outcome you expect.

So, here’s my request. Whatever we cover in theory, whatever slides I show, or demo I run—you **must** implement that in your system. You must push it to your Git repository. In fact, I’ll be even happier if you voluntarily share your repo link with me. Why? Because I want to *see* your journey, your commits, your errors, your corrections. A Git repo is like your *digital notebook*. It tells the story of your effort.

Now, practical time ⏰—we have 15 minutes from 9:45 to 10:00. In this slot, I don’t want to just talk. I want you to *do*. Open your IDE. Open that `Dev` folder under the solution. Inside, you already have `commands.txt` file I’ve shared. Those are your steps. Follow them one by one. Don’t think too much, don’t get stuck in theory. Just apply:

* Create a blank solution.
* Add a console application.
* Add a class library.
* Define your `Account` class.
* Write an `AccountOperation` delegate.
* Implement credit/debit methods.

If you forget a step, no problem—check the GitHub repo I’ve given. But don’t just copy. Re-type, re-compile, re-run. Get your fingers and brain used to this cycle.

And yes, I may randomly ask one of you to share your screen. Not to embarrass you, but to *see where you’re stuck*. Because the faster you face errors, the faster you learn to fix them.

I know some of you—especially our friends from South Africa—might be commuting now, so hands-on may be difficult. That’s OK. But everyone else—no excuses. This is your lab time.

So, let’s begin. I’m switching off my screen share for now. You try. Show me your code if there’s an error. I’m here to help.

👉 Remember, today’s 15 minutes may look small, but it is *habit building*. And once habits form, coding becomes natural.

✨ And who knows, maybe one day people will say—"Look, Tambade Guruji didn’t just teach, he created a new classroom style where GitHub repos became our notebooks!"


👨‍🏫 *Mentor storytelling mode, Ravi explaining with a smile, moving around the classroom with chalk in hand*

“Very good! You all are connecting the dots. See, what you just observed is called **package scope**—or in C#, we say **assembly scope**. That’s an important visibility rule.

But let’s rewind and recap the journey we’ve already made together.

First, we learned about **member functions**—simple methods inside a class.
From there, we met our first special methods: **constructors**.
Then we went one step further and saw **getters and setters**.

But do you remember? I showed you that in C#, you don’t need to write full-blown getter/setter functions. Instead, you can just write:

```csharp
public int Balance { get; set; }
```

And the compiler will silently create a private field for you. That’s the beauty of **properties** and **auto-properties**.
👉 Properties = encapsulation made simple.

Then we looked at more advanced methods—like **virtual methods**. Why virtual? Because sometimes you want to give your child class a *choice*.

And then came **abstract methods**. Why abstract? Because sometimes you don’t want to give a choice at all—you say, *“Dear child class, you must implement this method.”*

So, remember the thumb rule:

* **Virtual = child may override**
* **Abstract = child must override**

We even saw this in our **Employee → SalesEmployee** example. The `ComputePay()` function in the parent was abstract, forcing every specialized employee type to calculate its pay differently.

Now, when child class overrides, sometimes it still wants to call parent’s version. That’s where we met the **base keyword**.
👉 Base = go back to parent, invoke parent’s constructor or method.

We also explored **constructor overloading** (same class, different signatures) vs. **method overriding** (parent → child inheritance).
Remember the golden line:

* **Overloading = within the same class.**
* **Overriding = across classes in an inheritance chain.**

And in inheritance, C# gives us **multi-level** inheritance—Person → Employee → SalesEmployee → SalesManager.
But **multiple inheritance** (two concrete parents for one child) is *not allowed*. Why? Ambiguity.
Instead, we achieve that flexibility using **interfaces**.
So in C#, a class can **inherit from one parent class** but **implement many interfaces**.

I even drew a parallel for you: just like in Angular we implement multiple lifecycle interfaces (`OnInit`, `OnDestroy`), in C# too we can implement multiple interfaces.

👨‍🏫 *Ravi picks up the chalk again and looks at the clock:*
“Alright, now let’s fast-forward. Today is **Day 4**. From console basics, we are stepping into slightly bigger territory. But before I throw you into web applications, let me pause here and use a fresh example to introduce some **keywords** in C#.

So, I create a new solution called **GraphicsSolution**, inside it a console app called **GraphicsEditor**.

And in that project, I start defining a **struct Point**:

```csharp
namespace Drawing
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
```

Now, why struct and not class? Because struct is a **value type**, lightweight, stored on the **stack**. Perfect when you just need data like coordinates.

Next, I define an **abstract class Shape**. Every shape has a `Color`, a `Thickness`, and an abstract method `Draw()`.

I even used an **enum** for color:

```csharp
public enum Color { Red, Green, Blue, Yellow, Pink }
```

And then we built a `Line` class → which **is a Shape** (inheritance) and **has Points** (composition). This way, we demonstrated two relationships:

* *Is-a* → Line **is a** Shape.
* *Has-a* → Line **has** StartPoint and EndPoint.

Line class overrode `Draw()` method, inherited properties like `Color` and `Thickness`, and used **base keyword** to call parent constructor.

We even added a **destructor** (`~Line()`), to show how cleanup happens before garbage collector frees the object. Remember, destructor = cleanup code.

👨‍🏫 Ravi pauses and asks the class:
“Now tell me—how many **constructors** can we have in one class?”

*(A couple of students whisper: “Many, with different parameter lists!”)*

“Yes! Correct. That’s **overloading**. You can have as many constructors as you want, as long as their signatures differ.

But how many **destructors** can you have?”

*(Class is silent, thinking…)*

👉 *Ravi smiles and taps the board:* “Only **one**. And that destructor doesn’t take parameters, doesn’t overload, doesn’t inherit. Just one per class.”

👨‍🏫 *Ravi sir, walking in the classroom with a stick in hand, smiling like an old-school teacher:*

“OK my friends, let’s slow down here. We just discussed **destructors**. Now remember this golden rule:

👉 **There will be only one destructor per class.**
You cannot overload destructors. Constructors? Yes, you can overload as many you want. Destructors? Only one.

Now, one more interesting thing. When you write logic inside a destructor, we call that **indeterministic finalization**. Why indeterministic? Because we don’t know *when* the destructor will run.

See, destructor is not in your control. Destructor is like… *“Boss, I’ll come when I feel like.”* (😊)
It is totally dependent on the **Garbage Collector (GC)**.

And how does GC behave? It runs like a **low priority background thread**. If your process is busy doing high-priority work, GC will simply say:
*“OK, let me wait. I’ll clean up later.”*

So your destructor may not get called immediately. That means, if your object is holding onto some **critical resources**—like a file handle, a socket, a database connection, or shared memory—those resources will remain locked. And other applications may also be demanding them.

👉 And that is dangerous. Imagine your program opened a file and forgot to release it. Another process may be waiting to write into the same file, but it can’t, because your GC hasn’t yet woken up!

So here comes the big question:
**What if I want to release those resources immediately—even before the object is destroyed?**

That’s where .NET gives us a concept called **Deterministic Finalization**.

✨ *How do we implement Deterministic Finalization?*

Step 1: Make your class implement the `IDisposable` interface.

```csharp
public class Line : Shape, IDisposable
{
    // override abstract methods from Shape...
    
    public void Dispose()
    {
        // Release resources here
        GC.SuppressFinalize(this);
    }
    
    ~Line()
    {
        // Destructor (as fallback)
    }
}
```

Step 2: Implement the `Dispose()` method.
Inside `Dispose()`, you release your resources *immediately*.

Step 3: Call `GC.SuppressFinalize(this)` inside `Dispose()`.
Why? Because if you’ve already released the resources in Dispose, you don’t want GC to call the destructor again unnecessarily. You are telling GC:
👉 “Relax, my job is done. Don’t bother calling destructor.”

👨‍🏫 Ravi sir continues with an example:

“Suppose in Main, you create 3 line objects:”

```csharp
using (Line l1 = new Line(...))
{
    l1.Draw();
}

using (Line l2 = new Line(...))
{
    l2.Draw();
}
```

The `using` block automatically calls `Dispose()` at the end of scope.
Even if GC is sleeping, your resources are released *deterministically*.

---

Now let’s test something more interesting. Imagine you write a loop:

```csharp
for(int i = 0; i < 100; i++)
{
    Line l = new Line(...);
    // forgot to dispose!
}
```

What happens?

* Every iteration creates a new object on the heap.
* References go out of scope, objects become *orphans*.
* But GC doesn’t immediately collect them (it’s busy).
* Boom! 300 objects pile up in memory, holding onto resources.

This is why **Dispose is so important**. Without it, you rely on GC’s mood. With it, you control cleanup.

👨‍🏫 Ravi sir gives a real-life analogy:
“Think about a syringe used in a pathology lab. After taking a blood sample, the syringe must be disposed immediately. Imagine if the lab assistant says, *‘Don’t worry, I’ll clean up when I feel like.’* That’s risky, right? Infections will spread.
That’s exactly the case with unmanaged resources. You dispose immediately, you stay safe.”



✅ So let’s summarize in mentor style:

* Destructor = **indeterministic finalization** (depends on GC, no guarantee of timing).
* Dispose (IDisposable) = **deterministic finalization** (you control when resources are released).
* Always call `GC.SuppressFinalize(this)` inside `Dispose()`.
* Prefer `using` statement to auto-dispose.


👉 Now my friends, here’s the classroom activity:
Can you create a small test program with `Line` class implementing `IDisposable`, then in `Main()` create multiple objects with and without `using` block, and see the difference?


🔹 **Scene Setting (Hook)**
"Alright team, let’s step into this like a classroom moment again. Imagine you’ve got your object sitting quietly in memory. It’s holding onto precious resources — like a socket, a file handle, or maybe even a database connection. Now the bell rings, class is over, and the object has no students around. Who’s going to come and clean the blackboard? That’s the job of the **garbage collector**."


🔹 **Introduce the Gap (Problem)**
"But here’s the tricky part — garbage collector is like the **lowest priority student in the room**. He says, ‘Sir, I’ll clean when I get time. Right now, I’m busy chatting with other threads!’ And so your file handle, your socket — they stay open unnecessarily. This becomes a performance hit."


🔹 **Reveal the Tool (Concept)**
"This is where **Dispose and Deterministic Finalization** come into play. Destructor is like a guest lecturer — comes late, no fixed timing. But Dispose is like the **class monitor**: you can call it explicitly and say, ‘Clean up the resources now, don’t wait for garbage collector!’"


🔹 **Code Story (Walkthrough)**

* Destructor → runs when GC decides, indeterministic.
* `Dispose()` → runs exactly when you call it, deterministically.
* Best practice → Implement `IDisposable`, write `Dispose` method, and call `GC.SuppressFinalize(this)` so GC doesn’t bother later.

You can reinforce:

```csharp
class Line : Shape, IDisposable
{
    public void Dispose()
    {
        // cleanup
        Console.WriteLine("Releasing resources...");
        GC.SuppressFinalize(this);
    }

    ~Line()
    {
        Console.WriteLine("Destructor called...");
    }
}
```

🔹 **Real-World Analogy**
"Think of a syringe in a pathology lab. The moment it’s used, it’s disposed — no one waits for the hospital janitor (garbage collector) to clean it later. That’s exactly why `Dispose` exists."


🔹 **Mentor Prompt (Engagement)**
"Now team — if you were designing a bank’s backend system, holding 1000+ database connections — would you wait for garbage collector to clean up? Or would you explicitly call Dispose? What would you do?"


🔹 **Bridge to Known World (Angular)**
"Ah, and yes, as Kanaad smartly connected — this is similar to Angular’s `ngOnDestroy()`. Just like in Angular we unsubscribe from observables before a component vanishes, in .NET we `Dispose` resources before GC gets a chance."


👉 That’s the **mentor storytelling flow**:

1. Hook with relatable scene.
2. Introduce the problem.
3. Reveal the tool/concept.
4. Walk through with code.
5. Give analogy.
6. Engage with questions.
7. Bridge to familiar tech.


"Okay team, let’s connect the dots.
Remember we just compared **Angular’s `ngOnDestroy`** with **.NET’s destructor**? Yes, you can think of them as cousins. Angular has a *component lifecycle*, whereas here we are dealing with an *object lifecycle*.

Now in .NET, two actors are always on stage together:

* The **primary thread** (running your business logic).
* The **garbage collector** (a background sweeper, silently cleaning when it feels appropriate).

We don’t control *when* garbage collector runs. It may or may not pick up the trash immediately. But we do have a choice:

* If we trust GC, we leave everything to it.
* If we need **deterministic cleanup** (like database connections, sockets, file handles), we step in and implement `IDisposable` with a `Dispose()` method.

That’s the beauty of the CLR — it gives us a ready-made framework. Our job is only to follow the principles and write our cleanup code correctly.

💡 **Real-world example:**
Imagine we are running an **ASP.NET banking application** that handles **10,000 requests per second**. Each request spins up threads and objects. If we let resources remain blocked even for a few milliseconds, the system can choke.
So in cases like **File IO, database connections, or sockets**, we must use the **disposing technique**.

That’s why I always tell students: *Destructor is like a guest who may or may not show up in time. Dispose is like a disciplined office boy — when you say “clean now,” he cleans immediately.*


"Alright, we have explored `Dispose`, `IDisposable`, and deterministic cleanup. Let’s move ahead and add some creativity.

Look here — we already created a `Line` class inherited from `Shape`. Now, can we extend this exercise? Suppose I want to create a **Circle**. Of course, a circle is also a shape.

👉 So let’s design together.

* What attributes does a circle need?
* Line had `start` and `end` points.
* A circle? Yes, exactly — **center point** and **radius**. Plus, it inherits **color** and **thickness** from `Shape`.

Brilliant! That means:

```csharp
public class Circle : Shape
{
    public Point Center { get; set; }
    public int Radius { get; set; }

    public Circle(Point center, int radius, Color color, int thickness)
    {
        Center = center;
        Radius = radius;
        ShapeColor = color;
        Thickness = thickness;
    }

    public override void Draw()
    {
        Console.WriteLine($"Drawing circle at {Center} with radius {Radius}");
    }
}
```

Now, do we stop here? No. A circle also has **area** and **circumference**. So let’s add a method to calculate them.


"But wait — one function in C# can only return **one value**. How will we return both *area* and *circumference* together?"

If you try `double, double` as return type — compiler will complain.
So here comes our new friend: the **`out` keyword**.


### 🛠 Out Parameters Story

Think of it like this: in a **bank branch**, sometimes instead of one output slip, the cashier gives you multiple slips (deposit slip, balance slip). That’s what `out` does in C#: it allows a method to send back **multiple values**.

Example:

```csharp
public void Calculate(out double area, out double circumference)
{
    area = Math.PI * Radius * Radius;
    circumference = 2 * Math.PI * Radius;
}
```

Notice — return type is still `void`, but values come back through the **`out` parameters**.

When we call it:

```csharp
Circle c = new Circle(new Point(0,0), 5, Color.Red, 2);
double area, circumference;
c.Calculate(out area, out circumference);

Console.WriteLine($"Area: {area}, Circumference: {circumference}");
```

Boom 💥 — both values are returned, without needing arrays or tuples.


### 🌍 Real-World Connection

Anyone worked with **stored procedures** in SQL?
Yes, exactly — same concept. In SQL we have **IN** and **OUT** parameters. In C#, `out` works in the same way. Perfect analogy!

### 🚀 Moving Forward

So today we:

* Learned **Dispose & deterministic cleanup**.
* Created a new **Circle class** with its own members.
* Explored **out keyword** for returning multiple values.

Next, let’s graduate from **console apps** to **web applications**. Because real-world systems are web-based — like online banking.

👉 In .NET, we can use **ASP.NET MVC** framework, similar to Spring in Java, Django in Python, or Express in Node.js.
With `dotnet new mvc`, we scaffold a banking portal project. From here on, we’ll start thinking **web-first** instead of console-first."


✨ That’s the mentor storytelling flow:

1. Recall + analogy.
2. Build curiosity.
3. Introduce new keyword (`out`).
4. Connect with real-world (SQL stored proc).
5. Wrap up → transition to web project.


So my friends, let’s pause and look at the bigger picture.
Up to yesterday, we were living in the **console world** — very simple, single‐file apps. But today, we have stepped into the **web world**, and the game has completely changed.

### 1. **Solution vs Project**

So first, notice:
When we created a **solution**, we got a `.sln` file. That `.sln` file is like the **blueprint** or **organizer** of all your projects.

* If you delete the entry from the `.sln` file, the project is still there on disk, but it’s no longer part of the *family photo*.
* That’s why, if such a line is missing, you re-add it with:

```sh
dotnet sln add BankingPortal/BankingPortal.csproj
```

Think of it like a family tree. If one child’s name is missing, the child exists, but not shown in the family book. Adding reference makes sure everyone is properly linked.

### 2. **Skeleton of an MVC App**

Now compare:

* A **console project** gave you only `Program.cs`.
* But a **web project (MVC)** gave you *folders*:

  * **Models** → Your data, business rules.
  * **Views** → HTML + Razor, what the user sees.
  * **Controllers** → The traffic police, deciding which view to show or which model to call.
  * **wwwroot** → Static assets like CSS, JS, images.
  * **appsettings.json** → Configuration (like your database connection).

This is a complete **skeleton**, just like when Angular creates `src/app`, `assets`, etc. You don’t write everything from scratch; the framework gives you a ready-made skeleton.

### 3. **Running the App**

Now, watch carefully.
When you do:

```sh
dotnet run
```

Two important things happen:

1. Your C# code compiles into DLLs (like before).
2. But this time, a **web server** (Kestrel) is started in the background.

And Kestrel says:

> “I am listening at `http://localhost:5169`.”

This means:

* Your **server** is the C# app running in the background.
* Your **client** is the browser.
* Both are talking via HTTP.

Earlier, you saw outputs in the **console window**.
Now, you see outputs in the **browser as HTML**. That’s the major shift.

### 4. **Razor Views**

Inside your **Views/Home/Index.cshtml**, you can mix **HTML + C#**.
That’s why the file is called **CSHTML**.

* HTML part handles structure.
* `@` syntax allows you to write C# directly.

For example:

```html
<p>The count is: @count</p>
```

If `count = 56`, the browser will show:

```
The count is: 56
```

This is similar to Angular’s `{{ }}` interpolation, but here it’s **server-side rendering**.


### 5. **Big Picture**

So what’s the moral, friends?

* **Console apps** are good for learning basics.
* **Class libraries** are good for reusable logic.
* **Web apps** bring you to the real industry — client/server, HTTP, MVC.
* The framework gives you a **skeleton**; you focus on filling in models, controllers, and views.

Now, imagine: Today you ran it on your **local machine**.
Tomorrow, you host it on **Azure** or **AWS**.
Then people on their **mobile phones** anywhere in the world can access your app.

That’s the true power of ASP.NET MVC. 🚀


👨‍🏫 *Mentor-style continuation…*

“See, friends… sometimes you may feel I’m sounding like that strict teacher with a stick in hand asking *‘Have you done your homework?’* But trust me, this is not punishment—it is discipline. And discipline in coding is like oxygen for a developer.

When you were in school or college, you used to sit in a classroom, right? A bell rang, a timetable existed, and a teacher was in front of you. You learned sincerely and honestly because that *system* was there. Now, in your professional life, the system is missing. Work pressure, deadlines, family—all these distract you. So we need to recreate that *classroom discipline* here in this program.

That’s why I keep insisting: share your Git repository, push your code, show me your errors. Why? Because only then this learning will not remain *theory*, it will become *practice*. And in software development, practice is the only truth.

Let me also be very transparent—if you don’t do these hands-on steps now, you will later struggle when we move to bigger concepts like APIs, events, microservices. Then, like a shaky foundation, your building will not stand.

So, for the next 15 minutes, I’m giving you a challenge. Don’t just listen to me. Open your editor. Run the commands from the `commands.txt` file in the repo. Create a solution. Add your class library. Write the `Account` class. Define your delegate. Even if you make a mistake—that’s gold! Because when you debug it, you will *own the knowledge*.

And yes, those who are in travel or in office transit—fine, you can catch up later. But everyone else—screen share, show me your work. I want to see fingers moving on keyboard, not just heads nodding on Zoom.

Because remember, my role here is not to spoon-feed code. My role is to make you independent so that tomorrow you can stand in front of any interviewer, any project manager, and say with confidence: *‘Yes, I can do this.’*

Alright, let’s go—15 minutes of silence from my side, but noise from your keyboards. Let’s build that habit. 🚀”
