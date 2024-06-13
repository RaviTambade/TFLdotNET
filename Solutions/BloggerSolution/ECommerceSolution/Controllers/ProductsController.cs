using ECommerceSolution.Models;
using ECommerceSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSolution.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;
        public ProductsController(IProductService service) {
            _productService = service;
        }

        //CRUD Operation Action Methods
        public IActionResult Index()
        {
            List<Product> products = _productService.GetAll();
            ViewData["products"]=products;
            return View();
        }

        public IActionResult Details(int id)
        {
            Product product = _productService.GetById(id);
            ViewData["product"]=product;
            return View();
        }
        public IActionResult Remove(int id)
        {
            _productService.Remove(id);
            return RedirectToAction("Index");
        }


        //Data Entry form
        //You need to handle get request as well as post request

        [HttpGet]
        public IActionResult Update(int id)
        {
            Product theProduct = _productService.GetById(id);
            return View(theProduct);
        }

                      //MetaData
        //Attribute   ( in Java it is called annotation)
        [HttpPost]    // typescript decorator
        public IActionResult Update(Product product)
        {
            _productService.Update(product);
            return RedirectToAction("Index");
        }

        
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Product product)
        {
            _productService.Insert(product);
            return RedirectToAction("Index");
        }
    }
}
