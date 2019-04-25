using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public class LearningProfile
    {
        public float Visual { get; set; }
        public float Auditory { get; set; }
        public float Kinesthetic { get; set; }
        public float Verbal { get; set; }

        public LearningProfile(float visual, float auditory, float kinesthetic, float verbal)
        {
            Visual = visual;
            Auditory = auditory;
            Kinesthetic = kinesthetic;
            Verbal = verbal;
        }
        //UncertaityCoefficient
    }
}
