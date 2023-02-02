using Microsoft.AspNetCore.Mvc;
using IOCWebApp.Models;
using IOCWebApp.Services;


namespace IOCWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    //Each action method is mapped to HTTP Request type
        private IProductService _svc;
        public ProductsController(IProductService svc)
        {
            this._svc = svc;
        }

        //action method
        [HttpGet]
        [Route("api/products")]
        public IActionResult GetProducts(){
            //invoke service method to resturn products
            // send received data as message to outside world
            try{
                    var message=_svc.GetProducts();
                    if(message==null){
                        return NotFound();
                    }
                return Ok(message);
            }
            catch(Exception){
                return BadRequest();
            }
        }
   
        [HttpPost]
        [Route("api/products")]
        public IActionResult Insert([FromBody] Product product){
            try{

                bool status= _svc.Insert(product);
                if(status == false){
                    return BadRequest();
                }
                else{
                    return Ok();
                }
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
 
        [HttpGet("api/products/{id}")]
        public IActionResult GetById(int id){
             try{

                    var  message= _svc.GetProductById(id);
                    if(message == null){
                        return BadRequest();
                     }
                    else{
                        return Ok(message);
                    }
            }
            catch(Exception ){
                return BadRequest();
            }
        }

         // GET: api/Products/5
        [HttpDelete("api/products/{id}")]
        public IActionResult Delete(int id){
             try{
                    bool status= _svc.Delete(id);
                    if(status == false){
                        return BadRequest();
                     }
                    else{
                        return Ok();
                    }
            }
            catch(Exception ){
                return BadRequest();
            }
        }

        [HttpPut("api/products")]
        public IActionResult Update(Product product){
            try{
                bool status= _svc.Update(product);
                if(status == false){
                    return BadRequest();
                }
                else{
                    return Ok();
                }
            }
            catch(Exception ){
                return BadRequest();
            }
        }
}
    
