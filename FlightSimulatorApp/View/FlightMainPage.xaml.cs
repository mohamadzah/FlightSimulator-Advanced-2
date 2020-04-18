﻿using System;
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
using System.Windows.Shapes;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for FlightMainPage.xaml
    /// </summary>
    public partial class FlightMainPage : Window
    {
        private IModel ml;

        //Constructor.
        public FlightMainPage(IModel _ml)
        {
            this.ml = _ml;
            InitializeComponent();
        }
        //Disconnect from the application and go back to home page.
        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            ml.disconnect();
            Home main = new Home();
            main.Show();
            Close();
        }
    }
}
