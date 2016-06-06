using Microsoft.AspNet.Identity.EntityFramework;

namespace PathFinder.Security.Authentication.Models
{
    public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(SecurityContext context) : base(context)
        {
        }
    }
}
