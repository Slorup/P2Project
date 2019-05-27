using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            return username != null && username.Length != 0;
        }

        //Check if user is in database. Navigate to main page.
        private void ExecuteLoginClick(object param)
        {
            try
            {
                User currentUser = DBConnection.GetUserByUsername((string)param);
                if (currentUser != null)
                {
                    currentUser.GiveNewExercise();
                    MainPage mainPage = new MainPage();
                    MainViewModel mainvm = new MainViewModel(currentUser);
                    mainPage.DataContext = mainvm;
                    Navigator.MainNavigationService.Navigate(mainPage);
                }
                else
                {
                    MessageBox.Show("Der findes ingen profil med det ønskede brugernavn");
                }
            }
            catch(SqlException)
            {
                MessageBox.Show("Kunne ikke oprette forbindelse til databasen!");
            }
        }

        private ICommand _createUserCommand;

        public ICommand CreateUserCommand
        {
            get
            {
                return _createUserCommand ?? (_createUserCommand = new RelayCommand(param => CreateUserClick(param)));
            }
        }

        private void CreateUserClick(object param)
        {
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            CreateUserPage createUserPage = new CreateUserPage();
            createUserPage.DataContext = createUserViewModel;
            Navigator.MainNavigationService.Navigate(createUserPage);
        }

    }
}
