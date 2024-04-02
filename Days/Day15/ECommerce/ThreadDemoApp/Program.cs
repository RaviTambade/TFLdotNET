using Utility;
using System.Threading;
Thread th1=Thread.CurrentThread;
Console.WriteLine("Primary Thread Id:"+th1.ManagedThreadId);
Console.WriteLine("Primary Thread Priority:"+th1.Priority);

ThreadStart  job1=new ThreadStart(Helper.Operation1);
Thread th2=new Thread(job1);
th2.Start();

ThreadStart  job2=new ThreadStart(Helper.Operation1);
Thread th3=new Thread(job2);
th3.Start();

Console.WriteLine("Main Thread Execution is about to finish......");
