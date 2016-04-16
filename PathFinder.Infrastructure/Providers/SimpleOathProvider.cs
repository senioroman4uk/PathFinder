using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;

namespace PathFinder.Infrastructure.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.UserName == null || context.Password == null)
            {
                context.SetError("invalid_credentials", "The user name or password is incorrect.");
                return;
            }
            var manager = context.OwinContext.GetUserManager<AppUserManager>();
            
            AppUser user = await manager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_credentials", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            context.Validated(identity);

        }
    }
}
