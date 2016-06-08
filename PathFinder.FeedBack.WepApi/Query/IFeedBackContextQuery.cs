using PathFinder.FeedBack.DAL.Model;
using System.Linq;

namespace FeedBack.WepApi.Query
{
    public interface IFeedBackContextQuery
    {
        IOrderedQueryable<User> Users { get; }
    }
}
