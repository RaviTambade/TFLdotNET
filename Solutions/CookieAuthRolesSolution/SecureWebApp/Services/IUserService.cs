using SecureWebApp.Models;
namespace SecureWebApp.Services
{
    public interface IUserService
    {
        User Validate(string email, string password);
        public string[] GetRoles(string email);
    }
}
