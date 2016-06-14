using System.Linq;
using PathFinder.Trips.DAL.Model;

namespace PathFinder.Trips.WebApi.Extensions
{
    internal static class TripsExtensions
    {
        public static IntermediatePoint GetStartPoint(this Trip trip)
        {
            return trip.IntermediatePoints.FirstOrDefault(ip => ip.IsStart == 1);
        }

        public static IntermediatePoint GetEndPoint(this Trip trip)
        {
            return trip.IntermediatePoints.FirstOrDefault(ip => ip.IsEnd == 1);
        }
    }
}
