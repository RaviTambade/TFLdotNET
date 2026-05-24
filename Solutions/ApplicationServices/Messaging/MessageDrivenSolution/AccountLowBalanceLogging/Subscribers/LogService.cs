using Banking.AccountManagement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLowBalanceLogging.Subscribers
{
    public class LogService
    {
        public void LogLowBalance(object sender, BalanceEventArgs e)
        {
            Console.WriteLine($"📝 Log: [{e.AccountHolder}] balance dropped to {e.CurrentBalance}");
        }
    }
}
