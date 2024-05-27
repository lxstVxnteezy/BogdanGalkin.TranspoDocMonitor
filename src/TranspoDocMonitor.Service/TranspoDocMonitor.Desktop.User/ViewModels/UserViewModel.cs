using TranspoDocMonitor.Desktop.Common.Base;
using Prism.Commands;
using Prism.Mvvm;
using TranspoDocMonitor.Desktop.Common.DialogService;

namespace TranspoDocMonitor.Desktop.User.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;

        public UserViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private DelegateCommand _showDialog;
        public DelegateCommand ShowDialog =>
            _showDialog ??= new DelegateCommand(ExecuteShowDialog);

        private void ExecuteShowDialog()
        {
            _dialogService.CreateUserShowDialog();
        }
    }

}
