using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public class LearningProfile
    {
        public decimal Visual { get; set; }
        public decimal Auditory { get; set; }
        public decimal Kinesthetic { get; set; }
        public decimal Verbal { get; set; }

        public LearningProfile(decimal visual, decimal auditory, decimal kinesthetic, decimal verbal)
        {
            Visual = visual;
            Auditory = auditory;
            Kinesthetic = kinesthetic;
            Verbal = verbal;
        }
        //UncertaityCoefficient
    }
}
