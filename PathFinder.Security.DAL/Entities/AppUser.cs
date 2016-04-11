using Microsoft.AspNet.Identity.EntityFramework;

namespace PathFinder.Security.DAL.Entities
{
    class AppUser : IdentityUser<int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>
    {

    }
}
