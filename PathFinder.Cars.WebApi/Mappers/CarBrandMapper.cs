using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Models;

namespace PathFinder.Cars.WebApi.Mappers
{
    internal static class CarBrandMapper
    {
        public static BrandReadModel ToModel(this CarBrand brand)
        {
            return new BrandReadModel()
            {
                Name = brand.Name,
                Id = brand.Id
            };
        }
    }
}
