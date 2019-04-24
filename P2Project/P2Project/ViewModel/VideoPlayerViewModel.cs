using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class VideoPlayerViewModel : BaseViewModel
    {
        public bool IsPlaying { get; set; }
        public TimeSpan CurrentTime { get; set; }
        private string _videoPath;

        private bool _isFullScreen;

        public string VideoPath
        {
            get { return _videoPath; }
            set { SetProperty(ref _videoPath, value); }
        }

        private ICommand _fullScreenCommand;
        public ICommand FullScreenCommand
        {
            get
            {
                return _fullScreenCommand ?? (_fullScreenCommand = new RelayCommand(param => FullScreenClick(param)));
            }
        }

        private void FullScreenClick(object param)
        {
            if (_isFullScreen)
            {
                _isFullScreen = false;
                Navigator.MainNavigationService.GoBack();
            }
            else
            {
                _isFullScreen = true;
                VideoPlayerPage page = new VideoPlayerPage() { DataContext = this };
                Navigator.MainNavigationService.Navigate(page);
            }
        }

        public VideoPlayerViewModel(string path, TimeSpan currentTime, bool isFullScreen)
        {
            VideoPath = path;
            CurrentTime = currentTime;
            IsPlaying = true;
            _isFullScreen = isFullScreen;
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            if (IsPlaying)
            {
                //VideoPlayer.Pause();
                IsPlaying = false;
            }
            else
            {
                //VideoPlayer.Play();
                IsPlaying = true;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            //STOP
            //PLAY
        }
    }
}
