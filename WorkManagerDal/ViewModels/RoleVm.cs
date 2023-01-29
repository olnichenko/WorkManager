using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.ViewModels
{
    public class RoleVm : BaseEntity
    {
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; } = new();
    }
}
