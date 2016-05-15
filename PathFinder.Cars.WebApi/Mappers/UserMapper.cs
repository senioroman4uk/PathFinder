using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Models;

namespace PathFinder.Cars.WebApi.Mappers
{
    internal static class UserMapper
    {
        public static UserModel ToUserModel(this User user)
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
