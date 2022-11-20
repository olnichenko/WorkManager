using Microsoft.EntityFrameworkCore;

namespace WorkManagerDal.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity<long>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; } = new();
        public bool IsAdmin { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool IsBlocked { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
