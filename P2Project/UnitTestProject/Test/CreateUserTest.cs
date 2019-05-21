using Microsoft.VisualStudio.TestTools.UnitTesting;
using P2Project.Model;
using P2Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.Comparer;

namespace UnitTestProject
{
    [TestClass]
    public class CreateUserTest
    {
        [TestMethod]
        public void FillSurvey_CorrectValues()
        {
            //Arrange
            CreateUserViewModel vm = new CreateUserViewModel();
            List<SurveyQuestion> expected = new List<SurveyQuestion>();
            expected.Add(new SurveyQuestion("1. Jeg lærer godt ved hjælp af tekster"));
            expected.Add(new SurveyQuestion("2. Jeg lærer godt ved hjælp af billeder"));
            expected.Add(new SurveyQuestion("3. Jeg lærer godt ved hjælp af at lytte"));
            expected.Add(new SurveyQuestion("4. Jeg lærer godt ved hjælp af at tale"));
            expected.Add(new SurveyQuestion("5. Jeg lærer godt ved hjælp af at bruge mine hænder"));
            expected.Add(new SurveyQuestion("6. Jeg lærer godt ved hjælp af at være fysisk aktiv"));

            //Act
            List<SurveyQuestion> result = vm.Survey.QuestionList;

            //Assert
            CollectionAssert.AreEqual(expected, result, new SurveyQuestionComparer());
        }

        [TestMethod]
        public void CalcLearningProfile_NormalValues_CorrectValues()
        {
            //Arrange
            CreateUserViewModel vm = new CreateUserViewModel();
            vm.Survey.QuestionList[0].SliderValue = 5;
            vm.Survey.QuestionList[1].SliderValue = 1;
            vm.Survey.QuestionList[2].SliderValue = 3;
            vm.Survey.QuestionList[3].SliderValue = 2;
            vm.Survey.QuestionList[4].SliderValue = 4;
            vm.Survey.QuestionList[5].SliderValue = 1;
            List<double> expected = new List<double>() { 5.0/16, 1.0/16, 3.0/16, 2.0/16, 4.0/16, 1.0/16 };

            //Act
            List<double> result = vm.CalcLearningProfile();

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CalcLearningProfile_SameValues_CorrectValues()
        {
            //Arrange
            CreateUserViewModel vm = new CreateUserViewModel();
            List<double> expected = new List<double>() { 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6 };

            //Act
            List<double> result = vm.CalcLearningProfile();

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CalcLearningProfile_PrefAmountNotEqualSurveyQuestionCount_DefaultValues()
        {
            //Arrange
            CreateUserViewModel vm = new CreateUserViewModel();
            vm.Survey.QuestionList.Add(new SurveyQuestion("Question", 5));
            List<double> expected = new List<double>() { 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6 };

            //Act
            List<double> result = vm.CalcLearningProfile();

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CalcLearningProfile_EmptyList_DefaultValues()
        {
            //Arrange
            CreateUserViewModel vm = new CreateUserViewModel();
            vm.Survey.QuestionList.Clear();
            List<double> expected = new List<double>() { 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6 };

            //Act
            List<double> result = vm.CalcLearningProfile();

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }
    }
}
