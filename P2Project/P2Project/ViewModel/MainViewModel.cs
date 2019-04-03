using P2Project.DAL;
using P2Project.Model;
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
    public class MainViewModel
    {
        public Page FramePage { get; set; }
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
    }
}
