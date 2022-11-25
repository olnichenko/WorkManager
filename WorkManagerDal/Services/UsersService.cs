using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<User> GetActiveUserByEmailAndPasswordAsync(string email, string password)
        {
            password = GetHash(password);
            var user = await _unitOfWork.Users
                .FindByCondition(x => !x.IsBlocked && x.Email == email && x.Password == password)
                .Include("Roles.Permissions")
                .SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> GetActiveUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users
                .FindByCondition(x => !x.IsBlocked && x.Email == email)
                .Include("Roles.Permissions")
                .SingleOrDefaultAsync();
            return user;
        }

        public async Task<bool> IsEmailUseAsync(string email)
        {
            var existUser = await _unitOfWork.Users.FindByCondition(x => x.Email == email).AnyAsync();
            return existUser;
        }

        public async Task<User> RegisterAsync(User user)
        {
            user.IsAdmin = false;
            return await RegisterUserAsync(user);
        }

        public async Task<bool> IsAdminExistAsync()
        {
            var result = await _unitOfWork.Users.FindByCondition(x => x.IsAdmin).AnyAsync();
            return result;
        }

        public async Task<User> RegisterAdminAsync(User user)
        {
            user.IsAdmin = true;
            return await RegisterUserAsync(user);
        }

        private async Task<User> RegisterUserAsync(User user)
        {
            user.DateRegistration = DateTime.Now;
            user.Password = GetHash(user.Password);
            _unitOfWork.Users.Create(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        protected virtual string GetHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
