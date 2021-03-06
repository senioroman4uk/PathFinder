﻿using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PathFinder.EntryPoint;
using PathFinder.Infrastructure.Extensions;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using FluentValidation.WebApi;
using PathFinder.Infrastructure.ActionFilters;
using PathFinder.Security.Authentication.Models;
using PathFinder.Security.Authentication.Providers;

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
            configuration.Filters.Add(new AuthorizeAttribute());
            configuration.Filters.Add(new ModelStateFilterAttribute());
            FluentValidationModelValidatorProvider.Configure(configuration,
                provider => provider.ValidatorFactory = new FluentValidatorFactory(container));
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
