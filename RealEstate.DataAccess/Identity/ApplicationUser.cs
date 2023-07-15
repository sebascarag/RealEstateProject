using Microsoft.AspNetCore.Identity;

namespace RealEstate.DataAccess.Identity
{
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationRole : IdentityRole
    {
    }

    public class ApplicationUserRoles : IdentityUserRole<string>
    {
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationRole? Role { get; set; }
    }
}
