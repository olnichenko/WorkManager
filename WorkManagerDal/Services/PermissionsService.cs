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
        private readonly List<PermissionData> _allPermissions;

        public const string Users_List = "Users_List";
        public const string Users_Edit = "Users_Edit";
        public const string Roles_List = "Roles_List";
        public const string Role_Edit = "Role_Edit";
        public const string Permission_Edit = "Permission_Edit";
        public const string Logs_View = "Logs_View";
        public const string Add_Project = "Add_Project";

        public PermissionsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _workManagerUnitOfWork = unitOfWork;

            _allPermissions = new List<PermissionData>
            {
                new PermissionData(Users_List, "View all registered users"),
                new PermissionData(Roles_List, "View all roles"),
                new PermissionData(Role_Edit, "Edit role"),
                new PermissionData(Permission_Edit, "Edit roles permissions"),
                new PermissionData(Logs_View, "Access to view logs"),
                new PermissionData(Users_Edit, "Acces to edit users"),
                new PermissionData(Add_Project, "Create new projects")
            };
        }

        public async Task<Permission> CreateAndGetPermissionAsnyc(string permissionName)
        {
            var permission = await _workManagerUnitOfWork.Permissions.FindByConditionWithTracking(x => x.Name == permissionName).Include("Roles").SingleOrDefaultAsync();
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
            var result = user.Role?.Permissions.Any(x => x.Name == permissionName);
            return result.HasValue ? result.Value : false;
        }

        public IEnumerable<PermissionData> GetAllPermissions()
        {
            return _allPermissions;
        }
    }
}
