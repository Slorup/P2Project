﻿using P2Project.ViewModel;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        bool firstTime;
        MainViewModel vm;

        public LoginPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            vm = mainViewModel;
            DataContext = vm;
            firstTime = true;
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (firstTime)
            {
                TextBox tb = (TextBox)sender;
                tb.Text = string.Empty;
                firstTime = false;
            }
        }
    }
}
