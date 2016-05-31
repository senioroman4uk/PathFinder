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
    public class CarColorsControllerTests
    {
        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[]
                {
                    new []
                    {
                        new CarColor {Color = "Black", Id = 1}
                    }
                };
            }
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async void GetColorOkResult(IEnumerable<CarColor> colors)
        {
            // Arrange
            var query = Substitute.For<ICarsContextQuery>();
            query.CarColors.Returns(new MockForDbSet<CarColor>(colors));
            var controller = new CarColorsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetColor(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<ColorReadModel>>();
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async void GetColorNotFoundResult(IEnumerable<CarColor> colors)
        {
            // Arrange
            var query = Substitute.For<ICarsContextQuery>();
            query.CarColors.Returns(new MockForDbSet<CarColor>(colors));
            var controller = new CarColorsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetColor(0);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
