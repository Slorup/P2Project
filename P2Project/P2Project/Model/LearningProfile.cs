using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public abstract class LearningProfile
    {
        public virtual double TextVisual { get; set; }
        public virtual double ImageVisual { get; set; }
        public virtual double Auditory { get; set; }
        public virtual double Tactile { get; set; }
        public virtual double Kinesthetic { get; set; }
        public virtual double Verbal { get; set; }

        public double CalcProfileSum()
        {
            return Auditory + Kinesthetic + Verbal + TextVisual + ImageVisual + ImageVisual;
        }
    }
}
