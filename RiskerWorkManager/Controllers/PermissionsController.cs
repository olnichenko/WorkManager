using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionsService _permissionsService;
        public PermissionsController(PermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        [HttpGet]
        [AuthorizePermission(PermissionsService.Permission_Edit)]
        public IEnumerable<PermissionData> PermissionDataList()
        {
            return _permissionsService.GetAllPermissions();
        }
    }
}
