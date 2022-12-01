﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.Services;
using System.Formats.Asn1;
using WorkManagerDal.Models;
using WorkManagerDal.Repositories;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProjectsController : ControllerBase, IDisposable
    {
        private readonly IProjectsService _projectsService;
        private readonly IUserIdentityService _userIdentityService;
        public ProjectsController(IProjectsService projectsService, IUserIdentityService userIdentityService)
        {
            _projectsService = projectsService;
            _userIdentityService = userIdentityService;
        }

        [HttpPost]
        [AuthorizePermission(PermissionsService.Add_Project)]
        public async Task<Project> CreateProject(Project project)
        {
            project.DateCreated = DateTime.Now;
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var result = await _projectsService.CreateProjectAsync(project, user.Id);
            return result;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<Project>> GetMyProjects()
        {
            var userId = _userIdentityService.GetCurrentUser(HttpContext).Id;
            var projects = await _projectsService.GetUserProjectsAsync(userId);
            return projects;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<Project> GetProject(long id)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var project = await _projectsService.GetProjectByIdAsync(id);
            if (project == null || project.UserCreated == null || project.UsersHasAccess == null)
            {
                return null;
            }
            if (project.UserCreated.Id != user.Id && project.UsersHasAccess.All(x => x.Id != user.Id))
            {
                return null;
            }
            return project;
        }

        public void Dispose()
        {
            _projectsService.Dispose();
        }
    }
}
