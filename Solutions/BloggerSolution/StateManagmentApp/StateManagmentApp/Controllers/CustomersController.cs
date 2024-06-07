using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StateManagmentApp.Models;

namespace StateManagmentApp.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            Customer newCustomer = new Customer();
            newCustomer.OrgList = PopulateOrgs();
            return View(newCustomer);
        }

        [HttpPost]
        public IActionResult Register(Customer theCustomer, string[] organizations )
        {
            theCustomer.OrgList = PopulateOrgs();
            foreach (SelectListItem item in theCustomer.OrgList)
            {
                if (item != null)
                {
                    if (organizations.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
            //send complete theCustomer to database using Entity Frameowk or database connectivity or 
            //by using 
            return RedirectToAction("Index");
        }

        public static List<SelectListItem> PopulateOrgs()
        {  
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem{ Text = "CDAC",Value = "CDAC",Selected=false});
            items.Add(new SelectListItem { Text = "IBM", Value = "IBM", Selected = false });
            items.Add(new SelectListItem { Text = "SEED", Value = "SEED", Selected = false });
            items.Add(new SelectListItem { Text = "Microsoft", Value = "Microsoft", Selected = false });
            items.Add(new SelectListItem { Text = "Google", Value = "Google", Selected = false });
            return items;
        }
    }
}