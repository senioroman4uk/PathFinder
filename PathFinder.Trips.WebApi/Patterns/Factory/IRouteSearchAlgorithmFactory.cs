using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Trips.WebApi.Patterns.Strategy;

namespace PathFinder.Trips.WebApi.Patterns.Factory
{
    interface IRouteSearchAlgorithmFactory
    {
        IRouteSearchAlgorithm GetRouteSearchAlgorithm(string name);
    }
}
