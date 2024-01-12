using Microsoft.AspNetCore.Identity;

namespace BACKEND.Entities
{
    public class User : IdentityUser
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public List<BMI> BMIs { get; set; }
        public List<RMB> RMBs { get; set; }
        public List<ArterialTension> ArterialTensions { get; set; }
        public List<Cholesterol> Cholesterols { get; set; }
        public List<Triglycerides> Triglycerides { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}