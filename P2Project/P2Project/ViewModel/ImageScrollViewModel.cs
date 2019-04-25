using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class ImageScrollViewModel : BaseViewModel
    {
        private string _currentImagePath;

        public string CurrentImagePath
        {
            get { return _currentImagePath; }
            set { SetProperty(ref _currentImagePath, value); }
        }

        private List<string> _imagePaths;

        public List<string> ImagePaths
        {
            get { return _imagePaths; }
            set { SetProperty(ref _imagePaths, value); }
        }

        private int _imageindex;
        private bool _isFullScreen;

        public ImageScrollViewModel(List<string> imagepaths)
        {
            _imageindex = 0;
            if (imagepaths != null && imagepaths.Count > 0)
            {
                ImagePaths = imagepaths;
                CurrentImagePath = ImagePaths[_imageindex];
            }
        }

        private ICommand _goLeftCommand;

        public ICommand GoLeftCommand
        {
            get
            {
                return _goLeftCommand ?? (_goLeftCommand = new RelayCommand(param => GoLeftClick(param), param => CanGoLeftClick(param)));
            }
        }

        private bool CanGoLeftClick(object param)
        {
            if (ImagePaths != null && ImagePaths.Count > 0)
                if (_imageindex > 0)
                    return true;
            return false;
        }

        private void GoLeftClick(object param)
        {
            _imageindex--;
            CurrentImagePath = ImagePaths[_imageindex];
        }

        private ICommand _goRightCommand;

        public ICommand GoRightCommand
        {
            get
            {
                return _goRightCommand ?? (_goRightCommand = new RelayCommand(param => GoRightClick(param), param => CanGoRightClick(param)));
            }
        }

        private bool CanGoRightClick(object param)
        {
            if (ImagePaths != null && ImagePaths.Count > 0)
                if (_imageindex < ImagePaths.Count - 1)
                    return true;
            return false;
        }

        private void GoRightClick(object param)
        {
            _imageindex++;
            CurrentImagePath = ImagePaths[_imageindex];
        }

        private ICommand _imageClickCommand;
        public ICommand ImageClickCommand
        {
            get
            {
                return _imageClickCommand ?? (_imageClickCommand = new RelayCommand(param => ImageClick(param)));
            }
        }

        private void ImageClick(object param)
        {
            if (_isFullScreen)
            {
                _isFullScreen = false;
                Navigator.MainNavigationService.GoBack();
            }
            else
            {
                _isFullScreen = true;
                ImageScrollPage page = new ImageScrollPage() { DataContext = this };
                Navigator.MainNavigationService.Navigate(page);
            }
        }
    }
}
