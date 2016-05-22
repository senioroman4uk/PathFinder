// file:	patterns\strategy\igreadyroutesearchalgorithm.cs
//
// summary:	Implements the igreadyroutesearchalgorithm class

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    /// <summary>   A gready route search algorithm. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    internal class IGreadyRouteSearchAlgorithm : IRouteSearchAlgorithm
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
            int distanse = 0;
            int waypointsToFind = weights.GetLength(0) - 1;

            // we know our first and last point. We don't need to find them
            for (int i = 1; i < waypointsToFind; i++)
            {
                int min = int.MaxValue;
                int index = 1;

                for (int j = 0; j < waypointsToFind + 1; j++)
                {
                    if (i == j || route.Contains(j))
                        continue;

                    if (weights[i, j] < min)
                    {
                        min = (int)weights[i, j];
                        index = j;
                    }
                }
                route.Add(index);
                distanse += (int)weights[i, index];
            }
            route.Add(destination);

            return new Route() { Distanse = distanse, Sequence = route.ToList() };
        }
    }
}
