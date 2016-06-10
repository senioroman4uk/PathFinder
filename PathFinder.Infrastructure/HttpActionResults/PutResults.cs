using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using PathFinder.Infrastructure.Extensions;

namespace PathFinder.Infrastructure.HttpActionResults
{
    public static class PutResults
    {
        public static IHttpActionResult Accepted(ApiController controller)
        {
            controller.CheckNotNull();
            return new StatusCodeResult(HttpStatusCode.Accepted, controller);
        }

        public static IHttpActionResult Accepted<TModel>(ApiController controller, TModel model)
        {
            controller.CheckNotNull();
            return new NegotiatedContentResult<TModel>(HttpStatusCode.Accepted, model, controller);
        }
    }
}
