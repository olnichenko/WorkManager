using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Services
{
    public abstract class BaseService : IDisposable
    {
        protected readonly WorkManagerUnitOfWork _unitOfWork;
        public BaseService(WorkManagerUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
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
