using P2Project.DAL;
using P2Project.Model;
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
    class CreateUserViewModel
    {
        public Page MainPage { get; set; }
        public string UserName { get; set; }
        public int SelectedUserTypeIndex { get; set; }
        
        public CreateUserViewModel(Page mainPage)
        {
            MainPage = mainPage;
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
            if (!FileData.UserExist(UserName))
            {
                FileData.CreateUser(new User(UserName, new LearningProfile(0.25, 0.25, 0.25, 0.25), (UserType)SelectedUserTypeIndex, new List<int>()));
                //MainPage = new LoginPage();
            }
            else
                MessageBox.Show("Brugernavnet er optaget!");
        }

        private bool CanCreateUserClick(object param) { return true; }


    }
}
