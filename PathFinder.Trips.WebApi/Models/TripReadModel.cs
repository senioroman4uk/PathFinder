using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PathFinder.Trips.WebApi.Models
{
    public class TripReadModel
    {
        public DateTime DepartureDate { get; set; }
        
        public Decimal Price { get; set; }

        public GooglePlaceModel StartPoint { get; set; }

        public GooglePlaceModel EndPoint { get; set; }

        public IEnumerable<WayPointModel> WayPoints { get; set; }

        public string Algorithm { get; set; }

        public Route Route { get; set; }
    }

    public class Route
    {
        public IList<int> Sequence { get; set; }
        public int Distanse { get; set; }
    }
}
