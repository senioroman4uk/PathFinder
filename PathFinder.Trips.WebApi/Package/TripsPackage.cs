using PathFinder.Trips.WebApi.Patterns.Factory;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PathFinder.Trips.WebApi.Package
{
    class TripsPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IRouteSearchAlgorithmFactory, RouteSearchAlgorithmFactory>(Lifestyle.Singleton);
        }
    }
}
