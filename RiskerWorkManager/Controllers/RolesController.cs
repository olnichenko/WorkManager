using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase, IDisposable
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public void Dispose()
        {
            _rolesService.Dispose();
        }

        [HttpGet]
        [AuthorizePermission(PermissionsService.Roles_List)]
        public async Task<IEnumerable<Role>> RolesList()
        {
            var roles = await _rolesService.GetRolesAsync();
            return roles;
        }

        [HttpGet]
        [AuthorizePermission(PermissionsService.Role_Edit)]
        public async Task<bool> IsRoleExist(string name)
        {
            var result = await _rolesService.IsRoleExistAsync(name);
            return result;
        }

        [HttpPost]
        [AuthorizePermission(PermissionsService.Role_Edit)]
        public async Task<Role> CreateRole(Role role)
        {
            var result = await _rolesService.CreateRoleAsync(role);
            return result;
        }

        [HttpPost]
        [AuthorizePermission(PermissionsService.Role_Edit)]
        public async Task<Role> UpdateRole(Role role)
        {
            var result = await _rolesService.UpdateRoleAsync(role);
            return result;
        }
    }
}
