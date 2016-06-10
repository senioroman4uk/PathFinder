// file:	Commands\RegisterUserCommand.cs
//
// summary:	Implements the register user command class

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PathFinder.Security.Authentication.Models;

namespace PathFinder.Security.UserManagement.Commands
{
    /// <summary>   Interface for register user command. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    public interface ISecurityContextCommand
    {
        /// <summary>   Registers the user described by userModel. </summary>
        /// 
        /// <param name="user">User that will be registred</param>
        /// <param name="password">Password of registred user</param>
        /// <returns>   A Task&lt;AppUser&gt; </returns>
        Task<IdentityResult> RegisterUser(AppUser user, string password);
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

        ///  <summary>   Registers the user described by userModel. </summary>
        /// 
        ///  <remarks>   Vladyslav, 24.05.2016. </remarks>
        /// 
        ///  <param name="user">User that will be created. </param>
        /// <param name="password">Password of created user</param>
        /// <returns>   A Task&lt;AppUser&gt; </returns>
        public async Task<IdentityResult> RegisterUser(AppUser user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public void UpdateUser(AppUser user)
        {
            _userManager.Update(user);

        }
    }
}
