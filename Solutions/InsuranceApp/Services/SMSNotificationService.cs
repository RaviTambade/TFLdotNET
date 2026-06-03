namespace MaxNewYorkInsurance;

//subscribers  or  Event handlers  or  Event Receiver or   Action Listeners
public class SMSNotificationService:INotificationService
{
    
    public void SendMessage()
    {
        Console.WriteLine("Welcome email sent to customer."); 
    }


}