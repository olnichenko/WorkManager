using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IPermissionsService : IBaseService
    {
        Task<Permission> CreateAndGetPermissionAsnyc(string permissionName);
        bool IsUserHaveAcces(User user, string permissionName);
        IEnumerable<PermissionData> GetAllPermissions();
    }
}
