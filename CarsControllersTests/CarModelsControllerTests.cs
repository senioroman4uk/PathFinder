using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using NSubstitute;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Controllers;
using PathFinder.Cars.WebApi.Models;
using PathFinder.Cars.WebApi.Queries;
using PathFinder.Tests.Infrastructure;
using Xunit;

namespace CarsControllersTests
{
    public class CarModelsControllerTests
    {
        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[]
                {
                    new []
                    {
                        new CarModel {Model = "Model1", BrandId=1, Id = 1}
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForGetModelsByBrandId(int id)
        {
            var models = new CarModel[]
            {
                new CarModel()
                {
                    Id = 1,
                    BrandId = 1,
                    Model = "Model 1"
                },
                new CarModel()
                {
                    Id = 1,
                    BrandId = 2,
                    Model = "Model 2"
                },
            };

            var brands = new CarBrand[]
            {
                new CarBrand()
                {
                    Id = 1,
                    Name = "Name 1",
                    CarModels = new List<CarModel> { models[0] }
                },
                new CarBrand()
                {
                    Id = 2,
                    Name = "Name 2",
                    CarModels = new List<CarModel> { models[1] }
                }
            };

            yield return new object[] { id, models, brands };
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async void GetModelOkResultById(IEnumerable<CarModel> models)
        {
            // Arrange
            var query = Substitute.For<ICarsContextQuery>();
            query.CarModels.Returns(new MockForDbSet<CarModel>(models));
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModel(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<CarModelReadModel>>();
        }

        [Theory]
        [MemberData("DataForGetModelsByBrandId", 1)]
        public async void GetModelOkResultByBrandId(int id, CarModel[] carModels, CarBrand[] carBrands)
        {
            // Arrange
            var builder = new CarContextQueryMockBuilder()
                .SetCarBrands(carBrands)
                .SetCarModels(carModels);
            var query = builder.CarsContextQuery;
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModelsByBrandId(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<CarModelReadModel>>>();
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async void GetModelNotFoundResultById(IEnumerable<CarModel> models)
        {
            // Arrange
            var query = Substitute.For<ICarsContextQuery>();
            query.CarModels.Returns(new MockForDbSet<CarModel>(models));
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModel(0);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void GetModelNotFoundResultByBrandId()
        {
            // Arrange
            var builder = new CarContextQueryMockBuilder();
            var query = builder.CarsContextQuery;
        
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModelsByBrandId(0);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
