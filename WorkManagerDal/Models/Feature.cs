using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class Feature : EntityWithProject
    {
        public Version? SolvedInVersion { get; set; }
    }
}
