using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.Services;
using System.Formats.Asn1;
using WorkManagerDal.Models;
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

        public void Dispose()
        {
            _projectsService.Dispose();
        }
    }
}
