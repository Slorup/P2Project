using P2Project.DAL;
using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class ExerciseViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }
        public Exercise CurrentExercise { get; set; }

        private Visibility _panelVisibility;

        public Visibility PanelVisibility
        {
            get { return _panelVisibility; }
            set { SetProperty(ref _panelVisibility, value); }
        }
        private string _exercisedescription;

        public string Exercisedescription
        {
            get { return _exercisedescription; }
            set { SetProperty(ref _exercisedescription, value); }
        }
        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }


        public ExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            if (CurrentExercise == null)
                GetNewExercise();
            Exercisedescription = CurrentExercise.Description.TextDescription;
        }

        private ICommand _panelShowCommand;

        public ICommand PanelShowCommand
        {
            get
            {
                return _panelShowCommand ?? (_panelShowCommand = new RelayCommand(param => PanelShowClick(param)));
            }
        }

        private void PanelShowClick(object param)
        {
            if (PanelVisibility == Visibility.Collapsed)
                PanelVisibility = Visibility.Visible;
            else
                PanelVisibility = Visibility.Collapsed;
        }
        private ICommand _finishedExerciseCommand;

        public ICommand FinishedExerciseCommand
        {
            get
            {
                return _finishedExerciseCommand ?? (_finishedExerciseCommand = new RelayCommand(param => FinishedExerciseClick(param)));
            }
        }

        private void FinishedExerciseClick(object param)
        {
            
        }
        private ICommand _skipExerciseCommand;

        public ICommand SkipExerciseCommand
        {
            get
            {
                return _skipExerciseCommand ?? (_skipExerciseCommand = new RelayCommand(param => SkipExerciseClick(param)));
            }
        }

        private void SkipExerciseClick(object param)
        {
            GetNewExercise();
        }

        private void GetNewExercise()
        {
            CurrentExercise = FileData.ImportExerciseByID(0);
        }
        private ICommand _goLeftCommand;

        public ICommand GoLeftCommand
        {
            get
            {
                return _goLeftCommand ?? (_goLeftCommand = new RelayCommand(param => GoLeftClick(param)));
            }
        }

        private void GoLeftClick(object param)
        {
            //exercise. = todo;
        }
        private ICommand _goRightCommand;

        public ICommand GoRightCommand
        {
            get
            {
                return _goRightCommand ?? (_goRightCommand = new RelayCommand(param => GoRightClick(param)));
            }
        }

        private void GoRightClick(object param)
        {
            //exercise. = todo;
        }
    }
}
