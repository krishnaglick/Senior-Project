using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using TestMVVMLight.Model;

namespace TestMVVMLight.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _welcomeTitle = string.Empty;
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }

        public ICommand LoginViewCommand { get; private set; }
        public ICommand ServiceEntryViewCommand { get; private set; }

        public LoginViewModel lvm = new LoginViewModel();
        public ServiceEntryViewModel sevm = new ServiceEntryViewModel();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.CurrentViewModel = new LoginViewModel();
            LoginViewCommand = new RelayCommand(() => LoadViewModel(lvm));
            ServiceEntryViewCommand = new RelayCommand(() => LoadViewModel(sevm));
        }

        private void LoadViewModel(ViewModelBase viewModel)
        {
            this.CurrentViewModel = viewModel;
        }
    }
}