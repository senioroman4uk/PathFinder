using PathFinder.Infrastructure.Constants;

namespace PathFinder.Trips.WebApi.Constants
{
    public static class TripsRouteConstants
    {
        private const string Trips = "/trips";

        public const string TripsControllerRoutePrefix = CommonRouteConstants.RouteBase + Trips;

        public const string CreateTrip = CommonRouteConstants.Empty;
    }
}
