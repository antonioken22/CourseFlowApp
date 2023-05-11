using CourseFlow.Models;
using CourseFlow.ViewModels;
using CourseFlow.Views.FlowsheetCRUD;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CourseFlow.Views
{
    /// <summary>
    /// Interaction logic for CourseFlowsheetView.xaml
    /// </summary>
    public partial class CourseFlowsheetView : UserControl
    {
        private readonly CourseFlowsheetViewModel courseFlowsheetViewModel;
        private UserModel currentUser;

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

        private void ButtonAddSubject_Clicked(object sender, EventArgs e)
        {
            SubjectsCRUDView subjectsCRUDView = new SubjectsCRUDView();
            //subjectsCRUDView.OnSaveButtonClickedHandler += OnCrudViewClosed;
            subjectsCRUDView.Closed += OnCrudViewClosed;
            subjectsCRUDView.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as CourseFlowsheetViewModel;
            var subject = dataContext.HoveredSubject;
            SubjectsCRUDView subjectsCRUDView = new SubjectsCRUDView(subject.Id);
            //subjectsCRUDView.OnSaveButtonClickedHandler += OnCrudViewClosed;
            subjectsCRUDView.Closed += OnCrudViewClosed;
            subjectsCRUDView.Show();
        }

        private void OnCrudViewClosed(object sender, EventArgs e)
        {
            var dataContext = this.DataContext as CourseFlowsheetViewModel;
            dataContext.LoadFlowsheetCommand.Execute(this);
        }
    }
}
