using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class Version : EntityWithProject
    {
        public List<Bug>? Bugs { get; set; } = new List<Bug>();
        public List<Feature>? Features { get; set;} = new List<Feature>();
        //public List<UploadedFile>? Files { get; set; } = new List<UploadedFile>();
    }
}
