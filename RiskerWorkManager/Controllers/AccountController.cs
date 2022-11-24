using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
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
        private readonly TokenService _tokenService;
        //private readonly PermissionService _permissionService;

        public AccountController(UsersService usersService, UserIdentityService userIdentityService, IMapper mapper, TokenService tokenService)
        {
            _usersService = usersService;
            _userIndentityService = userIdentityService;
            _mapper = mapper;
            _tokenService = tokenService;
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

        [HttpPost]
        public async Task<UserVm> RegisterAdmin(UserVm user, string password)
        {
            var isAdminExist = await IsAdminExist();
            if (isAdminExist)
            {
                return null;
            }
            var userEntity = _mapper.Map<User>(user);
            userEntity.Password = password;
            var result = await _usersService.RegisterAdminAsync(userEntity);
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
        public async Task<UserVm> Login(string email, string password)
        {
            var user = await _userIndentityService.LoginAsync(email, password, HttpContext);
            if (user != null)
            {
                var userVm = _mapper.Map<UserVm>(user);
                var token = _tokenService.GenerateToken(user);
                userVm.Token = token;
                return userVm;
            }
            return null;
        }

        [HttpPost]
        public async Task<UserVm> LoginByToken(string token)
        {
            var user = await _userIndentityService.LoginByTokenAsync(token, HttpContext);
            if (user != null)
            {
                var userVm = _mapper.Map<UserVm>(user);
                token = _tokenService.GenerateToken(user);
                userVm.Token = token;
                return userVm;
            }
            return null;
        }

        [HttpPost]
        [AuthorizePermission]
        public IResult Logout()
        {
            _userIndentityService.Logout(HttpContext);
            return Results.Ok();
        }

        void IDisposable.Dispose()
        {
            _usersService.Dispose();
        }
    }
}
