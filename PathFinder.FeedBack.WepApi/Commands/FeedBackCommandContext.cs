using PathFinder.FeedBack.DAL.Model;

namespace FeedBack.WepApi.Commands
{
    public class FeedBackCommandContext : IFeedBackCommandContext
    {
        private readonly FeedBackContext _context;

        public FeedBackCommandContext(FeedBackContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
