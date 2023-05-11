using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CourseFlow.Models;
using CourseFlow.Views;

namespace CourseFlow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserAccountModel CurrentUser { get; private set; }
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
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
