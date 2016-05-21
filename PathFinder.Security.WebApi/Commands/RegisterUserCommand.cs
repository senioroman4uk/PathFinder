using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;
using PathFinder.Security.WebApi.Mappers;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Commands
{
    public interface IRegisterUserCommand
    {
        Task<AppUser> RegisterUser(RegisterUserModel userModel);
    }

    internal class RegisterUserCommand : IRegisterUserCommand
    {
        private readonly AppUserManager _userManager;

        public RegisterUserCommand(AppUserManager userManager)
        {
            _userManager = userManager;
        }
       
        public async Task<AppUser> RegisterUser(RegisterUserModel userModel)
        {
            var user = userModel.ToUserEntity();

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            return result.Succeeded ? user : null;
        }
    }
}
