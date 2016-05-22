using System;
using System.Collections.Generic;

namespace PathFinder.Trips.WebApi.Models
{
    public class TripReadModel
    {
        public DateTime DepartureDate { get; set; }
        
        public Decimal Price { get; set; }

        public GooglePlaceModel StartPoint { get; set; }

        public GooglePlaceModel EndPoint { get; set; }

        public IEnumerable<GooglePlaceModel> WayPoints { get; set; }
    }
}
