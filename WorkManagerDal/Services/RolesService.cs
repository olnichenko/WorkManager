using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class RolesService : BaseService
    {
        public RolesService(WorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            var roles = await _unitOfWork.Roles.FindAll().ToListAsync();
            return roles;
        }
        public async Task<bool> IsRoleExistAsync(string name)
        {
            var result = await _unitOfWork.Roles.FindByCondition(x => x.Name == name).AnyAsync();
            return result;
        }
        public async Task<Role> CreateRoleAsync(Role role)
        {
            _unitOfWork.Roles.Create(role);
            await _unitOfWork.SaveAsync();
            return role;
        }
        public async Task<Role> UpdateRoleAsync(Role role)
        {
            _unitOfWork.Roles.Update(role);
            await _unitOfWork.SaveAsync();
            return role;
        }
        public async Task<Role> GetRole(int id)
        {
            var role = await _unitOfWork.Roles.FindByCondition(x => x.Id == id).Include<Permission>.SingleOrDefaultAsync();
            return role;
        }
    }
}
