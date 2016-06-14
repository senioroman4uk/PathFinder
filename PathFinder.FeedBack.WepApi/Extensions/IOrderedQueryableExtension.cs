using PathFinder.FeedBack.DAL.Model;
using System;
using System.Linq;

namespace FeedBack.WepApi.Extensions
{
    public static class IOrderedQueryableExtension
    {
        public static User GetUserById(this IOrderedQueryable<User> collection, int id)
        {
            return collection.FirstOrDefault(x => x.Id == id);          
        }
    }
}
