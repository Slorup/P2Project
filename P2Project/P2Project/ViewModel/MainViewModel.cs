using P2Project.DAL;
using P2Project.Model;
using P2Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace P2Project.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Page _framePage;

        public Page FramePage
        {
            get { return _framePage; }
            set { SetProperty(ref _framePage, value); }
        }

        public User CurrentUser { get; set; }

        public MainViewModel() {}

    }
}
