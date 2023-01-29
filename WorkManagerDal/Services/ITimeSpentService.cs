using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface ITimeSpentService : IBaseService
    {
        Task<List<TimeSpent>> GetByBugAsync(long bugId);
        Task<List<TimeSpent>> GetByFeatureAsync(long featureId);
        Task<List<TimeSpent>> GetByProjectAsync(long projectId);
        Task CreateOrUpdateTimeSpentAsync(TimeSpent timeSpent, long userId, long featureId, long bugId);
        Task DeleteAsync(TimeSpent timeSpent);
    }
}
