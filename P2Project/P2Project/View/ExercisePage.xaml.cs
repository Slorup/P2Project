using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool IsPlaying { get; set; }
        public ExercisePage()
        {
            InitializeComponent();
            IsPlaying = true;
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            if (IsPlaying)
            {
                VideoPlayer.Pause();
                IsPlaying = false;
            }
            else
            {
                VideoPlayer.Play();
                IsPlaying = true;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonFullScreen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
