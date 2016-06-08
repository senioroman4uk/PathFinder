using System;
using PathFinder.FeedBack.DAL.Model;

namespace FeedBack.WepApi.Extensions
{
    public static class UserExtensions
    {
        public static Comment CreateFeedBack(this User user, string message)
        {
            return new Comment
            {
                Message = message,
                Author = string.Format("{0} {1}", user.FirstName, user.LastName),
                CreatedAt = DateTime.Now,
                Email = user.Email
            };
        }
    }
}
