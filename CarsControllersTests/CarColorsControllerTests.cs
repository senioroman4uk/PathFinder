using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using NSubstitute;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Controllers;
using PathFinder.Cars.WebApi.Models;
using PathFinder.Cars.WebApi.Queries;
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
        public async Task GetColorOkResult(ICollection<CarColor> colors)
        {
            // Arrange
            var query = new CarContextQueryMockBuilder().SetCarColors(colors).CarsContextQuery;
            var controller = new CarColorsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetColor(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<ColorReadModel>>();
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async Task GetColorNotFoundResult(ICollection<CarColor> colors)
        {
            // Arrange
            var query = new CarContextQueryMockBuilder().SetCarColors(colors).CarsContextQuery;
            var controller = new CarColorsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetColor(0);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
