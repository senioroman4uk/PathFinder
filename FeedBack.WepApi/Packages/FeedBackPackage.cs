using SimpleInjector;
using SimpleInjector.Packaging;
using FeedBack.WepApi.Query;

namespace FeedBack.WepApi.Packages
{
    public class FeedBackPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IFeedBackContextQuery, FeedBackContextQuery>(Lifestyle.Scoped);
        }
    }
}
