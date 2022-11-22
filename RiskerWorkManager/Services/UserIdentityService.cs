using WorkManagerDal.Models;
using WorkManagerDal.Services;
using RiskerWorkManager.Extensions;

namespace RiskerWorkManager.Services
{
    public class UserIdentityService
    {
        private const string _userSessionKey = "_userSessionKey";
        private readonly UsersService _usersService;
        private readonly TokenService _tokenService;
        public UserIdentityService(UsersService usersService, TokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        public async Task<User> LoginAsync(string username, string password, HttpContext context)
        {
            var user = await _usersService.GetActiveUserByEmailAndPasswordAsync(username, password);
            if (user == null) {
                return null;
            }

            context.Session.Set(_userSessionKey, user);
            return user;
        }

        public async Task<User> LoginByTokenAsync(string token, HttpContext context)
        {
            var email = _tokenService.GetEmailFromToken(token);
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _usersService.GetActiveUserByEmailAsync(email);
                if (user == null)
                {
                    return null;
                }

                context.Session.Set(_userSessionKey, user);
                return user;
            }
            return null;
        }

        public void Logout(HttpContext context)
        {
            context.Session.Clear();
        }

        public bool IsUserLoggedIn(HttpContext context)
        {
            return context.Session.Keys.Contains(_userSessionKey);
        }

        public User GetCurrentUser(HttpContext context)
        {
            return context.Session.Get<User>(_userSessionKey);
        }
    }
}
