using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IUsersService : IBaseService
    {
        Task<List<User>> GetUsersPageAsync(int page, int pageSize, string email = null);
        Task<int> GetUsersCountAsync(string email = null);
        Task<User> GetActiveUserByEmailAndPasswordAsync(string email, string password);
        Task<User> GetActiveUserByEmailAsync(string email);
        Task<bool> IsEmailUseAsync(string email);
        Task<User> RegisterAsync(User user);
        Task<bool> IsAdminExistAsync();
        Task<User> RegisterAdminAsync(User user);
    }
}
