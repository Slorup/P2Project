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
        public string TextVisual { get; set; }
        public string ImageVisual { get; set; }
        public string Auditory { get; set; }
        public string Tactile { get; set; }
        public string Kinesthetic { get; set; }
        public string Verbal { get; set; }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _numberofExercises;

        public string NumberofExercises
        {
            get { return _numberofExercises; }
            set { SetProperty(ref _numberofExercises, value); }
        }

        private string _danishUserType;

        public string DanishUserType
        {
            get { return _danishUserType; }
            set { SetProperty(ref _danishUserType, value); }
        }

        public ProfileViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            Username = CurrentUser.UserName;
            NumberofExercises = "Du har lavet " + CurrentUser.CompletedExercisesID.Count.ToString() + " opgave(r)";
            DanishUserType = "Brugertype: " + Danishtype(CurrentUser.Type);
            UpdateProfileStrings();
        }

        private void UpdateProfileStrings()
        {
            double sum = CurrentUser.Profile.CalcProfileSum();
            TextVisual = ConvertProfileToString(CurrentUser.Profile.TextVisual, sum);
            ImageVisual = ConvertProfileToString(CurrentUser.Profile.ImageVisual, sum);
            Auditory = ConvertProfileToString(CurrentUser.Profile.Auditory, sum);
            Tactile = ConvertProfileToString(CurrentUser.Profile.Tactile, sum);
            Kinesthetic = ConvertProfileToString(CurrentUser.Profile.Kinesthetic, sum);
            Verbal = ConvertProfileToString(CurrentUser.Profile.Verbal, sum);
        }

        private string ConvertProfileToString(double stat, double sum)
        {
            return Math.Round(100 * stat / sum).ToString() + "%";
        }

        private string Danishtype(UserType usertype)
        {
            if (usertype == UserType.Pupil)
                return "Elev";
            else if (usertype == UserType.Teacher)
                return "Underviser";
            else if (usertype == UserType.Admin)
                return "Admin";
            else
                return "Ukendt";
        }
        
        private ICommand _logofCommand;
        public ICommand LogofCommand
        {
            get
            {
                return _logofCommand ?? (_logofCommand = new RelayCommand(param => LogofCommandClick(param)));
            }
        }

        private void LogofCommandClick(object param)
        {
            LoginPage page = new LoginPage();
            LoginViewModel vm = new LoginViewModel();
            page.DataContext = vm;
            Navigator.MainNavigationService.Navigate(page);

        }
    }
}
