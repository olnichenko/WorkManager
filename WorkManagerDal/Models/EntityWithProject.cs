using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class EntityWithProject : BaseEntity<long>
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public Project? Project { get; set; }
        public User? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
