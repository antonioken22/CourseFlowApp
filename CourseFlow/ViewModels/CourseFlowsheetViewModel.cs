using CourseFlow.Models;
using CourseFlow.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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
        private IRelationshipTypeRepository _relationshipTypeRepository;

        // Properties
        public CourseModel SelectedCourse { get; set; }
        public AcademicYearModel SelectedAcademicYear { get; set; }
        public YearLevelModel LoadedYearLevel { get; set; }
        public SemesterModel LoadedSemesterModel { get; set; }
        public SubjectModel HoveredSubject { get; set; }


        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<AcademicYearModel> AcademicYears { get; set; }
        public ObservableCollection<YearLevelModel> YearLevels { get; set; }
        public ObservableCollection<SemesterModel> Semesters { get; set; }
        public ObservableCollection<SubjectModel> Subjects { get; set; }
        public ObservableCollection<SubjectRelationshipModel> SubjectRelationships { get; set; }
        public ObservableCollection<RelationshipTypeModel> RelationshipTypes { get; set; }

        public ObservableCollection<YearLevelData> FlowsheetData { get; set; }
        public class YearLevelData
        {
            public YearLevelData()
            {
                Semesters = new ObservableCollection<SemesterData>();
            }
            public YearLevelModel YearLevel { get; set; }

            public ObservableCollection<SemesterData> Semesters { get; set; }
        }
        public class SemesterData
        {
            public SemesterModel Semester { get; set; }
            public ObservableCollection<SubjectModel> Subjects { get; set; }
        }


        public ICommand LoadCoursesCommand { get; }
        public ICommand LoadAcademicYearsCommand { get; }
        public ICommand LoadYearLevelsCommand { get; }
        public ICommand LoadSemestersCommand { get; }
        public ICommand LoadSubjectsCommand { get; }
        public ICommand LoadSubjectRelationshipsCommand { get; }
        public ICommand LoadRelationshipTypesCommand { get; }

        public ICommand LoadFlowsheetCommand { get; }
        public ICommand OnPageLoadCommand { get; }

        public ICommand SubjectMouseEnterCommand { get; }
        public ICommand SubjectMouseLeaveCommand { get; }


        // Constructors
        public CourseFlowsheetViewModel()
        {
            _courseRepository = new CourseRepository();
            _academicYearRepository = new AcademicYearRepository();
            _yearLevelRepository = new YearLevelRepository();
            _semesterRepository = new SemesterRepository();
            _subjectRepository = new SubjectRepository();
            _subjectRelationshipRepository = new SubjectRelationshipRepository();
            _relationshipTypeRepository = new RelationshipTypeRepository();

            Courses = new ObservableCollection<CourseModel>();
            AcademicYears = new ObservableCollection<AcademicYearModel>();
            YearLevels = new ObservableCollection<YearLevelModel>();
            Semesters = new ObservableCollection<SemesterModel>();
            Subjects = new ObservableCollection<SubjectModel>();
            SubjectRelationships = new ObservableCollection<SubjectRelationshipModel>();
            RelationshipTypes = new ObservableCollection<RelationshipTypeModel>();

            LoadCoursesCommand = new ViewModelCommand(param => LoadCourses());
            LoadAcademicYearsCommand = new ViewModelCommand(param => LoadAcademicYears());
            LoadYearLevelsCommand = new ViewModelCommand(param => LoadYearLevels());
            LoadFlowsheetCommand = new ViewModelCommand(param => LoadFlowsheet());
            LoadSubjectRelationshipsCommand = new ViewModelCommand(param => LoadSubjectRelationships(param as SubjectModel));

            FlowsheetData = new ObservableCollection<YearLevelData>();
            OnPageLoadCommand = new ViewModelCommand(param => OnPageLoad());

            SubjectMouseEnterCommand = new ViewModelCommand(param => OnSubjectMouseEnter(param as SubjectModel));
            SubjectMouseLeaveCommand = new ViewModelCommand(param => OnSubjectMouseLeave());
        }

        // Mouse Events Methods
        private void OnSubjectMouseEnter(SubjectModel subject)
        {
            if (subject == null)
            {
                return;
            }

            HoveredSubject = subject;

            var subjectRelationships = _subjectRelationshipRepository.GetSubjectRelationshipsBySubject(subject);
            foreach (var subjectRelationship in subjectRelationships)
            {
                var relatedSubject = Subjects.FirstOrDefault(s => s.Id == subjectRelationship.RelatedSubjectID);
                if (relatedSubject != null)
                {
                    switch (subjectRelationship.RelationshipTypeID)
                    {
                        case 1: // Pre-requisite
                            relatedSubject.BackgroundColor = Brushes.Yellow;
                            break;
                        case 2: // Co-requisite
                            relatedSubject.BackgroundColor = Brushes.Green;
                            break;
                    }
                }
            }
        }

        private void OnSubjectMouseLeave()
        {
            if (HoveredSubject == null)
            {
                return;
            }

            var subjectRelationships = _subjectRelationshipRepository.GetSubjectRelationshipsBySubject(HoveredSubject);
            foreach (var subjectRelationship in subjectRelationships)
            {
                var relatedSubject = Subjects.FirstOrDefault(s => s.Id == subjectRelationship.RelatedSubjectID);
                if (relatedSubject != null)
                {
                    relatedSubject.BackgroundColor = Brushes.Transparent;
                }
            }

            HoveredSubject = null;
        }


        // Page Load Methods
        private void OnPageLoad()
        {
            LoadCourses();
            LoadAcademicYears();
        }

        private void LoadCourses()
        {
            Courses = new ObservableCollection<CourseModel>(_courseRepository.GetAll());
            OnPropertyChanged(nameof(Courses));
        }

        private void LoadAcademicYears()
        {
            AcademicYears = new ObservableCollection<AcademicYearModel>(_academicYearRepository.GetAll());
            OnPropertyChanged(nameof(AcademicYears));
        }

        private void LoadFlowsheet()
        {
            if (SelectedCourse == null || SelectedAcademicYear == null)
            {
                return;
            }

            FlowsheetData.Clear();
            LoadYearLevels();

            OnPropertyChanged(nameof(FlowsheetData));
        }

        private void LoadYearLevels()
        {
            Subjects = new ObservableCollection<SubjectModel>(_subjectRepository.GetSubjectsByCourseAndAcademicYear(SelectedCourse, SelectedAcademicYear));
            var yearLevelsInSubjects = Subjects.Select(s => s.YearLevelID).Distinct().OrderBy(id => id).ToList();

            foreach (var yearLevelID in yearLevelsInSubjects)
            {
                var yearLevel = _yearLevelRepository.GetById(yearLevelID);
                var yearLevelData = new YearLevelData { YearLevel = yearLevel };

                LoadSemesters(yearLevelData);

                FlowsheetData.Add(yearLevelData);
            }
        }

        private void LoadSemesters(YearLevelData yearLevelData)
        {
            var subjectsByYearLevelCourseAndAcademicYear = Subjects.Where(x => x.YearLevelID == yearLevelData.YearLevel.Id).ToList();
            var semesterIdsInSubjects = subjectsByYearLevelCourseAndAcademicYear.Select(s => s.SemesterID).Distinct().OrderBy(id => id).ToList();

            var semesters = _semesterRepository.GetAll().Where(s => semesterIdsInSubjects.Contains(s.Id));
            foreach (var semester in semesters)
            {
                var semesterData = new SemesterData { Semester = semester };
                semesterData.Subjects = new ObservableCollection<SubjectModel>();

                var subjects = subjectsByYearLevelCourseAndAcademicYear.Where(s => s.SemesterID == semester.Id).ToList();
                foreach (var subject in subjects)
                {
                    semesterData.Subjects.Add(subject);
                }

                yearLevelData.Semesters.Add(semesterData);
            }
        }

        private void LoadSubjectRelationships(SubjectModel subjectModel)
        {
            var subjectRelationships = _subjectRelationshipRepository.GetSubjectRelationshipsBySubject(subjectModel);
            foreach (var subjectRelationship in subjectRelationships)
            {
                var relationshipType = _relationshipTypeRepository.GetById(subjectRelationship.RelationshipTypeID);
                subjectRelationship.RelationshipType = relationshipType;
            }
        }
    }
}