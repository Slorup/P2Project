using Microsoft.VisualStudio.TestTools.UnitTesting;
using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.Comparer;

namespace UnitTestProject
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CheckProfileBounds_NormalProfile_DontChange()
        {
            //Arrange
            List<double> profile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", profile, UserType.Teacher, new List<int>());
            List<double> expected = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };

            //Act
            user.CheckProfileBounds();
            List<double> result = user.Profile;

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CheckProfileBounds_NegativeValue_CorrectBounds()
        {
            //Arrange
            List<double> profile = new List<double>() { 0.3, 0.3, -0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", profile, UserType.Teacher, new List<int>());
            List<double> expected = new List<double>() { 0.28, 0.28, 0, 0.08, 0.18, 0.18 };

            //Act
            user.CheckProfileBounds();
            List<double> result = user.Profile;

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CheckProfileBounds_MultipleNegativeValue_CorrectBounds()
        {
            //Arrange
            List<double> profile = new List<double>() { 0.3, 0.4, -0.1, -0.1, 0.3, 0.2 };
            User user = new User("Test", profile, UserType.Teacher, new List<int>());
            List<double> expected = new List<double>() { 0.25, 0.35, 0, 0, 0.25, 0.15 };

            //Act
            user.CheckProfileBounds();
            List<double> result = user.Profile;

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CheckProfileBounds_NegativeValueWithZero_CorrectBounds()
        {
            //Arrange
            List<double> profile = new List<double>() { 0.5, 0, -0.2, 0.1, 0.3, 0.3 };
            User user = new User("Test", profile, UserType.Teacher, new List<int>());
            List<double> expected = new List<double>() { 0.45, 0, 0, 0.05, 0.25, 0.25 };

            //Act
            user.CheckProfileBounds();
            List<double> result = user.Profile;

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }

        [TestMethod]
        public void CheckProfileBounds_NegativeValueAndAboveUpperBound_CorrectBounds()
        {
            //Arrange
            List<double> profile = new List<double>() { 1.1, 0, -0.05, -0.05, 0, 0 };
            User user = new User("Test", profile, UserType.Teacher, new List<int>());
            List<double> expected = new List<double>() { 1, 0, 0, 0, 0, 0 };

            //Act
            user.CheckProfileBounds();
            List<double> result = user.Profile;

            //Assert
            CollectionAssert.AreEqual(expected, result, new ListToleranceComparer(0.0001));
        }
        
        [TestMethod]
        public void CalcProfileDifferenceSum_NormalValues_CorrectValue()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 0.3, 0.1, 0, 0.2, 0.2, 0.2 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            double expected = 0.04;

            //Act
            double result = user.CalcProfileDifferencePowSum(exercise);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcProfileDifferenceSum_NoDifference_CorrectValue()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            double expected = 0;

            //Act
            double result = user.CalcProfileDifferencePowSum(exercise);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcProfileDifferenceSum_MaxDifference_CorrectValue()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0, 0, 0, 0, 0, 1 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 1, 0, 0, 0, 0, 0 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            double expected = 2;

            //Act
            double result = user.CalcProfileDifferencePowSum(exercise);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcChanceLikeExercise_NormalValues_CorrectValue()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 0.1, 0.1, 0.2, 0.1, 0.2, 0.3 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            int expected = 86;

            //Act
            int result = user.CalcChanceLikeExercise(exercise);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcChanceLikeExercise_MaxLiking_CorrectValue()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 1, 0, 0, 0, 0, 0 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 1, 0, 0, 0, 0, 0 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            int expected = 100;

            //Act
            int result = user.CalcChanceLikeExercise(exercise);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalcChanceLikeExercise_MinLiking_CorrectValue()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0, 1, 0, 0, 0, 0 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 1, 0, 0, 0, 0, 0 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            int expected = 0;

            //Act
            int result = user.CalcChanceLikeExercise(exercise);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CalcChanceLikeExercise_NullExercise_NullReferenceException()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0, 1, 0, 0, 0, 0 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());

            //Act
            int result = user.CalcChanceLikeExercise(null);

            //Assert - Expects exception
        }

        [TestMethod]
        public void SelectRandomExerciseFromLiking_NormalValues_NewExercise()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<double> exerciseProfile = new List<double>() { 0.1, 0.1, 0.2, 0.1, 0.2, 0.3 };
            Exercise exercise = new Exercise("Name", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            Exercise exercise2 = new Exercise("Name2", new ExerciseDescription("Description"), exerciseProfile, "Creator", DateTime.Now, "uri");
            List<Exercise> exerciseList = new List<Exercise>();
            exerciseList.Add(exercise);
            exerciseList.Add(exercise2);

            //Act
            user.SelectRandomExerciseFromLiking(exerciseList);
            Exercise result = user.CurrentExercise;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SelectRandomExerciseFromLiking_EmptyList_CurrentExerciseNull()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<Exercise> exerciseList = new List<Exercise>();

            //Act
            user.SelectRandomExerciseFromLiking(exerciseList);
            Exercise result = user.CurrentExercise;

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SelectRandomExerciseFromLiking_NullList_ArgumentNullException()
        {
            //Arrange
            List<double> userProfile = new List<double>() { 0.2, 0.2, 0.1, 0.1, 0.2, 0.2 };
            User user = new User("Test", userProfile, UserType.Teacher, new List<int>());
            List<Exercise> exerciseList = null;

            //Act
            user.SelectRandomExerciseFromLiking(exerciseList);

            //Assert - Expects exception
        }
    }
}
