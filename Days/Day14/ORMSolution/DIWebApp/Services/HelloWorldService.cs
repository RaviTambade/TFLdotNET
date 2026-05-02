using DIWebApp.Interfaces;
namespace DIWebApp.Services
{
    public class HelloWorldService : IHelloWorldService
    {
        public  HelloWorldService(){ 
            Console.WriteLine("HelloWorld Service instance Initialized....");
        }
        public string SaysHello()
        {
            return "Wish you have a great career Opportunity";
        }
    }
}
