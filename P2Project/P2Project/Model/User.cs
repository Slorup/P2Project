using P2Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class User
    {
        public string UserName { get; private set; }
        public UserType Type { get; set; }
        public LearningProfile Profile { get; set; }
        public List<int> CompletedExercisesID { get; set; }
        public Exercise CurrentExercise { get; set; }

        public User(string username, LearningProfile profile, UserType type, List<int> completedExercisesID)
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

            var chancelist = exerciselist.GroupBy(p => p).Select(p => new { ID = p.Key, Liking = CalcChanceLikeExercise(p.Key) });
            foreach (var exercise in chancelist)
            {

            }
            //Get all exercises fom DB (maybe where completed ids isnt chosen)
            //(Remove completed ones)
            //Calc chance of liking them
            //Get "random" exercise from chances

            CurrentExercise = DBConnection.GetExerciseByID(4); //todo
        }

        private int CalcChanceLikeExercise(Exercise exercise)
        {
            double sum = Profile.Auditory + Profile.Kinesthetic + Profile.Verbal + Profile.Visual;
            double total = GetAbsDifference(exercise.Profile.Visual, Profile.Visual / sum) + GetAbsDifference(exercise.Profile.Verbal, Profile.Verbal / sum) + GetAbsDifference(exercise.Profile.Kinesthetic, Profile.Kinesthetic / sum) + GetAbsDifference(exercise.Profile.Auditory, Profile.Auditory / sum);
            return (int)((1 - (total / 4)) * 100); 
        }

        private double GetAbsDifference(double a, double b)
        {
            return Math.Abs(a - b);
        }

        public void ExerciseCompleted()
        {
            DBConnection.InsertCompletedExercise(UserName, CurrentExercise.ID);
            CompletedExercisesID.Add(CurrentExercise.ID);
            GiveNewExercise();
        }
    }
}
