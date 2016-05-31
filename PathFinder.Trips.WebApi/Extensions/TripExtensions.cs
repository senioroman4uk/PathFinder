using System.Data.Entity;
using System.Linq;
using PathFinder.Trips.DAL.Model;
using PathFinder.Trips.WebApi.Mappers;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Extensions
{
    internal static class TripExtensions
    {
        public static IQueryable<Trip> FindTrips(this IQueryable<Trip> trips, TripQueryModel request)
        {
            var origin = request.Start.ToIntermediatePoint();
            var destination = request.End.ToIntermediatePoint();
            var radius = request.Radius;

            trips = trips.Include(x => x.IntermediatePoints)
                .Where(x => x.IntermediatePoints.Any(
                    ip => ip.Coordinates.Distance(origin.Coordinates) < radius))
                .Where(x => x.IntermediatePoints.Any(
                    ip => ip.Coordinates.Distance(destination.Coordinates) < radius));

            return trips;
        }

    }
}
