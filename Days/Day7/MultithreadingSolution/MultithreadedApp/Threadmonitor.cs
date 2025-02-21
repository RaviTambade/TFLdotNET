namespace Monitoring;
public class ThreadMonitor{

    public static void Monitor( ){
            Thread theThread=Thread.CurrentThread;
            Console.WriteLine($"Primary Thread Id:{theThread.ManagedThreadId}");
            Console.WriteLine($"Primary Thread Name:{theThread.Name}");
            Console.WriteLine($"Primary Thread Priority:{theThread.Priority}");
            Console.WriteLine($"Primary Thread State:{theThread.ThreadState}");
    }
}