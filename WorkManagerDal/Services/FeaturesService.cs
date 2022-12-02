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

        public async Task CreateFeatureAsync(Feature feature)
        {
            _unitOfWork.Features.Create(feature);
            await _unitOfWork.SaveAsync();
        }
    }
}
