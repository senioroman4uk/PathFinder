using PathFinder.FeedBack.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBack.WepApi.Query
{
    public class FeedBackCommandContext : IFeedBackCommandContext
    {
        private readonly FeedBackContext _context;

        public FeedBackCommandContext(FeedBackContext contetx)
        {
            _context = contetx;
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
