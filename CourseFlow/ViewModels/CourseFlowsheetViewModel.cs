using CourseFlow.Models;
using CourseFlow.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        public ICommand LoadRelationshipTypesCommand { get; }

        public ICommand LoadFlowsheetCommand { get; }
        public ICommand OnPageLoadCommand { get; }

        public ICommand SubjectMouseEnterCommand { get; }
        public ICommand SubjectMouseLeaveCommand { get; }

        public ICommand RemoveSubjectAndItsRelationshipCommand { get; }



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

            FlowsheetData = new ObservableCollection<YearLevelData>();
            OnPageLoadCommand = new ViewModelCommand(param => OnPageLoad());

            SubjectMouseEnterCommand = new ViewModelCommand(param => OnSubjectMouseEnter(param as SubjectModel));
            SubjectMouseLeaveCommand = new ViewModelCommand(param => OnSubjectMouseLeave());

            RemoveSubjectAndItsRelationshipCommand = new ViewModelCommand(param => RemoveSubjectAndItsRelationship(param as SubjectModel));
        }

        // Edit and Remove
        private void RemoveSubjectAndItsRelationship(SubjectModel subject)
        {
            if (MessageBox.Show("Are you sure you want to Delete this Subject and its Relationships?", caption: $"Removing {subject.SubjectCode} {subject.SubjectName} and its Relationships.", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    _subjectRelationshipRepository.RemoveBySubjectId(subject.Id);
                    _subjectRepository.Remove(subject.Id);
                    MessageBox.Show($"Successfully Removed the Subject and its Relationships: {subject.SubjectCode} {subject.SubjectName}");
                    LoadFlowsheet();
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            }
        }

        // Mouse Events Methods
        private void OnSubjectMouseEnter(SubjectModel subject)
        {
            if (subject == null)
            {
                return;
            }

            HoveredSubject = subject;
            subject.BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#eee"));
            var subjectRelationships = _subjectRelationshipRepository.GetSubjectRelationshipsBySubject(subject.Id);
            foreach (var subjectRelationship in subjectRelationships)
            {
                var relatedSubject = Subjects.FirstOrDefault(s => s.Id == subjectRelationship.RelatedSubjectID);
                if (relatedSubject != null)
                {
                    switch (subjectRelationship.RelationshipTypeID)
                    {
                        case 1: // Pre-requisite
                            relatedSubject.BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffe54a"));
                            break;
                        case 2: // Co-requisite
                            relatedSubject.BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#98ff6e"));
                            break;
                        case 3: // Post-requisite
                            relatedSubject.BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a1f8ff"));
                            break;
                    }
                }
            }
            OnPropertyChanged(nameof(FlowsheetData));
            OnPropertyChanged(nameof(SubjectModel.BackgroundColor));
        }

        private void OnSubjectMouseLeave()
        {
            if (HoveredSubject == null)
            {
                return;
            }
            HoveredSubject.BackgroundColor = Brushes.Transparent;
            var subjectRelationships = _subjectRelationshipRepository.GetSubjectRelationshipsBySubject(HoveredSubject.Id);
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

                var subjects = subjectsByYearLevelCourseAndAcademicYear.Where(s => s.SemesterID == semester.Id).OrderBy(s => s.SubjectCode).ToList();
                foreach (var subject in subjects)
                {
                    semesterData.Subjects.Add(subject);
                }

                yearLevelData.Semesters.Add(semesterData);
            }
        }

        // Role Authority
        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }

        public Visibility ButtonVisibility
        {
            get
            {
                if (IsEditMode && App.CurrentUser?.Role == "Admin")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }
    }
}