using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.Cars.DAL.Model;

namespace PathFinder.Cars.WebApi.Extensions.OrderedQuerybleExtensions
{
    internal static class CarsContextCarsExtensions
    {
        public static async Task<Car> FindCarByIdAsync(this IOrderedQueryable<Car> cars, int id)
        {
            return await cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public static IEnumerable<Car> FindCarsByBrandId(this IOrderedQueryable<Car> carsSet, int brandId)
        {
            IEnumerable<Car> cars = carsSet
                .Include(c => c.Model)
                .Include(c => c.Model.CarBrand)
                .Include(c => c.Color)
                .Include(c => c.Owner)
                .Where(c => c.Model.BrandId == brandId)
                .AsNoTracking();

            return cars;
        }
    }
}
