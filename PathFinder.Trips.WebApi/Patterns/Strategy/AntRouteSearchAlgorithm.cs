using System;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    // TODO: Use own implementation
    // 
    class AntRouteSearchAlgorithm : IRouteSearchAlgorithm
    {
        private readonly Random _random;

        // influence of pheromone on direction
        private static int alpha = 3;
        // influence of adjacent node distance
        private static int beta = 2;

        // pheromone decrease factor
        private static double rho = 0.01;
        // pheromone increase factor
        private static double Q = 2.0;

        public AntRouteSearchAlgorithm()
        {
            _random = new Random();
        }

        public Route FindRoute(double[,] weights, int origin, int destination)
        {
            int numCities = weights.GetLength(0);
            int numAnts = 4;
            int maxTime = 1000;

            int[][] ants = InitAnts(numAnts, numCities, origin, destination);
            int[] bestTrail = BestTrail(ants, weights);
            double[][] pheromones = InitPheromones(numCities);

            // determine the best initial trail
            double bestLength = Length(bestTrail, weights);

            for (int time = 0; time < maxTime; time++)
            {
                UpdateAnts(ants, pheromones, weights, origin, destination);
                UpdatePheromones(pheromones, ants, weights);

                int[] currBestTrail = BestTrail(ants, weights);
                double currBestLength = Length(currBestTrail, weights);
                if (currBestLength < bestLength)
                {
                    bestLength = currBestLength;
                    bestTrail = currBestTrail;
                }
            }

            return new Route {Distanse = bestLength, Sequence = bestTrail};
        }

        int[][] InitAnts(int numAnts, int numCities, int origin, int destination)
        {
            int[][] ants = new int[numAnts][];
            for (int k = 0; k < numAnts; ++k)
            {
                ants[k] = RandomTrail(origin, destination, numCities);
            }
            return ants;
        }

        int[] RandomTrail(int origin, int destination, int numCities) // helper for InitAnts
        {
            int[] trail = new int[numCities];
            trail[0] = origin;
            trail[numCities - 1] = destination;

            for (int i = 1; i < numCities - 1; ++i)
                if (i != origin && i != destination)
                    trail[i] = i; // sequential

            for (int i = 1; i < numCities - 1; ++i) // Fisher-Yates shuffle
            {
                int r = _random.Next(i, numCities - 1);
                int tmp = trail[r];
                trail[r] = trail[i];
                trail[i] = tmp;
            }

            return trail;
        }

        int IndexOfTarget(int[] trail, int target) // helper for RandomTrail
        {
            for (int i = 0; i < trail.Length; ++i)
            {
                if (trail[i] == target)
                    return i;
            }
            throw new Exception("Target not found in IndexOfTarget");
        }

        double Length(int[] trail, double[,] dists) // total length of a trail
        {
            double result = 0.0;
            for (int i = 0; i < trail.Length - 1; ++i)
                result += Distance(trail[i], trail[i + 1], dists);
            return result;
        }

        int[] BestTrail(int[][] ants, double[,] dists) // best trail has shortest total length
        {
            double bestLength = Length(ants[0], dists);
            int idxBestLength = 0;
            for (int k = 1; k < ants.Length; ++k)
            {
                double len = Length(ants[k], dists);
                if (len < bestLength)
                {
                    bestLength = len;
                    idxBestLength = k;
                }
            }
            int numCities = ants[0].Length;
            int[] bestTrail = new int[numCities];
            ants[idxBestLength].CopyTo(bestTrail, 0);
            return bestTrail;
        }

        double[][] InitPheromones(int numCities)
        {
            double[][] pheromones = new double[numCities][];
            for (int i = 0; i <= numCities - 1; i++)
            {
                pheromones[i] = new double[numCities];
            }
            for (int i = 0; i <= pheromones.Length - 1; i++)
            {
                for (int j = 0; j <= pheromones[i].Length - 1; j++)
                {
                    pheromones[i][j] = 0.01;
                    // otherwise first call to UpdateAnts -> BuiuldTrail -> NextNode -> MoveProbs => all 0.0 => throws
                }
            }
            return pheromones;
        }

        void UpdateAnts(int[][] ants, double[][] pheromones, double[,] dists, int origin, int destination)
        {
            for (int k = 0; k <= ants.Length - 1; k++)
            {
                int[] newTrail = BuildTrail(origin, destination, pheromones, dists);
                ants[k] = newTrail;
            }
        }

        int[] BuildTrail(int start, int destination, double[][] pheromones, double[,] dists)
        {
            int numCities = pheromones.Length;
            int[] trail = new int[numCities];
            bool[] visited = new bool[numCities];
            trail[0] = start;
            trail[numCities - 1] = destination;
            visited[start] = true;
            visited[destination] = true;

            for (int i = 0; i < numCities - 2; i++)
            {
                int cityX = trail[i];
                int next = NextCity(cityX, visited, pheromones, dists);
                trail[i + 1] = next;
                visited[next] = true;
            }
            return trail;
        }

        int NextCity(int cityX, bool[] visited, double[][] pheromones, double[,] dists)
        {
            // for ant k (with visited[]), at nodeX, what is next node in trail?
            double[] probs = MoveProbs(cityX, visited, pheromones, dists);

            double[] cumul = new double[probs.Length + 1];
            for (int i = 0; i <= probs.Length - 1; i++)
            {
                cumul[i + 1] = cumul[i] + probs[i];
                // consider setting cumul[cuml.Length-1] to 1.00
            }

            double p = _random.NextDouble();

            for (int i = 0; i <= cumul.Length - 2; i++)
            {
                if (p >= cumul[i] && p < cumul[i + 1])
                {
                    return i;
                }
            }
            throw new Exception("Failure to return valid city in NextCity");
        }

        double[] MoveProbs(int cityX, bool[] visited, double[][] pheromones, double[,] dists)
        {
            // for ant k, located at nodeX, with visited[], return the prob of moving to each city
            int numCities = pheromones.Length;
            double[] taueta = new double[numCities];
            // inclues cityX and visited cities
            double sum = 0.0;
            // sum of all tauetas
            // i is the adjacent city
            for (int i = 0; i <= taueta.Length - 1; i++)
            {
                if (i == cityX || visited[i])
                {
                    // prob of moving to self is 0
                    // prob of moving to a visited city is 0
                    taueta[i] = 0.0;
                }
                else
                {
                    taueta[i] = Math.Pow(pheromones[cityX][i], alpha)*Math.Pow((1.0/Distance(cityX, i, dists)), beta);
                    // could be huge when pheromone[][] is big
                    if (taueta[i] < 0.0001)
                    {
                        taueta[i] = 0.0001;
                    }
                    else if (taueta[i] > (double.MaxValue/(numCities*100)))
                    {
                        taueta[i] = double.MaxValue/(numCities*100);
                    }
                }
                sum += taueta[i];
            }

            double[] probs = new double[numCities];
            for (int i = 0; i <= probs.Length - 1; i++)
            {
                probs[i] = taueta[i]/sum;
                // big trouble if sum = 0.0
            }
            return probs;
        }

        void UpdatePheromones(double[][] pheromones, int[][] ants, double[,] dists)
        {
            for (int i = 0; i <= pheromones.Length - 1; i++)
            {
                for (int j = i + 1; j <= pheromones[i].Length - 1; j++)
                {
                    for (int k = 0; k <= ants.Length - 1; k++)
                    {
                        double length = Length(ants[k], dists);
                        // length of ant k trail
                        double decrease = (1.0 - rho)*pheromones[i][j];
                        double increase = 0.0;
                        if (EdgeInTrail(i, j, ants[k]))
                        {
                            increase = (Q/length);
                        }

                        pheromones[i][j] = decrease + increase;

                        if (pheromones[i][j] < 0.0001)
                        {
                            pheromones[i][j] = 0.0001;
                        }
                        else if (pheromones[i][j] > 100000.0)
                        {
                            pheromones[i][j] = 100000.0;
                        }

                        pheromones[j][i] = pheromones[i][j];
                    }
                }
            }
        }

        bool EdgeInTrail(int cityX, int cityY, int[] trail)
        {
            // are cityX and cityY adjacent to each other in trail[]?
            int lastIndex = trail.Length - 1;
            int idx = IndexOfTarget(trail, cityX);

            if (idx == 0 && trail[1] == cityY)
            {
                return true;
            }
            if (idx == 0 && trail[lastIndex] == cityY)
            {
                return true;
            }
            if (idx == 0)
            {
                return false;
            }
            if (idx == lastIndex && trail[lastIndex - 1] == cityY)
            {
                return true;
            }
            if (idx == lastIndex && trail[0] == cityY)
            {
                return true;
            }
            if (idx == lastIndex)
            {
                return false;
            }
            if (trail[idx - 1] == cityY)
            {
                return true;
            }
            if (trail[idx + 1] == cityY)
            {
                return true;
            }
            return false;
        }

        double Distance(int cityX, int cityY, double[,] dists)
        {
            return dists[cityX, cityY];
        }
    }
}