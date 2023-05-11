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

        private int _yearLevel;
        public int YearLevel
        {
            get { return _yearLevel; }
            set
            {
                _yearLevel = value;
                OnPropertyChanged(nameof(YearLevel));
            }
        }

        private int _semester;
        public int Semester
        {
            get { return _semester; }
            set
            {
                _semester = value;
                OnPropertyChanged(nameof(Semester));
            }
        }

        private string _subjectCode;
        public string SubjectCode
        {
            get { return _subjectCode; }
            set
            {
                _subjectCode = value;
                OnPropertyChanged(nameof(SubjectCode));
            }
        }

        private string _subjectName;
        public string SubjectName
        {
            get { return _subjectName; }
            set
            {
                _subjectName = value;
                OnPropertyChanged(nameof(SubjectName));
            }
        }

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
        public ICommand RemoveRelationshipCommand { get; }



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
            RemoveRelationshipCommand = new ViewModelCommand(param=> RemoveRelationship(param as SubjectRelationshipModel));
            SaveAllCommand = new ViewModelCommand(param => SaveAll());

            SubjectRelationships = new ObservableCollection<SubjectRelationshipModel>();
        }


        // Edit Button Methods
        public bool LoadSelectedSubject(int id)
        {
            SelectedSubject = _subjectRepository.GetById(id);

            if (SelectedSubject == null)
            {
                return false;
            }

            SelectedCourse = Courses.Where(s => s.Id == _selectedSubject.CourseID).FirstOrDefault();
            SelectedAcademicYear = AcademicYears.Where(s => s.Id == _selectedSubject.AcademicYearID).FirstOrDefault();
            YearLevel = _selectedSubject.YearLevelID;
            Semester = _selectedSubject.SemesterID;
            SubjectCode = _selectedSubject.SubjectCode;
            SubjectName = _selectedSubject.SubjectName;

            var subjectRelationships = _subjectRelationshipRepository.GetSubjectRelationshipsBySubject(id);
            foreach (var subjectRelationship in subjectRelationships)
            {
                subjectRelationship.RelationshipType = RelationshipTypes.Where(s => s.Id == subjectRelationship.RelationshipTypeID).FirstOrDefault();
                subjectRelationship.RelatedSubject = Subjects.Where(s => s.Id == subjectRelationship.RelatedSubjectID).FirstOrDefault();
                SubjectRelationships.Add(subjectRelationship);
            }
            OnPropertyChanged(nameof(SubjectRelationships));

            return true;
        }

        // TODO - Make the RemoveRelationship method work   

        public void RemoveRelationship(SubjectRelationshipModel relationship)
        {
            if (relationship.RelatedSubject == null)
            {
                MessageBox.Show("The related subject or subject is missing. Unable to remove the relationship.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to remove this relationship?", $"Removing relationship between {relationship.RelatedSubject.SubjectCode}", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                relationship.IsRemoved = true;
                SubjectRelationships.Remove(relationship);
                OnPropertyChanged(nameof(SubjectRelationships));
            }
        }

        // Add Button Methods
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


        // Save Button Methods
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
                SubjectCode = String.Empty;
                SubjectName = String.Empty;
                SubjectRelationships.Clear();
                OnPropertyChanged(nameof(SubjectRelationships));
                LoadSubjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
