using CourseFlow.Models;
using CourseFlow.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CourseFlow.ViewModels
{
    public class CourseFlowsheetViewModel : ViewModelBase
    {
        // Fields
        private ICourseRepository _courseRepository;
        private IAcademicYearRepository _academicYearRepository;
        private IYearLevelRepository _yearLevelRepository;
        private ISemesterRepository _semesterRepository;
        private ISubjectRepository _subjectRepository;
        private ISubjectRelationshipRepository _subjectRelationshipRepository;

        // Properties
        public CourseModel SelectedCourse { get; set; }

        private AcademicYearModel selectedAcademicYear;
        public AcademicYearModel SelectedAcademicYear
        {
            get => selectedAcademicYear;
            set
            {
                selectedAcademicYear = value;
            }
        }

        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<AcademicYearModel> AcademicYears { get; set; }
        public ObservableCollection<YearLevelModel> YearLevels { get; set; }
        public ObservableCollection<SemesterModel> Semesters { get; set; }
        public ObservableCollection<SubjectModel> Subjects { get; set; }
        public ObservableCollection<SubjectRelationshipModel> SubjectRelationships { get; set; }

        // Nested collection: YearLevel -> Semester -> Subjects
        public ObservableCollection<YearLevelSubjects> YearLevelSubjectsCollection { get; set; }

        public ICommand LoadCoursesCommand { get; }
        public ICommand LoadAcademicYearsCommand { get; }
        public ICommand LoadYearLevelsCommand { get; }
        public ICommand LoadSemestersCommand { get; }
        public ICommand LoadSubjectsCommand { get; }
        public ICommand LoadSubjectRelationshipsCommand { get; }
        public ICommand LoadFlowsheetCommand { get; }

      
        // Events

        public ICommand OnPageLoadCommand { get; }
        public ICommand OnMouseEnter { get; }
        public ICommand OnMouseLeave { get; }

        // Control Componens
        private Brush textColor;
        public Brush TextColor
        {
            get => textColor;
            set
            {
                if (value is not null)
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
                else textColor = null;

            }
        }

        // Constructors
        public CourseFlowsheetViewModel()
        {
            _courseRepository = new CourseRepository();
            _academicYearRepository = new AcademicYearRepository();
            _yearLevelRepository = new YearLevelRepository();
            _semesterRepository = new SemesterRepository();
            _subjectRepository = new SubjectRepository();
            _subjectRelationshipRepository = new SubjectRelationshipRepository();

            Courses = new ObservableCollection<CourseModel>();
            AcademicYears = new ObservableCollection<AcademicYearModel>();
            YearLevels = new ObservableCollection<YearLevelModel>();
            Semesters = new ObservableCollection<SemesterModel>();
            Subjects = new ObservableCollection<SubjectModel>();
            SubjectRelationships = new ObservableCollection<SubjectRelationshipModel>();

            LoadCoursesCommand = new ViewModelCommand(param => LoadCourses());
            LoadAcademicYearsCommand = new ViewModelCommand(param => LoadAcademicYears());
            LoadYearLevelsCommand = new ViewModelCommand(param => LoadYearLevels());
            LoadSemestersCommand = new ViewModelCommand(param => LoadSemesters());
            LoadSubjectsCommand = new ViewModelCommand(param => LoadSubjects());
            LoadSubjectRelationshipsCommand = new ViewModelCommand(param => LoadSubjectRelationships());
            LoadFlowsheetCommand = new ViewModelCommand(param => LoadFlowsheet());

            YearLevelSubjectsCollection = new ObservableCollection<YearLevelSubjects>();

            OnPageLoadCommand = new ViewModelCommand(param => OnPageLoad());
            OnMouseEnter = new ViewModelCommand(param => OnMouseIn());
            OnMouseLeave = new ViewModelCommand(param => OnMouseOut());
        }

        // Methods

        private void OnMouseIn()
        {
            TextColor = Brushes.Red;
        }

        private void OnMouseOut()
        {
            TextColor = Brushes.White;
        }

        private void OnPageLoad()
        {
            LoadCourses();
            LoadAcademicYears();
        }

        private void LoadCourses()
        {
            var courses = _courseRepository.GetAll();
            Courses.Clear();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
            OnPropertyChanged(nameof(Courses));
        }

        private void LoadAcademicYears()
        {
            var academicYears = _academicYearRepository.GetAll();
            AcademicYears.Clear();
            foreach (var academicYear in academicYears)
            {
                AcademicYears.Add(academicYear);
            }
            OnPropertyChanged(nameof(AcademicYears));
        }

        private void LoadYearLevels()
        {
            var yearLevels = _yearLevelRepository.GetAll();
            YearLevels.Clear();
            foreach (var yearLevel in yearLevels)
            {
                YearLevels.Add(yearLevel);
            }
        }

        private void LoadSemesters()
        {
            var semesters = _semesterRepository.GetAll();
            Semesters.Clear();
            foreach (var semester in semesters)
            {
                Semesters.Add(semester);
            }
        }

        private void LoadSubjects()
        {
            var subjects = _subjectRepository.GetAll();
            Subjects.Clear();
            foreach (var subject in subjects)
            {
                Subjects.Add(subject);
            }
        }

        private void LoadSubjectRelationships()
        {
            var subjectRelationships = _subjectRelationshipRepository.GetAll();
            SubjectRelationships.Clear();
            foreach (var subjectRelationship in subjectRelationships)
            {
                SubjectRelationships.Add(subjectRelationship);
            }
        }

        private void LoadFlowsheet()
        {
            if (SelectedCourse == null || SelectedAcademicYear == null)
            {
                return;
            }

            // Retrieve subjects for the selected course and academic year
            var subjects = _subjectRepository.GetByCourseAndAcademicYear(SelectedCourse.Id, SelectedAcademicYear.Id);

            // Group subjects by year level and semester
            var subjectsByYearLevelAndSemester = subjects.GroupBy(s => new { s.YearLevelID, s.SemesterID });

            // Clear the year level subjects collection
            YearLevelSubjectsCollection.Clear();

            // Add subjects to the corresponding year level and semester
            foreach (var group in subjectsByYearLevelAndSemester)
            {
                var yearLevelID = group.Key.YearLevelID;
                var semesterID = group.Key.SemesterID;

                var yearLevel = YearLevels.FirstOrDefault(yl => yl.Id == yearLevelID);
                var semester = Semesters.FirstOrDefault(s => s.Id == semesterID);

                if (yearLevel == null || semester == null) continue;

                var yearLevelSubjects = YearLevelSubjectsCollection.FirstOrDefault(yls => yls.YearLevelID == yearLevelID);
                if (yearLevelSubjects == null)
                {
                    yearLevelSubjects = new YearLevelSubjects(yearLevelID, yearLevel.YearLevel);
                    YearLevelSubjectsCollection.Add(yearLevelSubjects);
                }

                yearLevelSubjects.AddSubjects(semesterID, semester.Semester, group.ToList());
            }
        }
    }

    public class YearLevelSubjects
    {
        public int YearLevelID { get; }
        public string YearLevelName { get; }
        public ObservableCollection<Tuple<string, ObservableCollection<SubjectModel>>> SemesterSubjects { get; }

        public YearLevelSubjects(int yearLevelID, string yearLevelName)
        {
            YearLevelID = yearLevelID;
            YearLevelName = yearLevelName;
            SemesterSubjects = new ObservableCollection<Tuple<string, ObservableCollection<SubjectModel>>>();
        }

        public void AddSubjects(int semesterID, string semesterName, List<SubjectModel> subjects)
        {
            if (SemesterSubjects.Count < semesterID)
            {
                SemesterSubjects.Add(new Tuple<string, ObservableCollection<SubjectModel>>(semesterName, new ObservableCollection<SubjectModel>()));
            }

            var semesterSubjects = SemesterSubjects[semesterID - 1].Item2;
            semesterSubjects.Clear();

            foreach (var subject in subjects)
            {
                semesterSubjects.Add(subject);
            }
        }
    }
}
