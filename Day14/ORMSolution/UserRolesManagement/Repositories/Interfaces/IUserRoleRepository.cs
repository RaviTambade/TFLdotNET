using UserRolesManagement.Models;

namespace UserRolesManagement.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> GetAll();
        Task<UserRole> GetById(int userRoleId);
        Task<List<string>> GetFarmersId();
        Task<List<string>> GetRolesByUserId(int userId);
        Task<bool> Insert(UserRole userRole);
        Task<bool> Update(UserRole userRole);
        Task<bool> Delete(int userRoleId);
    }
}
