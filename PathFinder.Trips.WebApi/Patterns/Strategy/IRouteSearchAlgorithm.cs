namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    internal interface IRouteSearchAlgorithm
    {
        int FindRoute(double[,] weights, int origin, int destination);
    }
}
