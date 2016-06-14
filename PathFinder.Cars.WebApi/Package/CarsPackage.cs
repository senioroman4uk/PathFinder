using PathFinder.Cars.DAL.Model;
using PathFinder.Cars.WebApi.Queries;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PathFinder.Cars.WebApi.Package
{
    public class CarsPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<CarsContext>(Lifestyle.Scoped);
            container.Register<ICarsContextQuery, CarsContextQuery>(Lifestyle.Scoped);
        }
    }
}
