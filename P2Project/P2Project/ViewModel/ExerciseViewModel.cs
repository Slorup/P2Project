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
    class ExerciseViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }

        private Page _videoFrame;

        public Page VideoFrame
        {
            get { return _videoFrame; }
            set { SetProperty(ref _videoFrame, value); }
        }

        private Visibility _panelVisibility;

        public Visibility PanelVisibility
        {
            get { return _panelVisibility; }
            set { SetProperty(ref _panelVisibility, value); }
        }

        private string _currentImagePath;

        public string CurrentImagePath
        {
            get { return _currentImagePath; }
            set { SetProperty(ref _currentImagePath, value); }
        }

        public ExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            VideoPlayerPage page = new VideoPlayerPage();
            VideoPlayerViewModel vm = new VideoPlayerViewModel(CurrentUser.CurrentExercise.Description.VideoPath, new TimeSpan(0), false);
            page.DataContext = vm;
            VideoFrame = page;
            //if (CurrentUser.CurrentExercise.Description.ImagePaths != null && CurrentUser.CurrentExercise.Description.ImagePaths.Count > 0)
                CurrentImagePath = @"C:\Users\Slorup\Desktop\Kurt.png";
                    //CurrentImagePath = CurrentUser.CurrentExercise.Description.ImagePaths[0];
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
            //TODO
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
            CurrentUser.GiveNewExercise();
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
