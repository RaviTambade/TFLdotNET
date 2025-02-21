
namespace  TFLMultitasing;
using Monitoring;
public class TaskManager{
    //These are the methods that will be called by the threads
    //These tasks are independent of each other and can be executed in parallel
    //These are multiple tasks that will be executed by the threads

    public static  void Task1(){
        ThreadMonitor.Monitor();
        Console.WriteLine("Task1 started");
        Thread.Sleep(2000);
        Console.WriteLine("Task1 completed");
    }
    public static  void Task2(){
        ThreadMonitor.Monitor();
        Console.WriteLine("Task2 started");
        Thread.Sleep(7000);
        Console.WriteLine("Task2 completed");
    }
    public static void Task3(){
        ThreadMonitor.Monitor();
        Console.WriteLine("Task3 started");
        Thread.Sleep(4000);
        Console.WriteLine("Task3 completed");
    }
}