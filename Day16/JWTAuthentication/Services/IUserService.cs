using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        
    }
}