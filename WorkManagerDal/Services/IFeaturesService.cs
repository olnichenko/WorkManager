using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IFeaturesService : IBaseService
    {
        Task CreateOrUpdateFeatureAsync(Feature feature, long userId, long projectId, long solvedInversion);
        Task<List<Feature>> GetFeaturesByProjectAsync(long projectId);
        Task DeleteAsync(Feature feature);
        Task<Feature> FindAsync(long featureId);
    }
}
