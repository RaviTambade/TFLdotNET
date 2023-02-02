namespace Util;
using System.Threading;
public static class Helper{
    //non blocking call
        public static  async  Task  StoreData(){
            // defining callback function and invoking callback function
            // using internal thread pool
              await Task.Run(()=>{
                    Console.WriteLine("storing data to JSON file");
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
        }

        public static  async  Task  GetRemoteData(){
             await Task.Run(()=>{
                Console.WriteLine("getting  data from external world");
                 Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
        }


}