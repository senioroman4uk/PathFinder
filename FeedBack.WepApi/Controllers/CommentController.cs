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
        private readonly FeedBackCommandContext _command;

        public CommentController(FeedBackContextQuery query, FeedBackCommandContext command)
        {
            _query = query;
            _command = command;
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
            Comment feedBack = _command.CreateComment(user, comment);
            if(_command.SaveComment(feedBack))
                return Ok();
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost]
        [Route(FeedBackRouteConstants.CreateUnAuthComment)]
        public IHttpActionResult CreateComment(CommentModel feedBackModel)
        {
            Comment comment = feedBackModel.CommentMapper();
            if(_command.SaveComment(comment))
                return Ok();
            return BadRequest();
        }
    }
}
