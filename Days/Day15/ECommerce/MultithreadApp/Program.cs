using System;
using System.Threading;
using System.Threading.Tasks;
using Util;


Thread theTread=Thread.CurrentThread;
Console.WriteLine("Main fn Thread =" + theTread.ManagedThreadId);

//Invoke other reusable function
 //StoreData();
 //GetRemoteData();
 /*ThreadStart thd=new ThreadStart(StoreData);
 Thread secondaryThread=new Thread( thd);
 secondaryThread.Start();


 secondaryThread.SuspendThread();
 secondaryThread.ResumeThread()

*/


 Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
 
Helper.StoreData();
Helper.GetRemoteData();
Console.WriteLine("Hello World!");