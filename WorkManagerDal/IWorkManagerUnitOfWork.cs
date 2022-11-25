using WorkManagerDal.Repositories;

namespace WorkManagerDal
{
    public interface IWorkManagerUnitOfWork : IDisposable
    {
        UsersRepository Users { get; }
        RolesRepository Roles { get; }
        PermissionsRepository Permissions { get; }
        Task SaveAsync();
    }
}
