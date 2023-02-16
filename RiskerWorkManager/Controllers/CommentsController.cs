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
    public class CommentsController : ControllerBase, IDisposable
    {
        private readonly ICommentsService _commentsService;
        private readonly IProjectsService _projectsService;
        private readonly IUserIdentityService _userIdentityService;

        public CommentsController(ICommentsService commentsService, IUserIdentityService userIdentityService, IProjectsService projectsService)
        {
            _commentsService = commentsService;
            _userIdentityService = userIdentityService;
            _projectsService = projectsService;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<Comment> CreateOrUpdateComment(Comment comment, long featureId, long bugId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            //var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            //if (!editedProject.ValidateUserViewPermission(user))
            //{
            //    return null;
            //}

            await _commentsService.CreateOrUpdateAsync(comment, user.Id, featureId, bugId);
            return comment;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<Comment>> GetCommentsByFeature(long featureId)
        {
            var comments = await _commentsService.GetCommentsByFeatureAsync(featureId);
            return comments;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<bool> DeleteComment(long commentId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var comment = await _commentsService.FindAsync(commentId);
            var projectId = comment.Feature == null ? comment.Bug.Project.Id : comment.Feature.Project.Id;
            var project = await _projectsService.GetProjectByIdAsync(projectId);

            if (!project.ValidateUserViewPermission(user))
            {
                return false;
            }

            await _commentsService.DeleteAsync(commentId);
            return true;
        }

        public void Dispose()
        {
            _commentsService.Dispose();
            _projectsService.Dispose();
        }
    }
}
