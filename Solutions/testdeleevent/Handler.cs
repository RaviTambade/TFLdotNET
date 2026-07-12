namespace Banking;

public class Handler
{
    
    public void OnDeposit(string argument)
    {
        
        Console.WriteLine("Bank statement is updated "+ argument) ;

    }

     public void OnSendEmail(string argument)
    {
        
        Console.WriteLine("Bank statement is emailed "+ argument) ;

    }
}