using System;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    internal class IGreadyRouteSearchAlgorithm : IRouteSearchAlgorithm
    {
        public int FindRoute(double[,] weights, int origin, int destination)
        {
            bool[] visitedPoints = new bool[weights.GetLength(0)];

            throw new NotImplementedException();
        }

        private void GreedySearch(double[,] weights, bool[] visited, int current, int target)
        {
            visited[current] = true;
            
            if (current == target)
                return;
            

        }
    }
}
