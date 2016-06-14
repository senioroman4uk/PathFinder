using System.Linq;
using PathFinder.Cars.DAL.Model;

namespace PathFinder.Cars.WebApi.Queries
{
    public interface ICarsContextQuery
    {
        IOrderedQueryable<User> Users { get; }

        IOrderedQueryable<CarBrand> CarBrands { get; }

        IOrderedQueryable<CarColor> CarColors { get; }

        IOrderedQueryable<CarModel> CarModels { get; }

        IOrderedQueryable<Car> Cars { get; }
    }
}
