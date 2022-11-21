using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        [AuthorizePermission(PermissionsService.Roles_List)]
        public async Task<IEnumerable<Role>> List()
        {
            return null;
        }
    }
}
