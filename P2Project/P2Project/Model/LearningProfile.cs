using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    class LearningProfile
    {
        public double Visual { get; set; }
        public double Auditory { get; set; }
        public double Kinesthetic { get; set; }
        public double Verbal { get; set; }

        public LearningProfile(double visual, double auditory, double kinesthetic, double verbal)
        {
            Visual = visual;
            Auditory = auditory;
            Kinesthetic = kinesthetic;
            Verbal = verbal;
        }
        //UncertaityCoefficient
    }
}
