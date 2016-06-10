using System.Web.Http;
using FeedBack.WepApi.Commands;
using FeedBack.WepApi.Constants;
using FeedBack.WepApi.Extensions;
using FeedBack.WepApi.Mappers;
using FeedBack.WepApi.Models;
using FeedBack.WepApi.Query;
using PathFinder.FeedBack.DAL.Model;
using PathFinder.Infrastructure.Constants;
using PathFinder.Infrastructure.Extensions;
using PathFinder.Infrastructure.HttpActionResults;

namespace FeedBack.WepApi.Controllers
{
    [RoutePrefix(CommonRouteConstants.RouteBase)]
    public class CommentController : ApiController
    {
        private readonly IFeedBackContextQuery _query;
        private readonly IFeedBackCommandContext _command;

        public CommentController(IFeedBackContextQuery query, IFeedBackCommandContext command)
        {
            _query = query;
            _command = command;
        }

        [HttpPost]
        [Route(FeedBackRouteConstants.CreateComment)]
        public IHttpActionResult CreateComment(string comment)
        {
            int userId;
            if (!User.TryGetUserId(out userId))
                return Unauthorized();

            User user = _query.Users.GetUserById(userId);
            if (user == null)
                return NotFound();

            Comment feedBack = user.CreateFeedBack(comment);
            _command.AddComment(feedBack);
            var model = feedBack.ToFeedBackResponseModel();

            return PostResults.Created(this, model);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(FeedBackRouteConstants.CreateUnAuthComment)]
        public IHttpActionResult CreateComment(FeedBackModel feedBackModel)
        {
            Comment feedBack = feedBackModel.ToFeedBack();
            _command.AddComment(feedBack);
            var model = feedBack.ToFeedBackResponseModel();
            return PostResults.Created(this, model);
        }
    }
}
