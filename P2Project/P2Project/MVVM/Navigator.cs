using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace P2Project.MVVM
{
    public static class Navigator
    {
        public static NavigationService MainNavigationService { get; set; }
        public static NavigationService SubNavigationService { get; set; }
    }
}
