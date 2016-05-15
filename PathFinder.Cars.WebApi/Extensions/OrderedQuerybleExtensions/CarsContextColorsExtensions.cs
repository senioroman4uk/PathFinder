using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Queries;

namespace PathFinder.Cars.WebApi.Extensions
{
    internal static class CarsContextColorsExtensions
    {
        internal static async Task<CarColor> FindColorByIdAsync(this IOrderedQueryable<CarColor> colors, int id)
        {
            return await colors.FirstOrDefaultAsync(c => c.Id == id);
        }

        public static IEnumerable<CarColor> FindColors(this ICarsContextQuery query)
        {
            IEnumerable<CarColor> colors =  query.CarColors.AsNoTracking();

            return colors;
        }
    }
}
