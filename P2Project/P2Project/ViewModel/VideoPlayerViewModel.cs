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
using System.Windows.Threading;

namespace P2Project.ViewModel
{
    class VideoPlayerViewModel : BaseViewModel
    {
        public bool IsPlaying { get; set; }
        public TimeSpan CurrentTime { get; set; }
        private string _videoPath;
        private bool _isFullScreen; //Private set property senere
        private TimeSpan _totaltime;

        private TimeSpan _position;

        public TimeSpan Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }


        private Duration _naturalDuration;

        public Duration NaturalDuration
        {
            get { return _naturalDuration; }
            set { SetProperty(ref _naturalDuration, value); }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        private double _sliderValue;

        public double SliderValue
        {
            get { return _sliderValue; }
            set { SetProperty(ref _sliderValue, value); }
        }

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

            DispatcherTimer timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_tick);
            timerVideoTime.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if (NaturalDuration.TimeSpan.TotalSeconds > 0 && _totaltime.TotalSeconds > 0)
                SliderValue = Position.TotalSeconds / _totaltime.TotalSeconds;
        }

        public void Slider_MouseLeftButtonUp(Slider slider)
        {
            if (_totaltime.TotalSeconds > 0)
                Position = TimeSpan.FromSeconds(SliderValue * _totaltime.TotalSeconds);
        }
    }
}
