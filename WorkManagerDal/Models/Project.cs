using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class Project : BaseEntity<long>
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public User UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<User>? Developers { get; set; } = new List<User> { };
        public List<Note>? Notes { get; set; } = new List<Note> { };
        public List<Version>? Versions { get; set; } = new List<Version> { };
        public List<Bug> Bugs { get; set; } = new List<Bug>();
        public List<Feature> Features { get; set; } = new List<Feature>();
    }
}
