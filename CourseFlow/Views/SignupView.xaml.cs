using CourseFlow.ViewModels;
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
            signupViewModel.CloseWindowCommand = new ViewModelCommand(param => CloseWindow());
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            signupViewModel.User.Password = (PasswordBox.Password);
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            signupViewModel.ConfirmPassword = (ConfirmPasswordBox.Password);
        }

        private void CloseWindow()
        {
            Close();
        }
    }
}
