using System.Collections.Generic;
using SecureWebApp.Entity;
using SecureWebApp.Models;

namespace SecureWebApp.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}