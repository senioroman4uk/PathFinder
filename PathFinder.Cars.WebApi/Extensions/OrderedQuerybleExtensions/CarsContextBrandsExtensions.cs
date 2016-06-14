using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.Cars.DAL.Model;

namespace PathFinder.Cars.WebApi.Extensions
{
    internal static class CarsContextBrandsExtensions
    {
        public static async Task<CarBrand> FindBrandByIdAsync(this IOrderedQueryable<CarBrand> brands, int id)
        {
            return await brands.FirstOrDefaultAsync(b => b.Id == id);
        }

        public static IEnumerable<CarBrand> FindBrands(this IOrderedQueryable<CarBrand> carBrands)
        {
            IEnumerable<CarBrand> brands = carBrands.AsNoTracking();

            return brands;
        }
    }
}
