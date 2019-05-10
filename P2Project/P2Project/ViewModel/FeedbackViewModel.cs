using P2Project.Model;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    class FeedbackViewModel : BaseViewModel
    {
        private FeedbackWindow _window;
        public FeedbackViewModel(FeedbackWindow window)
        {
            _window = window;
        }

        public Feedback UserFeedback { get; set; }

        private ICommand _feedbackClickCommand;

        public ICommand FeedbackClickCommand
        {
            get
            {
                return _feedbackClickCommand ?? (_feedbackClickCommand = new RelayCommand(param => FeedbackClick(param)));
            }
        }

        private void FeedbackClick(object param)
        {
            int result = Convert.ToInt32(param); //TRY
            if (result == 1)
                UserFeedback = Feedback.Good;
            else if (result == -1)
                UserFeedback = Feedback.Bad;
            else
                UserFeedback = Feedback.Medium;
            _window.DialogResult = true;
            _window.Close();
        }
    }
}
