
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TestMVVMLight.Services;
using Utility.ExtensionMethod;

namespace TestMVVMLight.ViewModels
{
    public interface ILogin
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    public class LoginViewModel : ViewModelBase, ILogin
    {
        private readonly LoginService dataService = new LoginService();

        private string username = string.Empty;
        private string password = string.Empty;

        public string Username
        {
            get { return username; }
            set { Set(ref username, value); }
        }

        public string Password
        {
            get { return password; }
            set { Set(ref password, value); }
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new RelayCommand(Login));
            }
        }

        private void Login()
        {
            if (Username.IsEmpty() || Password.IsEmpty())
            {
                //Alert message here.
                return;
            }
            dataService.Login(this);
        }
    }
}
