using System.Linq;
using System.Web.Http;
using PathFinder.Trips.WebApi.Extensions;
using PathFinder.Trips.WebApi.Models;
using PathFinder.Trips.WebApi.Queries;

namespace PathFinder.Trips.WebApi.Controllers
{
    public class TripsQueryController : ApiController
    {
        private readonly ITripQuery tripQuery;

        public TripsQueryController(ITripQuery tripQuery)
        {
            this.tripQuery = tripQuery;
        }

        public IHttpActionResult GetTrips(TripQueryModel request)
        {
            var trips = tripQuery.Trips.FindTrips(request).ToList();

            return Ok(trips);
        }
    }
}
