using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;

namespace PathFinder.Security.WebApi.Queries
{
    class UserQuery : IUserQuery
    {
        private readonly AppUserManager _appUserManager;

        public UserQuery(AppUserManager userManager)
        {
            _appUserManager = userManager;
        }

        public IOrderedQueryable<AppUser> Users
        {
            get
            {
                return (IOrderedQueryable<AppUser>)_appUserManager.Users;
            }
        }
    }
}
