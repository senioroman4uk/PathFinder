using Microsoft.AspNet.Identity;
using PathFinder.Security.DAL.Entities;

namespace PathFinder.Security.DAL.Managers
{
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(IUserStore<AppUser, int> store) : base(store)
        {

        }
    }
}
