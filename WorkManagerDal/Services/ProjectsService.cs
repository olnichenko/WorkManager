﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;
using WorkManagerDal.ViewModels;

namespace WorkManagerDal.Services
{
    public class ProjectsService : BaseService, IProjectsService
    {
        public ProjectsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<MenuVm> GetProjectMenuVmAsync(long projectId)
        {
            var project = await _unitOfWork.Projects
                        .FindByCondition(x => x.Id == projectId)
                        .Include(x => x.Features)
                        .Include(x => x.Bugs)
                        .SingleAsync();
            if (project == null)
            {
                return null;
            }
            var featuresCount = project.Features
                                .Count(x => x.IsDeleted != true && x.SolvedInVersion == null);
            var bugsCount = project.Bugs
                            .Count(x => x.IsDeleted != true && x.SolvedInVersion == null);
            return new MenuVm
            {
                UnSolvedFeatures = featuresCount,
                UnSolvedBugs = bugsCount
            };
        }

        public async Task<Project> EditProjectAsync(Project project)
        {
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveAsync();
            return project;
        }

        public async Task<bool> AddUserToProjectAsync(Project project, string email)
        {
            var user = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Email == email).Include(x => x.ProjectsHasAccess).SingleOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            var projectsToUser = new ProjectsToUsers
            {
                Project = project,
                User = user
            };
            user.ProjectsHasAccess.Add(projectsToUser);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromProjectAsync(Project project, string email)
        {
            var user = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Email == email).Include("ProjectsHasAccess.Project").SingleOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            var projectToUser = user.ProjectsHasAccess.FirstOrDefault(x => x.Project.Id == project.Id);
            user.ProjectsHasAccess.Remove(projectToUser);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Project> GetProjectByIdAsync(long id)
        {
            var project = await _unitOfWork.Projects.FindByCondition(x => x.Id == id)
                .Include(x => x.UserCreated)
                .Include("UsersHasAccess.User")
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
            var projects = await _unitOfWork.Projects
                .FindByCondition(x => x.UserCreated.Id == userId && x.IsDeleted != true)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
            return projects;
        }

        public async Task<List<Project>> GetUserHaveAccessProjectsAsync(long userId)
        {
            var project = await _unitOfWork.Projects
                .FindByCondition(x => x.UsersHasAccess.Any(y => y.User.Id == userId) && x.IsDeleted != true)
                .OrderByDescending(x => x.DateCreated).ToListAsync();
            return project;
        }
    }
}
