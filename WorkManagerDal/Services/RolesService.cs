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
    }
}
