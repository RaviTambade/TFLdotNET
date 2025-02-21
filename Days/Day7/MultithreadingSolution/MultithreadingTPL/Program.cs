
using MultithreadingTPL;
using System.Threading.Tasks;

Console.WriteLine("Hello, World Main Method invocation!");
int result1 = await Task.Run(() => Manager.DoWork("Printing"));
int result2 = await Task.Run(() => Manager.DoWork("Downloading"));
int result3= await Task.Run(() => Manager.DoWork("Uploading")); 

 
Console.WriteLine("Result 1: " + result1);
Console.WriteLine("Result 2: " + result2);
Console.WriteLine("Result 3: " + result3);
Console.WriteLine("Hello, World Main Method invocation completed!");
