using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    class GeneticRouteSearchAlgorithm : IRouteSearchAlgorithm
    {
        private static IEnumerable<List<int>> Allcombinations(List<int> arg, List<int> awithout)
        {

            if (arg.Count == 1)
            {
                var result = new List<List<int>>();
                result.Add(new List<int>());
                result[0].Add(arg[0]);
                return result;
            }
            else
            {
                var result = new List<List<int>>();

                foreach (var first in arg)
                {
                    var others0 = new List<int>(arg.Except(new int[1] {first}));
                    awithout.Add(first);
                    var others = new List<int>(others0.Except(awithout));

                    var combinations = Allcombinations(others, awithout);
                    awithout.Remove(first);

                    foreach (var tail in combinations)
                    {
                        tail.Insert(0, first);
                        result.Add(tail);
                    }
                }
                return result;
            }
        }

        //==================================================
        public Route FindRoute(double[,] arr, int origin, int destination)
        {
            int r = arr.GetLength(0);
            int[] totalarray = new int[r - 2];
            int j = 0;
            for (int i = 0; i < r; i++)
            {
                if (i != origin && i != destination)
                {
                    totalarray[j] = i;
                    j++;
                }
            }

            var totallist = new List<int>(totalarray);
            var allcombi = Allcombinations(totallist, new List<int>()).ToArray();
            int k = 0;
            int[,] rra = new int[allcombi.Count(), r - 2];
            foreach (var lst in allcombi)
            {
                int i = 0;
                foreach (var str in lst)
                {
                    rra[k, i] = str;
                    i++;
                }
                k++;
            }

            Route rout = Best(arr, rra, origin, destination);
            return rout;
        }

        //===================================================
        public static Route Best(double[,] e, int[,] a, int origin, int destination)
        {
            double[] w = new double[a.GetLength(0)];
            int z;
            if (a.GetLength(0) > 100)
                z = 100;
            else
                z = a.GetLength(0);
            for (int i = 0; i < z; i++)
            {
                int k = 0;
                for (int j = 0; j < e.GetLength(0); j++)
                {
                    if (k < a.GetLength(1) - 1 && j < a.GetLength(1) - 1)
                        w[i] += e[a[i, j], a[i, k + 1]];
                    if (j == origin)
                        w[i] += e[origin, a[i, 0]];
                    if (j == a.GetLength(1) - 1)
                        w[i] += e[a[i, a.GetLength(1) - 1], destination];
                    if (k < a.GetLength(1))
                        Console.Write(a[i, k]);
                    k++;
                }
            }
            double min = int.MaxValue;
            int index = 0;
            for (int g = 0; g < z; g++)
            {
                if (w[g] < min)
                {
                    min = w[g];
                    index = g;
                }
            }

            int[] res = new int[a.GetLength(1) + 2];
            int c = 0;
            for (int h = 1; h < a.GetLength(1) + 1; h++)
            {
                res[h] = a[index, c];
                c++;
            }

            res[0] = origin;
            res[res.GetLength(0) - 1] = destination;

            Route route = new Route();
            route.Sequence = res;
            route.Distanse = min;
            return route;
        }
    }
}
