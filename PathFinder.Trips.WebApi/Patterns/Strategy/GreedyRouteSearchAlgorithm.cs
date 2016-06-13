// file:	patterns\strategy\igreadyroutesearchalgorithm.cs
//
// summary:	Implements the igreadyroutesearchalgorithm class

using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    /// <summary>   A gready route search algorithm. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    internal class GreedyRouteSearchAlgorithm : IRouteSearchAlgorithm
    {
        /// <summary>   Searches for the route. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="weights">      The weights matrix. </param>
        /// <param name="origin">       The origin. </param>
        /// <param name="destination">  The Destination. </param>
        ///
        /// <returns>   The found route. </returns>

        public Route FindRoute(double[,] weights, int origin, int destination)
        {
            HashSet<int> route = new HashSet<int>(new[] { origin });
            double distanse = 0;
            int waypointsToFind = weights.GetLength(0);
            int previous = origin;

            // we know our first and last point. We don't need to find them
            for (int i = 1; i < waypointsToFind - 1; i++)
            {
                double min = int.MaxValue;
                int index = -1;
                for (int j = 0; j < waypointsToFind; j++)
                {
                    if(previous == j || route.Contains(j) || j == destination)
                        continue;

                    if (weights[previous, j] < min)
                    {
                        min = weights[previous, j];
                        index = j;
                    }
                }
                route.Add(index);
                distanse += weights[previous, index];
                previous = index;
            }
            route.Add(destination);
            distanse += weights[previous, destination];

            return new Route() { Distanse = distanse, Sequence = route.ToList() };
        }
    }
}
