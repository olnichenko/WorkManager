using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PermissionsController : ControllerBase, IDisposable
    {
        private readonly IPermissionsService _permissionsService;
        private readonly IRolesService _rolesService;
        public PermissionsController(IPermissionsService permissionsService, IRolesService rolesService)
        {
            _permissionsService = permissionsService;
            _rolesService = rolesService;
        }

        [HttpGet]
        [AuthorizePermission(PermissionsService.Permission_Edit)]
        public IEnumerable<PermissionData> PermissionDataList()
        {
            return _permissionsService.GetAllPermissions();
        }

        /// <summary>
        /// Change role permissions
        /// </summary>
        /// <param name="permissionsData">pemission name, role id dictionary</param>
        /// <returns>result status</returns>
        [HttpPost]
        [AuthorizePermission(PermissionsService.Permission_Edit)]
        public async Task<bool> ChangePermission(int roleId, string permissionName, bool isEnabled)
        {
            var role = await _rolesService.GetRoleWithPermissionsAsync(roleId);
            var isRoleContainPermission = role.Permissions.Any(x => x.Name == permissionName);

            if (isEnabled)
            {
                if (!isRoleContainPermission)
                {
                    var permission = await _permissionsService.CreateAndGetPermissionAsnyc(permissionName);
                    await _rolesService.AddRoleToPermissionAsync(role.Id, permissionName);
                }
            }
            else
            {
                if (isRoleContainPermission)
                {
                    var permission = await _permissionsService.CreateAndGetPermissionAsnyc(permissionName);
                    await _rolesService.DeleteRoleToPemissionAsync(role.Id, permissionName);
                }
            }
            return true;
        }
        void IDisposable.Dispose()
        {
            _permissionsService.Dispose();
            _rolesService.Dispose();
        }
    }
}
