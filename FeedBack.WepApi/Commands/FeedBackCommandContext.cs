using System;
using FeedBack.WepApi.Query;
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

        public Comment CreateComment(User user, string commentMessage)
        {
            return new Comment
            {
                Message = commentMessage,
                Author = user.FirstName + " " + user.LastName,
                CreatedAt = DateTime.Now,
                Email = user.Email
            };
        }

        public bool SaveComment(Comment comment)
        {
            _context.Comments.Add(comment);
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
