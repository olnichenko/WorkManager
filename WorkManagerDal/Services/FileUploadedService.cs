using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Services
{
    public class FileUploadedService : BaseService, IFileUploadedService
    {
        public FileUploadedService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
