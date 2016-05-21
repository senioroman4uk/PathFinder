using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;
using PathFinder.Security.WebApi.Commands;
using PathFinder.Security.WebApi.Constants;
using PathFinder.Security.WebApi.Mappers;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Controllers
{
    [RoutePrefix(SecurityRouteConstants.AccountControllerRoutePrefix)]
    public class AccountController : ApiController
    {
        private readonly IRegisterUserCommand _registerUserCommand;
        private readonly AppUserManager _userManager;

        public AccountController(IRegisterUserCommand command, AppUserManager userManager)
        {
            _registerUserCommand = command;
            _userManager = userManager;
        }

        /// <summary>
        /// Registers new user into the system
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route(SecurityRouteConstants.Register)]
        public async Task<IHttpActionResult> Register(RegisterUserModel model)
        {
            AppUser user = await _registerUserCommand.RegisterUser(model);

            if (user == null)
                return new StatusCodeResult((HttpStatusCode)422, this);

            return Created(String.Format(EntityReturnUrls.UserEndPoint, user.Id), user);
        }

        /// <summary>
        /// Finds user by id
        /// </summary>
        /// <param name="id">user identifier</param>
        [HttpGet]
        [Authorize]
        [Route(SecurityRouteConstants.GetUser)]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            UserModel model = user.ToUserModel();
            return Ok(model);
        }
    }
}
