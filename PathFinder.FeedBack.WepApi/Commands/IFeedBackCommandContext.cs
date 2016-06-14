using PathFinder.FeedBack.DAL.Model;

namespace FeedBack.WepApi.Commands
{
    public interface IFeedBackCommandContext
    {
        void AddComment(Comment comment);
    }
}
