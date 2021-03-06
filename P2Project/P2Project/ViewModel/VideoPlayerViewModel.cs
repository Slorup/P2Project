﻿using P2Project.Model;
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
        private TimeSpan _totalTime;
        private MediaElement _player;

        public bool IsFullScreen { get; set; }

        public bool IsHolding { get; set; }

        public bool IsPlaying { get; set; }
        
        private double _sliderValue;

        public double SliderValue
        {
            get { return _sliderValue; }
            set { SetProperty(ref _sliderValue, value); }
        }

        private string _videoPath;

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

        private Visibility _PauseVisibility;

        public Visibility PauseVisibility
        {
            get { return _PauseVisibility; }
            set { SetProperty(ref _PauseVisibility, value); }
        }

        private Visibility _playVisibility;

        public Visibility PlayVisibility
        {
            get { return _playVisibility; }
            set { SetProperty(ref _playVisibility, value); }
        }

        private Visibility _mediaVisibility;

        public Visibility MediaVisibility
        {
            get { return _mediaVisibility; }
            set { SetProperty(ref _mediaVisibility, value); }
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
                PlayVisibility = Visibility.Visible;
                PauseVisibility = Visibility.Collapsed;
            }
            else
            {
                ((MediaElement)param).Play();
                IsPlaying = true;
                PlayVisibility = Visibility.Collapsed;
                PauseVisibility = Visibility.Visible;
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
            IsPlaying = true;
            PlayVisibility = Visibility.Collapsed;
            PauseVisibility = Visibility.Visible;
        }

        private void FullScreenClick(object param)
        {
            if (IsFullScreen)
            {
                IsFullScreen = false;
                Navigator.MainNavigationService.GoBack();
            }
            else
            {
                IsFullScreen = true;
                VideoPlayerPage page = new VideoPlayerPage() { DataContext = this };
                Navigator.MainNavigationService.Navigate(page);
            }
        }

        public VideoPlayerViewModel(string path, bool isAudioOnly)
        {
            VideoPath = path;
            PauseVisibility = Visibility.Collapsed;
            if (isAudioOnly)
                MediaVisibility = Visibility.Collapsed;
            else
                MediaVisibility = Visibility.Visible;
        }
        
        //Sets timespan for player and starts a timer for the slider
        public void Player_MediaOpened(MediaElement player)
        {
            _player = player;
            _totalTime = player.NaturalDuration.TimeSpan;

            _player.Play();
            IsPlaying = true;
            PlayVisibility = Visibility.Collapsed;
            PauseVisibility = Visibility.Visible;
            if (_totalTime.TotalSeconds > 0)
                _player.Position = TimeSpan.FromSeconds((SliderValue / 100) * _totalTime.TotalSeconds);

            DispatcherTimer timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_tick);
            timerVideoTime.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if(!IsHolding)
                if (_player.NaturalDuration.HasTimeSpan &&_player.NaturalDuration.TimeSpan.TotalSeconds > 0)
                    if (_totalTime.TotalSeconds > 0)
                        SliderValue = 100 * _player.Position.TotalSeconds / _totalTime.TotalSeconds;
        }

        public void Slider_MouseLeftButtonUp(Slider slider)
        {
            IsHolding = false;
            if (_totalTime.TotalSeconds > 0)
                _player.Position = TimeSpan.FromSeconds((SliderValue / 100) * _totalTime.TotalSeconds);
        }

        public void Slider_MouseLeftButtonDown()
        {
            IsHolding = true;
        }
    }
}
