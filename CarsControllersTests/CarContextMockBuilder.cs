using System.Collections.Generic;
using System.Linq;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Queries;

namespace CarsControllersTests
{
    public class CarContextQueryMockBuilder
    {
        private readonly CarContextMock _fakeQuery;

        public CarContextQueryMockBuilder()
        {
            _fakeQuery = new CarContextMock();
        }

        public ICarsContextQuery CarsContextQuery
        {
            get { return _fakeQuery; }
        }

        public CarContextQueryMockBuilder SetCars(IEnumerable<Car> cars)
        {
            _fakeQuery.Cars = new EnumerableQuery<Car>(cars);
            return this;
        }

        public CarContextQueryMockBuilder SetUsers(IEnumerable<User> users)
        {
            _fakeQuery.Users = new EnumerableQuery<User>(users);
            return this;
        }

        public CarContextQueryMockBuilder SetCarBrands(IEnumerable<CarBrand> carBrands)
        {
            _fakeQuery.CarBrands = new EnumerableQuery<CarBrand>(carBrands);
            return this;
        }

        public CarContextQueryMockBuilder SetCarColors(IEnumerable<CarColor> carColors)
        {
            _fakeQuery.CarColors = new EnumerableQuery<CarColor>(carColors);
            return this;
        }

        public CarContextQueryMockBuilder SetCarModels(IEnumerable<CarModel> carModels)
        {
            _fakeQuery.CarModels = new EnumerableQuery<CarModel>(carModels);
            return this;
        }
    }

    public class CarContextMock : ICarsContextQuery
    {
        public CarContextMock()
        {
            Users = new EnumerableQuery<User>(Enumerable.Empty<User>());
            CarColors = new EnumerableQuery<CarColor>(Enumerable.Empty<CarColor>());
            CarBrands = new EnumerableQuery<CarBrand>(Enumerable.Empty<CarBrand>());
            CarModels = new EnumerableQuery<CarModel>(Enumerable.Empty<CarModel>());
            Cars = new EnumerableQuery<Car>(Enumerable.Empty<Car>());
        }

        public IOrderedQueryable<User> Users { get; set; }

        public IOrderedQueryable<CarBrand> CarBrands { get; set;  }

        public IOrderedQueryable<CarColor> CarColors { get; set; }

        public IOrderedQueryable<CarModel> CarModels { get; set; }

        public IOrderedQueryable<Car> Cars { get; set; }
    }
}
