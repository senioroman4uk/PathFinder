using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Constants.RouteConstants;
using PathFinder.Cars.WebApi.Extensions;
using PathFinder.Cars.WebApi.Mappers;
using PathFinder.Cars.WebApi.Queries;

namespace PathFinder.Cars.WebApi.Controllers
{
    [RoutePrefix(CarsRouteConstants.CarModelsControllerPrefix)]
    public class CarModelsQueryController : ApiController
    {
        private readonly ICarsContextQuery _carsContextQuery;

        public CarModelsQueryController(ICarsContextQuery carsContextQuery)
        {
            _carsContextQuery = carsContextQuery;
        }

        [Route(CarsRouteConstants.GetModel)]
        public async Task<IHttpActionResult> GetModel(int id)
        {
            CarModel carModel = await _carsContextQuery.CarModels.FindModelByIdAsync(id);

            if (carModel == null)
                return NotFound();

            var model = carModel.ToReadModel();
            return Ok(model);
        }

        [Route(CarsRouteConstants.GetModelsByBrandId)]
        public async Task<IHttpActionResult> GetModelsByBrandId(int brandId)
        {
            IEnumerable<CarModel> carModels = await _carsContextQuery.CarBrands.FindModelsByBrandIdAsync(brandId);

            if (carModels == null)
                return NotFound();

            var models = carModels.Select(cm => cm.ToReadModel());
            return Ok(models);
        }
    }
}
