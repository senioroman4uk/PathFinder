using System;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace PathFinder.Infrastructure.Extensions
{
    public static class IPrincipalExtensions
    {
        public static bool TryGetUserId<T>(this IPrincipal user, out T id) where T : IConvertible
        {
            if (user.Identity == null)
            {
                id = default(T);
                return false;
            }

            id = user.Identity.GetUserId<T>();
            return true;
        }
    }
}
