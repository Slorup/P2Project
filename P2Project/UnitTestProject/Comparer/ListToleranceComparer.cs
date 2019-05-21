using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Comparer
{
    class ListToleranceComparer : Comparer<double>
    {
        private double _epsilon;

        public ListToleranceComparer(double epsilon)
        {
            _epsilon = epsilon;
        }

        public override int Compare(double x, double y)
        {
            double delta = Math.Abs(x - y);
            if (delta < _epsilon)
                return 0;
            return x.CompareTo(y);
        }
    }
}
