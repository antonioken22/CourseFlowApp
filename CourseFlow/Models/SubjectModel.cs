using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace CourseFlow.Models
{
    public class SubjectModel : AuditableEntity, INotifyPropertyChanged
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int CourseID { get; set; }
        public int AcademicYearID { get; set; }
        public int YearLevelID { get; set; }
        public int SemesterID { get; set; }

        Brush _BackgroundColor;
        public Brush BackgroundColor
        {
            get => _BackgroundColor;
            set
            {
                if (value != null)
                {
                    _BackgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
