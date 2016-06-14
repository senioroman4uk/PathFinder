using PathFinder.Trips.WebApi.Patterns.Strategy;

namespace PathFinder.Trips.WebApi.Patterns.Factory
{
    public interface IRouteSearchAlgorithmFactory
    {
        IRouteSearchAlgorithm GetRouteSearchAlgorithm(string name);
    }
}
