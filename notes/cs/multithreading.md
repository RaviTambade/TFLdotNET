# Multitasking  and Mulithreading

Building responsive and efficient applications in our rapidly-evolving digital world is more crucial now than ever. As software developers, we constantly look for ways to boost performance and improve the user experience. One such method is employing multithreading, a widely used yet often misunderstood feature.

## Multitasking and Multithreading Terms

Multitasking and multithreading are related concepts in computer science, both aimed at improving the efficiency and responsiveness of software systems, but they operate at different levels of abstraction and serve different purposes.

1. **Multitasking**:
   - Multitasking refers to the ability of an operating system to execute multiple tasks concurrently. These tasks can be applications or processes.
   - In a multitasking system, the CPU switches between tasks rapidly, giving the illusion of parallelism.
   - Multitasking can be achieved through two main approaches: 
     - **Preemptive multitasking**: The operating system decides when to switch between tasks, typically based on priorities and time slices allocated to each task.
     - **Cooperative multitasking**: Tasks voluntarily yield control to other tasks when they're idle or waiting for I/O operations.

2. **Multithreading**:
   - Multithreading is a programming technique where a single process or application can have multiple threads of execution running concurrently within it.
   - Threads share the same memory space and resources of the process they belong to, allowing them to communicate and synchronize with each other efficiently.
   - Multithreading enables concurrent execution of tasks within a single application, making it possible to perform multiple operations simultaneously.
   - Threads can run independently or cooperate to accomplish a common goal, depending on the design of the application.

Relationship:
- Multithreading is a way to achieve multitasking at the application level. By creating multiple threads within a process, an application can perform multiple tasks concurrently, taking advantage of modern multicore processors.
- Multitasking provided by the operating system allows multiple applications or processes to run concurrently, and each of these applications may utilize multithreading internally to achieve concurrent execution of tasks.
- Multithreading is a lower-level concept, implemented within the application code, while multitasking is managed by the operating system at a higher level.

In summary, multitasking is a feature provided by the operating system to run multiple tasks concurrently, while multithreading is a programming technique to achieve concurrency within a single application or process. They work together to enable efficient and responsive computing environments.
Threads make it possible to execute sevaral program pieces concurrently, enhancing the program's efficiency.

## What is Threading in C# ?

Threads are the backbone of any software application. In simple terms, a thread is a single sequence of instructions that a process can execute. In C#, the System.Threading namespace offers classes that allow you to manipulate threads. When a C# program starts, it’s a single threaded process by default. This “main” thread is responsible for executing your code line by line, creating what is known as a single threaded application.

 multithreading allows a process to manage two or more concurrent threads. Each thread can handle a task independently. For example, while one thread performs a complex calculation, another can update the user interface, preventing the application from freezing.

The choice between single threading and multithreading depends on your application’s requirements. Single threading is simpler to implement and debug, while multithreading can improve application performance by performing tasks concurrently.

Let us have a look at following code segment. MyFunction is the method the new thread will execute. The Start() method initiates the execution of the new thread.

```
void MyFunction()
{
    // Some work here
}
Thread myThread = new Thread(new ThreadStart(MyFunction));
myThread.Start();
```

Use multithreading when your application has tasks that can run concurrently, independently, and without needing to be sequentially organized. This is particularly useful when performing I/O-bound work or expensive computations while keeping the user interface responsive.

```

void Function1()
{
    // Some work here
}

void Function2()
{
    // Some other work here
}

Thread thread1 = new Thread(new ThreadStart(Function1));
Thread thread2 = new Thread(new ThreadStart(Function2));
thread1.Start();
thread2.Start();
```
## Thread Pool and Task 
In .NET, the ThreadPool and Tasks are related but serve different purposes in managing and executing asynchronous operations.

1. **ThreadPool**:
   - The ThreadPool is a collection of threads maintained by the .NET runtime. It provides a pool of pre-created threads that can be used to execute asynchronous tasks.
   - When you queue a work item to the ThreadPool, it assigns one of its available threads to execute that work item.
   - The ThreadPool manages the number of threads based on the current system load and the ThreadPool settings.
   - Using the ThreadPool is appropriate for executing short-lived, CPU-bound, or I/O-bound tasks that don't need finer-grained control over their execution.

2. **Task**:
   - Task is a higher-level abstraction for representing asynchronous operations or units of work in .NET.
   - Tasks encapsulate both the work to be done and the result of that work, making it easier to work with asynchronous code.
   - Tasks can be run on ThreadPool threads or on custom threads, depending on how they are created.
   - Tasks provide more features and flexibility compared to directly using ThreadPool threads, such as support for cancellation, continuation, exception handling, and more.
   - Task Parallel Library (TPL) in .NET provides APIs for working with tasks, including Task.Run, Task.Factory.StartNew, TaskCompletionSource, etc.

Relationship:
- When you use `Task.Run` or `Task.Factory.StartNew` to execute a piece of code asynchronously, the TPL internally uses the ThreadPool to execute that code on one of its threads.
- Tasks provide a higher-level abstraction over asynchronous operations compared to directly working with ThreadPool threads. They offer more features and control, making it easier to write and manage asynchronous code.
- You can think of Tasks as a more user-friendly interface for asynchronous programming, while the ThreadPool provides the underlying infrastructure for executing asynchronous tasks efficiently.


## ThreadPool
The ThreadPool class in C# is designed to make thread management more manageable by providing a pool of worker threads ready to be used. When a task is delegated to the ThreadPool, it will be executed by one of the free threads, eliminating the overhead of creating and destroying threads.

```
ThreadPool.QueueUserWorkItem((state) =>
{
    // Task to be executed by a thread from the pool
});
```

## Task for High-Level Multithreading
C# provides the Task class, a higher-level way to work with multithreading. A task represents an asynchronous operation and is often easier and safer than directly managing threads. Tasks can also return a result and handle exceptions more smoothly.

```
Task<int> task = Task.Run(() =>
{
    // Complex calculation here
    return result;
});
int result = await task; // Retrieve the result of the Task
```

By adhering to these best practices, you can maximize the benefits of multithreading in your C# applications while mitigating potential problems.



## Using Task (TPL)

However, managing threads manually could be error-prone and lead to complex code, particularly when synchronization is required. Thankfully, C# provides the Task Parallel Library (TPL) and the async and await patterns, simplifying multithreading. By using these high-level abstractions, you let the .NET runtime handle the intricacies of thread management:



```
async Task MyAsyncFunction()
{
    await Task.Run(() => Function1());
    await Task.Run(() => Function2());
}
MyAsyncFunction();

```

## Async and Await
The async and await keywords in C# simplify managing multithreaded applications. When you mark a method with the async keyword, it signifies that the method can contain the await keyword, which effectively tells C# to delegate the rest of the function’s execution to a worker thread, thus freeing up the main thread to perform other tasks.

```
public async Task MyFunction()
{
    await Task.Run(() =>
    {
        // Complex calculation here
    });
}
```

The complex calculation runs on a worker thread, leaving the main thread free to process other tasks, enhancing the responsiveness of your application.


## Thread Synchronization
In a multithreaded environment, multiple threads can simultaneously access and manipulate shared resources. This can lead to a race condition, where the output is determined by the sequence of thread execution. You can avoid this issue by using locks, which ensure only one thread can access a shared resource at a time.

```
private Object thisLock = new Object();

lock (thisLock)
{
    // Code that accesses shared resources
}
```

## Thread Priorities
C# allows you to set the priority of a thread, which determines the proportion of CPU time that a thread receives relative to other threads. However, misusing thread priority can result in starvation, where higher-priority threads consume all the CPU time. Use thread priority judiciously, ensuring most threads operate at the default priority.

## Deadlocks
A deadlock is a situation where two or more threads cannot progress because each is waiting for the other to release a resource. To avoid deadlocks, try to avoid scenarios where a thread holds a lock and simultaneously waits for another thread to release its lock.
