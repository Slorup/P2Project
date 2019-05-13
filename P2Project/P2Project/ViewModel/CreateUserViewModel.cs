﻿using P2Project.DAL;
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

        public UserLearningProfile StartLearningProfile(Survey Surveys)
        {
            UserLearningProfile Lp = new UserLearningProfile(Surveys.QuestionList[0].SliderValue, Surveys.QuestionList[1].SliderValue,
                Surveys.QuestionList[2].SliderValue, Surveys.QuestionList[3].SliderValue,
                Surveys.QuestionList[4].SliderValue, Surveys.QuestionList[5].SliderValue);
            return Lp;
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
