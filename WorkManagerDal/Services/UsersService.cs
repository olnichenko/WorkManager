using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkManagerDal.Models;
using WorkManagerDal.ViewModels;

namespace WorkManagerDal.Services
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task SetUserAdminRightsAsync(long userId, bool isAdmin)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.IsAdmin = isAdmin;
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task ChangeUserRoleAsync(long userId, int roleId)
        {
            var user = await GetUserByIdAsync(userId);
            var role = await _unitOfWork.Roles.FindByConditionWithTracking(x => x.Id == roleId).SingleOrDefaultAsync();
            if (role != null && user != null)
            {
                user.Role = role;
                //role.Users.Add(user);
                //_unitOfWork.Roles.Update(role);
                //_unitOfWork.Users.Update(user);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task ChangeUserBlockStatusAsync(long userId, bool isBlocked)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = isBlocked;
                await _unitOfWork.SaveAsync();
            }
        }

        private async Task<User> GetUserByIdAsync(long id)
        {
            return await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == id).Include("Role.Permissions").SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersPageAsync(int page, int pageSize, string email = null)
        {
            IQueryable<User> users;
            if (string.IsNullOrEmpty(email))
            {
                users = _unitOfWork.Users.FindAll();
            }
            else
            {
                users = _unitOfWork.Users.FindByCondition(x => x.Email.Contains(email));
            }

            return await users.Include("Role.Permissions").Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetUsersCountAsync(string email = null)
        {
            IQueryable<User> users;
            if (string.IsNullOrEmpty(email))
            {
                users = _unitOfWork.Users.FindAll();
            }
            else
            {
                users = _unitOfWork.Users.FindByCondition(x => x.Email.Contains(email));
            }
            return await users.CountAsync();
        }

        public async Task<User> GetActiveUserByEmailAndPasswordAsync(string email, string password)
        {
            password = GetHash(password);
            var user = await _unitOfWork.Users
                .FindByCondition(x => !x.IsBlocked && x.Email == email && x.Password == password)
                .Include("Role.Permissions")
                .SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> GetActiveUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users
                .FindByCondition(x => !x.IsBlocked && x.Email == email)
                .Include("Role.Permissions")
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
