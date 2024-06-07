using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace StateManagmentApp.Models
{
    public class Customer
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> OrgList { get; set; }

        public Customer() { 
                    OrgList = new List<SelectListItem>();
        }

    }
}
