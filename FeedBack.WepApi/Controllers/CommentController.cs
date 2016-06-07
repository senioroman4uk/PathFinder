using FeedBack.WepApi.Models;
using FeedBack.WepApi.Query;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PathFinder.FeedBack.DAL.Model;
using FeedBack.WepApi.Extensions;
using FeedBack.WepApi.Mappers;
using PathFinder.Infrastructure.Constants;
using FeedBack.WepApi.Constants;

namespace PathFinder.FeedBack.DAL.Controllers
{
    [RoutePrefix(CommonRouteConstants.RouteBase)]
    public class CommentController : ApiController
    {
        private readonly FeedBackContextQuery _query;

        public CommentController(FeedBackContextQuery query)
        {
            _query = query;
        }

        [Authorize]
        [HttpPost]
        [Route(FeedBackRouteConstants.CreateComment)]
        public IHttpActionResult CreateComment(string comment)
        {
            string id = User.Identity.GetUserId();
            User user = _query.Users.GetUserById(id);
            if (user == null)
                return NotFound();
            Comment feedBack =_query.CreateComment(user, comment);
            if(_query.SaveComment(feedBack))
                return Ok();
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost]
        [Route(FeedBackRouteConstants.CreateUnAuthComment)]
        public IHttpActionResult CreateComment(CommentModel feedBackModel)
        {
            Comment comment = feedBackModel.CommentMapper();
            if(_query.SaveComment(comment))
                return Ok();
            return BadRequest();
        }
    }
}
