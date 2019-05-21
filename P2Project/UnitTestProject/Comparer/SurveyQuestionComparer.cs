using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Comparer
{
    class SurveyQuestionComparer : Comparer<SurveyQuestion>
    {
        public override int Compare(SurveyQuestion x, SurveyQuestion y)
        {
            return x.QuestionText.CompareTo(y.QuestionText);
        }
    }
}
