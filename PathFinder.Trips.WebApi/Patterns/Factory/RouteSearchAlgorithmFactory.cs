using System.Collections.Generic;
using PathFinder.Trips.WebApi.Patterns.Strategy;

namespace PathFinder.Trips.WebApi.Patterns.Factory
{
    class RouteSearchAlgorithmFactory : IRouteSearchAlgorithmFactory
    {
        private static Dictionary<string, IRouteSearchAlgorithm> knownAlgorithms = new Dictionary<string, IRouteSearchAlgorithm>()
        {
            { "GridyAlgorithm", new IGreadyRouteSearchAlgorithm() }
        };

        public IRouteSearchAlgorithm GetRouteSearchAlgorithm(string name)
        {
            return knownAlgorithms[name];
        }
    }
}
