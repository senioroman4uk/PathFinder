namespace PathFinder.Trips.WebApi.Models
{
    public class TripQueryModel
    {
        public GooglePlaceModel Start { get; set; }

        public GooglePlaceModel End { get; set; }

        public int Radius { get; set; }
    }
}
