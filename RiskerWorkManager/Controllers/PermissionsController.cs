using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionsService _permissionsService;
        private readonly RolesService _rolesService;
        public PermissionsController(PermissionsService permissionsService, RolesService rolesService)
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
        public bool ChangePermissions(Dictionary<string, int> permissionsData)
        {
            foreach (var item in permissionsData)
            {
                
            }
        }
    }
}
