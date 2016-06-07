using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
