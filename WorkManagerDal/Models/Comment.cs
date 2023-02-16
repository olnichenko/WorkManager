using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class Comment : BaseEntity
    {
        public User? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Content { get; set; }
        //public List<UploadedFile>? Files { get; set; } = new List<UploadedFile>();
        public Bug? Bug { get; set; }
        public Feature? Feature { get; set; }
    }
}
