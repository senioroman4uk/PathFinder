using PathFinder.Infrastructure.Constants;

namespace PathFinder.Security.UserManagement.Constants
{
    public static class SecurityRouteConstants
    {
        private const string Account = "/account";

        public const string Users = "users";

        public const string AccountControllerRoutePrefix = CommonRouteConstants.RouteBase + Account;

        public const string Register = "register";

        public const string GetUser = Users + CommonRouteConstants.Slash + CommonRouteConstants.Id;
    }
}
