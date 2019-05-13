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
using System.Windows.Media;

namespace P2Project.ViewModel
{
    class ExerciseViewModel : BaseViewModel
    {
        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        /*private string _solutionPath;

        public string SolutionPath
        {
            get { return _solutionPath; }
            set { SetProperty(ref _solutionPath, value); }
        }*/

        private Visibility _solutionVisibility;

        public Visibility SolutionVisibility
        {
            get { return _solutionVisibility; }
            set { SetProperty(ref _solutionVisibility, value); }
        }

        private Page _videoFrame;

        public Page VideoFrame
        {
            get { return _videoFrame; }
            set{ SetProperty(ref _videoFrame, value); }
        }

        private Page _solutionFrame;

        public Page SolutionFrame
        {
            get { return _solutionFrame; }
            set { SetProperty(ref _solutionFrame, value); }
        }

        /*private bool _videoVisible;

        public bool VideoVisible
        {
            get { return _videoVisible; }
            set { SetProperty(ref _videoVisible, value); }
        }*/

        private Page _imageFrame;

        public Page ImageFrame
        {
            get { return _imageFrame; }
            set { SetProperty(ref _imageFrame, value); }
        }


        /*private bool _imageVisible;

        public bool ImageVisible
        {
            get { return _imageVisible; }
            set { SetProperty(ref _imageVisible, value); }
        }*/

        private Visibility _panelVisibility;

        public Visibility PanelVisibility
        {
            get { return _panelVisibility; }
            set { SetProperty(ref _panelVisibility, value); }
        }
        private MediaPlayer audioPlayer;

        public ExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            bool result = CurrentUser.GiveNewExercise();
            if (!result)
                MessageBox.Show("Tillykke! Du har gennemført alle opgaver!");
            UpdateExerciseDesc();
            audioPlayer = new MediaPlayer();
            SolutionVisibility = Visibility.Hidden;
        }

        private void UpdateExerciseDesc()
        {
            if (CurrentUser.CurrentExercise != null)
            {
                if (CurrentUser.CurrentExercise.Description.VideoPath != null && CurrentUser.CurrentExercise.Description.VideoPath != "")
                {
                    VideoPlayerPage videopage = new VideoPlayerPage();
                    VideoPlayerViewModel videovm = new VideoPlayerViewModel(CurrentUser.CurrentExercise.Description.VideoPath);
                    videopage.DataContext = videovm;
                    VideoFrame = videopage;
                }
                else
                {
                    VideoFrame = null;
                }

                if (CurrentUser.CurrentExercise.Description.ImagePaths != null && CurrentUser.CurrentExercise.Description.ImagePaths.Count != 0)
                {
                    ImageScrollPage imagepage = new ImageScrollPage();
                    ImageScrollViewModel imagevm = new ImageScrollViewModel(CurrentUser.CurrentExercise.Description.ImagePaths);
                    imagepage.DataContext = imagevm;
                    ImageFrame = imagepage;
                }
                else
                {
                    ImageFrame = null;
                }

                if(CurrentUser.CurrentExercise.Description.SolutionPath != null && CurrentUser.CurrentExercise.Description.SolutionPath != "")
                {
                    List<string> solutions = new List<string>();
                    solutions.Add(CurrentUser.CurrentExercise.Description.SolutionPath);
                    ImageScrollPage page = new ImageScrollPage();
                    ImageScrollViewModel vm = new ImageScrollViewModel(solutions);
                    page.DataContext = vm;
                    SolutionFrame = page;
                }
                else
                {
                    SolutionFrame = null;
                }

                if (CurrentUser.CurrentExercise.Description.AudioPath != null && CurrentUser.CurrentExercise.Description.AudioPath != "")
                    audioPlayer.Open(new Uri(CurrentUser.CurrentExercise.Description.AudioPath));
                else
                    audioPlayer = null;
            }
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
                return _finishedExerciseCommand ?? (_finishedExerciseCommand = new RelayCommand(param => FinishedExerciseClick(param), param => CanFinishedExerciseClick(param)));
            }
        }

        private ICommand _showSolutionCommand;

        public ICommand ShowSolutionCommand
        {
            get
            {
                return _showSolutionCommand ?? (_showSolutionCommand = new RelayCommand(param => ShowSolutionClick(param), param => CanShowSolutionClick(param)));
            }
        }

        private bool CanShowSolutionClick(object param)
        {
            return CurrentUser.CurrentExercise != null && CurrentUser.CurrentExercise.Description.SolutionPath != null && CurrentUser.CurrentExercise.Description.SolutionPath != "";
        }

        private void ShowSolutionClick(object param)
        {
            if (SolutionVisibility == Visibility.Visible)
                SolutionVisibility = Visibility.Hidden;
            else
                SolutionVisibility = Visibility.Visible;
        }

        private bool CanFinishedExerciseClick(object param)
        {
            return CurrentUser.CurrentExercise != null;
        }

        private void FinishedExerciseClick(object param)
        {
            FeedbackWindow window = new FeedbackWindow();
            FeedbackViewModel vm = new FeedbackViewModel(window);
            window.DataContext = vm;

            Feedback feedback = Feedback.Medium;
            bool? result = window.ShowDialog();
            if (result == true)
                feedback = vm.UserFeedback;
            CurrentUser.ExerciseCompleted(feedback);
            UpdateExerciseDesc();
            //TODO
        }

        private ICommand _skipExerciseCommand;

        public ICommand SkipExerciseCommand
        {
            get
            {
                return _skipExerciseCommand ?? (_skipExerciseCommand = new RelayCommand(param => SkipExerciseClick(param), param => CanFinishedExerciseClick(param)));
            }
        }

        private void SkipExerciseClick(object param)
        {
            CurrentUser.GiveNewExercise();
            UpdateExerciseDesc();
            //TODO
        }

        private ICommand _playAudioCommand;

        public ICommand PlayAudioCommand
        {
            get
            {
                return _playAudioCommand ?? (_playAudioCommand = new RelayCommand(param => PlayAudioClick(param), param => CanPlayAudioClick(param)));
            }
        }

        private bool CanPlayAudioClick(object param)
        {
            return audioPlayer != null && CurrentUser.CurrentExercise != null && CurrentUser.CurrentExercise.Description.AudioPath != null && CurrentUser.CurrentExercise.Description.AudioPath != "";
        }

        private void PlayAudioClick(object param)
        {
            audioPlayer.Play();
        }
    }
}
