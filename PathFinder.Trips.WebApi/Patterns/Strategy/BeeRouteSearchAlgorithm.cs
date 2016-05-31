using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Patterns.Strategy
{
    public class BeeRouteSearchAlgoritm : IRouteSearchAlgorithm
    {
        private BeeSearchParameters _parameters;

        public Route FindRoute(double[,] weights, int origin, int destination)
        {
            var length = (int)Math.Sqrt(weights.Length);
            Initialize(length);
            var allFlowers = new List<Route>();
            var eliteFlowers = new List<Route>();
            var globalRecord = new Route() { Distanse = int.MaxValue };
            // Первый глобальный поиск 
            for (int i = 0; i < _parameters.ScoutBeesCount; i++)
            {
                var flowerFound = false;
                while (!flowerFound)
                {
                    var newFlower = CreateFlower(origin, destination, length);
                    if (!RouteExists(newFlower, allFlowers))
                    {
                        flowerFound = true;
                        newFlower.Distanse = (int)GetFlowerCost(newFlower, weights);
                        if (newFlower.Distanse <= globalRecord.Distanse)
                        {
                            globalRecord = newFlower;
                        }
                        allFlowers.Add(newFlower);
                        AddEliteFlower(eliteFlowers, newFlower);
                    }
                }
            }
            int recordTime = 0;
            while (recordTime < _parameters.RecordTTL)
            {
                var recordNotChanged = true;

                // Рекрутинг фуражиров 
                var foragersOnFlowers = Recruite(eliteFlowers);

                // Локальный поиск 
                for (int i = 0; i < _parameters.FlowerPatchCount; i++)
                {
                    var flower = eliteFlowers[i];
                    var localRecord = flower;
                    for (int j = 0; j < foragersOnFlowers[flower] && allFlowers.Count < _parameters.FlowersCount; j++)
                    {
                        var neighbor = CreateNeighbor(flower, allFlowers);
                        if (neighbor != null)
                        {
                            allFlowers.Add(neighbor);
                            if (neighbor.Distanse <= localRecord.Distanse)
                            {
                                localRecord = neighbor;
                                if (localRecord.Distanse <= globalRecord.Distanse)
                                {
                                    globalRecord = localRecord;
                                    recordNotChanged = false;
                                }
                            }
                        }
                    }
                    eliteFlowers[i] = localRecord;
                }
                if (recordNotChanged)
                {
                    recordTime++;
                }
                else
                {
                    recordTime = 0;
                }
                if (recordTime < _parameters.RecordTTL)
                {
                    // Глобальный поиск 
                    for (int i = _parameters.FlowerPatchCount; i < eliteFlowers.Count && allFlowers.Count < _parameters.FlowersCount; i++)
                    {
                        var flowerFound = false;
                        while (!flowerFound)
                        {
                            var flower = CreateFlower(origin, destination, length);
                            if (!RouteExists(flower, allFlowers))
                            {
                                flowerFound = true;
                                flower.Distanse = (int)GetFlowerCost(flower, weights);
                                if (flower.Distanse <= globalRecord.Distanse)
                                {
                                    globalRecord = flower;
                                    recordTime = 0;
                                }
                                allFlowers.Add(flower);
                                // Удаление предыдущего цветка, так как переехали на новый 
                                eliteFlowers.RemoveAt(i);
                                AddEliteFlower(eliteFlowers, flower);
                            }
                        }
                    }
                }
            }
            return globalRecord;
        }

        private void AddEliteFlower(List<Route> eliteFlowers, Route flower)
        {
            var index = eliteFlowers.FindIndex(route => route.Distanse >= flower.Distanse);
            if (index < 0)
            {
                index = 0;
            }
            eliteFlowers.Insert(index, flower);
        }

        private Dictionary<Route, int> Recruite(List<Route> eliteFlowers)
        {
            var recruiteList = new Dictionary<Route, int>();
            var sum = (double)eliteFlowers.Take(_parameters.FlowerPatchCount).Sum(i => i.Distanse);
            foreach (var flower in eliteFlowers)
            {
                var value = Math.Round(_parameters.ForagersCount / sum * flower.Distanse);
                recruiteList.Add(flower, (int)value);
            }
            return recruiteList;
        }

        private Route CreateNeighbor(Route currentFlower, List<Route> allFlowers)
        {
            if (currentFlower.Sequence.Count <= 2)
            {
                return null;
            }
            Route newFlower = null;
            var flowerCreated = false;
            var attemptNumber = 0;
            while (!flowerCreated && attemptNumber < _parameters.FlowersCount)
            {
                newFlower = new Route()
                {
                    Distanse = currentFlower.Distanse,
                    Sequence
                = new List<int>(currentFlower.Sequence)
                };
                var generator = new Random((int)DateTime.Now.Ticks);
                var index1 = generator.Next(1, currentFlower.Sequence.Count - 2);
                var index2 = generator.Next(1, currentFlower.Sequence.Count - 2);
                newFlower.Sequence[index1] = currentFlower.Sequence[index2];
                newFlower.Sequence[index2] = currentFlower.Sequence[index1];
                if (!RouteExists(newFlower, allFlowers))
                {
                    flowerCreated = true;
                }
                attemptNumber++;
            }
            if (flowerCreated)
            {
                return newFlower;
            }
            return null;
        }

        private Route CreateFlower(int origin, int destination, int length)
        {
            var flower = new Route() { Sequence = new List<int>() };
            flower.Sequence.Add(origin);
            var indexes = new List<int> { origin, destination };
            var generator = new Random((int)DateTime.Now.Ticks);
            for (var i = 1; i < length - 1; i++)
            {
                var findValue = false;
                while (!findValue)
                {
                    var value = generator.Next(0, length - 1);
                    if (!indexes.Exists(index => index == value))
                    {
                        indexes.Add(value);
                        flower.Sequence.Add(value);
                        findValue = true;
                    }
                }
            }
            flower.Sequence.Add(destination);
            return flower;
        }

        private double GetFlowerCost(Route flower, double[,] weight)
        {
            double result = 0;
            for (var i = 0; i < flower.Sequence.Count - 1; i++)
            {
                result += weight[flower.Sequence[i], flower.Sequence[i + 1]];
            }
            return result;
        }

        private bool RouteExists(Route route, List<Route> routes)
        {
            return routes.IndexOf(route) >= 0;
        }

        private void Initialize(int length)
        {
            var flowersCount = Factorial(length - 2);
            _parameters = new BeeSearchParameters()
            {
                ScoutBeesCount = Math.Min(10, flowersCount),
                FlowerPatchCount = Math.Min(5, flowersCount),
                ForagersCount = 12,
                RecordTTL = 10,
                FlowersCount = flowersCount
            };
        }

        private int Factorial(int value)
        {
            var result = 1;
            for (int i = 2; i <= value; i++)
            {
                result *= i;
            }
            return result;
        }

        private class BeeSearchParameters
        {
            public int ScoutBeesCount { get; set; }
            public int FlowerPatchCount { get; set; }
            public int ForagersCount { get; set; }
            // ReSharper disable once InconsistentNaming 
            public int RecordTTL { get; set; }
            public int FlowersCount { get; set; }
        }
    }
}
