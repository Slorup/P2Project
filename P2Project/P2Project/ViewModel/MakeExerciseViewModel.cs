using Microsoft.Win32;
using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
using System;
using System.Collections.Generic;
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
        public string VideoPath { get; set; }
        public string AudioPath { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public LearningProfile ExerciseProfile { get; set; }

        private string _textBlock1;

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
        }

        public MakeExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            ImagePaths = new List<string>();
            ExerciseProfile = new LearningProfile(0,0,0,0);
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
            fileDialog.Filter = "Video files (.mp4)|*.mp4";

            fileDialog.ShowDialog();
            TextBlock1 = fileDialog.FileName;
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
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "Text documents (.txt)|*.txt"; 

            fileDialog.ShowDialog();
            TextBlock2 = fileDialog.FileName;
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
            TextBlock3 = "";
            foreach (string filename in fileDialog.FileNames) //CHECK IF DIALOG OK
                ImagePaths.Add(filename);

            foreach (string ImagePaths in ImagePaths)
            {
                TextBlock3 += ImagePaths + "\n";
            }
            
        }

        private ICommand _exerciseCreateCommand;
        public ICommand ExerciseCreateCommand
        {
            get
            {
                return _exerciseCreateCommand ?? (_exerciseCreateCommand = new RelayCommand(param => ExerciseCreateClick(param)));
            }
        }

        private void ExerciseCreateClick(object param)
        {
            //TRY
            ExerciseDescription exDescription = new ExerciseDescription(Description) { AudioPath = this.AudioPath, VideoPath = this.VideoPath, ImagePaths = this.ImagePaths };
            Exercise exercise = new Exercise(Name, exDescription, ExerciseProfile); //EXERCISE PROFILE TODO
            DBConnection.CreateExercise(exercise);
            Navigator.SubNavigationService.GoBack();
        }
    }
}
