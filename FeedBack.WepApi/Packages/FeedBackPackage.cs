using SimpleInjector;
using SimpleInjector.Packaging;
using FeedBack.WepApi.Query;
using PathFinder.FeedBack.DAL.Model;

namespace FeedBack.WepApi.Packages
{
    public class FeedBackPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<FeedBackContext>(Lifestyle.Scoped);
            container.Register<IFeedBackContextQuery, FeedBackContextQuery>(Lifestyle.Scoped);
        }
    }
}
