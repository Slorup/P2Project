﻿using P2Project.MVVM;
using P2Project.View;
using P2Project.ViewModel;
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

namespace P2Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //sets main navigator to frame. Navigates to loginpage
        public MainWindow()
        {
            InitializeComponent();
            Navigator.MainNavigationService = NavigationFrame.NavigationService;

            LoginViewModel loginvm = new LoginViewModel();
            LoginPage loginPage = new LoginPage();
            loginPage.DataContext = loginvm;
            Navigator.MainNavigationService.Navigate(loginPage);
        }
    }
}
