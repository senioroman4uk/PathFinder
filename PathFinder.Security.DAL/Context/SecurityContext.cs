using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PathFinder.Security.DAL.Entities;

namespace PathFinder.Security.DAL.Context
{
    class SecurityContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>
    {
        public SecurityContext() :
            // TODO: To constants
            base("PathFinder")
        {
            
        }
    }
}
