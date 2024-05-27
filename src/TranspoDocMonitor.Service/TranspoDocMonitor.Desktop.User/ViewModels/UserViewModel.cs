using TranspoDocMonitor.Desktop.Common.Base;
using Prism.Commands;
using Prism.Mvvm;
using TranspoDocMonitor.Desktop.Common.DialogService;

namespace TranspoDocMonitor.Desktop.User.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private IDialogService _dialogService = new DialogService();

        private DelegateCommand _showDialog;
        public DelegateCommand ShowDialog =>
            _showDialog ??= new DelegateCommand(ExecuteShowDialog);

        private void ExecuteShowDialog()
        {
            _dialogService.ShowDialog();
        }
    }

}
