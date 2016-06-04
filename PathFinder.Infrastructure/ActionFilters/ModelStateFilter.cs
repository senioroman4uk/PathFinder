using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PathFinder.Infrastructure.ActionFilters
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Response = context.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    context.ModelState);
            }
        }
    }
}
