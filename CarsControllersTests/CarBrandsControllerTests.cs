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
    public class CarBrandsControllerTests
    {
            public static IEnumerable<object[]> DataForOkResult
            {
                get
                {
                    yield return new object[]
                    {
                    new []
                    {
                        new CarBrand() {Name = "Brand", Id = 1}
                    }
                    };
                }
            }

            [Theory]
            [MemberData("DataForOkResult")]
            public async Task GetColorOkResult(ICollection<CarBrand> brands)
            {
                // Arrange
                var query = new CarContextQueryMockBuilder().SetCarBrands(brands).CarsContextQuery;;
                var controller = new CarBrandsQueryController(query);

                // Act
                IHttpActionResult result = await controller.GetBrand(1);

                // Assert

                result.Should().BeOfType<OkNegotiatedContentResult<BrandReadModel>>();
            }

            [Theory]
            [MemberData("DataForOkResult")]
            public async Task GetColorNotFoundResult(ICollection<CarBrand> brands)
            {
                // Arrange
                var query = new CarContextQueryMockBuilder().SetCarBrands(brands).CarsContextQuery;
                var controller = new CarBrandsQueryController(query);

                // Act
                IHttpActionResult result = await controller.GetBrand(0);

                // Assert
                result.Should().BeOfType<NotFoundResult>();
            }
        }
}
