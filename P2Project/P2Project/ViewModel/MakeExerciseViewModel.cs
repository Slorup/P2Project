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


        public MakeExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
        }
        private ICommand _browseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ?? (_browseCommand = new RelayCommand(param => BrowseCommandClick(param)));
            }
        }

        private void BrowseCommandClick(object param)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt"; // Required file extension 
            fileDialog.Filter = "Text documents (.txt)|*.txt"; // Optional file extensions

            fileDialog.ShowDialog();
            TextBlock1 = fileDialog.FileName;
            
        }
    }
}
