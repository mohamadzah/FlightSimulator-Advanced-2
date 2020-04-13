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
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IModel ml;

        public MainWindow(IModel _ml)
        {
            this.ml = _ml;
            InitializeComponent();
        }

        private void sliders_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            ml.disconnect();
            Home main = new Home();
            main.Show();
            Close();
        }
    }
}
