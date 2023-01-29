using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.ViewModels
{
    public class ProjectVm : BaseEntity
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public User? UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ProjectsToUsers>? UsersHasAccess { get; set; } = new List<ProjectsToUsers> { };
    }
}
