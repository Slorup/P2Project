using P2Project.DAL;
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
    class CreateUserViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public int SelectedUserTypeIndex { get; set; }
        public Survey Surveys { get; set; }
        public int SurveyWeight{ get; set; }

        public CreateUserViewModel()
        {
            Surveys = new Survey(6);
        }

        private ICommand _createUserCommand;

        public ICommand CreateUserCommand
        {
            get
            {
                return _createUserCommand ?? (_createUserCommand = new RelayCommand(param => CreateUserClick(param), param => CanCreateUserClick(param)));
            }
        }

        private void CreateUserClick(object param)
        {
            if (!DBConnection.UserExist(UserName)) //TRY CATCH
            {
                DBConnection.CreateUser(new User(UserName, StartLearningProfile(Surveys), (UserType)SelectedUserTypeIndex, new List<int>()));
                Navigator.MainNavigationService.GoBack();
            }
            else
                MessageBox.Show("Brugernavnet er optaget!");
        }

        private bool CanCreateUserClick(object param)
        {
            return UserName != "" && UserName != null;
        } 

        public List<double> StartLearningProfile(Survey Surveys)
        {
            List<double> lp = new List<double>();
            double sliderSum = 0;
            for (int i = 0; i < 6; i++)
                sliderSum += Surveys.QuestionList[i].SliderValue;

            for (int i = 0; i < 6; i++)
                lp.Add(Surveys.QuestionList[i].SliderValue / sliderSum);
            return lp;
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new RelayCommand(param => GoBackClick(param)));
            }
        }

        private void GoBackClick(object param)
        {
            Navigator.MainNavigationService.GoBack();
        }
    }
}
