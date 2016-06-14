using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Models;

namespace PathFinder.Cars.WebApi.Mappers
{
    internal static class CarMapper
    {
        public static CarReadModel ToReadModel(this Car car)
        {
            return new CarReadModel()
            {
                Color = car.Color.ToReadModel(),
                Comfort = car.Comfort,
                Id = car.Id,
                Model = car.Model.ToReadModel(),
                Owner = car.Owner.ToUserModel()
            };
        }
    }
}
