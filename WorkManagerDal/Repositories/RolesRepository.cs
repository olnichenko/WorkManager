using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class RolesRepository : RepositoryBase<Role>
    {
        public RolesRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
