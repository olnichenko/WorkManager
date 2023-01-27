﻿using Microsoft.EntityFrameworkCore;
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

        public async Task CreateOrUpdateFeatureAsync(Feature feature, long userId, long projectId)
        {
            if (feature.Id == 0)
            {
                feature.DateCreated = DateTime.UtcNow;
                _unitOfWork.Features.Create(feature);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                var tProject = await _unitOfWork.Projects.FindByConditionWithTracking(x => x.Id == projectId).SingleAsync();
                tUser.Features.Add(feature);
                tProject.Features.Add(feature);
            }
            else
            {
                var editedFiature = await _unitOfWork.Features.FindByCondition(x => x.Id == feature.Id).SingleOrDefaultAsync();
                if (editedFiature != null)
                {
                    editedFiature.Title = feature.Title;
                    editedFiature.Content = feature.Content;
                    _unitOfWork.Features.Update(editedFiature);
                }
                
            }
            
            await _unitOfWork.SaveAsync();
        }
    }
}
