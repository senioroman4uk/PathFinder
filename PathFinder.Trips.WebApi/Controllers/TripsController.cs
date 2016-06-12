// file:	Controllers\TripsController.cs
//
// summary:	Implements the trips controller class

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PathFinder.Trips.DAL.Model;
using PathFinder.Trips.WebApi.Constants;
using PathFinder.Trips.WebApi.Extensions;
using PathFinder.Trips.WebApi.Mappers;
using PathFinder.Trips.WebApi.Models;
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

        [AllowAnonymous]
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

            var route = _routeService.CalculateRoute(matrix, 0, waypoints.Count - 1, model.Algorithm);
            var trip = model.ToEntity();
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return Ok(route);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("calculateRoute")]
        public async Task<IHttpActionResult> CalculateRoute(string algorithm, int origin, int destination)
        {
            if (!Request.Content.IsMimeMultipartContent())
                return StatusCode(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            if (provider.Contents.Count == 0)
                return BadRequest("You have to add file with matrix for procesing");

            var parsedMatrix = await GetMatrixAsync(provider);
            if (!parsedMatrix.Validate() || !ValidateBoundaryPoints(origin, destination, parsedMatrix.Count))
                return StatusCode((HttpStatusCode)422);

            var matrix = parsedMatrix.ToSquareMatrix();
            Route route = _routeService.CalculateRoute(matrix, origin - 1, destination - 1, algorithm);
            return Ok(route);
        }

        private bool ValidateBoundaryPoints(int origin, int destination, int size)
        {
            return origin != destination && origin > 0 && destination > 0 && origin <= size && destination <= size;
        }

        private async Task<List<List<double>>> GetMatrixAsync(MultipartMemoryStreamProvider provider)
        {
            var file = provider.Contents.First();
            IList<string> matrixRows = new List<string>();
            using (var dataStream = await file.ReadAsStreamAsync())
            using (var reader = new StreamReader(dataStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    matrixRows.Add(line);
                }
            }

            var parsedMatrix = matrixRows.Select(_ => _.Split().Select(double.Parse).ToList()).ToList();
            return parsedMatrix;
        }
    }
}
