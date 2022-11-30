using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class ProjectsToUsers : BaseEntity<long>
    {
        public User User { get; set; }
        public Project Project { get; set; }
    }
}
