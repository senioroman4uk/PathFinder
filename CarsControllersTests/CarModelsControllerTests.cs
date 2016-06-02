using System.Collections.Generic;
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
        [MemberData("DataForOkResult")]
        public async void GetModelOkResultByBrandId(IEnumerable<CarModel> models)
        {
            // Arrange
            var query = Substitute.For<ICarsContextQuery>();
            query.CarModels.Returns(new MockForDbSet<CarModel>(models));
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModelsByBrandId(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<CarModelReadModel>>();
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

        [Theory]
        [MemberData("DataForOkResult")]
        public async void GetModelNotFoundResultByBrandId(IEnumerable<CarModel> models)
        {
            // Arrange
            var query = Substitute.For<ICarsContextQuery>();
            query.CarModels.Returns(new MockForDbSet<CarModel>(models));
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModelsByBrandId(0);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
