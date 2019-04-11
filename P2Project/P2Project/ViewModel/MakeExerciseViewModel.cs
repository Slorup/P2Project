using Microsoft.Win32;
using P2Project.Model;
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
        public List<string> Files { get; set; }
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
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "Text documents (.txt)|*.txt";

            fileDialog.ShowDialog();
            TextBlock1 = fileDialog.FileName;
            
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
            fileDialog.DefaultExt = ".txt"; 
            fileDialog.Filter = "Text documents (.txt)|*.txt";

            fileDialog.ShowDialog();
            TextBlock3 = fileDialog.FileName;

        }
    }
}
