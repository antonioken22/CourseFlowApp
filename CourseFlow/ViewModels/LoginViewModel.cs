using CourseFlow.Repositories;
using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;

namespace CourseFlow.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public SecureString Password { get { return _password; } set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ErrorMessage { get { return _errorMessage; } set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsViewVisible { get { return _isViewVisible; } set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
        }


        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3
                || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;

            return validData;
        }


        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = true; //userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsViewVisible = false;
                App.SignInUser(new Models.UserAccountModel());
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
