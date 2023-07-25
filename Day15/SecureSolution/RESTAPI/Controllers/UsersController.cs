using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;


namespace RESTAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase{
        public UsersController()
        {  }
        [HttpGet]
        public IActionResult GetAll()
        { 
            List<User> users=new List<User>();
            users.Add(new  User {UserName="ravi", Password="seed", Email="ravi.tambade@transflower.in", ContactNumber="9881735801"});
            users.Add(new  User {UserName="Rajan", Password="rte", Email="rajan.tambade@transflower.in", ContactNumber="9881735801"});
            users.Add(new  User {UserName="Seeta", Password="ssdfseed", Email="seeta.tambade@transflower.in", ContactNumber="9881735801"});
            users.Add(new  User {UserName="Charu", Password="sesdfsdfed", Email="charu.tambade@transflower.in", ContactNumber="9881735801"});

         
            //return Ok("Ravi ,Sachin, Manoj");
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Insert(User theUser)
        { 
            Console.WriteLine(theUser);  
            return Ok(theUser);
        }


         [HttpPut]
         [Route("{id}")]
        public IActionResult Update(int id,User theUser)
        { 
            Console.WriteLine("User Id "+ id + "to be updated.....");
            Console.WriteLine(theUser + "is going to be updated ");  
            return Ok(theUser);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        { 
            Console.WriteLine("User Id ="+ id + "  to be delted");  
            return Ok(id);
        }

}