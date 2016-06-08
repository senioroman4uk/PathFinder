// file:	Commands\RegisterUserCommand.cs
//
// summary:	Implements the register user command class

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;
using PathFinder.Security.WebApi.Mappers;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Commands
{
    /// <summary>   Interface for register user command. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    public interface ISecurityContextCommand
    {
        /// <summary>   Registers the user described by userModel. </summary>
        ///
        /// <param name="userModel">    The user model. </param>
        ///
        /// <returns>   A Task&lt;AppUser&gt; </returns>

        Task<AppUser> RegisterUser(RegisterUserModel userModel);
        void UpdateUser(AppUser user);
    }

    /// <summary>   A register user command. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    internal class SecurityContextCommand : ISecurityContextCommand
    {
        /// <summary>   Manager for user. </summary>
        private readonly AppUserManager _userManager;

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="userManager">  Manager for user. </param>

        public SecurityContextCommand(AppUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>   Registers the user described by userModel. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="userModel">    The user model. </param>
        ///
        /// <returns>   A Task&lt;AppUser&gt; </returns>

        public async Task<AppUser> RegisterUser(RegisterUserModel userModel)
        {
            var user = userModel.ToUserEntity();

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            return result.Succeeded ? user : null;
        }

        public void UpdateUser(AppUser user)
        {
            _userManager.Update(user);

        }
    }
}
