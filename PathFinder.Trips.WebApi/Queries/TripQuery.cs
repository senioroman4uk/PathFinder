using System.Linq;
using PathFinder.Trips.DAL.Model;

namespace PathFinder.Trips.WebApi.Queries
{
    internal class TripQuery : ITripQuery
    {
        private readonly TripsContext _context;

        public TripQuery(TripsContext context)
        {
            _context = context;
        }

        public IQueryable<Trip> Trips
        {
            get { return _context.Trips; }
        }

        public IQueryable<IntermediatePoint> IntermediatePoints
        {
            get { return _context.IntermediatePoints; }
        }
    }
}
