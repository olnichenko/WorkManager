using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class PermissionsRepository : RepositoryBase<Permission>
    {
        public PermissionsRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
