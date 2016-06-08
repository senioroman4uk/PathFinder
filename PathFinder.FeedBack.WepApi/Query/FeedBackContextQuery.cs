using PathFinder.FeedBack.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBack.WepApi.Query
{
    public class FeedBackContextQuery : IFeedBackContextQuery
    {
        private readonly FeedBackContext _context;

        public FeedBackContextQuery(FeedBackContext contetx)
        {
            _context = contetx;
        }

        public IOrderedQueryable<User> Users
        {
            get { return _context.Users; }
        }
    }
}
