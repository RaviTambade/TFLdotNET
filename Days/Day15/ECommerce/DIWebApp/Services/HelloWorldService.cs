namespace DIWebApp.Services
{
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
