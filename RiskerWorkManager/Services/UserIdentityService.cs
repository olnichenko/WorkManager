using WorkManagerDal.Models;
using WorkManagerDal.Services;
using RiskerWorkManager.Extensions;

namespace RiskerWorkManager.Services
{
    public class UserIdentityService
    {
        private const string _userSessionKey = "_userSessionKey";
        private readonly UsersService _usersService;
        public UserIdentityService(UsersService usersService) {
            _usersService= usersService;
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
