using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.Model
{
    public class SurveyQuestion
    {
        public int SliderValue { get; set; }
        public string QuestionText { get; set; }

        public SurveyQuestion(string question, int value = 3)
        {
            QuestionText = question;
            SliderValue = value;
        }
    }
}
    