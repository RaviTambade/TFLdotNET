namespace MaxNewYorkInsurance.Services;



//subscribers  or  Event handlers  or  Event Receiver or   Action Listeners
public class EmailNotificationService:INotificationService
{
    

    public void SendMessage()
    {
        Console.WriteLine("Welcome email sent to customer."); 
    }


}