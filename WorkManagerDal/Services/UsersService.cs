using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

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
            var existUser = await _unitOfWork.Users.FindByCondition(x => x.Email == email).FirstOrDefaultAsync();
            return existUser != null;
        }

        public async Task<User> RegisterAsync(User user)
        {
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
