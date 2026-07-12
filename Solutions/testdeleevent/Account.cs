

namespace Banking;
public class Account
{
    public  event AccountOperation trigger;
    public double balance;

    public Account(double amount)
    {
        this.balance=amount;
    }

    public void Deposit(double amount)
    {
        this.balance=this.balance+amount;
        trigger.Invoke("Data to be sent");
    }
}