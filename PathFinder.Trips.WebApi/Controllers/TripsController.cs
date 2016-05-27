// file:	Controllers\TripsController.cs
//
// summary:	Implements the trips controller class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using PathFinder.Trips.DAL.Model;
using PathFinder.Trips.WebApi.Constants;
using PathFinder.Trips.WebApi.Extensions;
using PathFinder.Trips.WebApi.Mappers;
using PathFinder.Trips.WebApi.Models;
using PathFinder.Trips.WebApi.Patterns.Factory;
using PathFinder.Trips.WebApi.Patterns.Strategy;
using PathFinder.Trips.WebApi.Queries;
using PathFinder.Trips.WebApi.Services;
using PathFinder.Trips.WebApi.Validators;

namespace PathFinder.Trips.WebApi.Controllers
{
    /// <summary>   A controller for handling trips. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    [RoutePrefix(TripsRouteConstants.TripsControllerRoutePrefix)]
    public class TripsController : ApiController
    {
        /// <summary>   The distance matrix query. </summary>
        private readonly IDistanceMatrixQuery _distanceMatrixQuery;

        /// <summary>   The route service. </summary>
        private readonly IRouteService _routeService;

        private readonly TripsContext _context;

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="distanceMatrixQuery">  The distance matrix query. </param>
        /// <param name="routeService">         The route service. </param>

        public TripsController(IDistanceMatrixQuery distanceMatrixQuery, IRouteService routeService, TripsContext context)
        {
            _distanceMatrixQuery = distanceMatrixQuery;
            _routeService = routeService;
            _context = context;
        }

        /// <summary>   (An Action that handles HTTP POST requests) creates a trip. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   The new trip. </returns>

        [HttpPost]
        [Route(TripsRouteConstants.CreateTrip)]
        public async Task<IHttpActionResult> CreateTrip(TripReadModel model)
        {
            var waypoints = model.WayPoints.Select(w => w.Place).ToList();
            waypoints.Insert(0, model.StartPoint);
            waypoints.Add(model.EndPoint);

            DistanseMatrixResponseModel distanseMatrixModel = await _distanceMatrixQuery.GetDistanceMatrix(waypoints);
            
            if (!distanseMatrixModel.Validate())
                throw new ApplicationException("Invalid response from distanse matrix API");

            double[,] matrix = distanseMatrixModel.ToArray();
            Route route = _routeService.CalculateRoute(matrix, 0, waypoints.Count - 1, model.Algorithm);
            var trip = model.ToEntity();
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return Ok(route);
        }
    }
}
