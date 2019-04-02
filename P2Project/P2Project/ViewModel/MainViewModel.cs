using P2Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace P2Project.ViewModel
{
    class MainViewModel
    {
        public Page FramePage { get; set; }

        public MainViewModel()
        {
            FramePage = new LoginPage();
        }
    }
}
