namespace PathFinder.Trips.WebApi.Models
{
    public struct LocationModel
    {
        public double Lat { get; set; }

        public double Lng { get; set; }

        public LocationModel(double lat, double lng) : this()
        {
            Lat = lat;
            Lng = lng;
        }
    }
}
