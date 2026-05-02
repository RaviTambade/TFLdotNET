namespace  TFLMultitasing;
using Monitoring;
public class TaskManager{
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
