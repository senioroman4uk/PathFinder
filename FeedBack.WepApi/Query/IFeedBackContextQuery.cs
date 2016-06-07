using PathFinder.FeedBack.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBack.WepApi.Query
{
    public interface IFeedBackContextQuery
    {
        IOrderedQueryable<User> Users { get; }
        Comment CreateComment(User user, string commentMessage);
        bool SaveComment(Comment comment);
    }
}
