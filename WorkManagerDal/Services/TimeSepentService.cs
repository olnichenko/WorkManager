using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;
using WorkManagerDal.ViewModels;

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
                .Include(x => x.Bug)
                .Include(x => x.Feature)
                .OrderByDescending(x => x.DateFrom)
                .ToListAsync();
            return timeSpents;
        }
        public async Task<List<TimeSpent>> GetByFilterAsync(TimeSheetFilterVm filter)
        {
            var timeSpents = _unitOfWork
                .TimeSpents
                .FindAll()
                .Include(x => x.UserCreated)
                .Include(x => x.Bug.Project)
                .Include(x => x.Feature.Project)
                .OrderByDescending(x => x.DateFrom) as IQueryable<TimeSpent>;

            if (filter.ProjectId > 0)
            {
                timeSpents = timeSpents.Where(x => x.Bug.Project.Id == filter.ProjectId || x.Feature.Project.Id == filter.ProjectId);
            }
            if (!string.IsNullOrEmpty(filter.UserCreatedEmail))
            {
                timeSpents = timeSpents.Where(x => x.UserCreated.Email == filter.UserCreatedEmail);
            }
            if (filter.StartDateFrom.HasValue)
            {
                timeSpents = timeSpents.Where(x => x.DateFrom.Value.Date >= filter.StartDateFrom.Value.Date);
            }
            if (filter.EndDateFrom.HasValue)
            {
                timeSpents = timeSpents.Where(x => x.DateFrom.Value.Date <= filter.EndDateFrom.Value.Date);
            }
            if (filter.TaskId > 0)
            {
                if (filter.TaskType == "Bug")
                {
                    timeSpents = timeSpents.Where(x => x.Bug.Id == filter.TaskId);
                }
                if (filter.TaskType == "Feature")
                {
                    timeSpents = timeSpents.Where(x => x.Feature.Id == filter.TaskId);
                }
                if (filter.TaskType == "All")
                {
                    timeSpents = timeSpents.Where(x => x.Feature.Id == filter.TaskId || x.Bug.Id == filter.TaskId);
                }
            }
            else
            {
                if (filter.TaskType == "Bug")
                {
                    timeSpents = timeSpents.Where(x => x.Bug != null);
                }
                if (filter.TaskType == "Feature")
                {
                    timeSpents = timeSpents.Where(x => x.Feature != null);
                }
            }
            
            var result = await timeSpents
                .ToListAsync();
            return result;
        }


        public async Task CreateOrUpdateTimeSpentAsync(TimeSpent timeSpent, long userId, Feature feature, Bug bug)
        {
            if (feature == null && bug == null)
            {
                return;
            }
            if (timeSpent.Id == 0)
            {
                timeSpent.DateCreated = DateTime.Now;
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
