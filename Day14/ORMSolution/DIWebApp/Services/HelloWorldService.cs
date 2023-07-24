using DIWebApp.Interfaces;
namespace DIWebApp.Services
{
    //Each services should be defined by using contract
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