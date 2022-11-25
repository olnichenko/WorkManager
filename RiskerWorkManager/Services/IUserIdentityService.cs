using WorkManagerDal.Models;

namespace RiskerWorkManager.Services
{
    public interface IUserIdentityService
    {
        Task<User> LoginAsync(string username, string password, HttpContext context);
        Task<User> LoginByTokenAsync(string token, HttpContext context);
        void Logout(HttpContext context);
        bool IsUserLoggedIn(HttpContext context);
        User GetCurrentUser(HttpContext context);
    }
}
