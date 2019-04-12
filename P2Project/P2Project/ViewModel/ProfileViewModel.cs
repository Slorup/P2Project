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
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }
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
            NumberofExercises = "Du har lavet " + CurrentUser.CompletedExercisesID.Count.ToString() + " opgaver";
            DanishUserType = "Bruger type: " + Danishtype(CurrentUser.Type);
        }
        private string Danishtype(UserType usertype)
        {
            if (usertype == UserType.Pupil)
                return "Elev";
            else if (usertype == UserType.Teacher)
                return "Lærer";
            else if (usertype == UserType.Admin)
                return "Admin";
            else
                return "0";
        }
        private ICommand _browseImageCommand;
        public ICommand BrowseImageCommand
        {
            get
            {
                return _browseImageCommand ?? (_browseImageCommand = new RelayCommand(param => BrowseImageCommandClick(param)));
            }
        }

        private void BrowseImageCommandClick(object param)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".jpg";
            fileDialog.Filter = "Image (.jpg)|*.jpg";

            fileDialog.ShowDialog();
            CurrentUser.ProfileImage = fileDialog.FileName;
            ImagePath = fileDialog.FileName;
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
