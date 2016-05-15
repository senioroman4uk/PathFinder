using System.Data.Entity;
using System.Linq;
using PathFinder.Cars.DAL.Model;

namespace PathFinder.Cars.WebApi.Queries
{
    internal class CarsContextQuery : ICarsContextQuery
    {
        private readonly CarsContext _carsContext;

        public CarsContextQuery(CarsContext carsContext)
        {
            _carsContext = carsContext;
        }


        public IOrderedQueryable<User> Users
        {
            get { return _carsContext.Users; }
        }

        public IOrderedQueryable<CarBrand> CarBrands
        {
            get { return _carsContext.CarBrands; }
        }

        public IOrderedQueryable<CarColor> CarColors
        {
            get { return _carsContext.CarColors; }
        }

        public IOrderedQueryable<CarModel> CarModels
        {
            get { return _carsContext.CarModels; }
        }

        public IOrderedQueryable<Car> Cars
        {
            get { return _carsContext.Cars; }
        }
    }
}
