using Newtonsoft.Json;

namespace PathFinder.Trips.WebApi.Models
{
    public class GooglePlaceModel
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        public string Name { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        public Geometry Geometry { get; set; }
    }
}
