using CourseFlow.ViewModels.FlowsheetCRUDViewModels;
using System.Windows;

namespace CourseFlow.Views.FlowsheetCRUD
{
    /// <summary>
    /// Interaction logic for SubjectsCRUDView.xaml
    /// </summary>
    public partial class SubjectsCRUDView : Window
    {
        private readonly SubjectsCRUDViewModel subjectsCRUDViewModel;

        public SubjectsCRUDView()
        {
            InitializeComponent();
            this.DataContext = subjectsCRUDViewModel = new SubjectsCRUDViewModel();
            this.Loaded += SubjectsCRUDView_Loaded;
        }

        private void SubjectsCRUDView_Loaded(object sender, RoutedEventArgs e)
        {
            subjectsCRUDViewModel.OnWindowLoadCommand.Execute(this);
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
