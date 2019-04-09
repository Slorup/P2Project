using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    class Survey
    {
        public List<SurvayQuestion> QuestionList { get; set; }

        public Survey(int NumberofSurveys)
        {
            QuestionList = new List<SurvayQuestion>();
            for (int i = 0; i < NumberofSurveys; i++)
            {
                QuestionList.Add(new SurvayQuestion());
            }
        }
    }
}
