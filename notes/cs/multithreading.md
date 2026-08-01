## Multithreading & Multiprocessing

### Multithreading

**"Imagine you're a chef in a busy kitchen..."** You’re chopping vegetables, boiling pasta, grilling paneer, and at the same time guiding your junior on how to plate the starter. Now, pause and think — are you doing all tasks at once, or are you just switching quickly between them? But as always, at Transflower, we won't begin with definitions.

We'll begin with a story.

#####  Multitasking — *The Smart Kitchen Manager*

## 🍽️ Imagine You're Running a Busy Restaurant

Imagine you own a popular restaurant. Customers are continuously placing orders. One table wants **Paneer Butter Masala**. Another wants **Veg Biryani**. Someone else wants **Masala Dosa**. Another customer only wants coffee.

##### 🔄 Multithreading — *The Sous Chefs Working Together*

* Takes the order
* Chops vegetables
* Boils rice
* Fries paneer
* Makes coffee
* Washes utensils
* Serves food

How long will customers wait? Very long. Now imagine instead you have **an entire kitchen team**. One chef prepares vegetables. Another cooks rice. Another prepares gravy. Someone serves customers. Someone prepares desserts. Suddenly... The restaurant becomes faster. Customers are happier. Orders are completed simultaneously. This is exactly what happens inside a computer.


# 💻 Computers Face the Same Challenge

Every application performs multiple activities. Think about **Microsoft Teams**. While you are attending a meeting...

* Audio is playing
* Video is streaming
* Chat messages are arriving
* Screen sharing is happening
* Files are downloading
* Notifications are appearing
 
How can one application do so many things? The answer is...

# 🧵 Multithreading

###  Before Understanding Multithreading...

Let's understand three important terms.

#### 1️⃣ Multitasking

Imagine your laptop. You have opened

* Chrome
* Visual Studio
* Spotify
* WhatsApp
* Outlook

All applications appear to run together. Actually... The Operating System rapidly switches CPU time between applications. This is called

#### Multitasking

The Operating System is the manager. It decides

> "Chrome gets CPU for a few milliseconds."

Then...

> "Visual Studio gets CPU."

Then...

> "Spotify."

Then...

> "Back to Chrome."

This switching happens thousands of times every second. To us... Everything appears simultaneous.


## Types of Multitasking

### Preemptive Multitasking

Operating System decides. Like a restaurant manager assigning work.The employee has no choice. Windows works this way.


### Cooperative Multitasking

Applications voluntarily say

> "I'm finished."

"You can execute someone else now." Like polite chefs sharing the stove. Older operating systems used this.


#### 2️⃣ Process

Now let's understand

##### Process

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

### 🔐 Thread Synchronization — *Taking Turns in the Kitchen*

Think of it as

🏠 An independent house.

Each house has

* Separate kitchen
* Separate bedroom
* Separate electricity

One house cannot directly access another house.


##### 3️⃣ Thread

Inside every house... Family members work together.One cooks. One cleans. One watches TV. One studies. These family members are Threads. A Process may have 1 thread or many threads. They all live inside the same house.

They share

* Memory
* Variables
* Objects
* Files

This sharing makes communication fast. But... Sharing also creates problems. We'll see those shortly.

 

### Single Thread Example

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

Everything happens one after another. If PDF generation takes 10 seconds... Everything waits. The application feels slow.


##  Multithreading

Now imagine One thread reads customer data. Second thread calculates premium. Third thread generates PDF. ourth thread sends email. Now work overlaps. Application becomes responsive.


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
```

### ThreadPool & Tasks — *The Central Kitchen of Experts*

You’ve got a team of experts who are always ready — not tied to one job, but on-call for short tasks. That’s what **ThreadPool** is.

Both methods execute together.

But when Sharing memory creates danger. Imagine two students updating attendance. Student 1 writes

```
Present = 21
```

Student 2 simultaneously writes

```
Present = 22
```

What should the final value be? Sometimes 21 Sometimes 22 Sometimes Unexpected. This problem is called Race Condition

 

### Thread Synchronization

To avoid conflicts, only one thread should access shared resources. C# provides

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


### Common Multithreading Problems

#### Race Condition

Two threads update same data. Wrong result.

  

#### Deadlock

Imagine Chef A has the knife. Chef B has the pan. Chef A waits for pan. Chef B waits for knife. Nobody proceeds. This is Deadlock.

 
#### Starvation

One thread never gets CPU. Other threads continuously occupy resources. Like a student never getting a chance to ask a question.


#### ThreadPool

Creating threads is expensive. Imagine hiring new chefs every minute. Interview. Salary. Training. Uniform. Instead... Maintain a ready team. This is ThreadPool. 

Example

```csharp
ThreadPool.QueueUserWorkItem(state =>
{
    Console.WriteLine("Work completed");
});
```

.NET maintains worker threads automatically. Fast. Efficient. Reusable.


###  Task Parallel Library (TPL)

Modern .NET developers rarely create threads manually. Instead, they use

```
Task
```

Think of Task as 

#### Multiprocessing — *Running Entire Kitchens in Parallel*

What if, instead of one big kitchen, we had **multiple independent kitchens**, each with their own chefs, tools, and menus? That’s **Multiprocessing**. Unlike threads that share memory, **processes are isolated**. They don’t bump into each other. They don’t share memory by default. That means better safety — but also **more overhead**.

Example

```csharp
Task.Run(() =>
{
    Console.WriteLine("Processing...");
});
```

#### Thread vs Task vs Process — Quick Recap

```csharp
int result = await Task.Run(() =>
{
    return 50 + 20;
});

Console.WriteLine(result);
```

### Closing Wisdom — Code Like a Conductor

* Easier
* Cleaner
* Safer
* Recommended

And like any good system — when managed well — concurrency gives speed, responsiveness, and a better user experience. But remember…
> **“With great concurrency comes great responsibility.”**