using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Queries;

namespace PathFinder.Cars.WebApi.Extensions
{
    internal static class CarsContextModelsExtensions
    {
        public static async Task<CarModel> FindModelByIdAsync(this IOrderedQueryable<CarModel> models, int id)
        {
            CarModel carModel = await models.FirstOrDefaultAsync(cm => cm.Id == id);

            return carModel;
        }

        public static async Task<IEnumerable<CarModel>> FindModelsByBrandIdAsync(this IOrderedQueryable<CarBrand> brands, int brandId)
        {
            CarBrand brand = await brands
                .Include(b => b.CarModels).FirstOrDefaultAsync(b => b.Id == brandId);

            return brand == null ? null : brand.CarModels;
        }
    }
}
