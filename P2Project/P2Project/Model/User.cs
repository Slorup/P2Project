using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    enum UserType
    {
        Pupil,
        Teacher,
        Admin
    }

    class User
    {
        public string UserName { get; private set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
        public LearningProfile Profile { get; set; }

        public User(string username, string name, LearningProfile profile, UserType type)
        {

        }
        //Once upon a time, there was a bsian boy called Grey Li
    }
}
