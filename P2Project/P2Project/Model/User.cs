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

        public bool GiveNewExercise() //TODO - Opdeles i flere metoder
        {
            bool newExerciseFound = false;
            List<Exercise> exerciseList = GetExercisesNotCompleted();

            var chancelist = exerciseList.GroupBy(p => p).Select(p => new { ExerciseInfo = p.Key, Liking = CalcChanceLikeExercise(p.Key) });
            if (chancelist.Count() != 0)
            {
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
                        CurrentExercise = exerciseList.First(p => p.ID == exercise.ExerciseInfo.ID);
                        newExerciseFound = true;
                        break;
                    }
                }
            }
            else
                CurrentExercise = null;
            return newExerciseFound;
            //Get all exercises fom DB (maybe where completed ids isnt chosen)
            //(Remove completed ones)
            //Calc chance of liking them
            //Get "random" exercise from chances
        }

        private List<Exercise> GetExercisesNotCompleted()
        {
            List<Exercise> exerciselist = DBConnection.GetAllExercises();
            return exerciselist.Where(p => !CompletedExercisesID.Contains(p.ID)).ToList();
        }

        //Stardardafvigelse: sqrt(1/2 * Sum((E_x - S_x)^2))
        private int CalcChanceLikeExercise(Exercise exercise)
        {
            double profileDifferenceSum = CalcProfileDifferenceSum(exercise);
            return (int)((1 - Math.Sqrt(profileDifferenceSum / 2)) * 100); 
        }

        private double CalcProfileDifferenceSum(Exercise exercise)
        {
            double sum = Profile.CalcProfileSum();
            return Math.Pow(exercise.Profile.TextVisual - Profile.TextVisual / sum, 2) +
                Math.Pow(exercise.Profile.ImageVisual - Profile.ImageVisual / sum, 2) +
                Math.Pow(exercise.Profile.Verbal - Profile.Verbal / sum, 2) +
                Math.Pow(exercise.Profile.Kinesthetic - Profile.Kinesthetic / sum, 2) +
                Math.Pow(exercise.Profile.Auditory - Profile.Auditory / sum, 2) +
                Math.Pow(exercise.Profile.Tactile - Profile.Tactile / sum, 2);
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

        private void UpdateProfileValues(Feedback feedback) //TODO
        {
            double sum = Profile.CalcProfileSum();
            if (feedback == Feedback.Good)
            {
                Profile.TextVisual += Math.Abs((Profile.TextVisual / sum) - CurrentExercise.Profile.TextVisual) * 5;
                Profile.ImageVisual += Math.Abs((Profile.ImageVisual / sum) - CurrentExercise.Profile.ImageVisual) * 5;
                Profile.Auditory += Math.Abs((Profile.Auditory / sum) - CurrentExercise.Profile.Auditory) * 5;
                Profile.Tactile += Math.Abs((Profile.Tactile / sum) - CurrentExercise.Profile.Tactile) * 5;
                Profile.Kinesthetic += Math.Abs((Profile.Kinesthetic / sum) - CurrentExercise.Profile.Kinesthetic) * 5;
                Profile.Verbal += Math.Abs((Profile.Verbal / sum) - CurrentExercise.Profile.Verbal) * 5;
            }
            if (feedback == Feedback.Bad)
            {
                Profile.TextVisual -= Math.Abs((Profile.TextVisual / sum) - CurrentExercise.Profile.TextVisual) * 5;
                Profile.ImageVisual -= Math.Abs((Profile.ImageVisual / sum) - CurrentExercise.Profile.ImageVisual) * 5;
                Profile.Auditory -= Math.Abs((Profile.Auditory / sum) - CurrentExercise.Profile.Auditory) * 5;
                Profile.Tactile -= Math.Abs((Profile.Tactile / sum) - CurrentExercise.Profile.Tactile) * 5;
                Profile.Kinesthetic -= Math.Abs((Profile.Kinesthetic / sum) - CurrentExercise.Profile.Kinesthetic) * 5;
                Profile.Verbal -= Math.Abs((Profile.Verbal / sum) - CurrentExercise.Profile.Verbal) * 5;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
