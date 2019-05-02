using P2Project.DAL;
using P2Project.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public enum UserType //IT PROB SHOULDNT BE HERE
    {
        Pupil,
        Teacher,
        Admin
    }

    public class User : INotifyPropertyChanged
    {
        public string UserName { get; private set; }
        public UserType Type { get; set; }
        public UserLearningProfile Profile { get; set; }
        public List<int> CompletedExercisesID { get; set; }
        private Exercise _currentExercise;

        public Exercise CurrentExercise
        {
            get { return _currentExercise; }
            set
            {   _currentExercise = value;
                OnPropertyChanged();
            }
        }


        public User(string username, UserLearningProfile profile, UserType type, List<int> completedExercisesID)
        {
            UserName = username;
            Profile = profile;
            Type = type;
            CompletedExercisesID = completedExercisesID;
            GiveNewExercise();
        }

        public void GiveNewExercise()
        {
            List<Exercise> exerciselist = DBConnection.GetAllExercises();
            exerciselist = exerciselist.Where(p => !CompletedExercisesID.Contains(p.ID)).ToList();

            var chancelist = exerciselist.GroupBy(p => p).Select(p => new { ExerciseInfo = p.Key, Liking = CalcChanceLikeExercise(p.Key) });
            int likingsum = 0;
            foreach (var exercise in chancelist)
                likingsum += exercise.Liking;
            Random rnd = new Random();
            int number = rnd.Next(1, likingsum + 1);
            foreach (var exercise in chancelist)
            {
                number -= exercise.Liking;
                if (number <= 0)
                {
                    CurrentExercise = exerciselist.First(p => p.ID == exercise.ExerciseInfo.ID);
                    break;
                }
            }
            //Get all exercises fom DB (maybe where completed ids isnt chosen)
            //(Remove completed ones)
            //Calc chance of liking them
            //Get "random" exercise from chances
        }

        private int CalcChanceLikeExercise(Exercise exercise)
        {
            double sum = Profile.CalcProfileSum();
            double profileDifferenceSum = CalcProfileDifferenceSum(exercise, sum);
            return (int)((1 - (profileDifferenceSum / 6)) * 100); 
        }

        

        private double CalcProfileDifferenceSum(Exercise exercise, double sum)
        {
            return Math.Abs(exercise.Profile.TextVisual - Profile.TextVisual / sum) +
                Math.Abs(exercise.Profile.ImageVisual - Profile.ImageVisual / sum) +
                Math.Abs(exercise.Profile.Verbal - Profile.Verbal / sum) +
                Math.Abs(exercise.Profile.Kinesthetic - Profile.Kinesthetic / sum) +
                Math.Abs(exercise.Profile.Auditory - Profile.Auditory / sum) +
                Math.Abs(exercise.Profile.Tactile - Profile.Tactile / sum);
        }

        public void ExerciseCompleted(Feedback feedback = Feedback.Medium)
        {
            if(CurrentExercise != null)
            {
                DBConnection.InsertCompletedExercise(UserName, CurrentExercise.ID);
                UpdateProfileValues(feedback);
                CompletedExercisesID.Add(CurrentExercise.ID);
                GiveNewExercise();
            }
        }

        private void UpdateProfileValues(Feedback feedback)
        {
            double sum = Profile.CalcProfileSum();
            if (feedback == Feedback.Good)
            {
                Profile.Auditory += Math.Abs(Profile.Auditory / sum - CurrentExercise.Profile.Auditory) * 5; //Change 5 to uncertaincy
            }
            if (feedback == Feedback.Bad)
            {
                Profile.Auditory -= Math.Abs(Profile.Auditory / sum - CurrentExercise.Profile.Auditory) * 5; //Change 5 to uncertaincy
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
