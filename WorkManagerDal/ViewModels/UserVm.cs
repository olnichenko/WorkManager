using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.ViewModels
{
    public class UserVm : BaseEntity
    {
        public DateTime DateRegistration { get; set; }
        public bool IsBlocked { get; set; }
        public string Email { get; set; }
        public RoleVm? Role { get; set; }
        public string? RoleName { get; set; }
        public bool IsAdmin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Token { get; set; }
    }
}
