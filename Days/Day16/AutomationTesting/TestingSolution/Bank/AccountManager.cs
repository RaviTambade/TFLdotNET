using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership
{
   public  class AccountManager
    {
        public static bool  Validate(string email, string password)
        {
            bool status = false;

            //Call ado.net code
            //Call some web service
            if(email =="ravi.tambade@transflower.in" && password == "tfl@7867")
            {
                status = true;
            }
            return status;
        }
    }
}
