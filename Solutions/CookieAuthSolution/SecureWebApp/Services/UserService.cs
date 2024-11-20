using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Models;

namespace SecureWebApp.Services
{
    public class UserService
    {
        public List<User> users;
        public UserService() { 
            users = new List<User>();
            users.Add(new User { Email="ravi.tambade@transflower.in", Password="seed" });
            users.Add(new User { Email = "shubhangi.tambade@gmail.com", Password = "transflower" });
        }  
        
        public User Validate(string email, string password)
        {
            return users.FirstOrDefault(user=>user.Email == email && user.Password == password);
        }
    }
}
