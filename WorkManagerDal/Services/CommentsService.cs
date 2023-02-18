using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class CommentsService : BaseService, ICommentsService
    {
        public CommentsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Comment>> GetCommentsByFeatureAsync(long featureId)
        {
            var comments = await _unitOfWork.Comments
                .FindByCondition(x => x.Feature.Id == featureId)
                .Include(x => x.UserCreated)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
            return comments;
        }
        public async Task<List<Comment>> GetCommentsByBugAsync(long bugId)
        {
            var comments = await _unitOfWork.Comments
                .FindByCondition(x => x.Bug.Id == bugId)
                .Include(x => x.UserCreated)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
            return comments;
        }
        public async Task CreateOrUpdateAsync(Comment comment, long userId, long featureId, long bugId)
        {
            if (comment.Id == 0)
            {
                comment.DateCreated = DateTime.Now;
                _unitOfWork.Comments.Create(comment);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                tUser.Comments.Add(comment);
                if (featureId > 0)
                {
                    var tFeature = await _unitOfWork.Features.FindByConditionWithTracking(x => x.Id == featureId).SingleAsync();
                    tFeature.Comments.Add(comment);
                }
                if (bugId > 0)
                {
                    var tBug = await _unitOfWork.Bugs.FindByConditionWithTracking(x => x.Id == bugId).SingleAsync();
                    tBug.Comments.Add(comment);
                }
            }
            else
            {
                var updatedComment = await _unitOfWork.Comments.FindByConditionWithTracking(x => x.Id == comment.Id).SingleAsync();
                updatedComment.Content = comment.Content;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long commentId)
        {
            var comment = await _unitOfWork.Comments.FindAsync(commentId);
            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Comment> FindAsync(long commentId)
        {
            var comment = await _unitOfWork.Comments
                .FindByCondition(x => x.Id == commentId)
                .Include(x => x.Feature.Project)
                .Include(x => x.Bug.Project)
                .SingleAsync();
            return comment;
        }
    }
}
