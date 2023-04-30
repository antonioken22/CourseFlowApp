using CourseFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
