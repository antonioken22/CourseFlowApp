using CourseFlow.ViewModels.FlowsheetCRUDViewModels;
using System;
using System.Windows;

namespace CourseFlow.Views.FlowsheetCRUD
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
            this.DataContext = subjectsCRUDViewModel = new SubjectsCRUDViewModel();
            this.Loaded += SubjectsCRUDView_Loaded;
        }

        public SubjectsCRUDView(int selectedSubject)
        {
            InitializeComponent();
            this.selectedSubject = selectedSubject;
            this.DataContext = subjectsCRUDViewModel = new SubjectsCRUDViewModel();
            this.Loaded += SubjectsCRUDView_Loaded;
        }

        private void SubjectsCRUDView_Loaded(object sender, RoutedEventArgs e)
        {
            subjectsCRUDViewModel.OnWindowLoadCommand.Execute(this);
            var temp = this.DataContext as SubjectsCRUDViewModel;
            if(selectedSubject.HasValue && temp != null)
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
