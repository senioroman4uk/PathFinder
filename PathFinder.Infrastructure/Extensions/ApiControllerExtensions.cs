using System;
using System.Web.Http;

namespace PathFinder.Infrastructure.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void CheckNotNull(this ApiController controller)
        {
            if (controller == null)
                throw new ArgumentException("controller");
        }
    }
}
