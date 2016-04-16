using PathFinder.Security.DAL.Entities;
using PathFinder.Security.WebApi.Commands;
using PathFinder.Security.WebApi.Queries;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PathFinder.Security.WebApi.Package
{
    public class SecurityWebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            
            container.Register<IRegisterUserCommand, RegisterUserCommand>(Lifestyle.Scoped);
            container.Register<IUserQuery, UserQuery>(Lifestyle.Scoped);
        }
    }
}
