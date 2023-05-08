using CourseFlow.Models;
using CourseFlow.ViewModels.FlowsheetCRUDViewModels;
using System.Windows;
using System.Windows.Controls;
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
            this.selectedSubject = selectedSubject;
            InitializeComponent();
            this.DataContext = subjectsCRUDViewModel = new SubjectsCRUDViewModel();
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
    }
}
