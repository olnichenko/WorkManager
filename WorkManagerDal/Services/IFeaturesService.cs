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
        Task CreateOrUpdateFeatureAsync(Feature feature, long userId, long projectId);
        Task<List<Feature>> GetFeaturesByProjectAsync(long projectId);
    }
}
