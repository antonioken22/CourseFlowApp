using CourseFlow.Models;
using CourseFlow.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseFlow.Views
{
    /// <summary>
    /// Interaction logic for CourseFlowsheetView.xaml
    /// </summary>
    public partial class CourseFlowsheetView : UserControl
    {
        private readonly CourseFlowsheetViewModel courseFlowsheetViewModel;

        public CourseFlowsheetView()
        {
            InitializeComponent();
            this.DataContext = courseFlowsheetViewModel = new CourseFlowsheetViewModel();
            this.Loaded += CourseFlowsheetView_Loaded;
        }
            
        private void CourseFlowsheetView_Loaded(object sender, RoutedEventArgs e)
        {
            courseFlowsheetViewModel.OnPageLoadCommand.Execute(this);
        }

        private void SubjectBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border && border.DataContext is SubjectModel subject)
            {
                courseFlowsheetViewModel.HighlightRelatedSubjects(subject);
            }
        }

        private void SubjectBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border && border.DataContext is SubjectModel subject)
            {
                courseFlowsheetViewModel.ResetRelatedSubjects(subject);
            }
        }
    }
}
