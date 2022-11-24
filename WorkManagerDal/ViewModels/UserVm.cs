using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.ViewModels
{
    public class UserVm : BaseEntity<long>
    {
        public string Email { get; set; }
        public List<RoleVm>? Roles { get; set; }
        public bool IsAdmin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Token { get; set; }
    }
}
