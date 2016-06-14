using PathFinder.Infrastructure.Constants;

namespace PathFinder.Security.UserManagement.Constants
{
    static class EntityReturnUrls
    {
        public const string UserEndPoint =
            SecurityRouteConstants.AccountControllerRoutePrefix + CommonRouteConstants.Slash + SecurityRouteConstants.Users
            + CommonRouteConstants.Slash + "{0}";
    }
}
