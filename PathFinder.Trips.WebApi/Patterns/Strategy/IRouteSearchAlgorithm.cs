// file:	patterns\strategy\iroutesearchalgorithm.cs
//
// summary:	Implements the iroutesearchalgorithm interface

using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    /// <summary>   Interface for route search algorithm. </summary>
    ///
    /// <remarks>   Vladyslav, 25.05.2016. </remarks>

    public interface IRouteSearchAlgorithm
    {
        /// <summary>   Searches for the route. </summary>
        ///
        /// <param name="weights">      The weights. </param>
        /// <param name="origin">       The origin. </param>
        /// <param name="destination">  Destination for the. </param>
        ///
        /// <returns>   The found route. </returns>

        Route FindRoute(double[,] weights, int origin, int destination);
    }
}
