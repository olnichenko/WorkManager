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
    public class BugsService : BaseService, IBugsService
    {
        public BugsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Bug>> GetBugsByFilterAsync(ProjectItemFilterVm filter)
        {
            var bugs = _unitOfWork.Bugs
                .FindByCondition(x => x.Project.Id == filter.ProjectId && x.IsDeleted != true)
                .Include(x => x.UserCreated)
                .Include(x => x.SolvedInVersion)
                .OrderByDescending(x => x.DateCreated) as IQueryable<Bug>;
            if (!string.IsNullOrEmpty(filter.UserCreatedEmail))
            {
                bugs = bugs.Where(x => x.UserCreated.Email.Contains(filter.UserCreatedEmail));
            }
            if (!string.IsNullOrEmpty(filter.Title))
            {
                bugs = bugs.Where(x => x.Title.Contains(filter.Title));
            }
            if (filter.StartDateFrom.HasValue)
            {
                bugs = bugs.Where(x => x.DateCreated.Value.Date >= filter.StartDateFrom.Value.Date);
            }
            if (filter.EndDateFrom.HasValue)
            {
                bugs = bugs.Where(x => x.DateCreated.Value.Date <= filter.EndDateFrom.Value.Date);
            }
            if (filter.SolvedVersion > 0)
            {
                bugs = bugs.Where(x => x.SolvedInVersion.Id == filter.SolvedVersion);
            }
            var result = await bugs.ToListAsync();
            return result;
        }
        public async Task<Bug> FindAsync(long bugId)
        {
            var bug = await _unitOfWork.Bugs
                .FindByConditionWithTracking(x => x.Id == bugId)
                .Include(x => x.Project)
                .SingleOrDefaultAsync();
            return bug;
        }

        public async Task DeleteAsync(Bug bug)
        {
            bug.IsDeleted = true;
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Bug>> GetBugsByProjectAsync(long projectId)
        {
            var bugs = await _unitOfWork.Bugs
                .FindByCondition(x => x.Project.Id == projectId && x.IsDeleted != true)
                .Include(x => x.UserCreated)
                .Include(x => x.SolvedInVersion)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
            return bugs;
        }

        public async Task CreateOrUpdateBugAsync(Bug bug, long userId, long projectId, long solvedInVersionId)
        {
            var version = await _unitOfWork.Versions.FindByConditionWithTracking(x => x.Id == solvedInVersionId).SingleOrDefaultAsync();

            if (bug.Id == 0)
            {
                bug.DateCreated = DateTime.Now;
                _unitOfWork.Bugs.Create(bug);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                var tProject = await _unitOfWork.Projects.FindByConditionWithTracking(x => x.Id == projectId).SingleAsync();
                tUser.Bugs.Add(bug);
                tProject.Bugs.Add(bug);
                if (version != null)
                {
                    version.Bugs.Add(bug);
                }
            }
            else
            {
                var editedBug = await _unitOfWork.Bugs
                    .FindByCondition(x => x.Id == bug.Id)
                    .Include(x => x.SolvedInVersion)
                    .SingleOrDefaultAsync();
                if (editedBug != null)
                {
                    editedBug.Title = bug.Title;
                    editedBug.Content = bug.Content;
                    _unitOfWork.Bugs.Update(editedBug);
                    if (version != null)
                    {
                        version.Bugs.Add(editedBug);
                    }
                    else
                    {
                        if (editedBug.SolvedInVersion != null)
                        {
                            var previousVersion = await _unitOfWork.Versions
                                .FindByConditionWithTracking(x => x.Id == editedBug.SolvedInVersion.Id)
                                .Include(x => x.Bugs)
                                .SingleAsync();
                            previousVersion.Bugs.Remove(editedBug);
                        }
                    }
                }

            }

            await _unitOfWork.SaveAsync();
        }
    }
}
