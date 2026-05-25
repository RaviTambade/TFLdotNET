using Banking.AccountManagement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.AccountManagement.Publishers
{

    //C# Language level support for events
    //Domain Centric Application Behaviour
    //Event Delegation Model

    //business Entity
    //publisher
    //publish events: LowBalance, OverBalance,

    public class Account
    {
        public delegate void LowBalanceEventHandler(object sender, BalanceEventArgs e);
        
        public event LowBalanceEventHandler LowBalance;

        public string AccountHolder { get; set; }
        public decimal Balance { get; private set; }

        public Account(string name, decimal initialBalance)
        {
            AccountHolder = name;
            Balance = initialBalance;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) return;

            Balance -= amount;
            Console.WriteLine($"[{AccountHolder}] Withdrawn: {amount}, Balance: {Balance}");


            //Business Rule: If balance is less than 1000, fire an event

            if (Balance < 1000)
            {
                // Fire event
                //set payload: BalanceEventArgs
                OnLowBalance(new BalanceEventArgs(AccountHolder, Balance));
            }
        }


        //function will not be called  directly
        protected virtual void OnLowBalance(BalanceEventArgs e)
        {
            LowBalance?.Invoke(this, e); // Safe event call
        }
    }

}
