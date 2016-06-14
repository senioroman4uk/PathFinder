using Microsoft.AspNet.Identity;

namespace PathFinder.Security.Authentication.Models
{
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(IUserStore<AppUser, int> store) : base(store)
        {

        }
    }
}
