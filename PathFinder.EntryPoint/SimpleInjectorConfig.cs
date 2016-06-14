using FluentValidation;
using PathFinder.Cars.WebApi.Package;
using PathFinder.Trips.WebApi.Package;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Linq;
using FeedBack.WepApi.Packages;
using PathFinder.Security.UserManagement.Package;

namespace PathFinder.EntryPoint
{
    public static class SimpleInjectorConfiguration
    {
        public static Container ConfigurationSimpleInjector()
        {
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            return RegisterPackages(container);
        }

        private static Container RegisterPackages(Container container)
        {
            container.RegisterPackages(new[]
            {
                typeof(SecurityPackage).Assembly,
                typeof(CarsPackage).Assembly,
                typeof(TripsPackage).Assembly,
                typeof(FeedBackPackage).Assembly
            });
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            container.Register(typeof(IValidator<>), assemblies, Lifestyle.Singleton);
            container.Verify();

            return container;
        }
    }
}
