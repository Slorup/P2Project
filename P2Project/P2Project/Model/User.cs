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
        public string ProfileImage { get; set; }

        public User(string username, LearningProfile profile, UserType type, List<int> completedExercisesID)
        {
            UserName = username;
            Profile = profile;
            Type = type;
            CompletedExercisesID = completedExercisesID;
        }

        //Once upon a time, there was a bsian boy called Grey Li
    }
}
