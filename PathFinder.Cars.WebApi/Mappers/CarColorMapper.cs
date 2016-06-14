using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Models;

namespace PathFinder.Cars.WebApi.Mappers
{
    internal static class CarColorMapper
    {
        public static ColorReadModel ToReadModel(this CarColor color)
        {
            return new ColorReadModel()
            {
                Color = color.Color,
                Id = color.Id
            };
        }
    }
}
