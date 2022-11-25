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
        public WorkManagerUnitOfWork(WorkManagerDbContext dbContext)
        {
            _dbContext = dbContext;
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
