using Microsoft.Win32;
using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class MakeExerciseViewModel : BaseViewModel, IDataErrorInfo
    {
        public User CurrentUser { get; set; }

        private bool _isValidating;

        public bool IsValidating
        {
            get { return _isValidating; }
            set
            {
                SetProperty(ref _isValidating, value);
                OnPropertyChanged("Name");
                OnPropertyChanged("Description");
            }
        }

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

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string URI { get; set; }
        public double TextVisual { get; set; }
        public double ImageVisual { get; set; }
        public double Verbal { get; set; }
        public double Auditory { get; set; }
        public double Tactile { get; set; }
        public double Kinesthetic { get; set; }

        public MakeExerciseViewModel(User currentUser)
        {
            Name = string.Empty;
            Description = string.Empty;
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
            foreach (string filename in fileDialog.FileNames) 
                ImagePaths.Add(filename);

            foreach (string ImagePaths in ImagePaths)
                DisplayImagePaths += ImagePaths + "\n";
        }

        private ICommand _exerciseCreateCommand;
        public ICommand ExerciseCreateCommand
        {
            get
            {
                return _exerciseCreateCommand ?? (_exerciseCreateCommand = new RelayCommand(param => ExerciseCreateClick(param)));
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (!IsValidating) return null;
                string error = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        error = ValidateName();
                        break;
                    case "Description":
                        error = ValidateDescription();
                        break;
                }
                return error;
            }
        }

        private void ExerciseCreateClick(object param)
        {
            IsValidating = true;
            string error = string.Empty;
            if((error = ValidateName()) == null)
            {
                if((error = ValidateDescription()) == null)
                {
                    if (!Uri.IsWellFormedUriString(URI, UriKind.RelativeOrAbsolute)) URI = null;
                    List<double> profile = new List<double>() { TextVisual, ImageVisual, Verbal, Auditory, Tactile, Kinesthetic };
                    UpdateProfileValues(profile);
                    ExerciseDescription exDescription = new ExerciseDescription(Description) { AudioPath = this.AudioPath, VideoPath = this.VideoPath, ImagePaths = this.ImagePaths, SolutionPath = this.SolutionPath };
                    Exercise exercise = new Exercise(Name, exDescription, profile, CurrentUser.UserName, DateTime.Now, URI);
                    try
                    {
                        DBConnection.CreateExercise(exercise);

                        MakeExercisePage makeexercisePage = new MakeExercisePage();
                        makeexercisePage.DataContext = new MakeExerciseViewModel(CurrentUser);
                        Navigator.SubNavigationService.Navigate(makeexercisePage);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Kunne ikke oprette forbindelse til databasen!");
                    }
                }
                else
                    MessageBox.Show(error);
            }
            else
                MessageBox.Show(error);
        }

        private void UpdateProfileValues(List<double> profile)
        {
            double sum = profile.Sum();
            if (sum != 0)
            {
                for (int i = 0; i < profile.Count; i++)
                    profile[i] /= sum;
            }
            else
            {
                for (int i = 0; i < profile.Count; i++)
                    profile[i] = 1.0 / profile.Count;
            }
        }

        private string ValidateName()
        {
            if (this.Name.Length < 2)
                return "Navn skal mindst indeholde 2 tegn!";
            if (this.Name.Length > 25)
                return "Navn må højst indeholde 25 tegn!";
            if (!this.Name.All(c => Char.IsLetterOrDigit(c) || c.Equals(' ')))
                return "Navn må kun indeholde bogstaver, tal og mellemrum!";
            return null;
        }

        private string ValidateDescription()
        {
            if (this.Description.Length < 2)
                return "Beskrivelsen skal mindst indeholde 2 tegn!";
            if (this.Description.Length > 5000)
                return "Beskrivelsen må højst indeholde 5000 tegn!";
            return null;
        } 
    }
}
