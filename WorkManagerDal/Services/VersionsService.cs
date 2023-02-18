using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Services
{
    public class VersionsService : BaseService, IVersionsService
    {
        public VersionsService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task CreateOrUpdateVersionAsync(Models.Version version, long userId, long projectId)
        {
            if (version.Id == 0)
            {
                version.DateCreated = DateTime.Now;
                _unitOfWork.Versions.Create(version);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                var tProject = await _unitOfWork.Projects.FindByConditionWithTracking(x => x.Id == projectId).SingleAsync();
                tUser.Versions.Add(version);
                tProject.Versions.Add(version);
            }
            else{
                var editedVersion = await _unitOfWork.Versions.FindByConditionWithTracking(x => x.Id == version.Id).SingleOrDefaultAsync();
                editedVersion.Title = version.Title;
                editedVersion.Content = version.Content;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Models.Version>> GetVersionsByProjectAsync(long projectId)
        {
            var versions = await _unitOfWork.Versions
                .FindByCondition(x => x.Project.Id == projectId)
                .OrderByDescending(x => x.DateCreated)
                .Include(x => x.UserCreated).ToListAsync();
            return versions;
        }
    }
}

