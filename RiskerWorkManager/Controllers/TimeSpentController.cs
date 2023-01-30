using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.PermissionValidators;
using RiskerWorkManager.Services;
using WorkManagerDal.Models;
using WorkManagerDal.Services;
using WorkManagerDal.ViewModels;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TimeSpentController : ControllerBase, IDisposable
    {
        private readonly ITimeSpentService _timeSpentService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IFeaturesService _featuresService;
        private readonly IBugsService _bugsService;
        private readonly IProjectsService _projectsService;

        public TimeSpentController(ITimeSpentService timeSpentService, 
            IUserIdentityService userIdentityService, 
            IFeaturesService featuresService, 
            IBugsService bugsService, 
            IProjectsService projectsService)
        {
            _timeSpentService = timeSpentService;
            _userIdentityService = userIdentityService;
            _featuresService = featuresService;
            _bugsService = bugsService;
            _projectsService = projectsService;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<TimeSpent>> GetTimeSpentByBug(long bugId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var bug = await _bugsService.FindAsync(bugId);
            var project = await _projectsService.GetProjectByIdAsync(bug.Project.Id);

            if (!project.ValidateUserViewPermission(user))
            {
                return null;
            }

            var timeSpentList = await _timeSpentService.GetByBugAsync(bugId);
            return timeSpentList;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<TimeSpent>> GetTimeSpentByFeature(long featureId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var feature = await _featuresService.FindAsync(featureId);
            var project = await _projectsService.GetProjectByIdAsync(feature.Project.Id);

            if (!project.ValidateUserViewPermission(user))
            {
                return null;
            }

            var timeSpentList = await _timeSpentService.GetByFeatureAsync(featureId);
            return timeSpentList;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<TimeSpent> CreateOrUpdateTimeSpent(TimeSpent timeSpent, long featureId, long bugId)
        {
            if (featureId == 0 && bugId == 0)
            {
                return null;
            }
            var user = _userIdentityService.GetCurrentUser(HttpContext);

            Project editedProject = null;
            Feature editedFeature = null;
            Bug editedBug = null;
            if (featureId > 0)
            {
                editedFeature = await _featuresService.FindAsync(featureId);
                editedProject = await _projectsService.GetProjectByIdAsync(editedFeature.Project.Id);
            }
            else
            {
                editedBug = await _bugsService.FindAsync(bugId);
                editedProject = await _projectsService.GetProjectByIdAsync(editedBug.Project.Id);
            }

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }

            await _timeSpentService.CreateOrUpdateTimeSpentAsync(timeSpent, user.Id, editedFeature, editedBug);
            return timeSpent;
        }

        public void Dispose()
        {
            _timeSpentService.Dispose();
            _featuresService.Dispose();
            _bugsService.Dispose();
            _projectsService.Dispose();
        }
    }
}
