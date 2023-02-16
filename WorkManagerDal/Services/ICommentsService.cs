using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface ICommentsService : IBaseService
    {
        Task CreateOrUpdateAsync(Comment comment, long userId, long featureId, long bugId);
        Task DeleteAsync(long commentId);
        Task<Comment> FindAsync(long commentId);
        Task<List<Comment>> GetCommentsByFeatureAsync(long featureId);
    }
}
