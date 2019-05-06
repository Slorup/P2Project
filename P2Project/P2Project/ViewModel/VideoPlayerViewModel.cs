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

namespace P2Project.ViewModel
{
    class VideoPlayerViewModel : BaseViewModel
    {
        public bool IsPlaying { get; set; }
        private TimeSpan _currentTime;

        public TimeSpan CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }

        private string _videoPath;
        private bool _isFullScreen; //Private set property senere

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

        private ICommand _pauseCommand;
        public ICommand PauseCommand
        {
            get
            {
                return _pauseCommand ?? (_pauseCommand = new RelayCommand(param => PauseClick(param)));
            }
        }

        private void PauseClick(object param)
        {
            if (IsPlaying)
            {
                ((MediaElement)param).Pause();
                IsPlaying = false;
            }
            else
            {
                ((MediaElement)param).Play();
                IsPlaying = true;
            }
        }

        private ICommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand = new RelayCommand(param => ResetClick(param)));
            }
        }

        private void ResetClick(object param)
        {
            ((MediaElement)param).Stop();
            ((MediaElement)param).Play();
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

        public VideoPlayerViewModel(string path, TimeSpan currentTime)
        {
            VideoPath = path;
            CurrentTime = currentTime;
            IsPlaying = false;
            _isFullScreen = false;
        }
    }
}
