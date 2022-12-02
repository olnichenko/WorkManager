using WorkManagerDal.Repositories;

namespace WorkManagerDal
{
    public interface IWorkManagerUnitOfWork : IDisposable
    {
        UsersRepository Users { get; }
        RolesRepository Roles { get; }
        ProjectsRepository Projects { get; }
        PermissionsRepository Permissions { get; }
        FeaturesRepository Features { get; }
        Task SaveAsync();
    }
}
