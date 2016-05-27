using System.Net;
using System.Net.Http;
using PathFinder.Trips.DAL.Model;
using PathFinder.Trips.WebApi.Patterns.Factory;
using PathFinder.Trips.WebApi.Queries;
using PathFinder.Trips.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PathFinder.Trips.WebApi.Package
{
    public class TripsPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IRouteSearchAlgorithmFactory, RouteSearchAlgorithmFactory>(Lifestyle.Singleton);
            container.Register<IRouteService, RouteService>(Lifestyle.Singleton);
            container.Register(() => new HttpClient(), Lifestyle.Singleton);
            container.Register<IDistanceMatrixQuery, DistanceMatrixQuery>(Lifestyle.Scoped);
            container.Register<TripsContext>(Lifestyle.Scoped);
        }
    }
}
