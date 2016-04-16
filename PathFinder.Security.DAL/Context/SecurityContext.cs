using Microsoft.AspNet.Identity.EntityFramework;
using PathFinder.Security.DAL.Entities;

namespace PathFinder.Security.DAL.Context
{
    public class SecurityContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public SecurityContext() :
            // TODO: To constants
            base("PathFinder")
        {
            
        }
    }
}
