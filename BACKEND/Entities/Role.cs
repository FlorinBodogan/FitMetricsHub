using Microsoft.AspNetCore.Identity;

namespace BACKEND.Entities
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}