using WorkManagerDal.Models;

namespace RiskerWorkManager.PermissionValidators
{
    public static class ProjectPermissionVlidator
    {
        public static bool ValidateUserViewPermission(this Project project, User user)
        {
            if (project == null || project.UserCreated == null || project.UsersHasAccess == null)
            {
                return false;
            }
            if (project.UserCreated.Id != user.Id && project.UsersHasAccess.All(x => x.User.Id != user.Id))
            {
                return false;
            }
            return true;
        }
    }
}
