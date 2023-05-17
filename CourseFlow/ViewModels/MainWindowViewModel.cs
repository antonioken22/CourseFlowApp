using CourseFlow.Repositories;
using System.Threading;
using FontAwesome.WPF;
using System.Windows.Input;
using System;
using CourseFlow.Models;

namespace CourseFlow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Fields
        private ViewModelBase _currentChildView;
        private string _caption;
        private FontAwesomeIcon _icon;

        private IUserRepository userRepository;

        // Properties

        public UserAccountModel CurrentUserAccount { get; set; }

        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public FontAwesomeIcon Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        // Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCourseFlowsheetViewCommand { get; }
        public ICommand ShowCourseProspectusViewCommand { get; }
        public ICommand ShowDirectoryViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowLogoutViewCommand { get; }


        public MainWindowViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            LoadCurrentUserData();

            // Initialize Commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCourseFlowsheetViewCommand = new ViewModelCommand(ExecuteShowCourseFlowsheetViewCommand);
            ShowCourseProspectusViewCommand = new ViewModelCommand(ExecuteShowCourseProspectusViewCommand);
            ShowDirectoryViewCommand = new ViewModelCommand(ExecuteShowDirectoryViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);
            ShowLogoutViewCommand = new ViewModelCommand(ExecuteShowLogoutViewCommand);

            // Default View
            ExecuteShowHomeViewCommand(null);
        }

        private void ExecuteShowLogoutViewCommand(object obj)
        {
            CurrentChildView = new LogoutViewModel();
            Caption = "Log-out";
            Icon = FontAwesomeIcon.ChevronCircleLeft;
        }

        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = "Settings";
            Icon = FontAwesomeIcon.Gear;
        }

        private void ExecuteShowDirectoryViewCommand(object obj)
        {
            CurrentChildView = new DirectoryViewModel();
            Caption = "Directory";
            Icon = FontAwesomeIcon.Users;
        }

        private void ExecuteShowCourseProspectusViewCommand(object obj)
        {
            CurrentChildView = new CourseProspectusViewModel();
            Caption = "Course Prospectus";
            Icon = FontAwesomeIcon.ListAlt;
        }

        private void ExecuteShowCourseFlowsheetViewCommand(object obj)
        {
            App.SignInUser(CurrentUserAccount);
            CurrentChildView = new CourseFlowsheetViewModel();
            Caption = "Course Flowsheet";
            Icon = FontAwesomeIcon.Table;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = FontAwesomeIcon.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                CurrentUserAccount.FirstName = user.FirstName;
                CurrentUserAccount.LastName = user.LastName;
                CurrentUserAccount.ProfilePicture = user.ProfilePicture;
                CurrentUserAccount.Role = user.Role;
                App.SignInUser(CurrentUserAccount);
            }
            else
            {
                throw new Exception("UnAuthorized Access!");
                // Hide child views.
            }
        }
    }   
}
