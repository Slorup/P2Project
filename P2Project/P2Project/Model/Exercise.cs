using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    class Exercise
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public LearningProfile Profile { get; set; }
    }
}
