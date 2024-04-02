using UserRolesManagement.Services.Interfaces;
using UserRolesManagement.Repositories.Interfaces;
using UserRolesManagement.Models;

namespace UserRolesManagement.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repo;

        public UserRoleService(IUserRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UserRole>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<UserRole> GetById(int userRoleId)
        {
            return await _repo.GetById(userRoleId);
        }

        public async Task<List<string>> GetRolesByUserId(int userId)
        {
            return await _repo.GetRolesByUserId(userId);
        }

        public async Task<List<string>> GetFarmersId()
        {
            return await _repo.GetFarmersId();
        }

        public async Task<bool> Insert(UserRole userRole)
        {
            return await _repo.Insert(userRole);
        }

        public async Task<bool> Update(UserRole userRole)
        {
            return await _repo.Update(userRole);
        }

        public async Task<bool> Delete(int userRoleId)
        {
            return await _repo.Delete(userRoleId);
        }
    }
}
