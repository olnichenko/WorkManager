using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UsersService _usersService;
        public AccountController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IResult> IsEmailUse(string email)
        {
            var result = await _usersService.IsEmailUseAsync(email);
            return Results.Ok(result);
        }

        [HttpPost]
        public async Task<IResult> Register(User user)
        {
            var result = await _usersService.RegisterAsync(user);
            return Results.Ok(result);
        }
    }
}
