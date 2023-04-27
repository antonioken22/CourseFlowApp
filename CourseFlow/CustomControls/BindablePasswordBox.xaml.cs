using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace CourseFlow.CustomControls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(SecureString), typeof(BindablePasswordBox));

        public SecureString Password { get { return (SecureString)GetValue(PasswordProperty); } set { SetValue(PasswordProperty, value); } }

        public BindablePasswordBox()
        {
            InitializeComponent();
            textPassword.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = textPassword.SecurePassword;
        }
    }
}
