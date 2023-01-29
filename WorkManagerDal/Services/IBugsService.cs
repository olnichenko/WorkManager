using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IBugsService : IBaseService
    {
        Task<Bug> FindAsync(long bugId);
        Task DeleteAsync(Bug bug);
        Task<List<Bug>> GetBugsByProjectAsync(long projectId);
        Task CreateOrUpdateBugAsync(Bug bug, long userId, long projectId, long solvedInVersionId);
    }
}
