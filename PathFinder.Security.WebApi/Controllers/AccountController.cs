// file:	Controllers\AccountController.cs
//
// summary:	Implements the account controller class

using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using PathFinder.Infrastructure.Extensions;
using PathFinder.Infrastructure.HttpActionResults;
using PathFinder.Security.Authentication.Models;
using PathFinder.Security.UserManagement.Commands;
using PathFinder.Security.UserManagement.Constants;
using PathFinder.Security.UserManagement.Mappers;
using PathFinder.Security.UserManagement.Models;

namespace PathFinder.Security.UserManagement.Controllers
{
    /// <summary>   A controller for handling accounts. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    [RoutePrefix(SecurityRouteConstants.AccountControllerRoutePrefix)]
    public class AccountController : ApiController
    {
        /// <summary>   The register user command. </summary>
        private readonly ISecurityContextCommand _securityContextCommand;
        /// <summary>   Manager for user. </summary>
        private readonly AppUserManager _userManager;
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="securityContextCommand">  The security context user command. </param>
        /// <param name="userManager">          Manager for user. </param>

        public AccountController(ISecurityContextCommand securityContextCommand, AppUserManager userManager)
        {
            _securityContextCommand = securityContextCommand;
            _userManager = userManager;
        }

        /// <summary>   Registers new user into the system. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="model">    . </param>
        ///
        /// <returns>   A Task&lt;IHttpActionResult&gt; </returns>

        [AllowAnonymous]
        [HttpPost]
        [Route(SecurityRouteConstants.Register)]
        public async Task<IHttpActionResult> Register(RegisterUserModel model)
        {
            var user = model.ToUserEntity();
            var result = await _securityContextCommand.RegisterUser(user, model.Password);

            if (!result.Succeeded)
                return new StatusCodeResult((HttpStatusCode)422, this);

            var response = user.ToUserModel();
            return PostResults.Created(this, response);
        }

        /// <summary>   Finds user by id. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="id">   user identifier. </param>
        ///
        /// <returns>   The user. </returns>

        [HttpGet]
        [Route(SecurityRouteConstants.GetUser)]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            UserModel model = user.ToUserModel();
            return Ok(model);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UserUpdate(UpdateUserModel model)
        {
            int userId;
            if (!User.TryGetUserId(out userId))
                return Unauthorized();

            if (model.Id != userId)
                return StatusCode(HttpStatusCode.Forbidden);

            var userToUpdate = _userManager.FindById(model.Id);
            if (userToUpdate == null)
                return NotFound();

            _securityContextCommand.UpdateUser(userToUpdate, model);

            var response = userToUpdate.ToUserModel();
            return PutResults.Accepted(this, response);
        }
    }
}
