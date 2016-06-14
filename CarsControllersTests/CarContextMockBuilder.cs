using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using NSubstitute;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Queries;

namespace CarsControllersTests
{
    public class CarContextQueryMockBuilder
    {
        private DbSet<CarBrand> _brands;
        private DbSet<User> _users;
        private DbSet<CarModel> _models;
        private DbSet<Car> _cars;
        private DbSet<CarColor> _carColors;

        public CarContextQueryMockBuilder()
        {
            _brands = Substitute.For<DbSet<CarBrand>, IQueryable<CarBrand>, IDbAsyncEnumerable<CarBrand>>().SetupData(new List<CarBrand>());
            _users = Substitute.For<DbSet<User>, IQueryable<User>, IDbAsyncEnumerable<User>>().SetupData(new List<User>());
            _models = Substitute.For<DbSet<CarModel>, IQueryable<CarModel>, IDbAsyncEnumerable<CarModel>>().SetupData(new List<CarModel>());
            _cars = Substitute.For<DbSet<Car>, IQueryable<Car>, IDbAsyncEnumerable<Car>>().SetupData(new List<Car>());
            _carColors = Substitute.For<DbSet<CarColor>, IQueryable<CarColor>, IDbAsyncEnumerable<CarColor>>().SetupData(new List<CarColor>());
        }

        public ICarsContextQuery CarsContextQuery
        {
            get
            {
                var query =  Substitute.For<ICarsContextQuery>();
                query.CarBrands.Returns(_brands);
                query.Users.Returns(_users);
                query.CarModels.Returns(_models);
                query.Cars.Returns(_cars);
                query.CarColors.Returns(_carColors);

                return query;
            }
        } 
        public CarContextQueryMockBuilder SetCars(ICollection<Car> cars)
        {
            _cars = _cars.SetupData(cars);
            return this;
        }

        public CarContextQueryMockBuilder SetUsers(ICollection<User> users)
        {
            _users = _users.SetupData(users);
            return this;
        }

        public CarContextQueryMockBuilder SetCarBrands(ICollection<CarBrand> carBrands)
        {
            _brands = _brands.SetupData(carBrands);
            return this;
        }

        public CarContextQueryMockBuilder SetCarColors(ICollection<CarColor> carColors)
        {
            _carColors = _carColors.SetupData(carColors);
            return this;
        }

        public CarContextQueryMockBuilder SetCarModels(ICollection<CarModel> carModels)
        {
            _models = _models.SetupData(carModels);
            return this;
        }
    }
}
