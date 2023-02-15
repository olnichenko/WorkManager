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

        public async Task CreateOrUpdateAsync(Comment comment, long userId, long featureId, long bugId)
        {
            if (comment.Id == 0)
            {
                comment.DateCreated = DateTime.UtcNow;
                _unitOfWork.Comments.Create(comment);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                //tUser.Co
            }
        }
    }
}
