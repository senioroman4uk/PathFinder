using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using PathFinder.Infrastructure.Extensions;

namespace PathFinder.Infrastructure.HttpActionResults
{
    public static class PostResults
    {
        public static IHttpActionResult Created<TModel>(ApiController controller, TModel model)
        {
            controller.CheckNotNull();
            var res = controller.Request.CreateResponse(HttpStatusCode.Created, model);
            return new ResponseMessageResult(res);
        }
    }
}
