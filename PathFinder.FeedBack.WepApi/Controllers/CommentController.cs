using System.Web.Http;
using FeedBack.WepApi.Commands;
using FeedBack.WepApi.Constants;
using FeedBack.WepApi.Extensions;
using FeedBack.WepApi.Mappers;
using FeedBack.WepApi.Models;
using FeedBack.WepApi.Query;
using Microsoft.AspNet.Identity;
using PathFinder.FeedBack.DAL.Model;
using PathFinder.Infrastructure.Constants;
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
            if (User.Identity == null)
                return Unauthorized();

            int userId = User.Identity.GetUserId<int>();
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
