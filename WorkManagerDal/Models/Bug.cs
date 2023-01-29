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
    }
}
