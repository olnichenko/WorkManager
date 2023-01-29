using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IRolesService : IBaseService
    {
        Task<List<Role>> GetRolesAsync();
        Task<bool> IsRoleExistAsync(string name);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<Role> GetRoleWithPermissionsAsync(long id);
        Task DeleteRoleToPemissionAsync(long roleId, string permissionName);
        Task AddRoleToPermissionAsync(long roleId, string permissionName);
        Task DeleteRoleAsync(Role role);
    }
}
