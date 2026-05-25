using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProviderConsumerApp
{
    public  class Account
    {

        public event AccountHandler lowBalance;
        public double Balance { get; set; }

        public Account(double balance)
        {
            Balance = balance;
        }

        public void withdraw(double amount)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
           double calcualtedBalance= Balance - amount;
            if (calcualtedBalance < 10000)
            {
                //generate an event
                //fire an event, trigger an event
                //indirect call by raising an event
                lowBalance(Balance);
                //direct call 
                /*BankEventReciver bankEventReciver = new BankEventReciver();
                bankEventReciver.ApplyPenalty(amount);*/
            }
            else
            {
                Balance -= amount;
            }    
        }

        public void deposit(double amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Deposit amount must be positive");
            }
            Balance += amount;
        }

    }
}
