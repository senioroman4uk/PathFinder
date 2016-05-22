using System.Web.Http;
using PathFinder.Infrastructure.Constants;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Controllers
{
    public class TripsController : ApiController
    {
        [Route(CommonRouteConstants.RouteBase + "/trips")]
        [HttpPost]
        public IHttpActionResult CreateTrip(TripReadModel model)
        {
            return BadRequest();
        }
    }
}
