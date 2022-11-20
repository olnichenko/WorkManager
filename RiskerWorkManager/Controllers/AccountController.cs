using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Services;
using System;
using WorkManagerDal.Models;
using WorkManagerDal.Services;
using WorkManagerDal.ViewModels;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase, IDisposable
    {
        private readonly UsersService _usersService;
        private readonly UserIdentityService _userIndentityService;
        private readonly IMapper _mapper;

        public AccountController(UsersService usersService, UserIdentityService userIdentityService, IMapper mapper)
        {
            _usersService = usersService;
            _userIndentityService = userIdentityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<bool> IsEmailUse(string email)
        {
            var result = await _usersService.IsEmailUseAsync(email);
            return result;
        }

        [HttpPost]
        public async Task<UserVm> Register(UserVm user, string password)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.Password = password;
            var result = await _usersService.RegisterAsync(userEntity);
            user = _mapper.Map<UserVm>(result);
            return user;
        }

        [HttpGet]
        public async Task<bool> IsAdminExist()
        {
            var result = await _usersService.IsAdminExistAsync();
            return result;
        }

        [HttpPost]
        public async Task<User> Login(string email, string password)
        {
            var user = await _userIndentityService.LoginAsync(email, password, HttpContext);
            return user;
        }

        void IDisposable.Dispose()
        {
            _usersService.Dispose();
        }
    }
}
