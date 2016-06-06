// file:	Mappers\UserMapper.cs
//
// summary:	Implements the user mapper class

using PathFinder.Security.Authentication.Models;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Mappers
{
    /// <summary>   A user mapper. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    public static class UserMapper
    {
        /// <summary>
        ///     A RegisterUserModel extension method that converts a model to a user entity.
        /// </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="model">    The model to act on. </param>
        ///
        /// <returns>   model as an AppUser. </returns>

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

        /// <summary>   An AppUser extension method that converts a user to a user model. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="user"> The user to act on. </param>
        ///
        /// <returns>   user as an UserModel. </returns>

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
