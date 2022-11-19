using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity<long>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; } = new();
        public bool IsAdmin { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
