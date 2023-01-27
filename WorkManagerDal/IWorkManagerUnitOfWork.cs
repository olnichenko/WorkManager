﻿using WorkManagerDal.Repositories;

namespace WorkManagerDal
{
    public interface IWorkManagerUnitOfWork : IDisposable
    {
        UsersRepository Users { get; }
        RolesRepository Roles { get; }
        ProjectsRepository Projects { get; }
        PermissionsRepository Permissions { get; }
        FeaturesRepository Features { get; }
        VersionsRepository Versions { get; }
        Task SaveAsync();
    }
}
