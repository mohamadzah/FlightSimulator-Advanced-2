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
using System.Windows.Shapes;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private IModel model;
        ViewModel vm;
        public Home()
        {
            InitializeComponent();
            model = new MyModel(new MyTelnetClient());
            vm = new ViewModel(this.model);
            //binding context.
            DataContext = vm;

        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(model);
            main.dashboard.DataContext = vm.dvm;
            main.map.DataContext = vm.pvm;
            main.joystick.DataContext = vm.mvm;
            main.sliders.DataContext = vm.mvm;

            vm.VM_port = 5402;
            vm.VM_ip = "127.0.0.1";

            try
            {
                this.vm.Connect();
                main.Show();
                this.Close();
            }
            catch
            {
                Console.WriteLine("lol");
            }
        }
    }
}
