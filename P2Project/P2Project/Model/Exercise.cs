using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public class Exercise
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ExerciseLearningProfile Profile { get; set; }
        public ExerciseDescription Description { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }

        public Exercise(string name, ExerciseDescription description, ExerciseLearningProfile profile, string creator, DateTime creationDate)
        {
            Name = name;
            Profile = profile;
            Description = description;
            Creator = creator;
            CreationDate = creationDate;
        }
    }
}
