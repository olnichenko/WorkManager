using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using System.Formats.Asn1;
using WorkManagerDal.Services;
using WorkManagerDal.ViewModels;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [AuthorizePermission(PermissionsService.Users_List)]
        public async Task<List<UserVm>> UsersList()
        {
            return null;
        }
    }
}
