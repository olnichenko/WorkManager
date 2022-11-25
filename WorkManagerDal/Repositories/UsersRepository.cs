using WorkManagerDal.Models;

namespace WorkManagerDal.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        public UsersRepository(WorkManagerDbContext workManagerDbContext) : base(workManagerDbContext)
        {
        }
    }
}
