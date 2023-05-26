using CourseFlow.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;

namespace CourseFlow.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(nameof(Password)); } }
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
            string userSalt = userRepository.GetSaltByUsername(Username);

            if (userSalt != null && !string.IsNullOrEmpty(userSalt))
            {
                // Decode the salt from base64 string to byte array.
                byte[] salt = Convert.FromBase64String(userSalt);

                // Hash the password with the salt.
                string hashedPassword = HashPassword(Password, salt);

                // Now we can authenticate the user with the hashed password.
                var isValidUser = userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, hashedPassword));
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
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        public string HashPassword(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Combine the salt and password and return them as a string
            return Convert.ToBase64String(salt) + ":" + hashed;
        }

        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
