using System.Threading;
using TFLMultitasing;
Thread thread1=new Thread(TaskManager.Task1);
Thread thread2=new Thread(TaskManager.Task2);
Thread thread3=new Thread(TaskManager.Task3);
thread1.Start();
thread2.Start();
thread3.Start();
