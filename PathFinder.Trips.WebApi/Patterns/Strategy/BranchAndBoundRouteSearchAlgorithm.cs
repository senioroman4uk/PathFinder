using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Trips.WebApi.Models;
using PathFinder.Trips.WebApi.Patterns.EightPuzzle.Domain;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    internal class BranchAndBoundRouteSearchAlgorithm : IRouteSearchAlgorithm
    {
        public Route FindRoute(double[,] weights, int origin, int destination)
        {
            var initialState = new State { Target = origin, Weight = 0d, Path = new List<int> { origin } };
            State record = null;

            var brancehsToExpand = new PriorityQueue<State>(new [] { initialState }, new StateWeightComparer());
            while (brancehsToExpand.Count > 0)
            {
                // TODO: Needs to be optimized
                var currentState = brancehsToExpand.ExtractMin();

                if (currentState.IsTargetState(destination, weights.GetLength(0)))
                {
                    if (record != null && (currentState.Weight >= record.Weight))
                        continue;

                    record = currentState;
                    var filteredQueue = new PriorityQueue<State>(new StateWeightComparer());
                    foreach (var branch in brancehsToExpand)
                    {
                        if (branch.Weight < record.Weight)
                            filteredQueue.Insert(branch);
                    }
                    brancehsToExpand = filteredQueue;
                }
                else
                {
                    var branches = currentState.Branch(weights, destination);
                    foreach (var branch in branches)
                    {
                        if (record == null || branch.Weight < record.Weight)
                            brancehsToExpand.Insert(branch);
                    }
                }
            }

            if (record == null)
                throw new ApplicationException("Destination is unreachable");

            Route route = record.ToRoute();
            return route;
        }
    }

    public class State
    {
        public int Target { get; set; }
        public double Weight { get; set; }
        public List<int> Path { get; set; }

        public IEnumerable<State> Branch(double[,] weights, int destination)
        {
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                if (Path.Contains(i) || (i == destination && Path.Count != weights.GetLength(0) - 1))
                    continue;

                var newPath = new List<int>(Path) {i};
                var weight = Weight;
                var target = Target;
                yield return new State { Weight = weight + weights[target, i], Path = newPath, Target = i };
            }
        }

        public bool IsTargetState(int destination, int length)
        {
            return Path.Last() == destination && Path.Count == length;
        }
    }

    public static class StateMapper
    {
        public static Route ToRoute(this State state)
        {
            return new Route
            {
                Distanse = state.Weight,
                Sequence = state.Path
            };
        }
        
    }

    internal class StateWeightComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            if (x.Weight < y.Weight)
                return -1;

            return x.Weight > y.Weight ? 1 : 0;
        }
    }
}