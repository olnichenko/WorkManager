using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Services
{
    public interface IVersionsService : IBaseService
    {
        Task CreateOrUpdateVersionAsync(Models.Version version, long userId, long projectId);
        Task<List<Models.Version>> GetVersionsByProjectAsync(long projectId);
    }
}
