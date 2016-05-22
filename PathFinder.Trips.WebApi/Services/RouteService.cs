using System;
using PathFinder.Trips.WebApi.Patterns.Factory;
using PathFinder.Trips.WebApi.Patterns.Strategy;

namespace PathFinder.Trips.WebApi.Services
{
    internal class RouteService
    {
        IRouteSearchAlgorithmFactory _factory;

        public RouteService(IRouteSearchAlgorithmFactory factory)
        {
            _factory = factory;
        }

        public int CalculateRouteLength(double[,] matrix, int origin, int destination, string algorithmName)
        {
            IRouteSearchAlgorithm algorithm = _factory.GetRouteSearchAlgorithm(algorithmName);

            if (algorithm == null)
                throw new ApplicationException("Unknown Algorithm");

            return algorithm.FindRoute(matrix, origin, destination);
        }
    }
}
