﻿using P2Project.DAL;
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
        public List<double> Profile { get; set; }
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

        public User(string username, List<double> profile, UserType type, List<int> completedExercisesID)
        {
            UserName = username;
            Profile = profile;
            Type = type;
            CompletedExercisesID = completedExercisesID;
            GiveNewExercise();
        }

        public bool GiveNewExercise()
        {
            List<Exercise> exerciseList = GetExercisesNotCompleted(); //Get all non-completed exercises from DB
            bool newExerciseFound = SelectRandomExerciseFromLiking(exerciseList);
            return newExerciseFound;
        }

        //Calc chance of user liking the exercises
        //Get random exercise from chances
        public bool SelectRandomExerciseFromLiking(List<Exercise> exerciseList)
        {
            bool newExerciseFound = false;
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
        }

        //Get exercises from DB, then remove completed ones
        private List<Exercise> GetExercisesNotCompleted()
        {
            List<Exercise> exerciselist = DBConnection.GetAllExercises();
            return exerciselist.Where(p => !CompletedExercisesID.Contains(p.ID)).ToList();
        }

        //Udregner "liking" for en opgave.
        //Stardardafvigelse: sqrt(1/2 * Sum((E_x - S_x)^2))
        public int CalcChanceLikeExercise(Exercise exercise)
        {
            double profileDifferenceSum = CalcProfileDifferenceSum(exercise);
            return (int)((1 - Math.Sqrt(profileDifferenceSum / 2)) * 100); 
        }

        public double CalcProfileDifferenceSum(Exercise exercise)
        {
            double sum = 0;
            for (int i = 0; i < Profile.Count; i++)
                sum += Math.Pow(exercise.Profile[i] - Profile[i], 2);
            return sum;
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

        public void UpdateProfileValues(Feedback feedback)
        {
            if (feedback == Feedback.Good)
                for (int i = 0; i < Profile.Count; i++)
                    Profile[i] += (CurrentExercise.Profile[i] - Profile[i]) / 20;
            if (feedback == Feedback.Bad)
                for (int i = 0; i < Profile.Count; i++)
                    Profile[i] -= (CurrentExercise.Profile[i] - Profile[i]) / 20;
            CheckProfileBounds();
        }

        public void CheckProfileBounds() //TODO
        {
            int negativeValuesCount = Profile.Count(c => c < 0);
            while(negativeValuesCount > 0)
            {
                double negativeSum = Profile.Where(c => c < 0).Sum();
                int positiveValuesCount = Profile.Count(c => c > 0);
                Profile.Where(c => c < 0).Select(c => c = 0);
                Profile.Where(c => c > 0).Select(c => c -= negativeSum / positiveValuesCount);
                negativeValuesCount = Profile.Count(c => c < 0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
