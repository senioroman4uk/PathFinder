using PathFinder.FeedBack.DAL.Model;

namespace FeedBack.WepApi.Query
{
    public interface IFeedBackCommandContext
    {
        Comment CreateComment(User user, string commentMessage);
        bool SaveComment(Comment comment);
    }
}
