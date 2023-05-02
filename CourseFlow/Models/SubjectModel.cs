using System.ComponentModel;

namespace CourseFlow.Models
{
    public class SubjectModel : AuditableEntity
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int CourseID { get; set; }
        public int AcademicYearID { get; set; }
        public int YearLevelID { get; set; }
        public int SemesterID { get; set; }

        private ViewModelBase _viewModelBase = new ViewModelBase();
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _viewModelBase.PropertyChanged += value; }
            remove { _viewModelBase.PropertyChanged -= value; }
        }

        private string _backgroundColor;
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                _viewModelBase.OnPropertyChanged(nameof(BackgroundColor));
            }
        }
    }
}
