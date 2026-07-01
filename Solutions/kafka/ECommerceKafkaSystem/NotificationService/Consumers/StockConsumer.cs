using Messaging;

public class StockConsumer : KafkaConsumer
{
    protected override string Topic => "stock-reserved";
    protected override string GroupId => "notification-group";

    protected override void ProcessMessage(string message)
    {
        Console.WriteLine("Email Sent to Customer");
        Console.WriteLine(message);
    }
}