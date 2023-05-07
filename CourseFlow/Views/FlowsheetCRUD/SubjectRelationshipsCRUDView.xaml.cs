using System.Windows;

namespace CourseFlow.Views.FlowsheetCRUD
{
    /// <summary>
    /// Interaction logic for SubjectRelationshipsCRUDView.xaml
    /// </summary>
    public partial class SubjectRelationshipsCRUDView : Window
    {
        public SubjectRelationshipsCRUDView()
        {
            InitializeComponent();
        }

        public void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
