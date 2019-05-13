using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public class UserLearningProfile : LearningProfile
    {
        public UserLearningProfile(double textvisual, double imagevisual, double verbal, double auditory, double tactile, double kinesthetic)
        {
            TextVisual = textvisual;
            ImageVisual = imagevisual;
            Auditory = auditory;
            Tactile = tactile;
            Kinesthetic = kinesthetic;
            Verbal = verbal;
        }

        public override double TextVisual
        {
            get { return base.TextVisual; }
            set
            {
                if (value < 1)
                    base.TextVisual = 1;
                else if (value > double.MaxValue / 6)
                    base.TextVisual = value / 6;
                else
                    base.TextVisual = value;
            }
        }

        public override double ImageVisual
        {
            get { return base.ImageVisual; }
            set
            {
                if (value < 1)
                    base.ImageVisual = 1;
                else if (value > double.MaxValue / 6)
                    base.ImageVisual = value / 6;
                else
                    base.ImageVisual = value;
            }
        }

        public override double Auditory
        {
            get { return base.Auditory; }
            set
            {
                if (value < 1)
                    base.Auditory = 1;
                else if (value > double.MaxValue / 6)
                    base.Auditory = value / 6;
                else
                    base.Auditory = value;
            }
        }

        public override double Tactile
        {
            get { return base.Tactile; }
            set
            {
                if (value < 1)
                    base.Tactile = 1;
                else if (value > double.MaxValue / 6)
                    base.Tactile = value / 6;
                else
                    base.Tactile = value;
            }
        }

        public override double Kinesthetic
        {
            get { return base.Kinesthetic; }
            set
            {
                if (value < 1)
                    base.Kinesthetic = 1;
                else if (value > double.MaxValue / 6)
                    base.Kinesthetic = value / 6;
                else
                    base.Kinesthetic = value;
            }
        }

        public override double Verbal
        {
            get { return base.Verbal; }
            set
            {
                if (value < 1)
                    base.Verbal = 1;
                else if (value > double.MaxValue / 6)
                    base.Verbal = value / 6;
                else
                    base.Verbal = value;
            }
        }
    }
}
