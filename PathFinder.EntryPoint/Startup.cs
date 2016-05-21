using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PathFinder.EntryPoint;
using PathFinder.Infrastructure.Extensions;
using PathFinder.Infrastructure.Providers;
using PathFinder.Security.DAL.Context;
using PathFinder.Security.DAL.Managers;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(Startup))]

namespace PathFinder.EntryPoint
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SimpleInjectorConfiguration.ConfigurationSimpleInjector();

            app.UseSimpleInjectorContext(container);
            app.CreatePerOwinContext(container.GetInstance<SecurityContext>);
            app.CreatePerOwinContext(container.GetInstance<AppUserManager>);

            ConfigureOAuth(app);

            HttpConfiguration configuration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            configuration.MapHttpAttributeRoutes();
            configuration.EnsureInitialized();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
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
    }
}
