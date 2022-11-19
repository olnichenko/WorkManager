using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;
using WorkManagerDal.Repositories;

namespace WorkManagerDal.Services
{
    public class UsersService : IDisposable
    {
        private readonly WorkManagerUnitOfWork _unitOfWork;
        public UsersService(WorkManagerUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
