// file:	Controllers\AccountController.cs
//
// summary:	Implements the account controller class

using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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
        private readonly IRegisterUserCommand _registerUserCommand;
        /// <summary>   Manager for user. </summary>
        private readonly AppUserManager _userManager;
        private readonly IUpdateUserCommand _updateUserContext;
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="registerUserCommand">  The register user command. </param>
        /// <param name="userManager">          Manager for user. </param>

        public AccountController(IRegisterUserCommand registerUserCommand, AppUserManager userManager, 
            IUpdateUserCommand updateUserContext)
        {
            _registerUserCommand = registerUserCommand;
            _userManager = userManager;
            updateUserContext = _updateUserContext;
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
            AppUser user = await _registerUserCommand.RegisterUser(model);

            if (user == null)
                return new StatusCodeResult((HttpStatusCode)422, this);

            return Created(String.Format(EntityReturnUrls.UserEndPoint, user.Id), user);
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
        [Route(SecurityRouteConstants.AccountControllerRoutePrefix)]
        public IHttpActionResult UserUpdate(UpdateUserModel model)
        {
            _updateUserContext.UpdateUser(model.ToUserEntity());
            return Ok();
        }
    }
}
