using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface IProjectsService : IBaseService
    {
        Task<Project> CreateProjectAsync(Project project, long userId);
        Task<List<Project>> GetUserProjectsAsync(long userId);
    }
}
