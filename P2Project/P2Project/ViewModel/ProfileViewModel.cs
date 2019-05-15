using Microsoft.Win32;
using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace P2Project.ViewModel
{
    class ProfileViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }

        private string _numberofExercises;

        public string NumberofExercises
        {
            get { return _numberofExercises; }
            set { SetProperty(ref _numberofExercises, value); }
        }

        public ProfileViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            NumberofExercises = "Du har lavet " + CurrentUser.CompletedExercisesID.Count.ToString() + " opgave(r)";
        }
        
        private ICommand _logoffCommand;
        public ICommand LogoffCommand
        {
            get
            {
                return _logoffCommand ?? (_logoffCommand = new RelayCommand(param => LogoffCommandClick(param)));
            }
        }

        private void LogoffCommandClick(object param)
        {
            LoginPage page = new LoginPage();
            LoginViewModel vm = new LoginViewModel();
            page.DataContext = vm;
            Navigator.MainNavigationService.Navigate(page);
        }
    }
}
