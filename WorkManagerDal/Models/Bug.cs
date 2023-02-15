using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class Bug : EntityWithProject
    {
        public Version? SolvedInVersion { get; set; }
        public bool? IsDeleted { get; set; }
        public List<TimeSpent>? TimeSpents { get; set; } = new List<TimeSpent>();
        public List<UploadedFile>? Files { get; set; } = new List<UploadedFile>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
