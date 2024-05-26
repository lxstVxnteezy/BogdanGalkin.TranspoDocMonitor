
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TranspoDocMonitor.Desktop.User.Views;

namespace TranspoDocMonitor.Desktop;

class  MainModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var region = containerProvider.Resolve<IRegionManager>();
        region.RegisterViewWithRegion("MainRegion", typeof(UserView));
    }
}