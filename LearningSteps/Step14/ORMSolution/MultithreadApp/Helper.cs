namespace Util;
using System.Threading;
public static class Helper{
        public static  async  Task  StoreData(){
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
