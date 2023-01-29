using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class BugsRepository : RepositoryBase<Bug>
    {
        public BugsRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
