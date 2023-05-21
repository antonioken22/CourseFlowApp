using System.Windows;
using System.Windows.Controls;

namespace CourseFlow.Views
{
    /// <summary>
    /// Interaction logic for LogoutView.xaml
    /// </summary>
    public partial class LogoutView : UserControl
    {
        public LogoutView()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.SignOutUser();
            var loginView = new LoginView();
            loginView.Show();
            Window.GetWindow(this)?.Close();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainWindowView = new MainWindowView();
                    mainWindowView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
