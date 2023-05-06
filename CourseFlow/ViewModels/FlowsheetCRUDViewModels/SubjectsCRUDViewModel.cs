using CourseFlow.Models;
using CourseFlow.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CourseFlow.ViewModels.FlowsheetCRUDViewModels
{
    public class SubjectsCRUDViewModel : ViewModelBase
    {
        // Fields
        private ICourseRepository _courseRepository;
        private IAcademicYearRepository _academicYearRepository;
        private IYearLevelRepository _yearLevelRepository;
        private ISemesterRepository _semesterRepository;
        private ISubjectRepository _subjectRepository;
        private ISubjectRelationshipRepository _subjectRelationshipRepository;
        private IRelationshipTypeRepository _relationshipTypeRepository;
        private CourseModel _selectedCourse;
        private AcademicYearModel _selectedAcademicYear;
        private RelationshipTypeModel _selectedRelationshipType;
        private SubjectModel _selectedSubject;

        // Properties
        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<AcademicYearModel> AcademicYears { get; set; }
        public ObservableCollection<YearLevelModel> YearLevels { get; set; }
        public ObservableCollection<SemesterModel> Semesters { get; set; }
        public ObservableCollection<SubjectModel> Subjects { get; set; }
        public ObservableCollection<SubjectRelationshipModel> SubjectRelationships { get; set; }
        public ObservableCollection<RelationshipTypeModel> RelationshipTypes { get; set; }

        public int YearLevel { get; set; }
        public int Semester { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }

        public CourseModel SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                OnPropertyChanged(nameof(SelectedCourse));
                LoadSubjects();
            }
        }

        public AcademicYearModel SelectedAcademicYear
        {
            get { return _selectedAcademicYear; }
            set
            {
                _selectedAcademicYear = value;
                OnPropertyChanged(nameof(SelectedAcademicYear));
                LoadSubjects();
            }
        }

        public RelationshipTypeModel SelectedRelationshipType
        {
            get { return _selectedRelationshipType; }
            set
            {
                _selectedRelationshipType = value;
                OnPropertyChanged(nameof(SelectedRelationshipType));
            }
        }

        public SubjectModel SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                OnPropertyChanged(nameof(SelectedSubject));
            }
        }

        public ICommand OnWindowLoadCommand { get; }
        public ICommand AddRelationshipsCommand { get; }
        public ICommand SaveAllCommand { get; }


        // Constructor
        public SubjectsCRUDViewModel()
        {
            _courseRepository = new CourseRepository();
            _academicYearRepository = new AcademicYearRepository();
            _yearLevelRepository = new YearLevelRepository();
            _semesterRepository = new SemesterRepository();
            _subjectRepository = new SubjectRepository();
            _subjectRelationshipRepository = new SubjectRelationshipRepository();
            _relationshipTypeRepository = new RelationshipTypeRepository();

            OnWindowLoadCommand = new ViewModelCommand(param => OnWindowLoad());
            AddRelationshipsCommand = new ViewModelCommand(param => AddRelationships());
            SaveAllCommand = new ViewModelCommand(param => SaveAll());

            SubjectRelationships = new ObservableCollection<SubjectRelationshipModel>();
        }

        private void OnWindowLoad()
        {
            LoadAcademicYears();
            LoadCourses();
            LoadYearLevels();
            LoadSemesters();
            LoadRelationshipTypes();
        }

        public void LoadCourses()
        {
            Courses = new ObservableCollection<CourseModel>(_courseRepository.GetAll());
            OnPropertyChanged(nameof(Courses));
        }

        public void LoadAcademicYears()
        {
            AcademicYears = new ObservableCollection<AcademicYearModel>(_academicYearRepository.GetAll());
            OnPropertyChanged(nameof(AcademicYears));
        }

        public void LoadYearLevels()
        {
            YearLevels = new ObservableCollection<YearLevelModel>(_yearLevelRepository.GetAll());
            OnPropertyChanged(nameof(YearLevels));
        }

        public void LoadSemesters()
        {
            Semesters = new ObservableCollection<SemesterModel>(_semesterRepository.GetAll());
            OnPropertyChanged(nameof(Semesters));
        }

        public void LoadRelationshipTypes()
        {
            RelationshipTypes = new ObservableCollection<RelationshipTypeModel>(_relationshipTypeRepository.GetAll());
            OnPropertyChanged(nameof(RelationshipTypes));
        }

        public void LoadSubjects()
        {
            if (SelectedCourse == null || SelectedAcademicYear == null)
            {
                return;
            }
            var subjectsByCourseAndAcademicYear = _subjectRepository.GetSubjectsByCourseAndAcademicYear(SelectedCourse, SelectedAcademicYear);
            Subjects = new ObservableCollection<SubjectModel>(subjectsByCourseAndAcademicYear);
            OnPropertyChanged(nameof(Subjects));
        }

        private void AddRelationships()
        {
            if (SelectedRelationshipType == null || SelectedSubject == null)
            {
                return;
            }

            var subjectRelationship = new SubjectRelationshipModel
            {
                SubjectID = null,
                RelationshipTypeID = SelectedRelationshipType.Id, 
                RelatedSubjectID = SelectedSubject.Id, 
                RelationshipType = SelectedRelationshipType,
                RelatedSubject = SelectedSubject
            }; 

            SubjectRelationships.Add(subjectRelationship);

            OnPropertyChanged(nameof(SubjectRelationships));

            SelectedRelationshipType = null;
            SelectedSubject = null;
        }

        private void SaveAll()
        {
            try
            {
                SubjectModel subject = new SubjectModel();
                subject.CourseID = SelectedCourse.Id;
                subject.AcademicYearID = SelectedAcademicYear.Id;
                subject.YearLevelID = YearLevel;
                subject.SemesterID = Semester;
                subject.SubjectCode = SubjectCode;
                subject.SubjectName = SubjectName;

                _subjectRepository.Add(subject);

                foreach (var subjectRelationship in SubjectRelationships)
                {
                    subjectRelationship.SubjectID = subject.Id;
                    _subjectRelationshipRepository.Add(subjectRelationship);
                }

                MessageBox.Show("Successfully saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Load data from database
        public void LoadData()
        {
            // Load data from your database using your data access methods
        }
    }
}
