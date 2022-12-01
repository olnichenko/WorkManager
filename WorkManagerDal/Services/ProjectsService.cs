using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class ProjectsService : BaseService, IProjectsService
    {
        public ProjectsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Project> GetProjectByIdAsync(long id)
        {
            var project = await _unitOfWork.Projects.FindByCondition(x => x.Id == id)
                .Include(x => x.UserCreated)
                .Include(x => x.UsersHasAccess)
                .SingleOrDefaultAsync();
            return project;
        }

        public async Task<Project> CreateProjectAsync(Project project, long userId)
        {
            _unitOfWork.Projects.Create(project);
            var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
            tUser.Projects.Add(project);
            await _unitOfWork.SaveAsync();
            return project;
        }

        public async Task<List<Project>> GetUserProjectsAsync(long userId)
        {
            var projects = await _unitOfWork.Projects.FindByCondition(x => x.UserCreated.Id == userId).ToListAsync();
            return projects;
        }
    }
}
