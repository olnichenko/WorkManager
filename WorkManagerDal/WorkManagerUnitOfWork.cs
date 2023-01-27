using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Repositories;

namespace WorkManagerDal
{
    public class WorkManagerUnitOfWork : IWorkManagerUnitOfWork
    {
        private WorkManagerDbContext _dbContext;
        private RolesRepository _rolesRepository;
        private UsersRepository _usersRepository;
        private PermissionsRepository _permissionsRepository;
        private ProjectsRepository _projectsRepository;
        private FeaturesRepository _featuresRepository;
        private VersionsRepository _versionsRepository;
        public WorkManagerUnitOfWork(WorkManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public VersionsRepository Versions
        {
            get
            {
                if (_versionsRepository == null)
                {
                    _versionsRepository = new VersionsRepository(_dbContext);
                }
                return _versionsRepository;
            }
        }

        public FeaturesRepository Features
        {
            get
            {
                if (_featuresRepository == null)
                {
                    _featuresRepository = new FeaturesRepository(_dbContext);
                }
                return _featuresRepository;
            }
        }

        public ProjectsRepository Projects
        {
            get
            {
                if (_projectsRepository == null)
                {
                    _projectsRepository = new ProjectsRepository(_dbContext);
                }
                return _projectsRepository;
            }
        }

        public PermissionsRepository Permissions
        {
            get
            {
                if (_permissionsRepository == null)
                {
                    _permissionsRepository = new PermissionsRepository(_dbContext);
                }
                return _permissionsRepository;
            }
        }

        public RolesRepository Roles
        {
            get
            {
                if (_rolesRepository == null)
                {
                    _rolesRepository = new RolesRepository(_dbContext);
                }
                return _rolesRepository;
            }
        }

        public UsersRepository Users
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UsersRepository(_dbContext);
                }
                return _usersRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
