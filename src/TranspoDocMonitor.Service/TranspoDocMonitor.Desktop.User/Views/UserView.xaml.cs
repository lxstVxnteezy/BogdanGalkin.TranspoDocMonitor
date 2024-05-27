
using System.Windows.Controls;
using TranspoDocMonitor.Desktop.User.ViewModels;


namespace TranspoDocMonitor.Desktop.User.Views
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView(UserViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
