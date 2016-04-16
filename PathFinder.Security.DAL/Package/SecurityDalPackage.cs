using System.Data.Entity;
using Microsoft.AspNet.Identity;
using PathFinder.Security.DAL.Context;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PathFinder.Security.DAL.Package
{
    public class SecurityDalPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
           
            container.Register<DbContext, SecurityContext>(Lifestyle.Scoped);
            container.Register<IUserStore<AppUser,int>, AppUserStore>(Lifestyle.Scoped);
            container.Register<AppUserManager>(Lifestyle.Scoped);
        }
    }
}
