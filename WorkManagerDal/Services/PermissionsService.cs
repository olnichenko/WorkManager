using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class PermissionsService : BaseService, IPermissionsService
    {
        private readonly IWorkManagerUnitOfWork _workManagerUnitOfWork;
        public const string Users_List = "Users_List";
        public const string Roles_List = "Roles_List";
        public const string Role_Edit = "Role_Edit";
        public const string Permission_Edit = "Permission_Edit";

        public PermissionsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _workManagerUnitOfWork = unitOfWork;
        }

        public async Task<Permission> CreateAndGetPermissionAsnyc(string permissionName)
        {
            var permission = await _workManagerUnitOfWork.Permissions.FindByCondition(x => x.Name == permissionName).Include("Roles").SingleOrDefaultAsync();
            if (permission == null)
            {
                permission = new Permission
                {
                    Name = permissionName
                };
                _workManagerUnitOfWork.Permissions.Create(permission);
                await _workManagerUnitOfWork.SaveAsync();
            }
            return permission;
        }

        public bool IsUserHaveAcces(User user, string permissionName)
        {
            if (user == null || user.IsBlocked)
            {
                return false;
            }
            if (user.IsAdmin)
            {
                return true;
            }
            var result = user.Roles.Any(x => x.Permissions.Any(y => y.Name == permissionName));
            return result;
        }

        public IEnumerable<PermissionData> GetAllPermissions()
        {
            var permissions = new List<PermissionData>();

            permissions.Add(new PermissionData(Users_List, "View all registered users"));
            permissions.Add(new PermissionData(Roles_List, "View all roles"));
            permissions.Add(new PermissionData(Role_Edit, "Edit role"));
            permissions.Add(new PermissionData(Permission_Edit, "Edit roles permissions"));

            return permissions;
        }
    }
}
