
using TranspoDocMonitor.Desktop.User.Views;
using IDialogService = TranspoDocMonitor.Desktop.Common.DialogService.IDialogService;

namespace TranspoDocMonitor.Desktop.User.Dialogs
{
    internal class DialogServiceCreateUser : IDialogService
    {
        public void CreateUserShowDialog()
        {
            var Dialog = new DialogCreateUserView();
            Dialog.ShowDialog();
        }
    }
}
