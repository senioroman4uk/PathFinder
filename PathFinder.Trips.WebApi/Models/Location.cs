namespace PathFinder.Trips.WebApi.Models
{
    public struct Location
    {
        public double Lat { get; private set; }

        public double Lng { get; private set; }

        public Location(double lat, double lng) : this()
        {
            Lat = lat;
            Lng = lng;
        }
    }
}
