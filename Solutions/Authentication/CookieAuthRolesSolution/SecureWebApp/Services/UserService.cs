using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Models;

namespace SecureWebApp.Services
{
    public class UserService:IUserService
    {
        public List<User> users;
        public UserService() { 
            users = new List<User>();
            users.Add(new User { Email="ravi.tambade@transflower.in", Password= "seed", Roles = new[] { "Admin"} });
            users.Add(new User { Email = "shubhangi.tambade@gmail.com", Password = "transflower" ,Roles = new[] { "User" } });
        }  
        
        public User Validate(string email, string password)
        {
            return users.FirstOrDefault(user=>user.Email == email && user.Password == password);
        }

        public string[] GetRoles(string email)
        {
            return users.FirstOrDefault(u => u.Email == email)?.Roles ?? new string[0];
        }
    }
}
