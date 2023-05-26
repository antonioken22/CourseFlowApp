using System.Windows;
using System.Windows.Controls;

namespace CourseFlow.CustomControls
{
    public partial class BindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox));

        public string Password { get { return (string)GetValue(PasswordProperty); } set { SetValue(PasswordProperty, value); } }

        public BindablePasswordBox()
        {
            InitializeComponent();
            textPassword.PasswordChanged += OnPasswordChanged;
            textVisiblePassword.TextChanged += OnVisiblePasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = textPassword.Password;
            textVisiblePassword.Text = textPassword.Password;
        }

        private void OnVisiblePasswordChanged(object sender, TextChangedEventArgs e)
        {
            if (textVisiblePassword.IsVisible)
            {
                textPassword.Password = textVisiblePassword.Text;
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = Visibility.Hidden;
            textVisiblePassword.Visibility = Visibility.Visible;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = Visibility.Visible;
            textVisiblePassword.Visibility = Visibility.Hidden;
        }
    }
}