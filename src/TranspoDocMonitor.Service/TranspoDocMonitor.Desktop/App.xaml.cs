using System.Configuration;
using System.Data;
using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;


namespace TranspoDocMonitor.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
        }
    }

}
