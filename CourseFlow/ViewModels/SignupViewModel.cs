using CourseFlow.Models;
using CourseFlow.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace CourseFlow.ViewModels
{
    public class SignupViewModel : ViewModelBase
    {
        // Fields
        private IUserRepository _userRepository;
        private UserModel _newUser;
        private string _confirmPassword;


        // Properties
        public UserModel User
        {
            get { return _newUser; }
            set
            {
                if (_newUser != value)
                {
                    _newUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RegisterCommand { get; private set; }
        public ICommand CloseWindowCommand { get; set; }

        public SignupViewModel()
        {
            _userRepository = new UserRepository();

            User = new UserModel();
            User.Role = "Student"; // Default Role

            RegisterCommand = new ViewModelCommand(param => Register());
        }

        public void Register()
        {
            // Check if fields are not null
            if (string.IsNullOrWhiteSpace(User.Username) ||
                string.IsNullOrWhiteSpace(User.Password) ||
                string.IsNullOrWhiteSpace(User.FirstName) ||
                string.IsNullOrWhiteSpace(User.LastName) ||
                string.IsNullOrWhiteSpace(User.Email) ||
                string.IsNullOrEmpty(User.ProfilePicture))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var passwordHash = HashPassword(User.Password, salt);
            var confirmPasswordHash = HashPassword(ConfirmPassword, salt);

            if (passwordHash == confirmPasswordHash)
            {
                User.Password = passwordHash;
                User.Salt = Convert.ToBase64String(salt);
                _userRepository.Add(User);
                MessageBox.Show("User successfully registered.");
                CloseWindowCommand?.Execute(null);
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
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
    }
}
