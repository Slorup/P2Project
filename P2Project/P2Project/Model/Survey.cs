using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public class Survey
    {
        public List<SurveyQuestion> QuestionList { get; set; }

        public Survey()
        {
            QuestionList = new List<SurveyQuestion>();
        }
    }
}
