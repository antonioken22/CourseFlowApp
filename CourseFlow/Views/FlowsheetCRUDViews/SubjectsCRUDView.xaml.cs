using CourseFlow.ViewModels.FlowsheetCRUDViewModels;
using System;
using System.Windows;

namespace CourseFlow.Views.FlowsheetCRUDViews
{
    /// <summary>
    /// Interaction logic for SubjectsCRUDView.xaml
    /// </summary>
    public partial class SubjectsCRUDView : Window
    {
        private readonly SubjectsCRUDViewModel subjectsCRUDViewModel;

        private readonly int? selectedSubject;

        public EventHandler OnSaveButtonClickedHandler;

        public SubjectsCRUDView()
        {
            InitializeComponent();
            subjectsCRUDViewModel = new SubjectsCRUDViewModel();
            this.DataContext = subjectsCRUDViewModel;
            this.Loaded += SubjectsCRUDView_Loaded;
        }

        public SubjectsCRUDView(int selectedSubject)
        {
            InitializeComponent();
            subjectsCRUDViewModel = new SubjectsCRUDViewModel();
            this.selectedSubject = selectedSubject;
            this.DataContext = subjectsCRUDViewModel;
            this.Loaded += SubjectsCRUDView_Loaded;
        }


        private void SubjectsCRUDView_Loaded(object sender, RoutedEventArgs e)
        {
            subjectsCRUDViewModel.OnWindowLoadCommand.Execute(this);
            var temp = this.DataContext as SubjectsCRUDViewModel;
            if (selectedSubject.HasValue && temp != null)
            {
                if (!temp.LoadSelectedSubject(selectedSubject.Value))
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
