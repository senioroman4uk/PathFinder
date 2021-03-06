﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Controllers;
using PathFinder.Cars.WebApi.Models;
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
                        new CarModel {Model = "Model1", BrandId = 1, Id = 1}
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForGetModelsByBrandId(int id)
        {
            var models = new[]
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

            var brands = new[]
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
        public async Task GetModelOkResultById(ICollection<CarModel> models)
        {
            // Arrange
            var builder = new CarContextQueryMockBuilder()
                .SetCarModels(models);
            var query = builder.CarsContextQuery;
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModel(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<CarModelReadModel>>();
        }

        [Theory]
        [MemberData("DataForGetModelsByBrandId", 1)]
        public async Task GetModelOkResultByBrandId(int id, CarModel[] carModels, CarBrand[] carBrands)
        {
            // Arrange
            var query = new CarContextQueryMockBuilder()
                .SetCarBrands(carBrands)
                .SetCarModels(carModels).CarsContextQuery;
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModelsByBrandId(1);

            // Assert

            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<CarModelReadModel>>>();
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async Task GetModelNotFoundResultById(ICollection<CarModel> models)
        {
            // Arrange
            var query = new CarContextQueryMockBuilder()
               .SetCarModels(models).CarsContextQuery;
            var controller = new CarModelsQueryController(query);

            // Act
            IHttpActionResult result = await controller.GetModel(0);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetModelNotFoundResultByBrandId()
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
