using CourseFlow.ViewModels.FlowsheetCRUDViewModels;
using System.Windows;
using System.Windows.Media;

namespace CourseFlow.Views.FlowsheetCRUD
{
    /// <summary>
    /// Interaction logic for SubjectsCRUDView.xaml
    /// </summary>
    public partial class SubjectsCRUDView : Window
    {
        private readonly SubjectsCRUDViewModel subjectsCRUDViewModel;

        private readonly int? selectedSubject;

        public SubjectsCRUDView()
        {
            InitializeComponent();
            this.DataContext = subjectsCRUDViewModel = new SubjectsCRUDViewModel();
            this.Loaded += SubjectsCRUDView_Loaded;
        }

        public SubjectsCRUDView(int selectedSubject)
        {
            InitializeComponent();
            this.selectedSubject = selectedSubject;
            this.DataContext = subjectsCRUDViewModel = new SubjectsCRUDViewModel(selectedSubject);
            this.Loaded += SubjectsCRUDView_Loaded;
        }

        private void SubjectsCRUDView_Loaded(object sender, RoutedEventArgs e)
        {
            subjectsCRUDViewModel.OnWindowLoadCommand.Execute(this);
            if (this.selectedSubject != null)
            {
                var isLoaded = subjectsCRUDViewModel.LoadSelectedSubjects(selectedSubject.Value);

                if (!isLoaded)
                {
                    MessageBox.Show("Cannot find the Subject in the Database.");
                    this.Close();
                }
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DarkModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["WindowBackground"] = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30));
            Application.Current.Resources["ControlBackground"] = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
            Application.Current.Resources["ControlForeground"] = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
        }

        private void DarkModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["WindowBackground"] = SystemColors.WindowBrush;
            Application.Current.Resources["ControlBackground"] = SystemColors.ControlBrush;
            Application.Current.Resources["ControlForeground"] = SystemColors.ControlTextBrush;
        }
    }
}
