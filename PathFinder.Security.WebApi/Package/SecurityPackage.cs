using System.Data.Entity;
using Microsoft.AspNet.Identity;
using PathFinder.Security.DAL.Context;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;
using PathFinder.Security.WebApi.Commands;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PathFinder.Security.WebApi.Package
{
    public class SecurityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DbContext, SecurityContext>(Lifestyle.Scoped);
            container.Register<IUserStore<AppUser, int>, AppUserStore>(Lifestyle.Scoped);
            container.Register<AppUserManager>(Lifestyle.Scoped);
            container.Register<IRegisterUserCommand, RegisterUserCommand>(Lifestyle.Scoped);

            // User validator configuration
            container.RegisterInitializer<AppUserManager>((manager =>
            {
                IIdentityValidator<AppUser> validator = new UserValidator<AppUser, int>(manager)
                {
                    RequireUniqueEmail = true,
                    AllowOnlyAlphanumericUserNames = true
                };
                manager.UserValidator = validator;
            }));
        }
    }
}
