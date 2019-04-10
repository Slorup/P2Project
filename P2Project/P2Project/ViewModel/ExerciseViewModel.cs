using P2Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2Project.ViewModel
{
    class ExerciseViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }

        public ExerciseViewModel(User currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}
