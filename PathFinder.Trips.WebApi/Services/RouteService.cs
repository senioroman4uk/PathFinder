// file:	services\routeservice.cs
//
// summary:	Implements the routeservice class

using System;
using PathFinder.Trips.WebApi.Models;
using PathFinder.Trips.WebApi.Patterns.Factory;
using PathFinder.Trips.WebApi.Patterns.Strategy;

namespace PathFinder.Trips.WebApi.Services
{
    /// <summary>   A route service. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    internal class RouteService : IRouteService
    {
        /// <summary>   The factory. </summary>
        private readonly IRouteSearchAlgorithmFactory _factory;

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="factory">  The factory. </param>

        public RouteService(IRouteSearchAlgorithmFactory factory)
        {
            _factory = factory;
        }

        /// <summary>   Calculates the route. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when route serch algorithm is not found. </exception>
        ///
        /// <param name="matrix">           The matrix. </param>
        /// <param name="origin">           The origin. </param>
        /// <param name="destination">      Destination for the. </param>
        /// <param name="algorithmName">    Name of the algorithm. </param>
        ///
        /// <returns>   The calculated route. </returns>

        public Route CalculateRoute(double[,] matrix, int origin, int destination, string algorithmName)
        {
            IRouteSearchAlgorithm algorithm = _factory.GetRouteSearchAlgorithm(algorithmName);

            if (algorithm == null)
                throw new ApplicationException("Unknown Algorithm");

            return algorithm.FindRoute(matrix, origin, destination);
        }
    }

    /// <summary>   Interface for route service. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    public interface IRouteService
    {
        /// <summary>   Calculates the route. </summary>
        ///
        /// <param name="matrix">           The matrix. </param>
        /// <param name="origin">           The origin. </param>
        /// <param name="destination">      Destination for the. </param>
        /// <param name="algorithmName">    Name of the algorithm. </param>
        ///
        /// <returns>   The calculated route. </returns>

        Route CalculateRoute(double[,] matrix, int origin, int destination, string algorithmName);
    }
}
