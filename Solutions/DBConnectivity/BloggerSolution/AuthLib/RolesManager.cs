using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Business Logic: Authorization
namespace AuthLib
{
    public  class RolesManager
    {
        public void AddRole(string roleName)
        {

        }

         public bool  RemoveRole(string roleName) { 
            return false;
        }
        public string GetRoleName(string userName) {
            return "Manager";
        }
    }
}
