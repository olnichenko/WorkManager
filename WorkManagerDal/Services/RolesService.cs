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
    public class RolesService : BaseService, IRolesService
    {
        public RolesService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            var roles = await _unitOfWork.Roles.FindAll().Include("Permissions").ToListAsync();
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
        public async Task DeleteRoleToPemissionAsync(int roleId, string permissionName)
        {
            var role = await _unitOfWork.Roles.FindByConditionWithTracking(x => x.Id == roleId).Include(x => x.Permissions).SingleOrDefaultAsync();
            var permission = await _unitOfWork.Permissions.FindByConditionWithTracking(x => x.Name == permissionName).Include(x => x.Roles).SingleOrDefaultAsync();
            role.Permissions.Remove(permission);

            await _unitOfWork.SaveAsync();
        }

        public async Task AddRoleToPermissionAsync(int roleId, string permissionName)
        {
            var role = await _unitOfWork.Roles.FindByConditionWithTracking(x => x.Id == roleId).Include(x => x.Permissions).SingleOrDefaultAsync();
            var permission = await _unitOfWork.Permissions.FindByConditionWithTracking(x => x.Name == permissionName).Include(x => x.Roles).SingleOrDefaultAsync();
            role.Permissions.Add(permission);

            await _unitOfWork.SaveAsync();
        }
        public async Task<Role> UpdateRoleAsync(Role role)
        {
            _unitOfWork.Roles.Update(role);
            await _unitOfWork.SaveAsync();
            return role;
        }
        public async Task<Role> GetRoleWithPermissionsAsync(int id)
        {
            var role = await _unitOfWork.Roles.FindByCondition(x => x.Id == id).Include("Permissions").SingleOrDefaultAsync();
            return role;
        }
    }
}
