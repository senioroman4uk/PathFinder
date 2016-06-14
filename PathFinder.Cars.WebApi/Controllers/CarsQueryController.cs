using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Constants.RouteConstants;
using PathFinder.Cars.WebApi.Extensions.OrderedQuerybleExtensions;
using PathFinder.Cars.WebApi.Mappers;
using PathFinder.Cars.WebApi.Models;
using PathFinder.Cars.WebApi.Queries;

namespace PathFinder.Cars.WebApi.Controllers
{
    [RoutePrefix(CarsRouteConstants.CarsControllerPrefix)]
    public class CarsQueryController : ApiController
    {
        private readonly ICarsContextQuery _carsContextQuery;

        public CarsQueryController(ICarsContextQuery carsContextQuery)
        {
            _carsContextQuery = carsContextQuery;
        }

        [Route(CarsRouteConstants.GetCar)]
        public async Task<IHttpActionResult> GetCar(int id)
        {
            Car car = await _carsContextQuery.Cars.FindCarByIdAsync(id);

            if (car == null)
                return NotFound();

            CarReadModel model = car.ToReadModel();
            return Ok(model);
        }

        [Route(CarsRouteConstants.GetCarsByBrand)]
        public IHttpActionResult GetCarsByBrand(int brandId)
        {
            IEnumerable<Car> cars = _carsContextQuery.Cars.FindCarsByBrandId(brandId).ToList();

            if (!cars.Any())
                return NotFound();

            IEnumerable<CarReadModel> models = cars.Select(c => c.ToReadModel());

            return Ok(models);
        }
    }
}
