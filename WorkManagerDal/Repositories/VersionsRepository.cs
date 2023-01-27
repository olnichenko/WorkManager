using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class VersionsRepository : RepositoryBase<Models.Version>
    {
        public VersionsRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
