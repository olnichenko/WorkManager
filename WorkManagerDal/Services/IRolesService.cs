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
    }
}
