using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class TimeSepentService : BaseService, ITimeSpentService
    {
        public TimeSepentService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<TimeSpent>> GetByBugAsync(long bugId)
        {
            var timeSpents = await _unitOfWork.TimeSpents
                .FindByCondition(x => x.Bug.Id == bugId)
                .Include(x => x.UserCreated)
                .OrderByDescending(x => x.DateFrom)
                .ToListAsync();
            return timeSpents;
        }
        public async Task<List<TimeSpent>> GetByFeatureAsync(long featureId)
        {
            var timeSpents = await _unitOfWork.TimeSpents
                .FindByCondition(x => x.Feature.Id == featureId)
                .Include(x => x.UserCreated)
                .OrderByDescending(x => x.DateFrom)
                .ToListAsync();
            return timeSpents;
        }
        public async Task<List<TimeSpent>> GetByProjectAsync(long projectId)
        {
            var timeSpents = await _unitOfWork.TimeSpents
                .FindByCondition(x => x.Feature.Project.Id == projectId || x.Bug.Project.Id == projectId)
                .Include(x => x.UserCreated)
                .OrderByDescending(x => x.DateFrom)
                .ToListAsync();
            return timeSpents;
        }
        public async Task CreateOrUpdateTimeSpentAsync(TimeSpent timeSpent, long userId, Feature feature, Bug bug)
        {
            if (feature == null && bug == null)
            {
                return;
            }
            if (timeSpent.Id == 0)
            {
                timeSpent.DateCreated = DateTime.UtcNow;
                _unitOfWork.TimeSpents.Create(timeSpent);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                tUser.TimeSpents.Add(timeSpent);
                if (feature != null)
                {
                    feature.TimeSpents.Add(timeSpent);
                }
                else
                {
                    bug.TimeSpents.Add(timeSpent);
                }
            }
            else
            {
                var editedTimeSpent = await _unitOfWork.TimeSpents.FindAsync(timeSpent.Id);
                if (editedTimeSpent != null)
                {
                    editedTimeSpent.Comment = timeSpent.Comment;
                    editedTimeSpent.HoursCount = timeSpent.HoursCount;
                    _unitOfWork.TimeSpents.Update(editedTimeSpent);
                }
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(TimeSpent timeSpent)
        {
            _unitOfWork.TimeSpents.Delete(timeSpent);
            await _unitOfWork.SaveAsync();
        }

    }
}
