using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Models;

namespace PathFinder.Cars.WebApi.Mappers
{
    internal static class CarModelMapper
    {
        public static CarModelReadModel ToReadModel(this CarModel carModel)
        {
            return new CarModelReadModel()
            {
                BrandId = carModel.BrandId,
                Model = carModel.Model,
                Id = carModel.Id
            };
        }
    }
}
