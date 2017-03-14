using System;
using System.Linq;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Routing;
using EPiServer.Web.Routing;
using EPiServer.CJansson.MediaDataVersionRouter.Routing;

namespace EPiServer.CJansson.MediaDataVersionRouter.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class MediaDataVersionRouterInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            RouteTable.Routes.RegisterPartialRouter(new MediaDataVersionPartialRouter());
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}