using CourseFlow.Models;
using CourseFlow.Views;
using System.Windows;

namespace CourseFlow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserAccountModel CurrentUser { get; private set; }
        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
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


        public static void SignInUser(UserAccountModel userModel)
        {
            CurrentUser = userModel;
        }

        public static void SignOutUser()
        {
            CurrentUser = null;
        }
    }
}
