namespace DIWebApp.Services
{
    //Each services should be defined by using contract
    public interface IHelloWorldService
    {
        string SaysHello();
    }
    public class HelloWorldService : IHelloWorldService
    {
        public  HelloWorldService(){ }
        public string SaysHello()
        {
            return "Good morning ";
        }
    }
}