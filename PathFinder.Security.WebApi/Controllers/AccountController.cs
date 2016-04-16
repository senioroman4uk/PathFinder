using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using PathFinder.Infrastructure.Constants;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.WebApi.Commands;
using PathFinder.Security.WebApi.Mappers;
using PathFinder.Security.WebApi.Models;
using PathFinder.Security.WebApi.Queries;

namespace PathFinder.Security.WebApi.Controllers
{
    [RoutePrefix(CommonRouteConstants.RouteBase)]
    public class AccountController : ApiController
    {
        private readonly IRegisterUserCommand _registerUserCommand;
        private readonly IUserQuery _userQuery;

        public AccountController(IRegisterUserCommand command, IUserQuery query)
        {
            _registerUserCommand = command;
            _userQuery = query;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("account/register")]
        public async Task<IHttpActionResult> Register(RegisterUserModel model)
        {
            int? id = await _registerUserCommand.RegisterUser(model);

            if (!id.HasValue)
                return new StatusCodeResult((HttpStatusCode)422, this);

            var user = model.ToUserEntity();
            return Created($"account/users/{id}", user);
        }

        [HttpGet]
        [Authorize]
        [Route("account/users/{id}")]
        public IHttpActionResult GetUser(int id)
        {
            AppUser user = _userQuery.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            var model = user.ToUserModel();
            return Ok(model);
        }
    }
}
