 
using Banking.AccountManagement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLowBalanceLogging.Subscribers
{
    public class EmailNotificationService
    {
        public void SendAlert(object sender, BalanceEventArgs e)
        {
            Console.WriteLine($"📧 Email to {e.AccountHolder}: Your balance is low: {e.CurrentBalance}");
        }

        internal void SendLowBalanceAlert(object sender, BalanceEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
