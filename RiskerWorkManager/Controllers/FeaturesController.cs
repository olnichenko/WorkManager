using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.PermissionValidators;
using RiskerWorkManager.Services;
using System.Runtime.InteropServices;
using WorkManagerDal.Models;
using WorkManagerDal.Services;
using WorkManagerDal.ViewModels;

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
        public async Task<bool> DeleteFeature(long featureId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var feature = await _featuresService.FindAsync(featureId);
            var project = await _projectsService.GetProjectByIdAsync(feature.Project.Id);

            if (!project.ValidateUserViewPermission(user))
            {
                return false;
            }

            await _featuresService.DeleteAsync(feature);
            return true;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<Feature>> GetFeaturesByFilter(ProjectItemFilterVm filter)
        {
            var features = await _featuresService.GetFeaturesByFilterAsync(filter);
            return features;
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
        public async Task<Feature> CreateOrUpdateFeature(Feature feature, long projectId, long solvedInversionId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }
            await _featuresService.CreateOrUpdateFeatureAsync(feature, user.Id, editedProject.Id, solvedInversionId);
            return feature;
        }

        public void Dispose()
        {
            _featuresService.Dispose();
            _projectsService.Dispose();
        }
    }
}
