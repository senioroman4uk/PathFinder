using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    internal class BranchAndBoundRouteSearchAlgorithm : IRouteSearchAlgorithm
    {
        public Route FindRoute(double[,] weights, int origin, int destination)
        {
            var initialState = new State {Target = origin, Weight = 0d, Path = new List<int> {origin}};
            State record = null;

            var branchsToExpand = new Dictionary<int, State>() {{initialState.Target, initialState}};
            while (branchsToExpand.Count > 0)
            {
                var currentState =
                    branchsToExpand.Aggregate(
                        (min, currentItem) => min.Value.Weight > currentItem.Value.Weight ? currentItem : min).Value;
                branchsToExpand.Remove(currentState.Target);

                if (currentState.IsTargetState(destination, weights.GetLength(0)))
                {
                    if (record == null || currentState.Weight < record.Weight)
                        record = currentState;
                }
                else
                {
                    var branches = currentState.Branch(weights);
                    foreach (var branch in branches)
                    {
                        if (!branchsToExpand.ContainsKey(branch.Target))
                        {
                            branchsToExpand.Add(branch.Target, branch);
                        }
                        else if (branchsToExpand[branch.Target].Weight > branch.Weight)
                        {
                            branchsToExpand[branch.Target] = branch;
                        }
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

        public IEnumerable<State> Branch(double[,] weights)
        {
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                if (Path.Contains(i))
                    continue;

                var newPath = new List<int>(Path) {i};
                var weight = Weight;
                var target = Target;
                yield return new State() {Weight = weight + weights[target, i], Path = newPath, Target = i};
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
            return new Route()
            {
                Distanse = (int) state.Weight,
                Sequence = state.Path
            };
        }
        
    }
}