using AutoMapper;
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
    public class UsersController : ControllerBase, IDisposable
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }
        [HttpGet]
        [AuthorizePermission(PermissionsService.Users_List)]
        public async Task<List<UserVm>> UsersList(int page, int pageSize, string email = null)
        {
            var result = await _usersService.GetUsersPageAsync(page, pageSize, email);
            return _mapper.Map<List<UserVm>>(result);
        }

        [HttpGet]
        [AuthorizePermission(PermissionsService.Users_List)]
        public async Task<int> GetUsersCount(string email = null)
        {
            var result = await _usersService.GetUsersCountAsync(email);
            return result;
        }

        public void Dispose()
        {
            //_rolesService.Dispose();
        }
    }
}
