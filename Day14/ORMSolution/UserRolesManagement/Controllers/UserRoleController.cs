using UserRolesManagement.Models;
using UserRolesManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserRolesManagement.Controllers
{
    [ApiController]
    [Route("/api/userroles/")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _srv;

        public UserRoleController(IUserRoleService srv)
        {
            _srv = srv;
        }

        [HttpGet]
        public async Task<List<UserRole>> GetAll()
        {
            return await _srv.GetAll();
        }

        [HttpGet("{userRoleId}")]
        public async Task<UserRole> GetById(int userRoleId)
        {
            return await _srv.GetById(userRoleId);
        }

        [HttpGet("roles/{userId}")]
        public async Task<List<string>> GetRolesByUserId(int userId)
        {
            return await _srv.GetRolesByUserId(userId);
        }

        [HttpPost]
        public async Task<bool> Insert(UserRole userRole)
        {
            return await _srv.Insert(userRole);
        }

        [HttpGet("farmersid")]
        public async Task<List<string>> GetFarmersId()
        {
            return await _srv.GetFarmersId();
        }

        [HttpPut]
        public async Task<bool> Update(UserRole userRole)
        {
            return await _srv.Update(userRole);
        }

        [HttpDelete("{userRoleId}")]
        public async Task<bool> Delete(int userRoleId)
        {
            return await _srv.Delete(userRoleId);
        }
    }
}
