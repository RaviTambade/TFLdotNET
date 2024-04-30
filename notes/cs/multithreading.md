# Multitasking, Mulithreading and MultiProcessing

Building responsive and efficient applications in our rapidly-evolving digital world is more crucial now than ever. As software developers, we constantly look for ways to boost performance and improve the user experience. One such method is employing multithreading.

## Multitasking and Multithreading

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

### Multithreading and Multitasking Relationship:
- Multithreading is a way to achieve multitasking at the application level. By creating multiple threads within a process, an application can perform multiple tasks concurrently, taking advantage of modern multicore processors.
- Multitasking provided by the operating system allows multiple applications or processes to run concurrently, and each of these applications may utilize multithreading internally to achieve concurrent execution of tasks.
- Multithreading is a lower-level concept, implemented within the application code, while multitasking is managed by the operating system at a higher level.

In summary, multitasking is a feature provided by the operating system to run multiple tasks concurrently, while multithreading is a programming technique to achieve concurrency within a single application or process. They work together to enable efficient and responsive computing environments.
Threads make it possible to execute sevaral program pieces concurrently, enhancing the program's efficiency.

## Threads in C#

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

## ThreadPool
   - The ThreadPool is a collection of threads maintained by the .NET runtime. It provides a pool of pre-created threads that can be used to execute asynchronous tasks.
   - When you queue a work item to the ThreadPool, it assigns one of its available threads to execute that work item.
   - The ThreadPool manages the number of threads based on the current system load and the ThreadPool settings.
   - Using the ThreadPool is appropriate for executing short-lived, CPU-bound, or I/O-bound tasks that don't need finer-grained control over their execution.

The ThreadPool class in C# is designed to make thread management more manageable by providing a pool of worker threads ready to be used. When a task is delegated to the ThreadPool, it will be executed by one of the free threads, eliminating the overhead of creating and destroying threads.

```
ThreadPool.QueueUserWorkItem((state) =>
{
    // Task to be executed by a thread from the pool
});
```

In .NET, the ThreadPool and Tasks are related but serve different purposes in managing and executing asynchronous operations.

## Task Parallel Library (TPL)

Task Parallel Library is  a set of APIs in the System.Threading.Tasks namespace that simplifies the process of adding parallelism and concurrency to applications. TPL provides constructs for running tasks asynchronously, parallelizing loops, coordinating tasks, and handling exceptions in parallel code. It abstracts away low-level threading details, making it easier for developers to write scalable and efficient concurrent code.

C# provides the Task class, a higher-level way to work with multithreading. A task represents an asynchronous operation and is often easier and safer than directly managing threads. Tasks can also return a result and handle exceptions more smoothly.

.NET provides Threading.Tasks class to let you create tasks and run them asynchronously. A task is an object that represents some work that should be done. The task can tell you if the work is completed and if the operation returns a result, the task gives you the result.
   - Task is a higher-level abstraction for representing asynchronous operations or units of work in .NET.
   - Tasks encapsulate both the work to be done and the result of that work, making it easier to work with asynchronous code.
   - Tasks can be run on ThreadPool threads or on custom threads, depending on how they are created.
   - Tasks provide more features and flexibility compared to directly using ThreadPool threads, such as support for cancellation, continuation, exception handling, and more.
   - Task Parallel Library (TPL) in .NET provides APIs for working with tasks, including Task.Run, Task.Factory.StartNew, TaskCompletionSource, etc.

```
Task<int> task = Task.Run(() =>
{
    // Complex calculation here
    return result;
});
int result = await task; // Retrieve the result of the Task
```

By adhering to these best practices, you can maximize the benefits of multithreading in your C# applications while mitigating potential problems.

Managing threads manually could be error-prone and lead to complex code, particularly when synchronization is required. Thankfully, C# provides the Task Parallel Library (TPL) and the async and await patterns, simplifying multithreading. By using these high-level abstractions, you let the .NET runtime handle the intricacies of thread management:


```
async Task MyAsyncFunction()
{
    await Task.Run(() => Function1());
    await Task.Run(() => Function2());
}
MyAsyncFunction();

```

## Differences Between Task And Thread
Here are some differences between a task and a thread.

- The Thread class is used for creating and manipulating a thread in Windows. A Task represents some asynchronous operation and is part of the Task Parallel Library, a set of APIs for running tasks asynchronously and in parallel.
- The task can return a result. There is no direct mechanism to return the result from a thread.
Task supports cancellation through the use of cancellation tokens. But Thread doesn't.
- A task can have multiple processes happening at the same time. Threads can only have one task running at a time.
We can easily implement Asynchronous using ’async’ and ‘await’ keywords.
- A new Thread()is not dealing with Thread pool thread, whereas Task does use thread pool thread.
- A Task is a higher level concept than Thread.


##  Multiprocessing

Multiprocessing and multithreading are both techniques used to achieve concurrency in software development, but they operate at different levels and have distinct characteristics:

1. **Multiprocessing**:

   - **Definition**: Multiprocessing involves the simultaneous execution of multiple processes on a multicore CPU or multiple CPUs. Each process has its own memory space, and communication between processes typically involves inter-process communication (IPC) mechanisms.
   
   - **Isolation**: Processes are isolated from each other, meaning they do not share memory by default. This isolation provides stronger protection against errors in one process affecting others.
   
   - **Scalability**: Multiprocessing can scale well across multiple CPU cores, as each process can potentially run on a separate core.
   
   - **Overhead**: Creating and managing processes typically incurs higher overhead compared to threads due to the need for separate memory spaces and context switching.
   
   - **Usage**: Multiprocessing is commonly used in scenarios where isolation between tasks is crucial, such as running multiple independent computations, parallelizing I/O-bound tasks, or implementing fault-tolerant systems.

2. **Multithreading**:

   - **Definition**: Multithreading involves the simultaneous execution of multiple threads within the same process. Threads share the same memory space, allowing them to communicate and synchronize directly.
   
   - **Shared Memory**: Threads within the same process share memory, which simplifies communication and data sharing but also requires synchronization mechanisms to avoid race conditions.
   
   - **Resource Efficiency**: Multithreading is typically more resource-efficient than multiprocessing, as threads within the same process share resources such as memory and file descriptors.
   
   - **Context Switching**: Context switching between threads within the same process is generally faster than switching between processes.
   
   - **Usage**: Multithreading is commonly used in scenarios where tasks need to share data and resources within the same process, such as GUI applications, web servers, and other concurrent systems.

In summary, multiprocessing and multithreading are both approaches to concurrency, each with its own advantages and use cases. Multiprocessing provides isolation and scalability but incurs higher overhead, while multithreading offers resource efficiency and direct data sharing but requires careful synchronization to avoid concurrency issues. The choice between them depends on the specific requirements and constraints of the application.