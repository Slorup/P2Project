using P2Project.DAL;
using P2Project.Model;
using P2Project.Singleton;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class CreateUserViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public int SelectedUserTypeIndex { get; set; }
        
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
            if (!FileData.UserExist(UserName)) //TRY CATCH
            {
                FileData.CreateUser(new User(UserName, new LearningProfile(0.25, 0.25, 0.25, 0.25), (UserType)SelectedUserTypeIndex, new List<int>()));
                LoginViewModel loginVM = new LoginViewModel();
                LoginPage loginPage = new LoginPage();
                loginPage.DataContext = loginVM;
                Navigator.NavigationService.Navigate(loginPage);
            }
            else
                MessageBox.Show("Brugernavnet er optaget!");
        }

        private bool CanCreateUserClick(object param) { return true; } //TODO

    }
}
