using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PathFinder.Security.DAL.Entities;

namespace PathFinder.Security.DAL.Context
{
    public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(DbContext context) : base(context)
        {
        }
    }
}
