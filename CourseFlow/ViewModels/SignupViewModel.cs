using CourseFlow.Models;
using CourseFlow.Repositories;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CourseFlow.ViewModels
{
    public class SignupViewModel : ViewModelBase
    {
        // Fields
        private IUserRepository _userRepository;
        private UserModel _newUser;
        private SecureString _confirmPassword;


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

        public SecureString ConfirmPassword
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
                User.Password == null ||
                string.IsNullOrWhiteSpace(User.FirstName) ||
                string.IsNullOrWhiteSpace(User.LastName) ||
                string.IsNullOrWhiteSpace(User.Email) ||
                string.IsNullOrEmpty(User.ProfilePicture))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            // Convert SecureString to string
            var passwordHash = HashPassword(User.Password);
            var confirmPasswordHash = HashPassword(ConfirmPassword);

            if (passwordHash == confirmPasswordHash)
            {
                User.Password = StringToSecureString(passwordHash); // Convert back to SecureString and store
                _userRepository.Add(User);
                User = new UserModel { Role = "Student" }; // Clear the form
                MessageBox.Show("User successfully registered.");
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
            }
        }

        private static SecureString StringToSecureString(string inputString)
        {
            SecureString securePassword = new SecureString();

            foreach (char c in inputString)
            {
                securePassword.AppendChar(c);
            }

            securePassword.MakeReadOnly();

            return securePassword;
        }

        private string HashPassword(SecureString secureString)
        {
            // Convert SecureString to string
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                string plainText = Marshal.PtrToStringUni(unmanagedString);

                // Compute hash from string
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                    StringBuilder hash = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        hash.Append(b.ToString("x2"));
                    }
                    return hash.ToString();
                }
            }
            finally
            {
                // Zero and free the unmanaged string
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
