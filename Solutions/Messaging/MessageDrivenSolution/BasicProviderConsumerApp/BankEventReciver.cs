using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProviderConsumerApp
{
    public  class BankEventReciver
    {
        //hook function
        //event handler

        public void ApplyPenalty(double amount)
        {
            amount = amount * 0.1; //10% penalty
            //apply penalty 
            Console.WriteLine($"Penalty applied: {amount}");
        }

        public void ApplyTax(double amount)
        {
            //apply tax
            amount = amount * 0.2; //20% tax
            Console.WriteLine($"Tax applied: {amount}");
        }

    }
}
