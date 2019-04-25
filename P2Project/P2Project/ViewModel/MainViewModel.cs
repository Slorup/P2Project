using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
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
        public User CurrentUser { get; set; }

        public MainViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            ExerciseViewModel exerciseVM = new ExerciseViewModel(CurrentUser);
            ExercisePage exercisePage = new ExercisePage();
            exercisePage.DataContext = exerciseVM;
            Navigator.SubNavigationService.Navigate(exercisePage);
        }

        private ICommand _menuProfileCommand;

        public ICommand MenuProfileCommand
        {
            get
            {
                return _menuProfileCommand ?? (_menuProfileCommand = new RelayCommand(param => MenuProfileClick(param)));
            }
        }

        private void MenuProfileClick(object param)
        {
            ProfileViewModel profileVM = new ProfileViewModel(CurrentUser);
            ProfilePage profilePage = new ProfilePage();
            profilePage.DataContext = profileVM;
            Navigator.SubNavigationService.Navigate(profilePage);
        }

        private ICommand _menuExerciseCommand;

        public ICommand MenuExerciseCommand
        {
            get
            {
                return _menuExerciseCommand ?? (_menuExerciseCommand = new RelayCommand(param => MenuExerciseClick(param)));
            }
        }

        private void MenuExerciseClick(object param) //CHECK IF ALREADY IN SUBPAGE
        {
            ExerciseViewModel exerciseVM = new ExerciseViewModel(CurrentUser);
            ExercisePage exercisePage = new ExercisePage();
            exercisePage.DataContext = exerciseVM;
            Navigator.SubNavigationService.Navigate(exercisePage);
        }

        private ICommand _menuMakeExerciseCommand;

        public ICommand MenuMakeExerciseCommand
        {
            get
            {
                return _menuMakeExerciseCommand ?? (_menuMakeExerciseCommand = new RelayCommand(param => MenuMakeExerciseClick(param), param => CanMenuMakeExerciseClick(param)));
            }
        }

        private bool CanMenuMakeExerciseClick(object param)
        {
            return CurrentUser.Type == UserType.Teacher || CurrentUser.Type == UserType.Admin;
        }

        private void MenuMakeExerciseClick(object param)
        {
            MakeExerciseViewModel makeexerciseVM = new MakeExerciseViewModel(CurrentUser);
            MakeExercisePage makeexercisePage = new MakeExercisePage();
            makeexercisePage.DataContext = makeexerciseVM;
            Navigator.SubNavigationService.Navigate(makeexercisePage);
        }
    }
}
