using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class CommentsRepository : RepositoryBase<Comment>
    {
        public CommentsRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
