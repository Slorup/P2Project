using P2Project.DAL;
using P2Project.Model;
using P2Project.MVVM;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    public class CreateUserViewModel : BaseViewModel, IDataErrorInfo
    {
        public int SelectedUserTypeIndex { get; set; }
        public Survey Survey { get; set; }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private bool _isValidating;

        public bool IsValidating
        {
            get { return _isValidating; }
            set
            {
                SetProperty(ref _isValidating, value);
                OnPropertyChanged("UserName");
            }
        }

        public CreateUserViewModel()
        {
            UserName = string.Empty;
            this.Survey = new Survey();
            FillSurvey();
        }

        public void FillSurvey()
        {
            this.Survey.QuestionList.Add(new SurveyQuestion("1. Jeg lærer godt ved hjælp af tekster"));
            this.Survey.QuestionList.Add(new SurveyQuestion("2. Jeg lærer godt ved hjælp af billeder"));
            this.Survey.QuestionList.Add(new SurveyQuestion("3. Jeg lærer godt ved hjælp af at lytte"));
            this.Survey.QuestionList.Add(new SurveyQuestion("4. Jeg lærer godt ved hjælp af at tale"));
            this.Survey.QuestionList.Add(new SurveyQuestion("5. Jeg lærer godt ved hjælp af at bruge mine hænder"));
            this.Survey.QuestionList.Add(new SurveyQuestion("6. Jeg lærer godt ved hjælp af at være fysisk aktiv"));
        }

        private ICommand _createUserCommand;

        public ICommand CreateUserCommand
        {
            get
            {
                return _createUserCommand ?? (_createUserCommand = new RelayCommand(param => CreateUserClick(param)));
            }
        }

        private void CreateUserClick(object param)
        {
            IsValidating = true;
            string error = string.Empty;
            if ((error = ValidateUserName()) == null)
            {
                try
                {
                    if (!DBConnection.UserExist(UserName))
                    {
                        DBConnection.CreateUser(new User(UserName, CalcLearningProfile(), (UserType)SelectedUserTypeIndex, new List<int>()));
                        Navigator.MainNavigationService.GoBack();
                    }
                    else
                        MessageBox.Show("Brugernavnet er optaget!");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Kunne ikke oprette forbindelse til databasen!");
                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        private string ValidateUserName()
        {
            if (this.UserName.Length < 2)
                return "Brugernavn skal mindst indeholde 2 tegn!";
            if (this.UserName.Length > 20)
                return "Brugernavn må højst indeholde 20 tegn!";
            if (!this.UserName.All(c => Char.IsLetterOrDigit(c) || c.Equals(' ')))
                return "Brugernavn må kun indeholde bogstaver, tal og mellemrum!";
            return null;
        }

        public List<double> CalcLearningProfile()
        {
            List<double> lp = new List<double>();
            int prefAmount = 6;

            for (int i = 0; i < 6; i++)
                lp.Add(1.0 / 6);

            if (Survey.QuestionList.Count >= prefAmount)
            {
                double sliderSum = 0;
                for (int i = 0; i < prefAmount; i++)
                    sliderSum += Survey.QuestionList[i].SliderValue;

                for (int i = 0; i < prefAmount; i++)
                    lp[i] = Survey.QuestionList[i].SliderValue / sliderSum;
            }
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

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (!IsValidating) return null;

                return ValidateUserName();
            }
        }

        private void GoBackClick(object param)
        {
            Navigator.MainNavigationService.GoBack();
        }
    }
}
