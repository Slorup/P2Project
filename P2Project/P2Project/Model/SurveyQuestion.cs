using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    class SurveyQuestion
    {
        public int SliderValue { get; set; }
        public string QuestionText { get; set; }
        public SurveyQuestion()
        {
            SliderValue = 3;
        }
    }
}
    