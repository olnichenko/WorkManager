using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class TimeSpentRepository : RepositoryBase<TimeSpent>
    {
        public TimeSpentRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
