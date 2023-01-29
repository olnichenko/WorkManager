using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.PermissionValidators;
using RiskerWorkManager.Services;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BugsController : ControllerBase, IDisposable
    {
        private readonly IBugsService _bugsService;
        private readonly IProjectsService _projectsService;
        private readonly IUserIdentityService _userIdentityService;

        public BugsController(IBugsService bugsService, IUserIdentityService userIdentityService, IProjectsService projectsService)
        {
            _bugsService = bugsService;
            _userIdentityService = userIdentityService;
            _projectsService = projectsService;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<bool> DeleteBug(long bugId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var bug = await _bugsService.FindAsync(bugId);
            var project = await _projectsService.GetProjectByIdAsync(bug.Project.Id);

            if (!project.ValidateUserViewPermission(user))
            {
                return false;
            }

            await _bugsService.DeleteAsync(bug);
            return true;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<Bug>> GetBugsByProject(long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var project = await _projectsService.GetProjectByIdAsync(projectId);

            if (!project.ValidateUserViewPermission(user))
            {
                return null;
            }

            var bugs = await _bugsService.GetBugsByProjectAsync(projectId);
            return bugs;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<Bug> CreateOrUpdateBug(Bug bug, long projectId, long solvedInversionId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }
            await _bugsService.CreateOrUpdateBugAsync(bug, user.Id, editedProject.Id, solvedInversionId);
            return bug;
        }

        public void Dispose()
        {
            _bugsService.Dispose();
            _projectsService.Dispose();
        }
    }
}
