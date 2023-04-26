using CourseFlow.Models;
using CourseFlow.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseFlow.ViewModels
{
    public class CourseFlowsheetViewModel : ViewModelBase
    {
        private ICourseRepository _courseRepository;
        private IAcademicYearRepository _academicYearRepository;
        private IYearLevelRepository _yearLevelRepository;
        private ISemesterRepository _semesterRepository;
        private ISubjectRepository _subjectRepository;
        private ISubjectRelationshipRepository _subjectRelationshipRepository;

        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<AcademicYearModel> AcademicYears { get; set; }
        public ObservableCollection<YearLevelModel> YearLevels { get; set; }
        public ObservableCollection<SemesterModel> Semesters { get; set; }
        public ObservableCollection<SubjectModel> Subjects { get; set; }
        public ObservableCollection<SubjectRelationshipModel> SubjectRelationships { get; set; }

        public ICommand LoadCoursesCommand { get; }
        public ICommand LoadAcademicYearsCommand { get; }
        public ICommand LoadYearLevelsCommand { get; }
        public ICommand LoadSemestersCommand { get; }
        public ICommand LoadSubjectsCommand { get; }
        public ICommand LoadSubjectRelationshipsCommand { get; }

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
        }

        private void LoadCourses()
        {
            var courses = _courseRepository.GetAll();
            Courses.Clear();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
        }

        private void LoadAcademicYears()
        {
            var academicYears = _academicYearRepository.GetAll();
            AcademicYears.Clear();
            foreach (var academicYear in academicYears)
            {
                AcademicYears.Add(academicYear);
            }
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
    }
}
