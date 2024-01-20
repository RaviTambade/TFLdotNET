# Web API

In computer programming, an application programming interface (API) is a set of subroutine definitions, protocols, and tools for building software and applications.

To put it in simple terms, API is some kind of interface which has a set of functions that allow programmers to access specific features or data of an application, operating system or other services.

Web API is a programming interface/application type that provides communication or interaction between software applications. Web API is often used to provide an interface for websites and client applications to have data access. Web APIs can be used to access data from a database and save data back to the database. 

ASP.NET Web API is a framework that make it easy to build HTTP web service that reaches a bored range of clients, including browser, mobile applications, Desktop application and IOTs.

## What is REST API?
Rest Stands for Representational state transfer. It is introduced in 2000 by Roy Fielding. In REST architecture, a REST Server simply provides access to resources and the REST client accesses and presents the resources. Here each resource is identified by URIs/ Global IDs. REST uses various representations to represent a resource like Text, JSON and XML. JSON is now the most popular format being used in Web Services.

## HTTP Methods

The following HTTP methods are most commonly used in a REST based architecture.

- GET − Provides a read only access to a resource.
- PUT − Used to create a new resource.
- DELETE − Used to remove a resource.
- POST − Used to update an existing resource or create a new resource. 

## REST Constraints

REST constraints are design rules that are applied to establish the distinct characteristics of the REST architectural style. The formal REST constraints are,
- Client-Server
- Stateless
- Cache
- Interface / Uniform Contract
- Layered System
- Code-On-Demand


## ASP.NET Web API Characteristics
- ASP.NET Web API is an ideal platform for building RESTful services.
- ASP.NET Web API is built on top of ASP.NET and supports ASP.NET request/response pipeline
- ASP.NET Web API maps HTTP verbs to method names.
- ASP.NET Web API supports different formats of response data. Built-in support for JSON, XML, BSON format.
-- ASP.NET Web API framework includes new HttpClient to communicate with Web API server. HttpClient can be used in ASP.MVC server side, Windows Form application, Console application or other apps.

## Example  API Controller

```
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
```

## Testing Web API using Postman Tool 
The Postman is the most popular and most powerful HTTP client for testing restful web services. Postman makes it easy to test the Restful Web APIs, as well as develops and documents Restful APIs by allowing the users to quickly put together both simple and complex HTTP requests. The Postman is available as both a Google Chrome in-browser app and Google Chrome Packaged App.


