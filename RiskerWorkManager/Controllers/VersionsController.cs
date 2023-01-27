using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.Services;
using WorkManagerDal.Services;
using WorkManagerDal.Models;
using RiskerWorkManager.PermissionValidators;
using WorkManagerDal.ViewModels;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VersionsController : ControllerBase, IDisposable
    {
        private readonly IProjectsService _projectsService;
        private readonly IVersionsService _versionsService;
        private readonly IUserIdentityService _userIdentityService;

        public VersionsController(IProjectsService projectsService, IVersionsService versionsService, IUserIdentityService userIdentityService)
        {
            _projectsService = projectsService;
            _versionsService = versionsService;
            _userIdentityService = userIdentityService;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<WorkManagerDal.Models.Version> CreateOrUpdateVersion(WorkManagerDal.Models.Version version, long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }

            await _versionsService.CreateOrUpdateVersionAsync(version, user.Id, projectId);
            return version;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<WorkManagerDal.Models.Version>> GetVersionsByProject(long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var project = await _projectsService.GetProjectByIdAsync(projectId);

            if (!project.ValidateUserViewPermission(user))
            {
                return null;
            }

            var versions = await _versionsService.GetVersionsByProjectAsync(projectId);
            return versions;
        }

        public void Dispose()
        {
            _projectsService.Dispose();
            _versionsService.Dispose();
        }
    }
}
