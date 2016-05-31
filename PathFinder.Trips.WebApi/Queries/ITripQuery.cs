using System.Linq;
using PathFinder.Trips.DAL.Model;

namespace PathFinder.Trips.WebApi.Queries
{
    public interface ITripQuery
    {
        IQueryable<IntermediatePoint> IntermediatePoints { get; }
        IQueryable<Trip> Trips { get; }
    }
}