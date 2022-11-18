using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class ControllerAction : BaseEntity<long>
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
