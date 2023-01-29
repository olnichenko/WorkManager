using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public List<Role> Roles { get; set; } = new();

    }
}
