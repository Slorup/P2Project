using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    class Survey
    {
        public List<SurveyQuestion> QuestionList { get; set; }

        public Survey(int NumberofSurveys)
        {
            QuestionList = new List<SurveyQuestion>();
            for (int i = 0; i < NumberofSurveys; i++)
            {
                QuestionList.Add(new SurveyQuestion());
            }
        }
    }
}
