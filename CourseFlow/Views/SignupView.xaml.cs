using CourseFlow.ViewModels;
using System.Security;
using System;
using System.Windows;

namespace CourseFlow.Views
{
    /// <summary>
    /// Interaction logic for SignupView.xaml
    /// </summary>
    public partial class SignupView : Window
    {
        private readonly SignupViewModel signupViewModel;

        public SignupView()
        {
            InitializeComponent();
            signupViewModel = new SignupViewModel();
            this.DataContext = signupViewModel;
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            signupViewModel.User.Password = ConvertToSecureString(PasswordBox.Password);
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            signupViewModel.ConfirmPassword = ConvertToSecureString(ConfirmPasswordBox.Password);
        }

        private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();

            return securePassword;
        }

    }
}
