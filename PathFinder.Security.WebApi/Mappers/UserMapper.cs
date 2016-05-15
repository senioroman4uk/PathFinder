using PathFinder.Security.DAL.Entities;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Mappers
{
    public static class UserMapper
    {
        public static AppUser ToUserEntity(this RegisterUserModel model)
        {
            return new AppUser()
            {
                Email =  model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };
        }

        public static UserModel ToUserModel(this AppUser user)
        {
            return new UserModel()
            {
                Id = user.Id,
                AvatarUrl = user.AvatarPath,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
        }
    }
}
