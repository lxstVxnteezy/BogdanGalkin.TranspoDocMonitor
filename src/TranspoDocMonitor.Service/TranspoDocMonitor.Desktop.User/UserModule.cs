
using Prism.Ioc;
using Prism.Modularity;
using TranspoDocMonitor.Desktop.User.Dialogs;
using IDialogService = TranspoDocMonitor.Desktop.Common.DialogService.IDialogService;

namespace TranspoDocMonitor.Desktop.User
{
    public class UserModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDialogService, DialogServiceCreateUser>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }
    }
}
