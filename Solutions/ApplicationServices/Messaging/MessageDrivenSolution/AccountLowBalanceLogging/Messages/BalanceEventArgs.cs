using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//pub-sub pattern
//event is a special kind of delegate


namespace Banking.AccountManagement.Messages
{
    public class BalanceEventArgs : EventArgs
    {
        public string AccountHolder { get; }
        public decimal CurrentBalance { get; }

        public BalanceEventArgs(string name, decimal balance)
        {
            AccountHolder = name;
            CurrentBalance = balance;
        }
    }

}
