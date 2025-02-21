namespace MultithreadingTPL;
 using System.Threading.Tasks;
public class Manager{
    public static int DoWork(string taskName ){
       /// ThreadMonitor.Monitor();
        Thread theThread=Thread.CurrentThread;
        Console.WriteLine($" Thread Id:{theThread.ManagedThreadId}");
        Console.WriteLine($" Thread Name:{theThread.Name}");
        Console.WriteLine($" Thread Priority:{theThread.Priority}");
        Console.WriteLine($" Thread State:{theThread.ThreadState}");

    Console.WriteLine("Task started "+ taskName );
        Task.Delay(5000).Wait();
        Console.WriteLine("Task completed " + taskName); 
        return 400;
    }
}