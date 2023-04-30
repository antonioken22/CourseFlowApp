using CourseFlow.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseFlow.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
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

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            courseFlowsheetViewModel.OnMouseEnter.Execute(sender);
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            courseFlowsheetViewModel.OnMouseLeave.Execute(sender);
        }
    }
}
