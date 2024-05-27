using System.Collections.ObjectModel;
using TranspoDocMonitor.Desktop.Common.Base;
using Prism.Commands;
using Prism.Mvvm;
using TranspoDocMonitor.Desktop.Common.DialogService;

namespace TranspoDocMonitor.Desktop.User.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        public ObservableCollection<Model.User> Users { get; set; }

        public UserViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Users = new ObservableCollection<Model.User>
            {
                new Model.User { ID = 1, Name = "Alice", Email = "alice@example.com" },
                new Model.User { ID = 2, Name = "Bob", Email = "bob@example.com" },
                new Model.User { ID = 3, Name = "Charlie", Email = "charlie@example.com" }
            };
        }

        #region DelegateCommands
        private DelegateCommand _showDialog;
        public DelegateCommand ShowDialog =>
            _showDialog ??= new DelegateCommand(ExecuteShowDialog);

        private DelegateCommand _removeUser;
        public DelegateCommand RemoveUser =>
            _removeUser ??= new DelegateCommand(DeleteUser);
        #endregion

        #region CommandMethods
        private void ExecuteShowDialog()
        {
            _dialogService.CreateUserShowDialog();
        }

        private void DeleteUser()
        {
            var selectedRemoveList = Users.Where(x => x.IsSelected).ToList();

            foreach (var user in selectedRemoveList)
            {
                Users.Remove(user);
            }
        }
        #endregion


    }

}
