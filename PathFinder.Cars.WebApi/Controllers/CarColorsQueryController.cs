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
    [RoutePrefix(CarsRouteConstants.CarColorsControllerPrefix)]
    public class CarColorsQueryController : ApiController
    {
        private readonly ICarsContextQuery _carsContextQuery;

        public CarColorsQueryController(ICarsContextQuery carsContextQuery)
        {
            _carsContextQuery = carsContextQuery;
        }

        [HttpGet]
        [Route(CarsRouteConstants.GetColor)]
        public async Task<IHttpActionResult> GetColor([FromUri] int id)
        {
            CarColor color = await _carsContextQuery.CarColors.FindColorByIdAsync(id);

            if (color == null)
                NotFound();

            ColorReadModel model = color.ToReadModel();

            return Ok(model);
        }

        [HttpGet]
        [Route(CarsRouteConstants.GetColors)]
        public IHttpActionResult GetColors()
        {
            IEnumerable<CarColor> colors = _carsContextQuery.FindColors().ToList();

            if (!colors.Any())
                return NotFound();

            IEnumerable<ColorReadModel> models = colors.Select(c => c.ToReadModel());
            return Ok(models);
        }
    }
}
