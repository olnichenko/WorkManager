using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class FeaturesService : BaseService, IFeaturesService
    {
        public FeaturesService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Feature>> GetFeaturesByProjectAsync(long projectId)
        {
            var features = await _unitOfWork.Features.FindByCondition(x => x.Project.Id == projectId).ToListAsync();
            return features;
        }

        public async Task CreateOrUpdateFeatureAsync(Feature feature)
        {
            if (feature.Id != 0)
            {
                _unitOfWork.Features.Create(feature);
            }
            else
            {
                _unitOfWork.Features.Update(feature);
            }
            
            await _unitOfWork.SaveAsync();
        }
    }
}
