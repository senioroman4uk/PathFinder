using System.Data.Entity.Spatial;
using System.Globalization;
using PathFinder.Trips.DAL.Model;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Mappers
{
    internal static class GooglePlaceModelMapper
    {
        public static IntermediatePoint ToIntermediatePoint(this GooglePlaceModel model, bool isStart = false, bool isEnd = false)
        {
            return new IntermediatePoint()
            {
                Coordinates =
                    DbGeography.FromText(
                        string.Format("POINT({0} {1})", model.Geometry.Location.Lat.ToString(CultureInfo.InvariantCulture), model.Geometry.Location.Lng.ToString(CultureInfo.InvariantCulture)), 4326),
                FormattedAddress = model.FormattedAddress,
                Name = model.Name,
                PlaceId = model.PlaceId,
                IsStart = isStart ? 1 : 0,
                IsEnd = isEnd ? 1 :0
            };
        }
    }
}
