
# Garbage Collection (GC)

In .NET Core, as in other versions of the .NET framework, automatic memory management is facilitated by the Garbage Collector (GC). The GC is responsible for reclaiming memory that is no longer in use by the application, thus preventing memory leaks and allowing developers to focus more on application logic rather than memory management.

Here's how the Garbage Collector works in .NET Core and its role in automatic memory management:

1. **Allocation**: When objects are created using the `new` keyword or similar mechanisms in .NET Core, memory is allocated from the managed heap to store those objects. The managed heap is the area of memory managed by the Garbage Collector.

2. **Reference Tracking**: The Garbage Collector tracks references to objects to determine which objects are still reachable and in use by the application. Objects that are no longer reachable are considered eligible for garbage collection.

3. **Garbage Collection Process**: Periodically, or when certain conditions are met (such as when the heap becomes full), the Garbage Collector executes a collection cycle. During this cycle, it identifies and marks objects that are still reachable (i.e., live objects) and compacts the memory to reclaim space occupied by unreachable objects (i.e., garbage).

4. **Generational Garbage Collection**: .NET Core's Garbage Collector uses a generational garbage collection algorithm. It divides objects into three generations based on their age. Most newly allocated objects are placed in Generation 0. Objects that survive garbage collection cycles are promoted to older generations (Generation 1 and Generation 2). This approach allows the GC to optimize collection by focusing on younger generations more frequently and avoiding unnecessary scanning of long-lived objects.

5. **Finalization**: Some objects may require cleanup operations before they are reclaimed. The Garbage Collector provides a mechanism for finalization, allowing objects to perform custom cleanup through the implementation of finalizers. However, it's essential to use finalizers judiciously, as they can impact performance and resource management.

6. **Tuning and Optimization**: .NET Core provides various options for configuring and optimizing the behavior of the Garbage Collector. Developers can adjust parameters such as heap size, generation thresholds, and collection modes to fine-tune the memory management performance of their applications.

Overall, the Garbage Collector in .NET Core automates memory management tasks, such as memory allocation, tracking object references, and reclaiming memory occupied by unreachable objects. This automatic memory management capability simplifies development, improves productivity, and helps ensure the stability and performance of .NET Core applications.


# Finalization in .NET

In .NET Core, as in other versions of the .NET framework, finalization is a mechanism provided by the Common Language Runtime (CLR) for performing cleanup operations on objects before they are reclaimed by the Garbage Collector. Finalization allows developers to implement custom cleanup logic for objects that require deterministic resource release, such as closing file handles or releasing unmanaged resources.

Here's how finalization works in .NET Core:

1. **Finalize Method**: Objects in .NET Core can define a special method called `Finalize`. This method is automatically called by the Garbage Collector before an object is garbage collected. The `Finalize` method is typically used to perform cleanup tasks, such as releasing unmanaged resources or closing handles.

   ```csharp
   class MyClass
   {
       // Finalizer
       ~MyClass()
       {
           // Cleanup logic
       }
   }
   ```

2. **Finalization Queue**: When an object with a finalizer is created, a reference to it is placed in a special queue called the finalization queue. The Garbage Collector maintains this queue to keep track of objects that need finalization.

3. **Finalization Process**: During garbage collection, the Garbage Collector identifies objects in the finalization queue and schedules them for finalization. This process occurs separately from the normal garbage collection cycle. After an object is finalized, it is removed from the finalization queue.

4. **Finalization Thread**: .NET Core uses a dedicated finalization thread to execute finalizers. This thread is responsible for invoking the `Finalize` method of objects in the finalization queue. Finalizers are executed in an undefined order and may introduce nondeterministic behavior in the application.

5. **Performance Considerations**: While finalization provides a mechanism for deterministic cleanup, it can impact performance due to the overhead of maintaining the finalization queue and executing finalizers. In some cases, finalization may lead to resource leaks if not implemented correctly, such as when objects hold onto limited resources for an extended period.

Overall, finalization in .NET Core provides a mechanism for performing cleanup operations on objects before they are garbage collected. However, it's essential to use finalization judiciously and consider alternative approaches, such as implementing `IDisposable`, to manage resource cleanup efficiently and minimize performance overhead.


## Deterministic Finalization vs. InDeterministic Finalization


In .NET, finalization refers to the process of cleaning up unmanaged resources held by an object before it is destroyed by the garbage collector. There are two types of finalization approaches: deterministic and non-deterministic (or indeterministic).

1. **Deterministic Finalization**:
   - In deterministic finalization, the developer has explicit control over when the finalization process occurs.
   - This is typically achieved by implementing the `IDisposable` interface along with the `Dispose()` method.
   - The `Dispose()` method is called explicitly by the developer when they no longer need the object or when it goes out of scope.
   - Deterministic finalization is often used for managing unmanaged resources such as file handles, database connections, or network sockets, where it's important to release resources promptly to avoid leaks.
   - Example: In C#, you might use the `using` statement to ensure `Dispose()` is called deterministically:

    ```csharp
    using (var resource = new Resource())
    {
        // Use resource
    }
    ```

2. **Indeterministic Finalization**:
   - In indeterministic finalization, the finalization process is handled automatically by the garbage collector, which runs at non-deterministic times.
   - Objects that require finalization but do not implement `IDisposable` rely on the garbage collector's finalization queue to perform cleanup.
   - Finalization occurs during the garbage collection process, which is non-deterministic and can lead to longer object lifetimes.
   - This approach is less predictable and can result in delays in resource release.
   - Example: If an object has a finalizer (destructor) but does not implement `IDisposable`, the finalization process is non-deterministic:

    ```csharp
    public class Resource
    {
        ~Resource()
        {
            // Cleanup code for unmanaged resources
        }
    }
    ```

In general, it's recommended to use deterministic finalization whenever possible, as it provides more control over resource management and ensures timely cleanup. However, in scenarios where deterministic finalization is not feasible or practical, indeterministic finalization can still be used to clean up resources when they are no longer needed, albeit with less control over when the cleanup occurs.