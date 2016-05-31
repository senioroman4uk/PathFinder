using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Trips.DAL.Model;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Mappers
{
    internal static class TripMapper
    {
        public static Trip ToEntity(this TripReadModel model)
        {
            var intermidiatePoints = model.WayPoints.Select(_ => _.Place.ToIntermediatePoint()).ToList();
            intermidiatePoints.Add(model.StartPoint.ToIntermediatePoint(true));
            intermidiatePoints.Add(model.EndPoint.ToIntermediatePoint(false, true));

            return new Trip()
            {
                DepartureDate = model.DepartureDate,
                Price = model.Price,
                IntermediatePoints = intermidiatePoints
            };
        }
    }
}
