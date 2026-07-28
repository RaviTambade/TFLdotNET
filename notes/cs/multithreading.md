# 🧵 Multithreading in C# — A Transflower Mentor's Classroom Journey

**"Good morning, everyone!"**

Today, we're going to learn one of the most important concepts in software engineering—**Multithreading**.

But as always, at Transflower, we won't begin with definitions.

We'll begin with a story.

---

## 🍽️ Imagine You're Running a Busy Restaurant

Imagine you own a popular restaurant.

Customers are continuously placing orders.

One table wants **Paneer Butter Masala**.
Another wants **Veg Biryani**.
Someone else wants **Masala Dosa**.
Another customer only wants coffee.

Now imagine **only one chef** is doing everything.

* Takes the order
* Chops vegetables
* Boils rice
* Fries paneer
* Makes coffee
* Washes utensils
* Serves food

How long will customers wait?

Very long.

Now imagine instead you have **an entire kitchen team**.

One chef prepares vegetables.
Another cooks rice.
Another prepares gravy.
Someone serves customers.
Someone prepares desserts.

Suddenly...

The restaurant becomes faster.

Customers are happier.

Orders are completed simultaneously.

This is exactly what happens inside a computer.

---

# 💻 Computers Face the Same Challenge

Every application performs multiple activities.

Think about **Microsoft Teams**.

While you are attending a meeting...

* Audio is playing
* Video is streaming
* Chat messages are arriving
* Screen sharing is happening
* Files are downloading
* Notifications are appearing

How can one application do so many things?

The answer is...

# 🧵 Multithreading

---

# Before Understanding Multithreading...

Let's understand three important terms.

## 1️⃣ Multitasking

Imagine your laptop.

You have opened

* Chrome
* Visual Studio
* Spotify
* WhatsApp
* Outlook

All applications appear to run together.

Actually...

The Operating System rapidly switches CPU time between applications.

This is called

## Multitasking

The Operating System is the manager.

It decides

> "Chrome gets CPU for a few milliseconds."

Then...

> "Visual Studio gets CPU."

Then...

> "Spotify."

Then...

> "Back to Chrome."

This switching happens thousands of times every second.

To us...

Everything appears simultaneous.

---

# Types of Multitasking

## Preemptive Multitasking

Operating System decides.

Like a restaurant manager assigning work.

The employee has no choice.

Windows works this way.

---

## Cooperative Multitasking

Applications voluntarily say

> "I'm finished."

"You can execute someone else now."

Like polite chefs sharing the stove.

Older operating systems used this.

---

# 2️⃣ Process

Now let's understand

## Process

A Process is simply

> A running application.

Examples

```
Chrome.exe

Spotify.exe

VisualStudio.exe

Word.exe
```

Each process has

* Its own memory
* Its own resources
* Its own security
* Its own address space

Think of it as

🏠 An independent house.

Each house has

* Separate kitchen
* Separate bedroom
* Separate electricity

One house cannot directly access another house.

---

# 3️⃣ Thread

Inside every house...

Family members work together.

One cooks.

One cleans.

One watches TV.

One studies.

These family members are

## Threads

A Process may have

1 thread

or

many threads.

They all live inside the same house.

They share

* Memory
* Variables
* Objects
* Files

This sharing makes communication fast.

But...

Sharing also creates problems.

We'll see those shortly.

---

# Single Thread Example

Suppose our application performs

```
Read Customer Data

↓

Calculate Premium

↓

Generate PDF

↓

Send Email
```

Everything happens one after another.

If PDF generation takes 10 seconds...

Everything waits.

The application feels slow.

---

# Multithreading

Now imagine

One thread

reads customer data.

Second thread

calculates premium.

Third thread

generates PDF.

Fourth thread

sends email.

Now work overlaps.

Application becomes responsive.

---

# Creating Threads in C#

```csharp
using System;
using System.Threading;

class Program
{
    static void PrintNumbers()
    {
        for(int i=1;i<=5;i++)
        {
            Console.WriteLine($"Number : {i}");
            Thread.Sleep(500);
        }
    }

    static void PrintLetters()
    {
        for(char c='A'; c<='E'; c++)
        {
            Console.WriteLine($"Letter : {c}");
            Thread.Sleep(500);
        }
    }

    static void Main()
    {
        Thread t1 = new Thread(PrintNumbers);
        Thread t2 = new Thread(PrintLetters);

        t1.Start();
        t2.Start();
    }
}
```

Output

```
Number : 1

Letter : A

Number : 2

Letter : B

...
```

Notice

Both methods execute together.

---

# But Wait...

Sharing memory creates danger.

Imagine two students updating attendance.

Student 1 writes

```
Present = 21
```

Student 2 simultaneously writes

```
Present = 22
```

What should the final value be?

Sometimes

21

Sometimes

22

Sometimes

Unexpected.

This problem is called

# Race Condition

---

# Thread Synchronization

To avoid conflicts,

only one thread should access shared resources.

C# provides

```csharp
lock
```

Example

```csharp
private static object locker = new object();

lock(locker)
{
    balance += 1000;
}
```

Only one thread enters.

Others wait.

Just like students standing in a queue.

---

# Common Multithreading Problems

## Race Condition

Two threads update same data.

Wrong result.

---

## Deadlock

Imagine

Chef A has the knife.

Chef B has the pan.

Chef A waits for pan.

Chef B waits for knife.

Nobody proceeds.

This is

Deadlock.

---

## Starvation

One thread never gets CPU.

Other threads continuously occupy resources.

Like a student never getting a chance to ask a question.

---

# ThreadPool

Creating threads is expensive.

Imagine hiring new chefs every minute.

Interview.

Salary.

Training.

Uniform.

Instead...

Maintain a ready team.

This is

## ThreadPool

Example

```csharp
ThreadPool.QueueUserWorkItem(state =>
{
    Console.WriteLine("Work completed");
});
```

.NET maintains worker threads automatically.

Fast.

Efficient.

Reusable.

---

# Task Parallel Library (TPL)

Modern .NET developers rarely create threads manually.

Instead,

they use

```
Task
```

Think of Task as

Assigning work to your kitchen manager.

Instead of choosing which chef cooks,

you simply say

> "Please prepare Biryani."

The kitchen manager assigns an available chef.

Example

```csharp
Task.Run(() =>
{
    Console.WriteLine("Processing...");
});
```

Even better

```csharp
int result = await Task.Run(() =>
{
    return 50 + 20;
});

Console.WriteLine(result);
```

Tasks are

* Easier
* Cleaner
* Safer
* Recommended

---

# Async vs Parallel

Students often confuse these.

Imagine ordering tea.

You place the order.

Instead of waiting,

you continue talking.

Tea arrives later.

This is

## Asynchronous Programming

Now imagine

Five chefs preparing five dishes simultaneously.

That is

## Parallel Programming

Async improves responsiveness.

Parallel improves speed.

Tasks support both.

---

# Multiprocessing

Suppose your restaurant becomes famous.

One kitchen isn't enough.

You open

* Pune Kitchen
* Mumbai Kitchen
* Bengaluru Kitchen

Each kitchen has

* Separate chefs
* Separate utensils
* Separate ingredients

One kitchen crashing doesn't affect another.

This is

## Multiprocessing

Processes are independent.

Advantages

* Safe
* Stable
* Crash isolation

Disadvantages

* More memory
* More communication overhead

---

# Thread vs Task vs Process

| Feature           | Thread    | Task      | Process          |
| ----------------- | --------- | --------- | ---------------- |
| Runs inside       | Process   | Process   | Operating System |
| Memory            | Shared    | Shared    | Separate         |
| Easy to use       | Medium    | Very Easy | Heavy            |
| Returns Result    | No        | Yes       | IPC Required     |
| Recommended Today | Sometimes | Yes       | Depends          |

---

# Where Is Multithreading Used?

Almost everywhere.

### 🌐 Web Servers

ASP.NET Core handles thousands of requests using asynchronous operations and the ThreadPool.

### 🏦 Banking Systems

One thread validates transactions.

Another sends SMS.

Another updates balances.

---

### 🛒 E-Commerce

Customer places order.

Meanwhile

* Inventory updates
* Payment processes
* Invoice generates
* Email sends

All concurrently.

---

### 📱 Mobile Apps

Background download.

UI remains responsive.

---

### 🤖 AI Applications

One thread loads data.

Another preprocesses images.

Another performs inference.

Another saves predictions.

---

# Best Practices

✅ Prefer **Task** over **Thread**.

✅ Use **async/await** for I/O-bound work.

✅ Minimize shared mutable state.

✅ Protect shared resources with `lock` or other synchronization primitives when necessary.

✅ Avoid blocking the UI thread.

✅ Don't create unnecessary threads.

---

# Mentor's Closing Message

My dear students,

Learning multithreading is **not about memorizing `Thread.Start()` or `Task.Run()`**.

It is about changing the way you think.

When you build enterprise software, ask yourself:

* Can this work happen independently?
* Can these operations run concurrently?
* What data is shared?
* How do I prevent conflicts?
* Will this improve responsiveness or throughput?

Great software engineers don't just write code—they design systems that coordinate many activities efficiently and safely.

As you grow from a programmer into a software architect, you'll realize:

> **Programming teaches a computer to execute instructions. System engineering teaches multiple instructions to work together.**

That is the true essence of **multithreading**.

**Remember:**

> 🧵 *One thread can complete a task.*
> 🚀 *Many well-managed threads can power an enterprise application.*
> 🎼 *A great software engineer is like an orchestra conductor—ensuring every thread performs its part in perfect harmony.*
