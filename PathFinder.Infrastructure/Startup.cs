using System.Web.Http;
using Microsoft.Owin;
using Owin;
using PathFinder.EntryPoint;

[assembly: OwinStartup(typeof(PathFinder.Infrastructure.Startup))]

namespace PathFinder.Infrastructure
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            app.UseWebApi(configuration);
        }
    }
}
