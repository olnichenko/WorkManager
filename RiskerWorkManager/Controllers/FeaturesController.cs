using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.PermissionValidators;
using RiskerWorkManager.Services;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase, IDisposable
    {
        private readonly IFeaturesService _featuresService;
        private readonly IProjectsService _projectsService;
        private readonly IUserIdentityService _userIdentityService;

        public FeaturesController(IFeaturesService featuresService, IUserIdentityService userIdentityService, IProjectsService projectsService)
        {
            _featuresService = featuresService;
            _userIdentityService = userIdentityService;
            _projectsService = projectsService;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<Feature> CreateFeature(long projectId, Feature feature)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }

            feature.Project = editedProject;
            await _featuresService.CreateFeatureAsync(feature);
            return feature;
        }

        public void Dispose()
        {
            _featuresService.Dispose();
            _projectsService.Dispose();
        }
    }
}
