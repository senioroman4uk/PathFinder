using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PathFinder.Security.DAL.Entities;
using PathFinder.Security.DAL.Managers;
using PathFinder.Security.WebApi.Mappers;
using PathFinder.Security.WebApi.Models;


namespace PathFinder.Security.WebApi.Commands
{
    public interface IUpdateUserCommand
    {
        void UpdateUser(AppUser user);
    }
    internal class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly AppUserManager _userManager;

        public void UpdateUser(AppUser user)
        {
            _userManager.Update(user);

        }
    }
}
