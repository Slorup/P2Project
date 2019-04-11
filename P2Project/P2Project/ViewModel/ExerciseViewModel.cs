using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class ExerciseViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }

        private Visibility _panelVisibility;

        public Visibility PanelVisibility
        {
            get { return _panelVisibility; }
            set { SetProperty(ref _panelVisibility, value); }
        }
        private string _exercisedescription;

        public string Exercisedescription
        {
            get { return _exercisedescription; }
            set { SetProperty(ref _exercisedescription, value); }
        }



        public ExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
        }

        private ICommand _panelShowCommand;

        public ICommand PanelShowCommand
        {
            get
            {
                return _panelShowCommand ?? (_panelShowCommand = new RelayCommand(param => PanelShowClick(param)));
            }
        }

        private void PanelShowClick(object param)
        {
            if (PanelVisibility == Visibility.Collapsed)
                PanelVisibility = Visibility.Visible;
            else
                PanelVisibility = Visibility.Collapsed;
        }
    }
}
