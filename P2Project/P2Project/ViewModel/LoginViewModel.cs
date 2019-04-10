using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class LoginViewModel : BaseViewModel
    {

        private ICommand _loginClickCommand;

        public ICommand LoginClickCommand
        {
            get
            {
                return _loginClickCommand ?? (_loginClickCommand = new RelayCommand(param => ExecuteLoginClick(param), param => CanExecuteLoginClick(param)));
            }
        }

        private bool CanExecuteLoginClick(object param)
        {
            string username = (string)param;
            return username.Length == 0 ? false : true;
        }

        private void ExecuteLoginClick(object param)
        {
            User currentUser = FileData.ImportUser((string)param); //TRYCATCH
            if (currentUser != null)
            {
                MainViewModel mainvm = new MainViewModel();
                MainPage mainPage = new MainPage();
                mainPage.DataContext = mainvm;
                Navigator.NavigationService.Navigate(mainPage);
            }
        }

        private ICommand _createUserCommand;

        public ICommand CreateUserCommand
        {
            get
            {
                return _createUserCommand ?? (_createUserCommand = new RelayCommand(param => CreateUserClick(param), param => CanCreateUserClick(param)));
            }
        }

        private void CreateUserClick(object param)
        {
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            CreateUserPage createUserPage = new CreateUserPage();
            createUserPage.DataContext = createUserViewModel;
            Navigator.NavigationService.Navigate(createUserPage);
        }

        private bool CanCreateUserClick(object param) { return true; }
    }
}
