using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IUsersService : IBaseService
    {
        Task<User> GetActiveUserByEmailAndPasswordAsync(string email, string password);
        Task<User> GetActiveUserByEmailAsync(string email);
        Task<bool> IsEmailUseAsync(string email);
        Task<User> RegisterAsync(User user);
        Task<bool> IsAdminExistAsync();
        Task<User> RegisterAdminAsync(User user);
    }
}
