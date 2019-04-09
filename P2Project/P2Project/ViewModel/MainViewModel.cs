using P2Project.DAL;
using P2Project.Model;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Page _framePage;

        public Page FramePage
        {
            get { return _framePage; }
            set { SetProperty(ref _framePage, value); }
        }

        public User CurrentUser { get; set; }

        public MainViewModel()
        {
            FramePage = new LoginPage(this);
        }

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
            CurrentUser = FileData.ImportUser((string)param); //TRYCATCH
            if(CurrentUser != null)
                FramePage = new MainPage(this);
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
            FramePage = new CreateUserPage(FramePage);
        }

        private bool CanCreateUserClick(object param) { return true; }

    }
}
