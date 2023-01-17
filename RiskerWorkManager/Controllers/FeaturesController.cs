using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.PermissionValidators;
using RiskerWorkManager.Services;
using System.Runtime.InteropServices;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
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
        public async Task<List<Feature>> GetFeaturesByProject(long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var project = await _projectsService.GetProjectByIdAsync(projectId);

            if (!project.ValidateUserViewPermission(user))
            {
                return null;
            }

            var features = await _featuresService.GetFeaturesByProjectAsync(projectId);
            return features;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<Feature> CreateOrUpdateFeature(Feature feature, long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }
            if (feature.Id == 0)
            {
                feature.Project = editedProject;
                feature.UserCreated = user;
                feature.DateCreated = DateTime.Now;
            }
            await _featuresService.CreateOrUpdateFeatureAsync(feature);
            return feature;
        }

        public void Dispose()
        {
            _featuresService.Dispose();
            _projectsService.Dispose();
        }
    }
}
