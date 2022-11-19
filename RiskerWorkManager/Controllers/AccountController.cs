using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase, IDisposable
    {
        private readonly UsersService _usersService;
        public AccountController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<bool> IsEmailUse(string email)
        {
            var result = await _usersService.IsEmailUseAsync(email);
            return result;
        }

        [HttpPost]
        public async Task<User> Register(User user)
        {
            var result = await _usersService.RegisterAsync(user);
            return result;
        }

        [HttpGet]
        public async Task<bool> IsAdminExist()
        {
            var result = await _usersService.IsAdminExistAsync();
            return result;
        }

        void IDisposable.Dispose()
        {
            _usersService.Dispose();
        }
    }
}
