using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class PermissionsService
    {
        public const string Users_List = "Users_List";
        public const string Roles_List = "Roles_List";
        public const string Role_Edit = "Role_Edit";
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

        public List<PermissionData> GetAllPermissions()
        {
            var permissions = new List<PermissionData>();

            permissions.Add(new PermissionData(Users_List, "View all registered users"));
            permissions.Add(new PermissionData(Roles_List, "View all roles"));
            permissions.Add(new PermissionData(Role_Edit, "Edit role"));

            return permissions;
        }
    }
}
