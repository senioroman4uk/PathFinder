﻿// file:	Patterns\Factory\RouteSearchAlgorithmFactory.cs
//
// summary:	Implements the route search algorithm factory class

using System.Collections.Generic;
using PathFinder.Trips.WebApi.Patterns.Strategy;

namespace PathFinder.Trips.WebApi.Patterns.Factory
{
    /// <summary>   A route search algorithm factory. </summary>
    ///
    /// <remarks>   Vladyslav, 25.05.2016. </remarks>

    class RouteSearchAlgorithmFactory : IRouteSearchAlgorithmFactory
    {
        /// <summary>   The known algorithms. </summary>
        private static readonly Dictionary<string, IRouteSearchAlgorithm> KnownAlgorithms = new Dictionary<string, IRouteSearchAlgorithm>
        {
            { "GreedyAlgorithm", new GreedyRouteSearchAlgorithm() },
            { "BeeAlgorithm", new BeeRouteSearchAlgoritm() },
            { "BranchAndBoundAlgorithm", new BranchAndBoundRouteSearchAlgorithm() },
            { "AntAlgorithm", new AntRouteSearchAlgorithm() },
            { "GeneticAlgorithm", new GeneticRouteSearchAlgorithm() }
        };

        /// <summary>   Gets route search algorithm. </summary>
        ///
        /// <remarks>   Vladyslav, 25.05.2016. </remarks>
        ///
        /// <param name="name"> The name. </param>
        ///
        /// <returns>   The route search algorithm. </returns>

        public IRouteSearchAlgorithm GetRouteSearchAlgorithm(string name)
        {
            return KnownAlgorithms[name];
        }
    }
}
