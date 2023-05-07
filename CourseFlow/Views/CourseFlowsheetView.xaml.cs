using CourseFlow.ViewModels;
using CourseFlow.Views.FlowsheetCRUD;
using System;
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

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            SubjectsCRUDView subjectsCRUDView = new SubjectsCRUDView();
            subjectsCRUDView.ShowDialog();
        }
    }
}
