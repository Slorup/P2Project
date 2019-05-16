using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P2Project.View
{
    /// <summary>
    /// Interaction logic for ExercisePage.xaml
    /// </summary>
    public partial class ExercisePage : Page
    {
        public ExercisePage()
        {
            InitializeComponent();
            this.Unloaded += (s, e) => { ExerciseViewer.Dispose(); };
        }

        private void ExerciseViewer_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            dynamic activeX = this.ExerciseViewer.GetType().InvokeMember("ActiveXInstance",
                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, this.ExerciseViewer, new object[] { });

            activeX.Silent = true;
        }
    }
}
