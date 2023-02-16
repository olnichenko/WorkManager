using Microsoft.EntityFrameworkCore;

namespace WorkManagerDal.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity
    {
        public List<TimeSpent>? TimeSpents { get; set; } = new List<TimeSpent>();
        public string Email { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool IsBlocked { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Project>? Projects { get; set; } = new List<Project>();
        public List<ProjectsToUsers>? ProjectsHasAccess { get; set; } = new List<ProjectsToUsers>();
        public List<Note>? Notes { get; set; } = new List<Note>();
        public List<Version>? Versions { get; set; } = new List<Version>();
        public List<Bug>? Bugs { get; set; } = new List<Bug>();
        public List<Feature>? Features { get; set; } = new List<Feature>();
        //public List<UploadedFile>? Files { get; set; } = new List<UploadedFile>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
