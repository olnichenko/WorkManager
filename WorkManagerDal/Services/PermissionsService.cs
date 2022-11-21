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
        public const string Test = "Test permission";
        public const string Users_List = "Users_List";
        public const string Roles_List = "Roles_List";
        public bool IsUserHaveAcces(User user, string permissionName)
        {
            if (user == null || !user.IsBlocked)
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
    }
}
