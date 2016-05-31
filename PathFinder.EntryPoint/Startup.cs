using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PathFinder.EntryPoint;
using PathFinder.Infrastructure.Extensions;
using PathFinder.Security.DAL.Context;
using PathFinder.Security.DAL.Managers;
using PathFinder.Security.WebApi.Providers;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(Startup))]
namespace PathFinder.EntryPoint
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // CORS needs to be first in the pipeline
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var container = SimpleInjectorConfiguration.ConfigurationSimpleInjector();
            app.UseSimpleInjectorContext(container);
            app.CreatePerOwinContext(container.GetInstance<SecurityContext>);
            app.CreatePerOwinContext(container.GetInstance<AppUserManager>);

            ConfigureOAuth(app);
            HttpConfiguration configuration = ConfigureWebApi(container);

            app.UseWebApi(configuration);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private HttpConfiguration ConfigureWebApi(Container container)
        {
            HttpConfiguration configuration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container),
            };
            configuration.MapHttpAttributeRoutes();
            configuration.EnsureInitialized();

            return configuration;
        }
    }
}
