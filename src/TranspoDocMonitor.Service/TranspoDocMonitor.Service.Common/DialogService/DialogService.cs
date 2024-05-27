

using Prism.Services.Dialogs;

namespace TranspoDocMonitor.Desktop.Common.DialogService
{

    public interface IDialogService
    {
        void ShowDialog();
    }


    public class DialogService : IDialogService
    {
        public void ShowDialog()
        {
            var dialog = new DialogWindow();

            dialog.ShowDialog();
        }
    }
}
