using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IRolesService : IBaseService
    {
        Task<List<Role>> GetRolesAsync();
        Task<bool> IsRoleExistAsync(string name);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<Role> GetRoleWithPermissionsAsync(int id);
        Task DeleteRoleToPemissionAsync(int roleId, string permissionName);
        Task AddRoleToPermissionAsync(int roleId, string permissionName);
    }
}
