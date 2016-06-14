using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Constants.RouteConstants;
using PathFinder.Cars.WebApi.Extensions;
using PathFinder.Cars.WebApi.Mappers;
using PathFinder.Cars.WebApi.Models;
using PathFinder.Cars.WebApi.Queries;

namespace PathFinder.Cars.WebApi.Controllers
{
    [RoutePrefix(CarsRouteConstants.CarBrandsControllerPrefix)]
    public class CarBrandsQueryController : ApiController
    {
        private readonly ICarsContextQuery _carsContextQuery;

        public CarBrandsQueryController(ICarsContextQuery carsContextQuery)
        {
            _carsContextQuery = carsContextQuery;
        }

        [HttpGet]
        [Route(CarsRouteConstants.GetBrand)]
        public async Task<IHttpActionResult> GetBrand([FromUri] int id)
        {
            CarBrand brand = await _carsContextQuery.CarBrands.FindBrandByIdAsync(id);

            if (brand == null)
                return NotFound();

            BrandReadModel model = brand.ToModel();

            return Ok(model);
        }

        [HttpGet]
        [Route(CarsRouteConstants.GetBrands)]
        public IHttpActionResult GetBrands()
        {
            IEnumerable<CarBrand> brands = _carsContextQuery.CarBrands.FindBrands().ToList();

            if (!brands.Any())
                return NotFound();

            IEnumerable<BrandReadModel> models = brands.Select(b => b.ToModel());
            return Ok(models);
        }
    }
}
