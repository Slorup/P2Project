using Microsoft.Win32;
using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class MakeExerciseViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }

        public List<string> ImagePaths { get; set; }

        private string _displayImagePaths;

        public string DisplayImagePaths
        {
            get { return _displayImagePaths; }
            set { SetProperty(ref _displayImagePaths, value); }
        }

        private string _videoPath;

        public string VideoPath
        {
            get { return _videoPath; }
            set { SetProperty(ref _videoPath, value); }
        }

        private string _audioPath;

        public string AudioPath
        {
            get { return _audioPath; }
            set { SetProperty(ref _audioPath, value); }
        }

        private string _solutionPath;

        public string SolutionPath
        {
            get { return _solutionPath; }
            set { SetProperty(ref _solutionPath, value); }
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public string URI { get; set; }
        public double TextVisual { get; set; }
        public double ImageVisual { get; set; }
        public double Verbal { get; set; }
        public double Auditory { get; set; }
        public double Tactile { get; set; }
        public double Kinesthetic { get; set; }

        /*private string _textBlock1;

        public  string TextBlock1
        {
            get { return _textBlock1; }
            set { SetProperty(ref _textBlock1, value); }
        }
        private string _textBlock2;

        public string TextBlock2
        {
            get { return _textBlock2; }
            set { SetProperty(ref _textBlock2, value); }
        }
        private string _textBlock3;

        public string TextBlock3
        {
            get { return _textBlock3; }
            set { SetProperty(ref _textBlock3, value); }
        }*/

        public MakeExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            ImagePaths = new List<string>();
        }

        private ICommand _browseCommandSolution;

        public ICommand BrowseCommandSolution
        {
            get
            {
                return _browseCommandSolution ?? (_browseCommandSolution = new RelayCommand(param => BrowseCommandSolutionClick(param)));
            }
        }

        private void BrowseCommandSolutionClick(object param)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image files (.jpg, .jpeg, .png)|*.jpg; *.jpeg; *.png";

            fileDialog.ShowDialog();
            SolutionPath = fileDialog.FileName;
        }

        private ICommand _browseCommandVideo;
        public ICommand BrowseCommandVideo
        {
            get
            {
                return _browseCommandVideo ?? (_browseCommandVideo = new RelayCommand(param => BrowseCommandVideoClick(param)));
            }
        }

        private void BrowseCommandVideoClick(object param)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Video files (.mp4)|*.mp4";

            fileDialog.ShowDialog();
            //TextBlock1 = fileDialog.FileName;
            VideoPath = fileDialog.FileName;
        }

        private ICommand _browseCommandSound;
        public ICommand BrowseCommandSound
        {
            get
            {
                return _browseCommandSound ?? (_browseCommandSound = new RelayCommand(param => BrowseCommandSoundClick(param)));
            }
        }

        private void BrowseCommandSoundClick(object param)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.DefaultExt = ".mp3";
            fileDialog.Filter = "Audio File (.mp3)|*.mp3"; 

            fileDialog.ShowDialog();
            //TextBlock2 = fileDialog.FileName;
            AudioPath = fileDialog.FileName;
        }

        private ICommand _browseCommandImage;
        public ICommand BrowseCommandImage
        {
            get
            {
                return _browseCommandImage ?? (_browseCommandImage = new RelayCommand(param => BrowseCommandImageClick(param)));
            }
        }

        private void BrowseCommandImageClick(object param)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Image files (.jpg, .jpeg, .png)|*.jpg; *.jpeg; *.png";

            fileDialog.ShowDialog();
            ImagePaths.Clear();
            DisplayImagePaths = "";
            foreach (string filename in fileDialog.FileNames) //CHECK IF DIALOG OK
                ImagePaths.Add(filename);

            foreach (string ImagePaths in ImagePaths)
            {
                DisplayImagePaths += ImagePaths + "\n";
            }
            
        }

        private ICommand _exerciseCreateCommand;
        public ICommand ExerciseCreateCommand
        {
            get
            {
                return _exerciseCreateCommand ?? (_exerciseCreateCommand = new RelayCommand(param => ExerciseCreateClick(param), param => CanExerciseCreateClick(param)));
            }
        }

        private bool CanExerciseCreateClick(object param)
        {
            return Name != null && Name != "" && Description != null && Description != "";
        }

        private void ExerciseCreateClick(object param)
        {
            //TRY
            List<double> profile = new List<double>() { TextVisual, ImageVisual, Verbal, Auditory, Tactile, Kinesthetic };
            double sum = profile.Sum();
            if(sum != 0)
            {
                for (int i = 0; i < profile.Count; i++)
                    profile[i] /= sum;
            }
            else
            {
                for (int i = 0; i < profile.Count; i++)
                    profile[i] = 1 / profile.Count;
            }

            ExerciseDescription exDescription = new ExerciseDescription(Description) { AudioPath = this.AudioPath, VideoPath = this.VideoPath, ImagePaths = this.ImagePaths, SolutionPath = this.SolutionPath };
            Exercise exercise = new Exercise(Name, exDescription, profile, CurrentUser.UserName, DateTime.Now, URI);
            DBConnection.CreateExercise(exercise);

            MakeExercisePage makeexercisePage = new MakeExercisePage();
            makeexercisePage.DataContext = new MakeExerciseViewModel(CurrentUser);
            Navigator.SubNavigationService.Navigate(makeexercisePage);
        }
    }
}
